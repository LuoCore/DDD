using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interface.ICommandEventsHandler
{
    public interface IMessage : IRequest<bool>
    {
        string MessageType { get;  set; }
        Guid AggregateId { get;  set; }
    }
}
