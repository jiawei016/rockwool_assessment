using Autofac;
using Autofac.Core;
using web_api.Repository;
using web_api.Repository.Interfaces;

namespace web_api.DependencyModule
{
    public class RepoModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NewsRepo>().As<INewsRepo>().SingleInstance();
        }
    }
}
