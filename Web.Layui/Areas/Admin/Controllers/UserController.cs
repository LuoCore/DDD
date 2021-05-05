using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common;

namespace Web.Layui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController :  Controller
    {
        private readonly Application.Interface.IServices.IUsersService _userService;
        // 将领域通知处理程序注入Controller
        private readonly Domain.Notifications.DomainNotificationHandler _notifications;
        public UserController(Application.Interface.IServices.IUsersService userService, MediatR.INotificationHandler<Domain.Notifications.DomainNotification> notifications)
        {
            _userService = userService;
            // 强类型转换
            _notifications = (Domain.Notifications.DomainNotificationHandler)notifications;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Application.Models.ViewModels.UserLoginViewModel vm)
        {
            string verifiCode = HttpContext.Session.GetString("SecurityCode");
            if (!verifiCode.Equals(vm.VerifiCode))
            {
                return Json(new { status = false, msg = "验证码错误！" });
            }
            var user = await _userService.Login(vm);
            if (user == null||string.IsNullOrWhiteSpace(user.UserName))
            {
                return Json(new { status = false, msg = "用户名或密码错误！" });
            }
            HttpContext.GetEndpoint();
            HttpContext.Response.Cookies.Append("User", user.ToJson());
            HttpContext.Request.Cookies.TryGetValue("User", out string value);
            return Json(new { status = true,msg="登录成功！" });
     
        }



        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Application.Models.ViewModels.UserCreateViewModel vm)
        {
            _userService.Register(new Application.Models.ViewModels.UserCreateViewModel()
            {
                UserName = vm.UserName,
                Password = vm.Password,
                Email = vm.Email,
                Phone = vm.Phone
            });
            // 是否存在消息通知
            if (!await _notifications.AsyncHasNotifications())
            {
                return Json(new { status = true, msg = "注册成功！" });
            }
            else
            {
                var notificationDatas = _notifications.GetNotifications();
                StringBuilder strMsg = new StringBuilder();
                foreach (var item in notificationDatas.Where(x => x.Key == "User").ToList())
                {
                    strMsg.Append(item.Value);
                }
                return Json(new { status = false, msg = strMsg.ToString() });
            }

        }

        public IActionResult ValidateCode()
        {
            var verifCode = new Infrastructure.Utility.VerificationCode(4, Infrastructure.Utility.VerificationCode.CodeType.数字);
            HttpContext.Session.SetString("SecurityCode", verifCode.CheckCode);
            return File(verifCode.CreateCheckCodeByteArray(), "image/" + System.Drawing.Imaging.ImageFormat.Gif.ToString());
        }
    }
}
