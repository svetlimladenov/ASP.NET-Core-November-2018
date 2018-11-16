using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PandaWebApp.Models.DbModels
{
    public class Receipt
    {
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        public string RecipientId { get; set; }

        public ApplicationUser Recipient { get; set; }

        public string PackageId { get; set; }

        public Package Package { get; set; }
    }
}
