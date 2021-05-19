using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Common;

namespace Domain.Models.User.EventModels
{
    public class PermissionDeleteEventModel : Infrastructure.CommandEventsHandler.Event
    {
        public PermissionDeleteEventModel(Guid gid)
        {
            PERMISSION_ID = gid;
            AggregateId = PERMISSION_ID;
        }
        public Guid PERMISSION_ID { get; set; }
    }
}
