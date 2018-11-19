using System;
using Chushka.Models;

namespace ChushkaWebApp.Models
{
    public class Order
    {
        public string Id { get; set; }

        public string ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string UserId { get; set; }

        public virtual ChushkaUser User { get; set; }

        public DateTime OrderedOn { get; set; } = DateTime.UtcNow;
    }
}
