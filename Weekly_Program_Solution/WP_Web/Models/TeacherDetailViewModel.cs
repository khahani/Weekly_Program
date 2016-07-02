using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP_Web.Models
{
    public class TeacherDetailViewModel
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public List<string> Lessons { get; set; }
        public List<string> CanTeach { get; set; }
        public List<string> Schedule { get; set; }

        public TeacherDetailViewModel()
        {
            Lessons = new List<string>();
            CanTeach = new List<string>();
            Schedule = new List<string>();
        }
    }
}