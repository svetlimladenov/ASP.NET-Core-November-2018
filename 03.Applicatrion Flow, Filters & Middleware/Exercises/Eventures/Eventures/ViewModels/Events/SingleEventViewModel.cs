using System;

namespace Eventures.ViewModels.Events
{
    public class SingleEventViewModel
    {
        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int Tickets { get; set; }
    }
}