using Application.Interface.IServices;
using Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.Layui.Models;

namespace Web.Layui.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsersService _studentAppService;
        private IMemoryCache _cache;
        // 将领域通知处理程序注入Controller
        private readonly DomainNotificationHandler _notifications;

        public HomeController(ILogger<HomeController> logger, IUsersService studentAppService, IMemoryCache cache, INotificationHandler<DomainNotification> notifications)
        {
            _logger = logger;
            _studentAppService = studentAppService;
            _cache = cache;
            // 强类型转换
            _notifications = (DomainNotificationHandler)notifications;
        }

        public IActionResult Index()
        {
            _studentAppService.Register(new Application.Models.ViewModels.UserViewModel() 
            {
                UserId=Guid.NewGuid(),
                Email="123@qq.com",
                Password="123",
                Phone="123456789",
                UserName="Admin"
            });
            // 是否存在消息通知
            if (!_notifications.HasNotifications())
                ViewBag.Sucesso = "Student Registered!";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
