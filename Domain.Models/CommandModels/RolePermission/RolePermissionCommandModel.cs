using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.CommandModels.RolePermission
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/22 15:20:28
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public abstract class RolePermissionCommandModel : Infrastructure.CommandEventsHandler.Command
    {
        public Guid RolePermissionId { get; set; }
        public string RoleId { get; set; }
        public string PermissionId { get; set; }

    }
}
