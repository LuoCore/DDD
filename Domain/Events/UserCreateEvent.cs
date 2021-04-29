using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/4/28 17:35:53
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class UserCreateEvent:Events.EventBase
    {
        public UserCreateEvent(Guid id, string name, string email, DateTime birthDate, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Phone = phone;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }

        public string Phone { get; private set; }
    }
}
