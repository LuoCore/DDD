using Domain.Interface.ICommandEventsHandler;
using Domain.Models.EventModels.Permission;
using Domain.Models.EventModels.Role;
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

       private readonly IMediatorHandler Bus;
        public RoleEventHandler(IMediatorHandler bus)
        {
            Bus = bus;
        }
        public Task<Boolean> Handle(Domain.Models.EventModels.Role.CreateEventModel req, CancellationToken cancellationToken)
        {
            var CreateCommand = new Domain.Models.CommandModels.RolePermission.CreateBatchCommandModel(Guid.NewGuid(),req.RoleId.ToString(),req.PermissionIds);
            return Bus.SendCommand(CreateCommand);
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
