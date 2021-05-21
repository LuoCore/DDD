using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using Infrastructure.CommandEventsHandler;
using MediatR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommandEventsHandler
{
    /// <summary>
    ///一个密封类，实现我们的中介内存总线
    /// </summary>
    public class MediatorHandler : IMediatorHandler
    {
        //构造函数注入
        private readonly IMediator _mediator;
        //注入服务工厂
        private readonly ServiceFactory _serviceFactory;
        private static readonly ConcurrentDictionary<Type, object> _requestHandlers = new ConcurrentDictionary<Type, object>();
        // 事件仓储服务
        private readonly IEventStoreRepository _eventStoreService;
        public MediatorHandler(IMediator mediator, ServiceFactory serviceFactory, IEventStoreRepository eventStoreService)
        {
            _mediator = mediator;
            _serviceFactory = serviceFactory;
            _eventStoreService = eventStoreService;
        }
        public Task RaiseEvent<T>(T @event,string eventName) where T : Event
        {
            // 除了领域通知以外的事件都保存下来
            if (!@event.MessageType.Equals("DomainNotification"))
                _eventStoreService?.Save(@event, eventName, System.Environment.UserName);

            // MediatR中介者模式中的第二种方法，发布/订阅模式
            return _mediator.Publish(@event);
        }

        public Task<bool> SendCommand<T>(T command) where T : Command
        {
            //这个是正确的
            return _mediator.Send(command);//请注意 入参 的类型
        }
    }
}
