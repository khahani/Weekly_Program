using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mail;
using System.Web.Mvc;
using WP_Web.Models;

namespace WP_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly WPDB Database;

        public HomeController()
        {
            Database = new WPDB();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Signup(UserViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var existsUser = Database.Users.Where(u => u.Email == user.Email).FirstOrDefault();

            if (existsUser != null)
            {
                ViewBag.Message = "کاربر با این ایمیل ثبت نام کرده است.";
                return View(user);
            }

            User newUser = new User
            {
                Email = user.Email,
                Password = user.Password,
                Active = false,
                Blocked = false
            };

            Database.Users.Add(newUser);

            await Database.SaveChangesAsync();

            //Send email for activation...

            var body = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/EmailTemplates/ActivateEmailMessageTemplate.html"));
            var activateLink = "http://" + Request.Url.Host + this.Url.Action("ActivateUser", new { key = newUser.UserId.ToString() });
            var message = new System.Net.Mail.MailMessage();
            message.To.Add(new MailAddress(user.Email));  // replace with valid value 
            message.Subject = "فعال سازی حساب کاربری";
            message.Body = string.Format(body, user.Email, activateLink);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(message);
            }

            return RedirectToAction("ActivateMessage");
        }

        public ActionResult ActivateMessage()
        {
            return View();
        }

        public ActionResult ActivateUser(string key)
        {
            var user = Database.Users.Where(u => u.UserId.ToString() == key).FirstOrDefault();

            if (user == null)
            {
                ViewBag.Message = "امکان فعالسازی حساب کاربری وجود ندارد. لطفا با پشتیبانی سامانه تماس بگیرید.";
                return View();
            }

            user.Active = true;

            Database.SaveChanges();

            return RedirectToAction("Login", new UserViewModel{ Email = user.Email, Password = user.Password });
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = Database.Users.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();

            if (user == null)
            {
                ViewBag.Message = "نام کاربری یا کلمه عبور اشتباه است.";
                return View(model);
            }

            if (!user.Active)
            {
                ViewBag.Message = "حساب کاربری شما فعال نشده است. لطفا ایمیل خود را بررسی نمایید.";
                return View(model);
            }

            AuthenticatedUser authenticated = new AuthenticatedUser
            {
                Email = user.Email,
                Authenticated = true
            };

            Session["Auth"] = authenticated;

            return RedirectToAction("AcademicYears", "User");
        }

    }
}