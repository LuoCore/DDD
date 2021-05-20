using Application.Models.ViewModels;
using Application.Models.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Layui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PermissionController : Controller
    {
       
        private readonly Application.Interface.IServices.IPermissionService _Service;
        // 将领域通知处理程序注入Controller
        private readonly Domain.Notifications.DomainNotificationHandler _notifications;
        public PermissionController(Application.Interface.IServices.IPermissionService userService, MediatR.INotificationHandler<Domain.Notifications.DomainNotification> notifications)
        {
            _Service = userService;
            // 强类型转换
            _notifications = (Domain.Notifications.DomainNotificationHandler)notifications;
        }
        public IActionResult Manage()
        {
            return View();
        }
        /// <summary>
        /// 权限管理表格查询
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Table(string parentId)
        {
            var res = await _Service.GetByPerentId(parentId);
            return Json(res);

        }

        /// <summary>
        /// 权限维护界面
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public IActionResult PermissionForm()
        {
            return PartialView();
        }



        /// <summary>
        /// 权限 创建
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(PermissionCreateViewModel vm)
        {
            bool commandBool = await _Service.Create(vm);
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
        [HttpGet]
        public async Task<IActionResult> Select()
        {
            List<LayuiSelectViewModel> listSelect = new List<LayuiSelectViewModel>();
            var resSelect = new LayuiSelectViewModel()
            {
                Name = "顶级",
                value = "0"
            };
            var resData = await _Service.SelectByParentId(resSelect.value);
            if (resData != null && resData.Count > 0)
            {
                resSelect.children = resData;
            }
            listSelect.Add(resSelect);
            return Json(listSelect);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteByIds(List<string> Ids)
        {

            if (Ids.Count < 1)
            {
                return Json(new { status = false, msg = "提交的数据为空" });
            }
            StringBuilder resMsg = new StringBuilder();
            foreach (var itemid in Ids)
            {
                bool commandBool = await _Service.DeleteById(itemid);
                if (commandBool)
                {
                    resMsg.Append(itemid + "，删除成功！" + Environment.NewLine);
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
                    resMsg.Append(itemid + "，删除失败！" + strMsg.ToString() + Environment.NewLine);
                }
            }
            return Json(new { status = true, msg = resMsg.ToString() });

        }
        [HttpPut]
        public async Task<IActionResult> Update()
        {
            return Json(new { });
        }
    }
}
