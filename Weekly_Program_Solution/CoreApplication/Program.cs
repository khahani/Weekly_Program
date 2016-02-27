using CoreApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Course> courses = new List<Course>
            {
                new Course {Id = 1, Title = "Math"},
                new Course {Id = 2, Title = "Physic" },
                new Course {Id = 3, Title = "History" }
            };

            #region Teachers
            Teacher t1 = new Teacher
            {
                Id = 1,
                Name = "Khahani",
                FreeTimes = new List<Schedule>
                {
                    new Schedule
                    {
                        Id = 1, DayOfWeek = DayOfWeek.Monday, Time = SpecificTime.Ring, AtThisHour = SchoolHours.First
                    },
                    new Schedule
                    {
                        Id = 2, DayOfWeek = DayOfWeek.Saturday, Time = SpecificTime.Ring, AtThisHour = SchoolHours.Second
                    },
                    new Schedule
                    {
                        Id = 3, DayOfWeek = DayOfWeek.Saturday, Time = SpecificTime.FirstHalfRing, AtThisHour = SchoolHours.Third
                    }
                },
                CanTeachCourses = new List<Course>
                {
                    courses.Where(c=>c.Title == "Math").First(),
                    courses.Where(c=>c.Title == "Physic").First()
                }
            };

            Teacher t2 = new Teacher
            {
                Id= 2,
                Name="Mazaheri",
                FreeTimes = new List<Schedule>
                {
                    new Schedule
                    {
                        Id = 4, DayOfWeek = DayOfWeek.Monday, Time = SpecificTime.Ring, AtThisHour = SchoolHours.Fifth
                    }
                },
                CanTeachCourses = new List<Course>
                {
                    courses.Where(c=>c.Title == "Math").First()
                }
            };
            #endregion

            Level l1 = new Level { Id = 1, Title = "Math and Physic" };

            #region CourseDetails
            List<CourseDetail> courseDetails = new List<CourseDetail>
            {
                new CourseDetail
                {
                    Id = 1,
                    Level = l1,
                    Course = courses.Where(c=>c.Title == "Math").First(),
                    MaxTimePerDay = new Dictionary<SpecificTime, int>
                    {
                        { SpecificTime.Ring, 2 }
                    },
                    RequiredTime = new Dictionary<SpecificTime, int>
                    {
                        {SpecificTime.Ring, 4 }
                    }
                },
                new CourseDetail
                {
                    Id = 2,
                    Level = l1,
                    Course = courses.Where(c=>c.Title =="Physic").First(),
                    MaxTimePerDay = new Dictionary<SpecificTime, int>
                    {
                        {SpecificTime.Ring, 2 }
                    },
                    RequiredTime = new Dictionary<SpecificTime, int>
                    {
                        {SpecificTime.Ring, 3 },
                        {SpecificTime.HalfRing, 2 }
                    }
                }
            };
            #endregion

            School school = new School
            {
                Id = 1,
                Title = "Emam Hossein1",
                Classes = new List<Class>
                {
                    new Class
                    {
                        Id = 1, Title = "Class-101", Level = l1
                    },
                    new Class
                    {
                        Id = 2, Title = "Class-102", Level = l1
                    }
                }
            };

            List<ClassSchedule> weeklyProgramForClass102 = new List<ClassSchedule>();

            //Create an empty weekly program
            int weeklyProgramIdCounter = 0;
            foreach (var day in Enum.GetNames(typeof(DayOfWeek)))
            {
                foreach (var hour in Enum.GetNames(typeof(SchoolHours)))
                {
                    weeklyProgramIdCounter++;
                    weeklyProgramForClass102.Add(new ClassSchedule
                    {
                        Id = weeklyProgramIdCounter,
                        Class = school.Classes.Where(c=> c.Title == "Class-101").First(),
                        DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day),
                        Hour = (SchoolHours)Enum.Parse(typeof(SchoolHours), hour)
                    });
                }
            }

            school.Classes.Where(c => c.Title == "Class-102").First().WeeklyProgram = weeklyProgramForClass102;

            List<Teacher> allTeacher = new List<Teacher> { t1, t2 };

            foreach (var @class in school.Classes)
            {
                //courses that should be teach in class
                var shoulBeTeach = courseDetails.Where(cd => cd.Level == @class.Level);
                
                foreach (var courseDetail in shoulBeTeach)
                {
                    var canTeachCourse = allTeacher.Where(t => t.CanTeachCourses.Any(c => c.Id == courseDetail.Course.Id));

                    //calcluate teacher free time from other classes

                    foreach (var teacher in canTeachCourse)
                    {
                        CrossFunctional cross = new CrossFunctional();

                        var freeTime = cross.GetCurrentFreeTimeOfTeacher(school, teacher);

                        if (freeTime == null)
                            continue;   //Next teacher


                    }

                }
            }
        }
    }
}
