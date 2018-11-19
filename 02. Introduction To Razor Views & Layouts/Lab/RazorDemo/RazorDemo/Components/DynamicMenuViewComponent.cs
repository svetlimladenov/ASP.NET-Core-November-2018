using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RazorDemo.Services;

namespace RazorDemo.Components
{
    [ViewComponent(Name = "menu")]
    public class DynamicMenuViewComponent : ViewComponent
    {
        private readonly IGreetingService greetingService;

        public DynamicMenuViewComponent(IGreetingService greetingService)
        {
            this.greetingService = greetingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new List<MenuItemViewModel>
            {
                new MenuItemViewModel() {Title = "Google", Url = "https://google.com"},
                new MenuItemViewModel() {Title = "Facebook", Url = "https://facebook.com"}
            };
            return this.View(viewModel);
        }
    }

    public class MenuItemViewModel
    {
        public string Title { get; set; }

        public string Url { get; set; }
    }
}
