using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entitys
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/6 11:06:05
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class Permission
    {
        public int ID { get; set; }
        public string PermissionId { get; set; }
        public string PermissionName { get; set; }

        public int PermissionType { get; set; }

        public string PermissionAction { get; set; }

        public bool IsValid { get; set; }
    }
}
