using Autofac;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using web_api.Extensions.Dapper;

namespace web_api.DependencyModule
{
    public class ExtensionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DapperHelper>().SingleInstance();
        }
    }
}
