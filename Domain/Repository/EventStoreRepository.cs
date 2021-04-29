using Domain.Events;
using Domain.Interface.IEvents;
using Domain.Interface.IRepository;
using Infrastructure.Factory;
using Infrastructure.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class EventStoreRepository : SqlSugarRepository<SqlSugarFactory>, IEventStoreRepository
    {
        public EventStoreRepository(SqlSugarFactory factory) : base(factory)
        {

        }
        public Task<IList<IEventStore>> All(Guid aggregateId)
        {
            throw new NotImplementedException();
        }

        public void Save<T>(T theEvent) where T : IEventBase
        {
            // 对事件模型序列化
            var serializedData = JsonConvert.SerializeObject(theEvent);

          
        }

        public void Store(IEventBase theEvent)
        {
            // 对事件模型序列化
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                "");

            Factory.GetDbContext((db) =>
            {
                db.Insertable<StoredEvent>(storedEvent).ExecuteCommand();
            });
        }
    }
}
