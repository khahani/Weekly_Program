﻿using System;
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
    public class TeachersController : Controller
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

        // GET: Teachers
        public ActionResult Index()
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            if (CurrentAcademicYear.AcademicYearId == -1)
            {
                return RedirectToAction("Index", "AcademicYears");
            }

            var teachers = db.Teachers
                .Where(t=>t.UserId == CurrentUser.UserId && t.AcademicYearId == CurrentAcademicYear.AcademicYearId);

            return View(teachers.ToList());
        }

        // GET: Teachers/Create
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

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherId,Name,Job")] Teacher teacher)
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            if (CurrentAcademicYear.AcademicYearId == -1)
            {
                return RedirectToAction("Index", "AcademicYears");
            }

            if (ModelState.IsValid)
            {
                teacher.UserId = CurrentUser.UserId;
                teacher.AcademicYearId = CurrentAcademicYear.AcademicYearId;

                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }
        // GET: Teachers/Edit/5
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
            Teacher teacher = db.Teachers
                .Where(t=>t.AcademicYearId == CurrentAcademicYear.AcademicYearId && t.UserId == CurrentUser.UserId && t.TeacherId == id)
                .FirstOrDefault();

            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherId,Name,Job")] Teacher teacher)
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            if (CurrentAcademicYear.AcademicYearId == -1)
            {
                return RedirectToAction("Index", "AcademicYears");
            }

            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                teacher.UserId = CurrentUser.UserId;
                teacher.AcademicYearId = CurrentAcademicYear.AcademicYearId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers
                .Where(t => t.UserId == CurrentUser.UserId && t.AcademicYearId == CurrentAcademicYear.AcademicYearId && t.TeacherId == id)
                .FirstOrDefault();

            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers
                .Where(t => t.UserId == CurrentUser.UserId && t.AcademicYearId == CurrentAcademicYear.AcademicYearId && t.TeacherId == id)
                .FirstOrDefault();

            db.Teachers.Remove(teacher);
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
