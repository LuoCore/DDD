using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interface.IEvents
{
    public interface IMessage : IRequest<bool>
    {
        public string MessageType { get;}
        public Guid AggregateId { get;}
    }
}
