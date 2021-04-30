using Domain.CommandEventsHandler.Events.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandEventsHandler.EventHandlers
{
    public class UserEventHandler :
        INotificationHandler<UserRegisteredEvent>
    {
        // 学习被注册成功后的事件处理方法
        public Task Handle(UserRegisteredEvent message, CancellationToken cancellationToken)
        {
            // 恭喜您，注册成功，欢迎加入我们。

            return Task.CompletedTask;
        }

       
    }
}
