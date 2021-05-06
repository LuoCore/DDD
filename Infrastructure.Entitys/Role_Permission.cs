using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entitys
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/6 11:08:55
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class Role_Permission
    {
        public int ID { get; set; }
        public string RolePermissionId { get; set; }
        public string RoleId { get; set; }
        public string PermissionId { get; set; }
    }
}
