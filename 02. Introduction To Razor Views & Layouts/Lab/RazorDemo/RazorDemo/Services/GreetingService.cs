using Microsoft.Extensions.Configuration;

namespace RazorDemo.Services
{
    public class GreetingService : IGreetingService
    {
        private readonly IConfiguration configuration;

        public GreetingService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public string GetGreeting()
        {
            return configuration["Greeting"];
        }
    }
}