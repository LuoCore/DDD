using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
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
            throw new NotImplementedException();
        }
    }
}
