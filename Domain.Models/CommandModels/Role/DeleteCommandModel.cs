using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.CommandModels.Role
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 18:03:41
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
   public class DeleteCommandModel:RoleCommandModel
    {
        public DeleteCommandModel(Guid gid)
        {
            this.RoleId = gid;
        }
    }
}
