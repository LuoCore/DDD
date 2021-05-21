using Application.Interface.IServices;
using Application.Models.ViewModels;
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
    [Area("Admin")]
    public class PermissionController : BaseController<IPermissionService>
    {
        public PermissionController(IPermissionService service, INotificationHandler<DomainNotification> notifications) : base(service, notifications)
        {
        }

        public IActionResult PermissionManage()
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
            var res = await _SERVICE.GetByPerentId(parentId);
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
        public async Task<IActionResult> Create(Application.Models.ViewModels.Permission.PermissionCreateViewModel vm)
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
            List<Application.Models.ViewModels.Layui.SelectViewModel> listSelect = new List<Application.Models.ViewModels.Layui.SelectViewModel>();
            var resSelect = new Application.Models.ViewModels.Layui.SelectViewModel()
            {
                Name = "顶级",
                value = "0"
            };
            var resData = await _SERVICE.SelectByParentId(resSelect.value);
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
                bool commandBool = await _SERVICE.DeleteById(itemid);
                if (commandBool)
                {
                    resMsg.Append(itemid + "，删除成功！" + Environment.NewLine);
                }
                else
                {
                    var notificationDatas = _NOTIFICATIONS.GetNotifications();
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
        public async Task<IActionResult> Update(Application.Models.ViewModels.Permission.PermissionUpdateViewModel vm)
        {
            bool commandBool = await _SERVICE.Update(vm);
            // 是否存在消息通知
            if (commandBool)
            {
                return Json(new { status = true, msg = "成功！" });
            }
            else
            {
                var notificationDatas = _NOTIFICATIONS.GetNotifications();
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
    }
}
