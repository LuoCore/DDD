using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Common;

namespace Domain.Models.User.EventModels
{
    public class UserCreateEventModel:Infrastructure.CommandEventsHandler.Event
    {
        public UserCreateEventModel(Domain.Models.Entitys.UserEntity user)
        {
            User = user.ENTITY_USER;
            AggregateId = user.Id;
        }
        public Infrastructure.Entitys.User User { get; set; }
    }
}
