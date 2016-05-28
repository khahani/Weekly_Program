using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WP_Web.Models
{
    public class WPDB : DbContext
    {
        public WPDB() : base("Server=WIN-SPK3MC24AA4\\DEV;Database=WPDB;Integrated Security=true;") { }
        public DbSet<User> Users { get; set; }
    }
}