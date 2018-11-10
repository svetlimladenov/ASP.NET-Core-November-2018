using Microsoft.Extensions.Configuration;

namespace MvcIntro.Services
{
    public class GreetingService : IGreetingService
    {
        private IConfiguration configuration;

        public GreetingService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public string GetGreeting()
        {
            return this.configuration["Greetings"]; 
        }
    }
}