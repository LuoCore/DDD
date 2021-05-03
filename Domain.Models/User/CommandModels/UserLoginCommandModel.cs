using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.User.CommandModels
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/3 17:11:44
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class UserLoginCommandModel : UserCommandModel, MediatR.IRequest<Infrastructure.Entitys.User>
    {
        public UserLoginCommandModel(string username,string password)
        {
            UserName = username;
            Password = password;
        }
       
        public override bool IsValid()
        {
            return true;
        }
    }
}
