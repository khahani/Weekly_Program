using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApplication.Model
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Schedule> FreeTimes { get; set; }
        public List<Course> CanTeachCourses { get; set; }
    }
}
