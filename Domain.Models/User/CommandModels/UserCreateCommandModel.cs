using Infrastructure.CommandEventsHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.User.CommandModels
{
    public class UserCreateCommandModel : Command
    {
        // set 受保护，只能通过构造函数方法赋值
        public UserCreateCommandModel(Guid userId, string userName, string email, string password, string phone, string createname)
        {
            this.AggregateId = userId;
            User = new Infrastructure.Entitys.User();
            User.UserId = userId.ToString();
            User.UserName = userName;
            User.Password = password;
            User.Email = email;
            User.Phone = phone;
            User.CreateName = createname;
        }

        public Infrastructure.Entitys.User User { get;protected  set; }
        // 重写基类中的 是否有效 方法
        // 主要是为了引入命令验证 RegisterStudentCommandValidation。
        public override bool IsValid()
        {
            //ValidationResult = new UserRegisterCommandValidation().Validate(this);
            return true;
        }
    }
}
