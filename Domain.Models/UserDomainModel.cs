using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Entitys
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
        public UserDomainModel(Guid id, string name, string email, string phone, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            BirthDate = birthDate;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; private set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; private set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime BirthDate { get; private set; }

    }
}
