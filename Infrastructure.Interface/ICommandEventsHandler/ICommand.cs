using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interface.ICommandEventsHandler
{
    /// <summary>
    /// 命令基类
    /// </summary>
    public interface ICommand : IMessage
    {
        //时间戳
        public DateTime Timestamp { get; }
        //验证结果，需要引用FluentValidation
        public ValidationResult ValidationResult { get; set; }
        //定义抽象方法，是否有效
        public abstract bool IsValid();
      
    }
}
