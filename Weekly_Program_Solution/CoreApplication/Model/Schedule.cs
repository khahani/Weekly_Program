using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApplication.Model
{
    public class Schedule : IEquatable<Schedule>
    {
        public int Id { get; set; }
        public SpecificTime Time { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public SchoolHours AtThisHour { get; set; }
        
        public bool Equals(Schedule other)
        {
            return (Time == other.Time && DayOfWeek == other.DayOfWeek && AtThisHour == other.AtThisHour);
        }
    }
}
