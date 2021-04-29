using Domain.Interface.IEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public class StoredEvent : IEventBase, Interface.IEvents.IEventStore
    {
        public StoredEvent()
        {

        }
        public StoredEvent(IEventBase theEvent, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }

        public Guid Id { get; set; }

        public string Data { get; set; }

        public string User { get; set; }

        public DateTime Timestamp { get; set; }

        public string MessageType { get; set; }

        public Guid AggregateId { get; set; }
    }
}
