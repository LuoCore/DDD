using Infrastructure.CommandEventsHandler;
using System;
using System.Collections.Generic;

namespace Domain.Models.EventModels.Role
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 17:18:28
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class CreateEventModel : RoleEventModel
    {
        
        public CreateEventModel(Guid gid, string RoleName,string RoleDescription,bool isvalid,List<string> permissionIds)
        {
            this.AggregateId = gid;
            this.RoleId = gid;
            this.RoleName = RoleName;
            this.RoleDescription = RoleDescription;
            this.IsValid = isvalid;
            this.PermissionIds = permissionIds;
        }
    }
}
