using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApplication.Model
{
    public class Schedule
    {
        public int Id { get; set; }
        public SpecificTime FreeTime { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public SchoolHours AtThisHour { get; set; }
    }
}
