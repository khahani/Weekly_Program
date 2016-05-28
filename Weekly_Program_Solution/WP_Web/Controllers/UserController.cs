using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WP_Web.Models;

namespace WP_Web.Controllers
{
    
    public class UserController : Controller
    {
        private readonly WPDB Database;

        public UserController()
        {
            Database = new WPDB();
        }
        private AuthenticatedUser AuthInfo
        {
            get
            {
                if (Session["Auth"] == null)
                    return new AuthenticatedUser { Authenticated = false };

                return (AuthenticatedUser)Session["Auth"];
            }
        }

        public ActionResult AcademicYears()
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            var aYears = Database.AcademicYears
                .Where(a => a.User == Database.Users
                    .Where(u => u.Email == AuthInfo.Email).FirstOrDefault())
                .OrderByDescending(a => a.AcademicYearId);

            return View(aYears.ToList());
        }
        public ActionResult WeeklySchedules()
        {
            return View();
        }
    }
}