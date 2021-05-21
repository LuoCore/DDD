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
        INotificationHandler<Domain.Models.EventModels.Permission.CreatePermissionEventModel>,
        INotificationHandler<Domain.Models.EventModels.Permission.UpdatePermissionEventModel>,
        INotificationHandler<Domain.Models.EventModels.Permission.DeletePermissionEventModel>
    {
        // 学习被注册成功后的事件处理方法
        public Task Handle(Domain.Models.EventModels.Permission.CreatePermissionEventModel req, CancellationToken cancellationToken)
        {

            return Task.FromResult(req);
        }

        public Task Handle(UpdatePermissionEventModel notification, CancellationToken cancellationToken)
        {
            return Task.FromResult(notification);
        }

        public Task Handle(DeletePermissionEventModel notification, CancellationToken cancellationToken)
        {
            return Task.FromResult(notification);
        }
    }
}
