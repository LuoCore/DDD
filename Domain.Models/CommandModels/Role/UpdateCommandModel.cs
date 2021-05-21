using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.CommandModels.Role
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 18:00:39
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class UpdateCommandModel : RoleCommandModel
    {
        public UpdateCommandModel(Guid gid, string rolename, string RoleDescription, bool IsValid)
        {
            this.RoleId = gid;
            this.RoleName = RoleName;
            this.RoleDescription = RoleDescription;
            this.IsValid = IsValid;
        }
    }
}
