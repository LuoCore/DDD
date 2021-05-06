﻿using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using Domain.Models.Entitys;
using Domain.Models.User.CommandModels;
using Domain.Models.User.EventModels;
using Domain.Notifications;
using Infrastructure.Entitys;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandEventsHandler.CommandHandlers
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/4/30 17:13:11
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<Domain.Models.User.CommandModels.UserCreateCommandModel, bool>,
        IRequestHandler<Domain.Models.User.CommandModels.PermissionCreateCommandModel, bool>
    {
        // 注入仓储接口
        private readonly IUsersRepository _userRepository;
        // 注入总线
        private readonly IMediatorHandler Bus;


        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="studentRepository"></param>
        /// <param name="uow"></param>
        /// <param name="bus"></param>
        /// <param name="cache"></param>
        public UserCommandHandler(IUsersRepository userRepository, IMediatorHandler bus) : base(bus)
        {
            _userRepository = userRepository;
            Bus = bus;
        }
        /// <summary>
        ///  // RegisterStudentCommand命令的处理程序
        /// 整个命令处理程序的核心都在这里
        /// 不仅包括命令验证的收集，持久化，还有领域事件和通知的添加
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> Handle(UserCreateCommandModel request, CancellationToken cancellationToken)
        {
            // 命令验证
            if (!request.IsValid())
            {
                // 错误信息收集
                NotifyValidationErrors(request);
                // 返回，结束当前线程
                return Task.FromResult(false);
            }

           var dmData= new Models.Entitys.UserEntity(request.AggregateId, request.User.UserName, request.User.Password, request.User.Email, request.User.Phone, request.User.CreateName);
            // 判断邮箱是否存在
            // 这些业务逻辑，当然要在领域层中（领域命令处理程序中）进行处理
            UserEntity user = new UserEntity() { User=new UserEntity().User.UserName= dmData.User.UserName };
            user.User = new User() { UserName = dmData.User.UserName };
            var existingStudent = _userRepository.ReadUser(user);
            if (existingStudent != null && existingStudent.Id != dmData.Id)
            {
                if (!existingStudent.Equals(dmData))
                {

                    Bus.RaiseEvent(new DomainNotification("", "该用户已存在！"));
                    return Task.FromResult(false);

                }
            }
            


            //提交
            if (_userRepository.CreateUser(dmData))
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等
              var ddd= Bus.RaiseEvent(new UserCreateEventModel(request.User));

            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(PermissionCreateCommandModel request, CancellationToken cancellationToken)
        {
            // 命令验证
            if (!request.IsValid())
            {
                // 错误信息收集
                NotifyValidationErrors(request);
                // 返回，结束当前线程
                return Task.FromResult(false);
            }

            // 判断邮箱是否存在
            // 这些业务逻辑，当然要在领域层中（领域命令处理程序中）进行处理
            if (_userRepository.ReadPermission(new Permission() { PermissionName=request.Permission.PermissionName}) != null)
            {
                //引发错误事件
                Bus.RaiseEvent(new DomainNotification("User", "该用户已存在！"));
                return Task.FromResult(false);
            }


            //提交
            if (_userRepository.CreatePermission(request.Permission))
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等
                Bus.RaiseEvent(new PermissionCreateEventModel(request.Permission));
            }

            return Task.FromResult(true);
        }
    }
}
