
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandEventsHandler.EventHandlers
{
    public class UserEventHandler :
        INotificationHandler<Domain.Models.User.EventModels.UserCreateEvent>
    {
        // 学习被注册成功后的事件处理方法
        public Task Handle(Domain.Models.User.EventModels.UserCreateEvent message, CancellationToken cancellationToken)
        {
            // 恭喜您，注册成功，欢迎加入我们。

            return Task.CompletedTask;
        }

       
    }
}
