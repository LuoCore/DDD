using System;

namespace Infrastructure.Entitys
{
    public class User
    {
        public int ID { get; set; }
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }
        

        public DateTime CreateTime { get; set; }
        public string CreateName { get; set; }
    }
}
