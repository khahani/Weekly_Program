using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP_Web.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int AcademicYearId { get; set; }
        public AcademicYear AcademicYear { get; set; }
        public string Name { get; set; }
    }
}