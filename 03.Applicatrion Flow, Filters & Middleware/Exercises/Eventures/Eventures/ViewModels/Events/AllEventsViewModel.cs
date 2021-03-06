﻿using System.Collections.Generic;

namespace Eventures.ViewModels.Events
{
    public class AllEventsViewModel
    {
        public ICollection<EventsBindingModel> Events { get; set; }

        public BuyTicketInputModel BuyTicketInputModel { get; set; }

        public int PageNumber { get; set; }

        public int TotalPagesCount { get; set; }
    }
}
