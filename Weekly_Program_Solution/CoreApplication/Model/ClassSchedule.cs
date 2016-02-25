using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApplication.Model
{
    public class ClassSchedule
    {
        public int Id { get; set; }
        public Class Class { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public SchoolHours Hour { get; set; }
        public Dictionary<SpecificTime, TeacherAndCoursePair> Program { get; set; }
        
        //Nested class
        public class TeacherAndCoursePair
        {
            public Teacher Teacher { get; set; }
            public Course Course { get; set; }
        }

        public bool IsValid()
        {
            foreach (var item in Program.Keys)
            {
                if (item == SpecificTime.Ring && Program.Count > 1)
                    return false; // there is only one ring in a Hour
            }


            //must complete later
            return true;
        }
    }
}
