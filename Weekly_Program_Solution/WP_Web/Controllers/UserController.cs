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

        }
        public ActionResult WeeklySchedules()
        {
            return View();
        }
    }
}