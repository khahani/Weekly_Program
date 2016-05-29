using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WP_Web.Models;

namespace WP_Web.Controllers
{
    public class WeeklySchedulesController : Controller
    {
        private WPDB db = new WPDB();

        private AuthenticatedUser AuthInfo
        {
            get
            {
                if (Session["Auth"] == null)
                    return new AuthenticatedUser { Authenticated = false };

                return (AuthenticatedUser)Session["Auth"];
            }
        }

        private User CurrentUser
        {
            get
            {
                return db.Users.Where(u => u.Email == AuthInfo.Email).FirstOrDefault();
            }
        }

        private AcademicYear CurrentAcademicYear
        {
            get
            {
                if (Session["AcademicYear"] == null)
                    return new AcademicYear { AcademicYearId = -1 };

                return (AcademicYear)Session["AcademicYear"];
            }
        }
        // GET: WeeklySchedules
        public ActionResult Index(int? id)
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            if (id == null)
            {
                if (CurrentAcademicYear.AcademicYearId == -1)
                    return HttpNotFound();
                else
                    id = CurrentAcademicYear.AcademicYearId;
            }

            var currentAcademicYear = db.AcademicYears.Include(a => a.User)
                .Where(a => a.User.UserId == CurrentUser.UserId && a.AcademicYearId == id).FirstOrDefault();

            if (currentAcademicYear == null)
            {
                return RedirectToAction("Index", "AcademicYears");
            }

            Session["CurrentAcademicYear"] = currentAcademicYear;

            return View();
        }
    }
}