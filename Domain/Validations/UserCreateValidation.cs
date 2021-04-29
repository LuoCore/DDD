using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validations
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/4/28 16:11:51
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class UserCreateValidation:UserValidation<Commands.User.UserCreateCommand>
    {
        public UserCreateValidation()
        {
            ValidateName();
            //可以自定义增加新的验证
        }
    }
}
