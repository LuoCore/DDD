using Domain.CommandEventsHandler.Commands.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validations.User
{
    /// <summary>
    /// 添加学生命令模型验证
    /// 继承 StudentValidation 基类
    /// </summary>
    public class UserRegisterCommandValidation : UserValidation<UserRegisterCommand>
    {
        public UserRegisterCommandValidation()
        {
            ValidateName();//验证姓名
            ValidateBirthDate();//验证年龄
            ValidateEmail();//验证邮箱
            ValidatePhone();//验证手机号
            //可以自定义增加新的验证
        }
    }
   
}
