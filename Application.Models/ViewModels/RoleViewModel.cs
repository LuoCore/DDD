using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.ViewModels
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/21 16:59:46
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public abstract class RoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public string RoleDescription { get; set; }

        public bool IsValid { get; set; }
    }
}
