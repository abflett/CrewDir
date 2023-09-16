using Microsoft.Extensions.Configuration;

namespace CrewDir.UIService
{
    public class TestService
    {
        private readonly IConfiguration _configuration;

        public TestService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetSomeString()
        {
            return _configuration["ApiUrl"];
        }
    }
}
