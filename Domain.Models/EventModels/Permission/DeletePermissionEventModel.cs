using Infrastructure.CommandEventsHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.EventModels.Permission
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 18:21:38
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class DeletePermissionEventModel : PermissionEventModel
    {
        public DeletePermissionEventModel(Guid gid)
        {
            this.AggregateId = gid;
            PermissionId = gid;
        }

    }
}
