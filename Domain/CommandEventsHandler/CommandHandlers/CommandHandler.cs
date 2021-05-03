using Domain.Interface.ICommandEventsHandler;
using Domain.Notifications;
using Infrastructure.CommandEventsHandler;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CommandEventsHandler.CommandHandlers
{
    /// <summary>
    /// 领域命令处理程序
    /// 用来作为全部处理程序的基类，提供公共方法和接口数据
    /// </summary>
    public class CommandHandler
    {
        // 注入中介处理接口（目前用不到，在领域事件中用来发布事件）
        private readonly IMediatorHandler _bus;


        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="bus"></param>
        public CommandHandler(IMediatorHandler bus)
        {
            _bus = bus;
        }


        //将领域命令中的验证错误信息收集
        //目前用的是缓存方法（以后通过领域通知替换）
        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                //将错误信息提交到事件总线，派发出去
                _bus.RaiseEvent(new DomainNotification("", error.ErrorMessage));
            }
        }


    }
}
