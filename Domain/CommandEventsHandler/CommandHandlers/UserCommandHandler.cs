using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using Domain.Models.Entitys;
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
    public class UserCommandHandler : CommandHandler, IRequestHandler<Domain.Models.User.CommandModels.UserCreateCommandModel, bool>
    {
        // 注入仓储接口
        private readonly IUsersRepository _userRepository;
        // 注入总线
        private readonly IMediatorHandler Bus;
        private IMemoryCache Cache;
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="studentRepository"></param>
        /// <param name="uow"></param>
        /// <param name="bus"></param>
        /// <param name="cache"></param>
        public UserCommandHandler(IUsersRepository userRepository,
                                      IMediatorHandler bus,
                                      IMemoryCache cache
                                      ) : base(bus, cache)
        {
            _userRepository = userRepository;
            Bus = bus;
            Cache = cache;
        }
        /// <summary>
        ///  // RegisterStudentCommand命令的处理程序
        /// 整个命令处理程序的核心都在这里
        /// 不仅包括命令验证的收集，持久化，还有领域事件和通知的添加
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> Handle(Domain.Models.User.CommandModels.UserCreateCommandModel request, CancellationToken cancellationToken)
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
            var userReq = new Domain.Models.User.UserDomainModel(Guid.NewGuid(), request.UserName, request.Password,request.Email, request.Phone);

            // 判断邮箱是否存在
            // 这些业务逻辑，当然要在领域层中（领域命令处理程序中）进行处理
            if (_userRepository.ReadId(userReq.UserName.ToString()) != null)
            {
                ////这里对错误信息进行发布，目前采用缓存形式
                //List<string> errorInfo = new List<string>() { "该邮箱已经被使用！" };
                //Cache.Set("ErrorData", errorInfo);

                //引发错误事件
                Bus.RaiseEvent(new DomainNotification("", "该用户已存在！"));
                return Task.FromResult(false);

            }



            // 统一提交
            if (_userRepository.Create(new User()
            {
                UserId= userReq.Id.ToString(),
                UserName= userReq.UserName,
                Email=userReq.Email,
                Password=userReq.Password,
                Phone=userReq.Phone,
                CreateName="后台添加",
                CreateTime=DateTime.Now
            }))
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等

                Bus.RaiseEvent(new UserCreateEvent(userReq.Id, userReq.UserName, userReq.Email, userReq.Password, userReq.Phone));
              
            }

            return Task.FromResult(true);
        }
    }
}
