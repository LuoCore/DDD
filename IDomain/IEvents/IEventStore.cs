using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interface.IEvents
{
    public interface IEventStore : IEventBase
    {
        public Guid Id { get; }

        public string Data { get;  }

        public string User { get; }
    }
}
