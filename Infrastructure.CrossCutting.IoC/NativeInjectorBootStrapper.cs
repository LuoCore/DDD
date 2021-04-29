using Domain.CommandHandlers;
using Domain.Interface.ICommandHandlers;
using Domain.Interface.IRepository;
using Domain.Interface.ISeedwork;
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

namespace Infrastructure.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // 命令总线Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            // 注入 应用层Application
            services.AddScoped<Application.Interface.IUsersService, Application.Services.UsersService>();

            // Domain - Events
            // 将事件模型和事件处理程序匹配注入

            // 领域通知
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            // 领域事件
            services.AddScoped<INotificationHandler<Domain.Events.UserCreateEvent>, Domain.EventHandlers.UserEventHandler>();


            // 领域层 - 领域命令
            // 将命令模型和命令处理程序匹配
            services.AddScoped<IRequestHandler<Domain.Commands.User.UserCreateCommand, bool>, Domain.CommandHandlers.UserCommandHandler > ();
            // 领域层 - Memory
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });

            // 注入 基础设施层 - 数据层
            services.AddScoped<IUsersRepository, UsersRepository>();

      


            // 注入 基础设施层 - 事件溯源
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();

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
