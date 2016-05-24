using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WP_API.Controllers
{
    public class TeachersController : ApiController
    {
        // GET: Teachers
        public ActionResult Index()
        {
            return View();
        }
    }
}