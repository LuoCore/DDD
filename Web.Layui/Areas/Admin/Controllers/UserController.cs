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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

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
        /// <summary>
        /// 用户登录界面
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(Application.Models.ViewModels.User.UserLoginViewModel vm)
        {
            vm.UserName = vm.UserName.ToLower();
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
            //HttpContext.GetEndpoint();
            //HttpContext.Response.Cookies.Append("User", user.ToJson());
            //HttpContext.Request.Cookies.TryGetValue("User", out string value);


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserDataInfo", user.ToJson()),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //应该允许刷新身份验证会话。
                AllowRefresh = false,
                //认证票据过期的时间。
                // 一个value将覆盖ExpireTimeSpan选项
                //CookieAuthenticationOptions设置AddCookie。
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                //身份验证会话是否持久化
                // 多个请求。当与cookie、控件一起使用时
                //是否cookie的生存期是绝对的(匹配
                //认证票据的生命周期)或基于会话的。
                IsPersistent = false,
                //颁发身份验证票据的时间。
                IssuedUtc = DateTimeOffset.UtcNow,
                //作为http的完整路径或绝对URI
                //重定向响应值。
                RedirectUri = "/Admin/User/Login"
            };
            await HttpContext.SignInAsync(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);


            return Json(new { status = true, msg = "登录成功！" });

        }


        /// <summary>
        /// 用户注册界面
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(Application.Models.ViewModels.User.UserCreateViewModel vm)
        {
            bool regBool = await _userService.UserRegister(new Application.Models.ViewModels.User.UserCreateViewModel()
            {
                UserName = vm.UserName.ToLower(),
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
        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult ValidateCode()
        {
            var verifCode = new Infrastructure.Utility.VerificationCode(4, Infrastructure.Utility.VerificationCode.CodeType.数字);
            HttpContext.Session.SetString("SecurityCode", verifCode.CheckCode);
            return File(verifCode.CreateCheckCodeByteArray(), "image/" + System.Drawing.Imaging.ImageFormat.Gif.ToString());
        }
        /// <summary>
        /// 权限管理页面
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Permission()
        {
            return View();
        }
        /// <summary>
        /// 权限管理表格查询
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PermissionTable(string parentId)
        {
            var res = await _userService.GetRecursivePermission(parentId);
            return Json(res);

        }

        /// <summary>
        /// 权限维护界面
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public IActionResult PermissionForm(PermissionViewModel vm)
        {
            return PartialView();

        }
        /// <summary>
        /// 权限 创建
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
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
                return Json(new { status = false, msg = strMsg.ToString() });
            }

        }
        /// <summary>
        /// 获取树形权限下拉框
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PermissionSelect()
        {
            List<LayuiSelectViewModel> listSelect = new List<LayuiSelectViewModel>();
            var resSelect = new LayuiSelectViewModel()
            {
                Name = "顶级",
                value = "0"
            };
            var resData = await _userService.GetPermissionSelect(resSelect.value);
            if (resData != null && resData.Count > 0)
            {
                resSelect.children = resData;
            }
            listSelect.Add(resSelect);
            return Json(listSelect);

        }


        public async Task<IActionResult> DeletePermissionArray(List<string> permissionIdArray) 
        {
           
            if (permissionIdArray.Count < 1) 
            {
                return Json(new { status = false, msg = "提交的数据为空" });
            }
            StringBuilder resMsg = new StringBuilder();
            foreach (var itemid in permissionIdArray)
            {
                bool commandBool = await _userService.DeletePermission(itemid);
                if (commandBool)
                {
                    resMsg.Append(itemid + "，删除成功！"+Environment.NewLine);
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
                    resMsg.Append(itemid + "，删除失败！"+ strMsg.ToString() + Environment.NewLine);
                }
            }
            return Json(new { status = true, msg = resMsg.ToString() });

        }

    }
}
