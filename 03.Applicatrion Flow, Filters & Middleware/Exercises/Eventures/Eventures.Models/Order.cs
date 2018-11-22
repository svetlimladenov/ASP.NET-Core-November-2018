﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Eventures.Models
{
    public class Order
    {
        public string Id { get; set; }

        public DateTime OrderedOn { get; set; }

        public string EventId { get; set; }

        public Event Event { get; set; }

        public string CustomerId { get; set; }

        public EventureUser Customer { get; set; }

        public int TicketsCount { get; set; }
    }
}
