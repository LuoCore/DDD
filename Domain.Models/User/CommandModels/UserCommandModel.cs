using Infrastructure.CommandEventsHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.User.CommandModels
{

    /// <summary>
    ///   定义一个抽象的  命令模型
    /// 继承 Command
    /// 这个模型主要作用就是用来创建命令动作的，所以是一个抽象类
    /// </summary>
    public abstract class UserCommandModel : Command
    {
        public Guid UserId { get; protected set; }

        public string UserName { get; protected set; }

        public string Email { get; protected set; }

        public string Password { get; protected set; }

        public string Phone { get; protected set; }

        public string CreateName { get; protected set; }


    }
}
