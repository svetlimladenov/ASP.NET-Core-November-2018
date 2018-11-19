using System;
using System.Collections.Generic;
using System.Text;
using ChushkaWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace Chushka.Models
{
    public class ChushkaUser : IdentityUser
    {
        public ChushkaUser()
        {
            this.Orders = new HashSet<Order>();
        }

        public string FullName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
