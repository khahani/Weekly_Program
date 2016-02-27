using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApplication.Model
{
    public class Class
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Level Level { get; set; }

        public List<ClassSchedule> WeeklyProgram { get; set; }
    }
}
