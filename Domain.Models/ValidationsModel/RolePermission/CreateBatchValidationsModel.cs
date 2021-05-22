using FluentValidation;
using System.Collections.Generic;

namespace Domain.Models.ValidationsModel.RolePermission
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/22 16:23:14
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class CreateBatchValidationsModel : RolePermissionValidationsModel<Domain.Models.CommandModels.RolePermission.CreateBatchCommandModel>
    {
        public CreateBatchValidationsModel()
        {
            ValidatePermissionIds();
            //可以自定义增加新的验证
        }

        protected void ValidatePermissionIds()
        {
            RuleFor(c => c.PermissionIds).Must(HaveListStringCount).WithMessage("权限标识不能为空！");

        }
        // 表达式
        protected static bool HaveListStringCount(List<string> PermissionIds)
        {
            if (PermissionIds == null || PermissionIds.Count < 1) 
            {
                return false;
            }
            return true;
        }
    }
}
