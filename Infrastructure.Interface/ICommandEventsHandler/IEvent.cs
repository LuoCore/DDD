using System;
using System.Collections.Generic;
using MediatR;

namespace Infrastructure.Interface.ICommandEventsHandler
{
    /// <summary>
    /// 事件模型 抽象基类，继承 INotification
    /// 也就是说，拥有中介者模式中的 发布/订阅模式
    /// 同时继承了Messgae 也就是继承了 请求/响应模式
    /// </summary>
    public interface IEvent: IMessage, INotification
    {
        // 时间戳
        public DateTime Timestamp { get;}
    }
}
