using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Layui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public abstract class BaseController<T> : Controller
    {
        protected readonly T _SERVICE;
        // 将领域通知处理程序注入Controller
        protected readonly Domain.Notifications.DomainNotificationHandler _NOTIFICATIONS;
        public BaseController(T service, MediatR.INotificationHandler<Domain.Notifications.DomainNotification> notifications)
        {
            _SERVICE = service;
            // 强类型转换
            _NOTIFICATIONS = (Domain.Notifications.DomainNotificationHandler)notifications;
        }

    }
}
