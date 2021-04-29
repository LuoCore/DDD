using Domain.Models.CommandModels;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands.User
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/4/28 15:26:56
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class UserCreateCommand : UserCreateCommandModel
    {
        public override DateTime Timestamp => DateTime.Now;

        public override ValidationResult ValidationResult { get; set; }

        // 重写基类中的 是否有效 方法
        // 主要是为了引入命令验证 RegisterStudentCommandValidation。
        public override bool IsValid()
        {
            ValidationResult = new Validations.UserCreateValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        // set 受保护，只能通过构造函数方法赋值
        public UserCreateCommand(string name, string email, DateTime birthDate, string phone, string province, string city, string county, string street)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Phone = phone;
            Province = province;
            City = city;
            County = county;
            Street = street;
        }
    }
}
