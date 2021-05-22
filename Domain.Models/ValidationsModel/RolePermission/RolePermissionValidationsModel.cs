using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.ValidationsModel.RolePermission
{
    /// <summary>
    /// 定义基于 StudentCommand 的抽象基类 StudentValidation
    /// 继承 抽象类 AbstractValidator
    /// 注意需要引用 FluentValidation
    /// 注意这里的 T 是命令模型
    /// </summary>
    /// <typeparam name="T">泛型类</typeparam>
    public abstract class RolePermissionValidationsModel<T> : AbstractValidator<T> where T : Domain.Models.CommandModels.RolePermission.RolePermissionCommandModel
    {

    }
}
