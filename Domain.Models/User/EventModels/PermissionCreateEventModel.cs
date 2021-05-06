using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Common;

namespace Domain.Models.User.EventModels
{
    public class PermissionCreateEventModel : Infrastructure.CommandEventsHandler.Event
    {
        public PermissionCreateEventModel(Infrastructure.Entitys.Permission permission)
        {
            Permission = permission;
            AggregateId = Permission.PermissionId.ToGuid();
        }
        public Infrastructure.Entitys.Permission Permission { get; set; }
    }
}
