using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Panda.Models
{
    [Table("Receipt")]
    public class Receipt
    {
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        public PandaUser Recipient { get; set; }

        public string RecipientId { get; set; }

        [ForeignKey("PackageId")]
        public Package Package { get; set; }

        [Column("PackageId")]
        public string PackageId { get; set; }

    }
}
