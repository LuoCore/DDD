using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.EventModels.RolePermission
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/22 17:26:44
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class RolePermissionEventModel : Infrastructure.CommandEventsHandler.Event
    {
        public Guid RolePermissionId { get; set; }
        public string RoleId { get; set; }
        public string PermissionId { get; set; }
    }
}
