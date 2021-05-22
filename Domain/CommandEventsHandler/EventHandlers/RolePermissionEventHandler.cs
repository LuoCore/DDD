using Domain.Models.EventModels.RolePermission;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandEventsHandler.EventHandlers
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/22 17:32:20
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class RolePermissionEventHandler : MediatR.INotificationHandler<Domain.Models.EventModels.RolePermission.CreateBatchEventModel>
    {
        public Task Handle(CreateBatchEventModel notification, CancellationToken cancellationToken)
        {
            return Task.FromResult(notification);
        }
    }
}
