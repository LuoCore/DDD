using Domain.Interface.IEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.IRepository
{
    public interface IEventStoreRepository 
    {
        void Store(IEventBase theEvent);
        Task<IList<IEventStore>> All(Guid aggregateId);

    }
}
