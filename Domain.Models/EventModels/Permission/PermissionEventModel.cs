using Infrastructure.CommandEventsHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.EventModels.Permission
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/21 10:48:07
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public abstract class PermissionEventModel : Event
    {
        public Guid PermissionId { get; set; }
        public string PermissionName { get; set; }

        public CommandModels.Permission.PermissionCommandModel.PermissionTypeEnum PermissionType { get; set; }

        public string PermissionAction { get; set; }

        public string PermissionParentId { get; set; }

        public bool IsValid { get; set; }
    }
}
