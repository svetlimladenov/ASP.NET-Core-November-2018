using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using RazorDemo.Services;

namespace RazorDemo.TagHelpers
{
    [HtmlTargetElement("h1")]
    public class GreetingHeaderTagHelper : TagHelper
    {
        private readonly IGreetingService greetingService;

        public GreetingHeaderTagHelper(IGreetingService greetingService)
        {
            this.greetingService = greetingService;
        }
        //asp-name
        public string AspName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetContent($"{this.greetingService.GetGreeting()} , {this.AspName}");
        }
    }
}
