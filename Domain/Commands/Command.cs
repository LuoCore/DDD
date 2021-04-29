using Domain.Interface.ICommands;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    /// <summary>
    /// 抽象命令基类
    /// </summary>
    public abstract class Command : ICommand
    {

        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        public string MessageType { get; private set; }

        public Guid AggregateId { get; private set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
        //定义抽象方法，是否有效
        public abstract bool IsValid();
    }
}
