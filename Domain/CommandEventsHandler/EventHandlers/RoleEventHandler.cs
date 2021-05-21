using Domain.Models.EventModels.Permission;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandEventsHandler.EventHandlers
{
    public class RoleEventHandler :
        INotificationHandler<Domain.Models.EventModels.Role.CreateEventModel>,
        INotificationHandler<Domain.Models.EventModels.Role.UpdateEventModel>,
        INotificationHandler<Domain.Models.EventModels.Role.DeleteEventModel>
    {
        // 学习被注册成功后的事件处理方法
        public Task Handle(Domain.Models.EventModels.Role.CreateEventModel req, CancellationToken cancellationToken)
        {

            return Task.FromResult(req);
        }

        public Task Handle(Domain.Models.EventModels.Role.UpdateEventModel notification, CancellationToken cancellationToken)
        {
            return Task.FromResult(notification);
        }

        public Task Handle(Domain.Models.EventModels.Role.DeleteEventModel notification, CancellationToken cancellationToken)
        {
            return Task.FromResult(notification);
        }
    }
}
