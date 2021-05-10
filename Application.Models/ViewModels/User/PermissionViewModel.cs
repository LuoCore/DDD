using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.ViewModels.User
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/7 9:55:45
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class PermissionViewModel
    {
        public string PermissionId { get; set; }
        public string PermissionName { get; set; }

        public int PermissionType { get; set; }

        public string PermissionAction { get; set; }

        public string PermissionParentId { get; set; }

        public bool PermissionLeve { get; set; }

        public bool IsValid { get; set; }
    }
}
