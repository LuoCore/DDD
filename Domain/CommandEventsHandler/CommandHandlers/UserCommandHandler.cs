using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using Domain.Notifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models.CommandModels.User;
using Infrastructure.Common;

namespace Domain.CommandEventsHandler.CommandHandlers
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/4/30 17:13:11
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<CreateUserCommandModel, bool>
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
        public Task<bool> Handle(CreateUserCommandModel request, CancellationToken cancellationToken)
        {
            // 命令验证
            if (!request.VerifyData())
            {
                // 错误信息收集
                NotifyValidationErrors(request);
                // 返回，结束当前线程
                return Task.FromResult(false);
            }

            
            Infrastructure.Entitys.User entity = new Infrastructure.Entitys.User() 
            {
                UserId=request.UserId.ToString(),
                UserName=request.UserName,
                Email=request.Email,
                Phone=request.Phone,
                Password=request.Password,
                CreateTime=DateTime.Now,
                CreateName=request.CreateName
            };
           
            // 判断邮箱是否存在
            // 这些业务逻辑，当然要在领域层中（领域命令处理程序中）进行处理
            var existingUSER = _userRepository.ReadUserName(entity.UserName);
            if (existingUSER != null)
            {
                Bus.RaiseEvent(new DomainNotification("User", "该用户已存在！"));
                return Task.FromResult(false);
            }
            //提交
            if (_userRepository.CreateUser(entity))
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等
                Bus.RaiseEvent(new Domain.Models.EventModels.User.UserCreateEventModel(entity.UserId.StringToGuid(),entity.UserName,entity.Password,entity.Email,entity.Phone,entity.CreateName,entity.CreateTime));

            }

            return Task.FromResult(true);
        }

       

      
    }
}
