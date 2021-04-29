using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Entitys
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/4/28 16:43:21
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
 

    public class UserEntity : EntityBase
    {
        protected UserEntity()
        {
        }
        public UserEntity(Guid id, string name, string email, string phone, DateTime birthDate, ViewObject.AddressViewObject address)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            BirthDate = birthDate;
            Address = address;
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

        /// <summary>
        /// 户籍
        /// </summary>
        public ViewObject.AddressViewObject Address { get; private set; }


    }
}
