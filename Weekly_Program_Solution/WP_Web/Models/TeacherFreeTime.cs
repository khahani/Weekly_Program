using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WP_Web.Models
{
    public class TeacherFreeTime
    {
        public int TeacherFreeTimeId  { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int AcademicYearId { get; set; }
        public AcademicYear AcademicYear { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int DayOfWeek { get; set; }
        public int RingNumber { get; set; }
        public bool FirstHourIsFree { get; set; }
        public bool SecondHourIsFree { get; set; }

        public static List<TeacherFreeTime> FromTableToDb(string[] values)
        {
            List<TeacherFreeTime> list = new List<TeacherFreeTime>();

            foreach (var item in values)
            {
                string[] splited = item.Split('-'); //0=RingNumber | 1=DayOfWeek | 2=Hours
                TeacherFreeTime newItem = new TeacherFreeTime
                {
                    RingNumber = int.Parse(splited[0]),
                    DayOfWeek = int.Parse(splited[1]),
                    FirstHourIsFree = int.Parse(splited[2]) == 0,
                    SecondHourIsFree = int.Parse(splited[2]) == 1
                };
                list.Add(newItem);
            }

            return list;
        }

        public static string[] GetCheckedIds(List<TeacherFreeTime> values)
        {
            List<string> list = new List<string>();

            foreach (var item in values)
            {
                string newItem = string.Format("{0}-{1}-",
                                                item.RingNumber,
                                                item.DayOfWeek);

                if (item.FirstHourIsFree)
                    list.Add(newItem + "0");
                if (item.SecondHourIsFree)
                    list.Add(newItem + "1");
            }

            return list.ToArray();
        }
    }
}