using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApplication.Model
{
    public class CrossFunctional
    {
        public IEnumerable<Schedule> GetCurrentFreeTimeOfTeacher(School school, Teacher teacher)
        {
            IList<Schedule> filledTime = new List<Schedule>();

            foreach (var @class in school.Classes)
            {
                foreach (var item in @class.WeeklyProgram)
                {
                    foreach (var ring in item.Program.Keys)
                    {
                        if (item.Program[ring].Teacher == teacher)
                        {
                            filledTime.Add(new Schedule
                            {
                                DayOfWeek = item.DayOfWeek,
                                AtThisHour = item.Hour,
                                Time = ring
                            });
                        }
                    }
                }
            }
            var currentFreeTime = teacher.FreeTimes.Except(filledTime);

            return currentFreeTime;
        }

        public Dictionary<SpecificTime, int> RemainTimeForCourse(Class @class, Course course)
        {

        }
    }
}
