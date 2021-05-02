using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.User.CommandModels
{
    public class UserCreateCommandModel:UserCommandModel
    {
        // set 受保护，只能通过构造函数方法赋值
        public UserCreateCommandModel(Guid userId,string userName, string email, string password, string phone)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            Email = email;
            Phone = phone;
           
        }

        // 重写基类中的 是否有效 方法
        // 主要是为了引入命令验证 RegisterStudentCommandValidation。
        public override bool IsValid()
        {
            //ValidationResult = new UserRegisterCommandValidation().Validate(this);
            return true;
        }
    }
}
