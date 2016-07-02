using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP_Web.Models
{
    public class WeeklyProgram
    {
        public int WeeklyProgramId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int AcademicYearId { get; set; }
        public AcademicYear AcademicYear { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public int DayOfWeek { get; set; }
        public int RingNumber { get; set; }
        public int HalfRingNumber { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}