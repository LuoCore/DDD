using Infrastructure.CommandEventsHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.EventModels.Permission
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 18:21:27
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class UpdatePermissionEventModel : PermissionEventModel
    {

        public UpdatePermissionEventModel(Guid gid, string permissionname, CommandModels.PermissionCommandModel.PermissionTypeEnum permissiontype, string permissionaction, string permissionparentid, bool isvalid)
        {
            this.AggregateId = gid;
            this.PermissionId = gid;
            this.PermissionName = permissionname;
            this.PermissionType = permissiontype;
            this.PermissionAction = permissionaction;
            this.PermissionParentId = permissionparentid;
            this.IsValid = isvalid;
        }
    }
}
