using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.ViewModels
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/3 16:58:48
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class UserLoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VerifiCode { get; set; }
    }
}
