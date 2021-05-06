using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Common;

namespace Domain.Models.User.EventModels
{
    public class UserCreateEventModel:Infrastructure.CommandEventsHandler.Event
    {
        public UserCreateEventModel(Infrastructure.Entitys.User user)
        {
            User = user;
            AggregateId = User.UserId.ToGuid();
        }
        public Infrastructure.Entitys.User User { get; set; }
    }
}
