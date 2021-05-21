using Application.Interface.IServices;
using Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Layui.Areas.Admin.Controllers
{
    public class RoleController : BaseController<Application.Interface.IServices.IRoleService>
    {
        public RoleController(IRoleService service, INotificationHandler<DomainNotification> notifications) : base(service, notifications)
        {
        }

        public IActionResult RoleManage()
        {
            return View();
        }

        public IActionResult FormDialog() 
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Table()
        {
            var res = await _SERVICE.GetAll();
            return Json(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Application.Models.ViewModels.Role.CreateViewModel vm)
        {
            bool commandBool = await _SERVICE.Create(vm);
            // 是否存在消息通知
            if (commandBool)
            {
                return Json(new { status = true, msg = "成功！" });
            }
            else
            {
                var notificationDatas = _NOTIFICATIONS.GetNotifications();
                StringBuilder strMsg = new StringBuilder();
                foreach (var item in notificationDatas.Where(x => x.Key == "Role").ToList())
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
    }
}
