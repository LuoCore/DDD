using Infrastructure.CommandEventsHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.CommandModels.User
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 16:09:54
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class CreateCommandModel :UserCommandModel
    {
        public CreateCommandModel(Guid gid,string username,string password,string email,string phone,string createname)
        {
            UserId = gid;
            UserName = username;
            Password = password;
            Email = email;
            Phone  = phone;
            CreateName = createname;
        }

    }
}
