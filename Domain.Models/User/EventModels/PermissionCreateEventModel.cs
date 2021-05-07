using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Common;

namespace Domain.Models.User.EventModels
{
    public class PermissionCreateEventModel : Infrastructure.CommandEventsHandler.Event
    {
        public PermissionCreateEventModel(Models.Entitys.PermissionEntity permission)
        {
            Permission = permission.PERMISSION;
            AggregateId = permission.Id;
        }
        public Infrastructure.Entitys.Permission Permission { get; set; }
    }
}
