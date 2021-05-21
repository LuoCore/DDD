using Infrastructure.CommandEventsHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.EventModels.Role
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 18:21:27
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class UpdateEventModel : RoleEventModel
    {

        public UpdateEventModel(Guid gid, string RoleName, string RoleDescription, bool isvalid)
        {
            this.AggregateId = gid;
            this.RoleId = gid;
            this.RoleName = RoleName;
            this.RoleDescription = RoleDescription;
            this.IsValid = isvalid;
        }
    }
}
