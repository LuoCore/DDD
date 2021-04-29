
using Domain.Interface.ICommands;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.CommandModels
{
    /// <summary>
    /// 定义一个抽象的 Student 命令模型
    /// 继承 Command
    /// 这个模型主要作用就是用来创建命令动作的，所以是一个抽象类
    /// </summary>
    public abstract class UserCreateCommandModel :ICommand
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Email { get; protected set; }

        public DateTime BirthDate { get; protected set; }

        public string Phone { get; protected set; }
        public string Province { get; protected set; }
        public string City { get; protected set; }
        public string County { get; protected set; }
        public string Street { get; protected set; }
        public abstract DateTime Timestamp { get; }
        public abstract ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();
    }
}
