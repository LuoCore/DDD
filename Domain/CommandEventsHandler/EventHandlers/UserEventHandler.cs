
using Domain.Models.User.EventModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandEventsHandler.EventHandlers
{
    public class UserEventHandler :
        INotificationHandler<Domain.Models.User.EventModels.UserCreateEventModel>,
        INotificationHandler<Domain.Models.User.EventModels.PermissionCreateEventModel>
    {
        // 学习被注册成功后的事件处理方法
        public Task Handle(Domain.Models.User.EventModels.UserCreateEventModel message, CancellationToken cancellationToken)
        {

            message.MessageType = "恭喜您，注册成功，欢迎加入我们。";
            return Task.FromResult(message);
        }

        public Task Handle(PermissionCreateEventModel notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
