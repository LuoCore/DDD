using Domain.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.User
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/4/30 17:20:47
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class UserDomainModel : DomainModelBase
    {
        protected UserDomainModel()
        {
        }
        public UserDomainModel(Guid id, string name,string password, string email, string phone)
        {
            Id = id;
            UserName = name;
            Password = password;
            Email = email;
            Phone = phone;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; private set; }
        public string Password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; private set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; private set; }

    }
}
