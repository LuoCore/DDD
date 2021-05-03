using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using Domain.Models.StoredEvent.EventModels;
using Infrastructure.CommandEventsHandler;
using Infrastructure.Interface.IFactory;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void Save<T>(T theEvent) where T : Event
        {
            // 对事件模型序列化
            var serializedData = Newtonsoft.Json.JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEventModel(
                theEvent,
                serializedData,
                "后台");
            Factory.GetDbContext((db) =>
            {
                db.Insertable<Infrastructure.Entitys.StoredEvent>(new 
                {
                    Data= storedEvent.Data,
                    AggregateId=storedEvent.AggregateId.ToString(),
                    EventId=storedEvent.Id.ToString(),
                    MessageType=storedEvent.MessageType,
                    CreateTime=DateTime.Now,
                    CreateName=storedEvent.User
                })
                .IgnoreColumns(ignoreNullColumn: true).ExecuteCommand();
            });
        }
    }
}
