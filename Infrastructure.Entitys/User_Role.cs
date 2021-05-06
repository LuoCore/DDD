using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entitys
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/6 11:07:13
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class User_Role
    {
        public int ID { get; set; }
        public string UserRoleId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
