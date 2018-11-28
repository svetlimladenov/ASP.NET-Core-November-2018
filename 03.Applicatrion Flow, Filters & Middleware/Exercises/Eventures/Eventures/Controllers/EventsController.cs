using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventures.Data;
using Eventures.Filters;
using Eventures.Models;
using Eventures.Services.EventsServices;
using Eventures.ViewModels;
using Eventures.ViewModels.Events;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Eventures.Controllers
{
    public class EventsController : Controller
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IEventsService eventsService;


        public EventsController(
            UserManager<EventureUser> userManager,
            ApplicationDbContext db,ILogger<EventsController> logger,
            IEventsService eventsService)
        {
            this.eventsService = eventsService;
            _logger = logger;
        }

        public IActionResult All()
        {
            var viewModel = new AllEventsViewModel();
            viewModel.Events = eventsService.GetAllEvents();
            return View(viewModel);
        }

        public IActionResult Create()
        {    
            return View();
        }

        
        [TypeFilter(typeof(AdminCreateEventFilter))]
        [HttpPost]
        public IActionResult Create(EventsBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            eventsService.CreateEvent(model, this.User);
            return Redirect("/");
        }

        public IActionResult BuyTicket(AllEventsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return All();
            }
            var id = model.BuyTicketInputModel.Id;
            var ticketsCount = model.BuyTicketInputModel.TicketsCount;

            var isBought = eventsService.CanBuyTickets(id,ticketsCount, User);
            if (!isBought)
            {
                return BadRequest("You cant buy this much tickets.");
            }
            return this.Redirect("/Events/MyEvents");
        }

        public IActionResult MyEvents()
        {
            var viewModel = new MyEventsViewModel();
            viewModel.MyEvents = eventsService.GetMyEvents(User);
            return this.View(viewModel);
        }
    }
}
