using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Eventures.Filters
{
    public class AdminCreateEventFilter : ActionFilterAttribute
    {
        private readonly ILogger<AdminCreateEventFilter> _logger;

        public AdminCreateEventFilter(ILogger<AdminCreateEventFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(
            ActionExecutingContext context)
        {
            
        }
        public override void OnActionExecuted(
            ActionExecutedContext context)
        {
           var eventName = context.HttpContext.Request.Form["Name"];
            var username = context.HttpContext.User.Identity.Name;
            var eventStart = context.HttpContext.Request.Form["Start"];
            var eventEnd = context.HttpContext.Request.Form["End"];
            _logger.LogInformation($"[{DateTime.Now}] Administrator {username} create event {eventName} ({eventStart} / {eventEnd}).");
        }
    }
}
