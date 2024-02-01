using thirdparty_api;
using web_api.Extensions.Dapper;
using web_api.Extensions.Process;
using web_api.Extensions.Redis;
using web_api.Repository.Interfaces;

namespace web_api.DependencyModule
{
    public class OtherAPIModule
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<NewsAPI>();
        }
    }
}
