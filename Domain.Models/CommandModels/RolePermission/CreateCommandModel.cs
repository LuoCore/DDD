using Domain.Models.CommandModels.Permission;
using System;
using System.Collections.Generic;

namespace Domain.Models.CommandModels.RolePermission
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 16:09:54
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class CreateCommandModel : RolePermissionCommandModel
    {

        public CreateCommandModel(Guid gid,string roleId,string permissionId)
        {

            this.RolePermissionId = gid;
            this.RoleId = roleId;
            this.PermissionId = permissionId;
        }

        public override bool VerifyData()
        {
            return true;
        }
    }
}
