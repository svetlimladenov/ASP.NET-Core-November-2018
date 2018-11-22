using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eventures.Data;
using Eventures.Models;
using Eventures.ViewModels;
using Eventures.ViewModels.Events;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Eventures.Services.EventsServices
{
    public class EventsService : IEventsService
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<EventureUser> userManager;
        public EventsService(ApplicationDbContext db, UserManager<EventureUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public EventsBindingModel[] GetAllEvents()
        {
            return this.db.Events.Select(x => new EventsBindingModel()
            {
                Id = x.Id,
                Name = x.Name,
                Start = x.Start,
                End = x.End,
                Place = x.Place,
            }).ToArray();
        }

        public void CreateEvent(EventsBindingModel model, ClaimsPrincipal user)
        {
            var currentEvent = new Event
            {
                Id = model.Id,
                Name = model.Name,
                End = model.End,
                Start = model.Start,
                Place = model.Place,
                PricePerTicket = model.PricePerTicket,
                TotalTickets = model.TotalTickets,
                UserId = userManager.GetUserId(user),
            };

            db.Events.Add(currentEvent);
            db.SaveChanges();
        }

        public void BuyTickets(string id, int count, ClaimsPrincipal user)
        {
            var order = new Order()
            {
                CustomerId = userManager.GetUserId(user),
                EventId = id,
                TicketsCount = count,
                OrderedOn = DateTime.Now,                
            };
            var hasAnyTickets =
                db.Orders.FirstOrDefault(x => x.EventId == order.EventId && x.CustomerId == order.CustomerId);
            if (hasAnyTickets != null)
            {
                hasAnyTickets.TicketsCount += count;
            }
            else
            {
                db.Orders.Add(order);
            }

            
            db.SaveChanges();
        }

        public SingleEventViewModel[] GetMyEvents(ClaimsPrincipal user)
        {
            var userId = userManager.GetUserId(user);
            var myEvents = db.Orders.Include(o => o.Event).Where(x => x.CustomerId == userId).Select(x =>
                new SingleEventViewModel()
                {
                    Name = x.Event.Name,
                    Start = x.Event.Start,
                    End = x.Event.End,
                    Tickets = x.TicketsCount,
                }).ToArray();

            return myEvents;
        }
    }
}
