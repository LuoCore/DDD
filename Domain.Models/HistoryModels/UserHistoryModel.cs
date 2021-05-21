using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.HistoryModels
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/21 10:34:11
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class UserHistoryModel
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}
