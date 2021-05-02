using Application.Interface;
using Application.Interface.IServices;
using Application.Models.ViewModels;
using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using Infrastructure.Factory;
using Infrastructure.Interface.IFactory;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UsersService : SqlSugarRepository<ISqlSugarFactory, IUsersRepository>, IUsersService
    {
        // 中介者 总线
        private readonly IMediatorHandler Bus;
        // 事件源仓储
        private readonly IEventStoreRepository _eventStoreRepository;

        public UsersService(ISqlSugarFactory factory, IUsersRepository repository,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository
            ) : base(factory)
        {
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }


        public void Register(UserViewModel vm)
        {
            //这里引入领域设计中的写命令 还没有实现
            //请注意这里如果是平时的写法，必须要引入Student领域模型，会造成污染

           
            var registerCommand = new Domain.Models.User.CommandModels.UserCreateCommandModel(vm.UserId,vm.UserName, vm.Email, vm.Password, vm.Phone);
            Bus.SendCommand(registerCommand);
        }
    }
}
