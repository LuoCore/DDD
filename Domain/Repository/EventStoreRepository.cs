using Domain.Interface.IRepository;
using Infrastructure.CommandEventsHandler;
using Infrastructure.Interface.IFactory;
using Infrastructure.Repository;
using System;

namespace Domain.Repository
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/4/30 16:14:11
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class EventStoreRepository: SqlSugarRepository<ISqlSugarFactory>,IEventStoreRepository
    {
        public EventStoreRepository(ISqlSugarFactory factory) : base(factory)
        {

        }

        public void Save<T>(T theEvent,string userName) where T : Event
        {
            // 对事件模型序列化
            var serializedData = Newtonsoft.Json.JsonConvert.SerializeObject(theEvent);
            Factory.GetDbContext((db) =>
            {
                db.Insertable<Infrastructure.Entitys.StoredEvent>(new 
                {
                    Data= serializedData,
                    AggregateId= theEvent.AggregateId.ToString(),
                    EventId= Guid.NewGuid().ToString(),
                    MessageType= theEvent.MessageType,
                    CreateTime=DateTime.Now,
                    CreateName= userName
                })
                .IgnoreColumns(ignoreNullColumn: true).ExecuteCommand();
            });
        }
    }
}
