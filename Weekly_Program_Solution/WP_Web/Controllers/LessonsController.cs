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
    public class LessonsController : Controller
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
                if (Session["CurrentAcademicYear"] == null)
                    return new AcademicYear { AcademicYearId = -1 };

                return (AcademicYear)Session["CurrentAcademicYear"];
            }
        }

        // GET: Lessons
        public ActionResult Index()
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            if (CurrentAcademicYear.AcademicYearId == -1)
            {
                return RedirectToAction("Index", "AcademicYears");
            }

            var lessons = db.Lessons
                .Where(l => l.AcademicYearId == CurrentAcademicYear.AcademicYearId &&
                            l.UserId == CurrentUser.UserId)
                .OrderBy(l=>l.Name);
            

            return View(lessons.ToList());
        }

        [HttpGet]
        public ActionResult Search()
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            if (CurrentAcademicYear.AcademicYearId == -1)
            {
                return RedirectToAction("Index", "AcademicYears");
            }

            return PartialView("_SearchFormPartial");
        }

        [HttpPost]
        public ActionResult Search(string filter)
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            if (CurrentAcademicYear.AcademicYearId == -1)
            {
                return RedirectToAction("Index", "AcademicYears");
            }

            var lessons = db.Lessons
                .Where(l => l.AcademicYearId == CurrentAcademicYear.AcademicYearId &&
                            l.UserId == CurrentUser.UserId);

            if (filter != string.Empty)
                lessons = lessons.Where(l => l.Name.StartsWith(filter));

            lessons = lessons.OrderBy(l => l.Name);

            return PartialView("_SearchResultPartial", lessons.ToList());
        }

        // GET: Lessons/Create
        public ActionResult Create()
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            if (CurrentAcademicYear.AcademicYearId == -1)
            {
                return RedirectToAction("Index", "AcademicYears");
            }
            
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LessonId,Name")] Lesson lesson)
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            if (CurrentAcademicYear.AcademicYearId == -1)
            {
                return RedirectToAction("Index", "AcademicYears");
            }

            if (ModelState.IsValid)
            {
                lesson.AcademicYearId = CurrentAcademicYear.AcademicYearId;
                lesson.UserId = CurrentUser.UserId;

                db.Lessons.Add(lesson);
                db.SaveChanges();

               return Redirect(Request.UrlReferrer.ToString());
            }

            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            if (CurrentAcademicYear.AcademicYearId == -1)
            {
                return RedirectToAction("Index", "AcademicYears");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons
                 .Where(l => l.AcademicYearId == CurrentAcademicYear.AcademicYearId &&
                            l.UserId == CurrentUser.UserId &&
                            l.LessonId == id)
                .FirstOrDefault();

            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LessonId,Name")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lesson).State = EntityState.Modified;
                lesson.UserId = CurrentUser.UserId;
                lesson.AcademicYearId = CurrentAcademicYear.AcademicYearId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons
                .Where(l => l.AcademicYearId == CurrentAcademicYear.AcademicYearId &&
                            l.UserId == CurrentUser.UserId &&
                            l.LessonId == id)
                .FirstOrDefault();
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lesson lesson = db.Lessons
                .Where(l => l.AcademicYearId == CurrentAcademicYear.AcademicYearId &&
                            l.UserId == CurrentUser.UserId &&
                            l.LessonId == id)
                .FirstOrDefault();
            db.Lessons.Remove(lesson);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
