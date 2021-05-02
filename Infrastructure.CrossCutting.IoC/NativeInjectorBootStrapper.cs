using Application.Interface.IServices;
using Application.Services;
using Domain.CommandEventsHandler;
using Domain.CommandEventsHandler.CommandHandlers;
using Domain.CommandEventsHandler.EventHandlers;
using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using Domain.Notifications;
using Domain.Repository;
using Infrastructure.Factory;
using Infrastructure.Interface.IFactory;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace Infrastructure.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {


        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ISqlSugarFactory, SqlSugarFactory>();


            AddSqlSugarClient<ISqlSugarFactory>(services, (sp, cc) =>
            {
                var ConnectStr = sp.GetService<IConfiguration>().GetConnectionString("SqlserverConnection");
                var ConnectStr2 = sp.GetService<IConfiguration>().GetConnectionString("SqlserverConnection");
                cc.ConnectionString = ConnectStr;//主库
                cc.DbType = DbType.Sqlite;
                cc.InitKeyType = InitKeyType.Attribute;//从特性读取主键和自增列信息
                cc.IsAutoCloseConnection = true;//开启自动释放模式和EF原理一样我就不多解释了
                //从库
                cc.SlaveConnectionConfigs = new List<SlaveConnectionConfig>()
                {
                     new SlaveConnectionConfig()
                     {
                         HitRate=10,//HitRate 越大走这个从库的概率越大
                          ConnectionString=ConnectStr2
                     }
                };
            });



            // 领域层 - Memory
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });

            // 命令总线Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            // 注入 基础设施层 - 数据层
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            // 领域通知
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            // 领域事件
            services.AddScoped<INotificationHandler<Domain.Models.User.EventModels.UserCreateEvent>, UserEventHandler>();
            // 领域层 - 领域命令
            // 将命令模型和命令处理程序匹配
            services.AddScoped<IRequestHandler<Domain.Models.User.CommandModels.UserCreateCommandModel, bool>, UserCommandHandler>();


            services.AddScoped<IUsersService, UsersService>();


        }
        // <summary>
        /// SqlSugar上下文注入
        /// </summary>
        /// <typeparam name="TSugarContext">要注册的上下文的类型</typeparam>
        /// <param name="serviceCollection"></param>
        /// <param name="configAction"></param>
        /// <param name="lifetime">用于在容器中注册TSugarClient服务的生命周期</param>
        /// <returns></returns>
        internal static IServiceCollection AddSqlSugarClient<TSugarContext>(IServiceCollection serviceCollection, Action<IServiceProvider, ConnectionConfig> configAction, ServiceLifetime lifetime = ServiceLifetime.Scoped)
             where TSugarContext : ISqlSugarFactory
        {
            serviceCollection.TryAdd(new ServiceDescriptor(typeof(ConnectionConfig), sp => ConnectionConfigFactory(sp, configAction), lifetime));
            serviceCollection.Add(new ServiceDescriptor(typeof(ConnectionConfig), p => ConnectionConfigFactory(p, configAction), lifetime));
            serviceCollection.TryAdd(new ServiceDescriptor(typeof(TSugarContext), typeof(TSugarContext), lifetime));
            return serviceCollection;
        }
        private static ConnectionConfig ConnectionConfigFactory(IServiceProvider applicationServiceProvider, Action<IServiceProvider, ConnectionConfig> configAction)
        {
            var config = new ConnectionConfig();

            configAction.Invoke(applicationServiceProvider, config);
            return config;
        }
    }
}
