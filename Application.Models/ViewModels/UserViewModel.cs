using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.ViewModels
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/4/28 17:45:57
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class UserViewModel
    {

        public Guid Id { get; set; }


        public string Name { get; set; }


        public string Email { get; set; }

        public DateTime BirthDate { get; set; }


        public string Phone { get; set; }



        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区县
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; set; }
    }
}
