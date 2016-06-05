using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP_Web.Models
{
    public class CanTeach
    {
        public int CanTeachId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}