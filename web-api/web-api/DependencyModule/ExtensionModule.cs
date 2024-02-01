using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using web_api.Extensions.Dapper;
using web_api.Extensions.Process;
using web_api.Extensions.Redis;

namespace web_api.DependencyModule
{
    public class ExtensionModule
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<DapperHelper>();
            services.AddScoped<RedisHelper>();
            services.AddScoped<ProcessHelper>();
        }
    }
}
