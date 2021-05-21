using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandEventsHandler.EventHandlers
{
    public class UserEventHandler :
        INotificationHandler<Domain.Models.EventModels.User.CreateUserEventModel>
    {
        // 学习被注册成功后的事件处理方法
        public Task Handle(Domain.Models.EventModels.User.CreateUserEventModel req, CancellationToken cancellationToken)
        {

           
            return Task.FromResult(req);
        }
    }
}
