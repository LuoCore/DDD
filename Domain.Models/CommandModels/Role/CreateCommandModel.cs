using Domain.Models.CommandModels.Permission;
using System;
using System.Collections.Generic;

namespace Domain.Models.CommandModels.Role
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 16:09:54
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class CreateCommandModel : RoleCommandModel
    {

        public CreateCommandModel(Guid gid,string RoleName, string RoleDescription,bool IsValid,List<string> permissionIds)
        {

            this.RoleId = gid;
            this.RoleName = RoleName;
            this.RoleDescription = RoleDescription;
            this.IsValid = IsValid;
            this.PermissionIds = permissionIds;
        }

        public override bool VerifyData()
        {
            return true;
        }
    }
}
