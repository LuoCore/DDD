using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entitys
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/3 9:17:28
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class StoredEvent
    {
        public int ID { get; set; }
        public string EventId { get; set; }
        public string EventName { get; set; }
        public string AggregateId { get; set; }
        public string MessageType { get; set; }
        public string Data { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateName { get; set; }

    }
}
