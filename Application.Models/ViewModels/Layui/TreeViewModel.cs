using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.ViewModels.Layui
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/22 13:02:45
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class TreeViewModel
    {
        public string id { get; set; }
        public string title { get; set; }
        
        public bool disabled { get; set; }
        public List<TreeViewModel> children { get; set; }
        
    }
}
