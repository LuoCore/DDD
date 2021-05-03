using Infrastructure.CommandEventsHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.StoredEvent.EventModels
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/3 9:24:00
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class StoredEventModel : Infrastructure.CommandEventsHandler.Event
    {
        /// <summary>
        /// 构造方式实例化
        /// </summary>
        /// <param name="theEvent"></param>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public StoredEventModel(Event theEvent, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }
        // 事件存储Id
        public Guid Id { get; private set; }
        // 存储的数据
        public string Data { get; private set; }
        // 用户信息
        public string User { get; private set; }
    }
}
