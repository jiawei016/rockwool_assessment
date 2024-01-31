using Autofac;
using thirdparty_api;
using web_api.Repository.Interfaces;

namespace web_api.DependencyModule
{
    public class OtherAPIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NewsAPI>().SingleInstance();
        }
    }
}
