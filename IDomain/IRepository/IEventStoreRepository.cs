using Domain.Interface.ICommandEventsHandler;
using Infrastructure.CommandEventsHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interface.IRepository
{
    /// <summary>
    /// 领域存储服务接口
    /// </summary>
    public interface IEventStoreRepository
    {
        /// <summary>
        /// 将命令模型进行保存
        /// </summary>
        /// <typeparam name="T"> 泛型：Event命令模型</typeparam>
        /// <param name="theEvent"></param>
        void Save<T>(T theEvent) where T : Event;
    }
}
