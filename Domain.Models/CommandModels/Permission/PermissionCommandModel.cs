using Infrastructure.CommandEventsHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.CommandModels.Permission
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 17:35:06
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public abstract class PermissionCommandModel : Command
    {
        public Guid PermissionId { get;protected set; }
        public string PermissionName { get; protected set; }

        public PermissionTypeEnum PermissionType { get; protected set; }

        public string PermissionAction { get; protected set; }

        public string PermissionParentId { get; protected set; }

        public bool IsValid { get; protected set; }

        public enum PermissionTypeEnum 
        {
            菜单=0,
            按钮=1
        }
    }
}
