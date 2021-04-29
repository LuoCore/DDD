using Domain.Commands.User;
using Domain.Interface.ICommandHandlers;
using Domain.Interface.ISeedwork;
using Domain.Notifications;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandHandlers
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/4/28 15:25:29
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class UserCommandHandler : CommandHandler, IRequestHandler<Commands.User.UserCreateCommand, bool>
    {
        // 注入仓储接口
        private readonly Domain.Interface.IRepository.IUsersRepository _UserRepository;
        public UserCommandHandler(Interface.IRepository.IUsersRepository userRepository, IUnitOfWork uow, IMediatorHandler bus, IMemoryCache cache) : base(uow, bus, cache)
        {
            _UserRepository = userRepository;
        }

        public Task<bool> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            // 命令验证
            if (!request.IsValid())
            {
                // 错误信息收集
                NotifyValidationErrors(request);
                // 返回，结束当前线程
                return Task.FromResult(false);
            }
            // 实例化领域模型，这里才真正的用到了领域模型
            // 注意这里是通过构造函数方法实现
            var address = new  Models.ViewObject.AddressViewObject(request.Province, request.City, request.County, request.Street);
            var student = new Models.Entitys.UserEntity(Guid.NewGuid(), request.Name, request.Email, request.Phone, request.BirthDate, address);

           

            // 判断邮箱是否存在
            // 这些业务逻辑，当然要在领域层中（领域命令处理程序中）进行处理
            if (_UserRepository.GetByEmail(student.Email) != null)
            {
                this._bus.RaiseEvent(new DomainNotification("", "该邮箱已经被使用！"));
                return Task.FromResult(false);
            }
            if (_UserRepository.Create(new Infrastructure.Entitys.User() { })) 
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等
                this._bus.RaiseEvent(new Events.UserCreateEvent(student.Id, student.Name, student.Email, student.BirthDate, student.Phone));
            }
            return Task.FromResult(true);

        }

      
    }
}
