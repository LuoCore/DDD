using Infrastructure.CommandEventsHandler;
using System;

namespace Domain.Models.EventModels.User
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 17:18:28
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class CreateUserEventModel: UserEventModel
    {
        

        public CreateUserEventModel(Guid gid, string username, string password, string email, string phone, string createname,DateTime createtime)
        {
            this.UserId = gid;
            this.UserName = username;
            this.Password = password;
            this.Email = email;
            this.Phone = phone;
            this.CreateName = createname;
            this.CreateTime = createtime;
        }
    }
}
