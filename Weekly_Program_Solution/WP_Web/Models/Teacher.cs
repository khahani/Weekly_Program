using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WP_Web.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        public int AcademicYearId { get; set; }
        public virtual AcademicYear AcademicYear { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }

    }
}