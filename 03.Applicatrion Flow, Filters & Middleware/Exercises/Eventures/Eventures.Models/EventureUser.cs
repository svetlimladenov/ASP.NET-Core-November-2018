using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Eventures.Models
{
    public class EventureUser : IdentityUser
    {
        public EventureUser()
        {
            this.Events = new HashSet<Event>();
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        //Unique Citized Number
        public string UCN { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
