using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.User.EventModels
{
    public class UserCreateEventModel:Infrastructure.CommandEventsHandler.Event
    {
        public UserCreateEventModel(Guid userId, string name, string email, string password, string phone)
        {
            UserId = userId;
            UserName = name;
            Email = email;
            Password = password;
            Phone = phone;
            AggregateId = userId;
        }
        public Guid UserId { get; protected set; }

        public string UserName { get; protected set; }

        public string Email { get; protected set; }

        public string Password { get; protected set; }

        public string Phone { get; protected set; }
    }
}
