using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common;
using Application.Models.ViewModels.User;
using Application.Models.ViewModels;

namespace Web.Layui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
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
        public async Task<IActionResult> Login(Application.Models.ViewModels.User.UserLoginViewModel vm)
        {
            string verifiCode = HttpContext.Session.GetString("SecurityCode");
            if (!verifiCode.Equals(vm.VerifiCode))
            {
                return Json(new { status = false, msg = "验证码错误！" });
            }
            var user = await _userService.UserLogin(vm);
            if (user == null || string.IsNullOrWhiteSpace(user.UserName))
            {
                return Json(new { status = false, msg = "用户名或密码错误！" });
            }
            HttpContext.GetEndpoint();
            HttpContext.Response.Cookies.Append("User", user.ToJson());
            HttpContext.Request.Cookies.TryGetValue("User", out string value);
            return Json(new { status = true, msg = "登录成功！" });

        }



        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Application.Models.ViewModels.User.UserCreateViewModel vm)
        {
            bool regBool = await _userService.UserRegister(new Application.Models.ViewModels.User.UserCreateViewModel()
            {
                UserName = vm.UserName,
                Password = vm.Password,
                Email = vm.Email,
                Phone = vm.Phone
            });
            // 是否存在消息通知
            if (regBool)
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
                if (strMsg.Length < 1)
                {
                    strMsg.Append("注册发生异常！");
                }
                return Json(new { status = false, msg = strMsg });
            }

        }

        public IActionResult ValidateCode()
        {
            var verifCode = new Infrastructure.Utility.VerificationCode(4, Infrastructure.Utility.VerificationCode.CodeType.数字);
            HttpContext.Session.SetString("SecurityCode", verifCode.CheckCode);
            return File(verifCode.CreateCheckCodeByteArray(), "image/" + System.Drawing.Imaging.ImageFormat.Gif.ToString());
        }

        public IActionResult Permission()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PermissionTable(PermissionViewModel vm)
        {
            var res = await _userService.QueryPermission(vm);
            return Json(res);

        }


        public IActionResult PermissionForm(PermissionViewModel vm)
        {
            return PartialView();

        }
        [HttpPost]
        public async Task<IActionResult> PermissionCreate(PermissionCreateViewModel vm)
        {
            bool commandBool = await _userService.CreatePermission(vm);
            // 是否存在消息通知
            if (commandBool)
            {
                return Json(new { status = true, msg = "成功！" });
            }
            else
            {
                var notificationDatas = _notifications.GetNotifications();
                StringBuilder strMsg = new StringBuilder();
                foreach (var item in notificationDatas.Where(x => x.Key == "Permission").ToList())
                {
                    strMsg.Append(item.Value);
                }
                if (strMsg.Length < 1)
                {
                    strMsg.Append("发生异常！");
                }
                return Json(new { status = false, msg = strMsg });
            }

        }

        [HttpPost]
        public async Task<IActionResult> PermissionSelect()
        {
            List<LayuiSelectViewModel> listSelect = new List<LayuiSelectViewModel>();
            var resSelect = new LayuiSelectViewModel()
            {
                Name = "顶级",
                value = ""
            };
            var resData = await _userService.GetPermissionSelect(resSelect.value);
            if (resData != null && resData.Count > 0)
            {
                resSelect.children = resData;
            }
            listSelect.Add(resSelect);
            return Json(listSelect);

        }

    }
}
