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
    public class UserCreateEventModel: Event
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public Guid UserId { get; protected set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; protected set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; protected set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; protected set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; protected set; }
        public DateTime CreateTime { get; protected set; }
        public string CreateName { get; protected set; }

        public UserCreateEventModel(Guid gid, string username, string password, string email, string phone, string createname,DateTime createtime)
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
