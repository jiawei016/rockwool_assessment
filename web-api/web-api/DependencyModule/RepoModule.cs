using thirdparty_api;
using web_api.Repository;
using web_api.Repository.Interfaces;

namespace web_api.DependencyModule
{
    public class RepoModule
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<INewsRepo, NewsRepo>();
        }
    }
}
