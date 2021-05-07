using Infrastructure.CommandEventsHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.User.CommandModels
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/6 13:19:11
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class PermissionCreateCommandModel : Command
    {
        public override bool IsValid()
        {
            return true;
        }
        // set 受保护，只能通过构造函数方法赋值
        public PermissionCreateCommandModel(Guid gid, string permissionName, Models.Entitys.PermissionEntity.PermissionTypeEnum permissionType,string permissionAction,string permissionParentId,bool isValid)
        {
            this.AggregateId = gid;
            PERMISSION = new Infrastructure.Entitys.Permission()
            {
                PermissionId = gid.ToString(),
                PermissionName = permissionName,
                PermissionType=(int) permissionType,
                PermissionAction= permissionAction,
                PermissionParentId= permissionParentId,
                IsValid= isValid
            };
        }

        public Infrastructure.Entitys.Permission PERMISSION { get; protected set; }
    }
}
