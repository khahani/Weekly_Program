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
    public class AcademicYearsController : Controller
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

        // GET: AcademicYears
        public ActionResult Index()
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            var academicYears = db.AcademicYears.Include(a=>a.User)
                .Where(a => a.User.UserId == CurrentUser.UserId)
                .OrderByDescending(a => a.AcademicYearId);

            return View(academicYears.ToList());
        }

        // GET: AcademicYears/Create
        public ActionResult Create()
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            return View();
        }

        // POST: AcademicYears/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AcademicYearId,Title")] AcademicYear academicYear)
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            if (ModelState.IsValid)
            {
                academicYear.User = CurrentUser;
                db.AcademicYears.Add(academicYear);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(academicYear);
        }

        // GET: AcademicYears/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicYear academicYear = db.AcademicYears.Include(a => a.User)
                .Where(a => a.User.UserId == CurrentUser.UserId && a.AcademicYearId == id)
                .FirstOrDefault();

            if (academicYear == null)
            {
                return HttpNotFound();
            }
            return View(academicYear);
        }

        // POST: AcademicYears/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AcademicYearId,Title")] AcademicYear academicYear)
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            if (ModelState.IsValid)
            {
                db.Entry(academicYear).State = EntityState.Modified;
                academicYear.User = CurrentUser;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(academicYear);
        }

        // GET: AcademicYears/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicYear academicYear = db.AcademicYears.Include(a => a.User)
                .Where(a => a.User.UserId == CurrentUser.UserId && a.AcademicYearId == id)
                .FirstOrDefault();
            if (academicYear == null)
            {
                return HttpNotFound();
            }
            return View(academicYear);
        }

        // POST: AcademicYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AcademicYear academicYear = db.AcademicYears.Include(a => a.User)
                .Where(a => a.User.UserId == CurrentUser.UserId && a.AcademicYearId == id)
                .FirstOrDefault();
            db.AcademicYears.Remove(academicYear);
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
