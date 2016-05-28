using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WP_Web.Models
{
    public class AcademicYear
    {
        [Key]
        public int AcademicYearId { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string Title { get; set; }
    }
}