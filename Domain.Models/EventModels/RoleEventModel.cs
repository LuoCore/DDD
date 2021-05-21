using Infrastructure.CommandEventsHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.EventModels
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/21 16:04:15
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class RoleEventModel : Event
    {
    
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }

        public string RoleDescription { get; set; }

        public bool IsValid { get; set; }
    }
}
