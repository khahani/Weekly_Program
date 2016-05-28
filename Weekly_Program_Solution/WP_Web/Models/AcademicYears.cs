using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WP_Web.Models
{
    public class AcademicYear
    {
        [Key]
        public int AcademicYearId { get; set; }

        public Guid UserId { get; set; }
    }
}