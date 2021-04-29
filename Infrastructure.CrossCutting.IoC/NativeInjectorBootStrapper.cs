using Infrastructure.Factory;
using Infrastructure.Interface.IFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SqlSugar;
using System;

namespace Infrastructure.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {

            services.AddScoped<ISqlSugarFactory, SqlSugarFactory>();

            services.TryAdd(new ServiceDescriptor(typeof(ConnectionConfig),sp=>ConnectionConfigFactory(sp), ServiceLifetime.Scoped));


        }

        private static ConnectionConfig ConnectionConfigFactory(IServiceProvider sp)
        {
            var config = new ConnectionConfig();
            config.ConnectionString = sp.GetService<IConfiguration>().GetConnectionString("lucy");
            config.DbType = DbType.MySql;
            config.IsAutoCloseConnection = true;
            config.InitKeyType = InitKeyType.Attribute;
            config.IsShardSameThread = true;
            return config;
        }
    }
}
