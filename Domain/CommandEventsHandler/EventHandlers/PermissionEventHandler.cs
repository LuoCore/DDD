using Domain.Models.EventModels.Permission;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandEventsHandler.EventHandlers
{
    public class PermissionEventHandler :
        INotificationHandler<Domain.Models.EventModels.Permission.PermissionCreateEventModel>,
        INotificationHandler<Domain.Models.EventModels.Permission.PermissionUpdateEventModel>,
        INotificationHandler<Domain.Models.EventModels.Permission.PermissionDeleteEventModel>
    {
        // 学习被注册成功后的事件处理方法
        public Task Handle(Domain.Models.EventModels.Permission.PermissionCreateEventModel req, CancellationToken cancellationToken)
        {

            return Task.FromResult(req);
        }

        public Task Handle(PermissionUpdateEventModel notification, CancellationToken cancellationToken)
        {
            return Task.FromResult(notification);
        }

        public Task Handle(PermissionDeleteEventModel notification, CancellationToken cancellationToken)
        {
            return Task.FromResult(notification);
        }
    }
}
