using Infrastructure.CommandEventsHandler;
using System;
using Infrastructure.Common;

namespace Domain.Models.User.CommandModels
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/19 15:32:44
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class PermissionDeleteCommandModel : Command
    {
        public override bool IsValid()
        {
            return true;
        }
        // set 受保护，只能通过构造函数方法赋值
        public PermissionDeleteCommandModel(Guid gid)
        {
            this.AggregateId = gid;
            PERMISSION_ID = this.AggregateId;
        }

        public Guid PERMISSION_ID
        {
            get; protected set;
        }
    }
}
