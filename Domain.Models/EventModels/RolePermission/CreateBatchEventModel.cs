using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.EventModels.RolePermission
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/22 17:28:08
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class CreateBatchEventModel: RolePermissionEventModel
    {
        public CreateBatchEventModel(Guid gid,string roleId, List<string> permissionIds)
        {
            this.AggregateId = gid;
            this.RolePermissionId = gid;
            this.RoleId = roleId;
            this.PermissionIds = permissionIds;
        }

        public List<string> PermissionIds { get; set; }
    }
}
