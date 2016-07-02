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
                .Where(t => t.UserId == CurrentUser.UserId && t.AcademicYearId == CurrentAcademicYear.AcademicYearId);

            return View(teachers.ToList());
        }

        public ActionResult Create()
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            if (CurrentAcademicYear.AcademicYearId == -1)
            {
                return RedirectToAction("Index", "AcademicYears");
            }

            TeacherDetailViewModel model = new TeacherDetailViewModel();
            model.Lessons = db.Lessons
                .Where(l => l.AcademicYearId == CurrentAcademicYear.AcademicYearId && l.UserId == CurrentUser.UserId)
                .OrderBy(l => l.Name)
                .Select(l => l.Name).ToList();

            var defaultRingCount = db.AcademicYears.Where(m => m.AcademicYearId == CurrentAcademicYear.AcademicYearId).FirstOrDefault().DefaultRingCount;

            ViewBag.RingCount = defaultRingCount;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "TeacherId,Name,Job, CanTeach, Schedule")] TeacherDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                Teacher newTeacher = new Teacher
                {
                    Name = model.Name,
                    Job = model.Job,
                    AcademicYearId = CurrentAcademicYear.AcademicYearId,
                    UserId = CurrentUser.UserId,
                };

                db.Teachers.Add(newTeacher);

                db.SaveChanges();

                List<CanTeach> abilities = new List<CanTeach>();

                foreach (var item in model.CanTeach)
                {
                    CanTeach ability = new CanTeach
                    {
                        TeacherId = newTeacher.TeacherId,
                        UserId = CurrentUser.UserId,
                        LessonId = db.Lessons.Where(m => m.Name == item).FirstOrDefault().LessonId
                    };

                    abilities.Add(ability);
                }

                db.CanTeaches.AddRange(abilities);

                db.SaveChanges();

                List<TeacherFreeTime> tfts = TeacherFreeTime.FromTableToDb(model.Schedule.ToArray());

                for (int i = 0; i < tfts.Count; i++)
                {
                    tfts[i].TeacherId = newTeacher.TeacherId;
                    tfts[i].AcademicYearId = CurrentAcademicYear.AcademicYearId;
                    tfts[i].UserId = CurrentUser.UserId;
                }

                db.TeacherFreeTimes.AddRange(tfts);

                db.SaveChanges();

                return RedirectToAction("Index", "Teachers");
            }



            model.Lessons = db.Lessons
                .Where(l => l.AcademicYearId == CurrentAcademicYear.AcademicYearId && l.UserId == CurrentUser.UserId)
                .OrderBy(l => l.Name)
                .Select(l => l.Name).ToList();

            return View(model);
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
                .Where(t => t.AcademicYearId == CurrentAcademicYear.AcademicYearId && t.UserId == CurrentUser.UserId && t.TeacherId == id)
                .FirstOrDefault();

            if (teacher == null)
            {
                return HttpNotFound();
            }

            TeacherDetailViewModel model = new TeacherDetailViewModel
            {
                TeacherId = teacher.TeacherId,
                Name = teacher.Name,
                Job = teacher.Job,
                Lessons = db.Lessons
                    .Where(l => l.AcademicYearId == CurrentAcademicYear.AcademicYearId && l.UserId == CurrentUser.UserId)
                    .OrderBy(l => l.Name)
                    .Select(l => l.Name).ToList(),
                CanTeach = db.CanTeaches
                    .Include(m => m.Lesson).
                    Where(m => m.TeacherId == teacher.TeacherId)
                    .Select(m => m.Lesson.Name).ToList(),
                Schedule = TeacherFreeTime.GetCheckedIds(db.TeacherFreeTimes.Where(m=>m.TeacherId == teacher.TeacherId).ToList()).ToList()
            };

            var defaultRingCount = db.AcademicYears.Where(m => m.AcademicYearId == CurrentAcademicYear.AcademicYearId).FirstOrDefault().DefaultRingCount;

            ViewBag.RingCount = defaultRingCount;

            return View(model);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherId,Name,Job, CanTeach, Schedule")] TeacherDetailViewModel model)
        {
            if (!AuthInfo.Authenticated)
                return RedirectToAction("Login", "Home");

            if (CurrentAcademicYear.AcademicYearId == -1)
            {
                return RedirectToAction("Index", "AcademicYears");
            }

            if (ModelState.IsValid)
            {
                Teacher teacher = db.Teachers
               .Where(t => t.AcademicYearId == CurrentAcademicYear.AcademicYearId && t.UserId == CurrentUser.UserId && t.TeacherId == model.TeacherId)
               .FirstOrDefault();

                db.Entry(teacher).State = EntityState.Modified;
                teacher.UserId = CurrentUser.UserId;
                teacher.AcademicYearId = CurrentAcademicYear.AcademicYearId;
                teacher.Name = model.Name;
                teacher.Job = model.Job;

                db.SaveChanges();

                //Insert new abilities
                List<CanTeach> canTeaches = new List<CanTeach>();

                foreach (var ability in model.CanTeach)
                {
                    var existsAbility = db.CanTeaches.Include(m => m.Lesson)
                        .Where(m => m.TeacherId == teacher.TeacherId && m.Lesson.Name == ability && m.UserId == CurrentUser.UserId).FirstOrDefault();

                    if (existsAbility != null)
                        continue;

                    var lesson = db.Lessons
                                .Where(l => l.AcademicYearId == CurrentAcademicYear.AcademicYearId && l.UserId == CurrentUser.UserId && l.Name == ability)
                                .FirstOrDefault();

                    if (lesson == null)
                    {
                        continue;
                    }

                    canTeaches.Add(
                        new CanTeach
                        {
                            TeacherId = teacher.TeacherId,
                            UserId = CurrentUser.UserId,
                            LessonId = lesson.LessonId
                        });

                }

                db.CanTeaches.AddRange(canTeaches);
                db.SaveChanges();

                //Delete removed abilities

                var mustDelete = db.CanTeaches.Include(m => m.Lesson)
                    .Where(m => !model.CanTeach.Contains(m.Lesson.Name) &&
                     m.TeacherId == teacher.TeacherId && m.UserId == CurrentUser.UserId);

                db.CanTeaches.RemoveRange(mustDelete);

                db.SaveChanges();


                //remove all tfts for teacher
                var existsTFTS = db.TeacherFreeTimes.Where(m => m.TeacherId == teacher.TeacherId && m.UserId == CurrentUser.UserId);

                db.TeacherFreeTimes.RemoveRange(existsTFTS);

                db.SaveChanges();

                //insert new tfts 
                List<TeacherFreeTime> tfts = TeacherFreeTime.FromTableToDb(model.Schedule.ToArray());

                for (int i = 0; i < tfts.Count; i++)
                {
                    tfts[i].TeacherId = teacher.TeacherId;
                    tfts[i].AcademicYearId = CurrentAcademicYear.AcademicYearId;
                    tfts[i].UserId = CurrentUser.UserId;
                }

                db.TeacherFreeTimes.AddRange(tfts);

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
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

            var tfts = db.TeacherFreeTimes.Where(m => m.UserId == CurrentUser.UserId && m.AcademicYearId == CurrentAcademicYear.AcademicYearId && m.TeacherId == teacher.TeacherId);

            db.TeacherFreeTimes.RemoveRange(tfts);

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
