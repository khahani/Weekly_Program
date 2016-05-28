using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP_Web.Models
{
    public class AuthenticatedUser
    {
        public string Email { get; set; }
        public bool Authenticated { get; set; }

    }
}