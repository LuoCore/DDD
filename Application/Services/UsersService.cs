﻿using Application.Interface;
using Application.Interface.IServices;
using Application.Models.ViewModels;
using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using Domain.Models.User.CommandModels;
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


        public void Register(UserCreateViewModel vm)
        {
            //这里引入领域设计中的写命令 还没有实现
            //请注意这里如果是平时的写法，必须要引入Student领域模型，会造成污染
            var registerCommand = new UserCreateCommandModel(Guid.NewGuid(), vm.UserName, vm.Email, vm.Password, vm.Phone, "用户注册");
            Bus.SendCommand(registerCommand);
        }

        public UserViewModel Login(UserLoginViewModel vm)
        {
            UserViewModel userRes = new UserViewModel();
            var userData = DbRepository.Login(vm.UserName, vm.Password);
            if (userData != null)
            {
                userRes.UserName = userData.UserName;
                userRes.Phone = userData.Phone;
                userRes.Email = userData.Email;
            }
            return userRes;
        }
    }
}
