using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Panda.Models
{
    public class PandaUser : IdentityUser
    {
        public PandaUser()
        {
            this.Packages = new HashSet<Package>();
            this.Receipts = new HashSet<Receipt>();
        }

        public ICollection<Package> Packages { get; set; }

        public ICollection<Receipt> Receipts { get; set; }
    }
}
