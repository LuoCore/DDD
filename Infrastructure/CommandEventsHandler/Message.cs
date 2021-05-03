using Infrastructure.Interface.ICommandEventsHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.CommandEventsHandler
{
    /// <summary>
    /// 抽象类Message，用来获取我们事件执行过程中的类名
    /// 然后并且添加聚合根
    /// </summary>
    public abstract class Message : IMessage
    {

        public string MessageType { get;  set; }
        public Guid AggregateId { get;  set; }
        protected Message()
        {
            MessageType = GetType().Name;
        }

        
       
    }
}
