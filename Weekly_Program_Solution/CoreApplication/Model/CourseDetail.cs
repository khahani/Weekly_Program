using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApplication.Model
{
    public class CourseDetail
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public Level Level { get; set; }
        public Dictionary<SpecificTime, int> RequiredTime { get; set; }
        public Dictionary<SpecificTime, int> MaxTimePerDay { get; set; }
    }
}
