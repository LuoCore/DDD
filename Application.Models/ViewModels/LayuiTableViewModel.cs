using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.ViewModels
{
    public class LayuiTableViewModel<T>
    {
        /// <summary>
        /// 接口状态
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 提示文本
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<T> data { get; set; }
    }
}
