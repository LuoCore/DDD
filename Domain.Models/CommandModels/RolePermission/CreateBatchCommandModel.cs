using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.CommandModels.RolePermission
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/22 15:50:06
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class CreateBatchCommandModel : RolePermissionCommandModel
    {
        public CreateBatchCommandModel(Guid gid, string roleId, List<string> permissionIds)
        {
            this.RolePermissionId = gid;
            this.RoleId = roleId;
            this.PermissionIds = permissionIds;
        }
        public override bool VerifyData()
        {
            ValidationResult = new ValidationsModel.RolePermission.CreateBatchValidationsModel().Validate(this);
            return ValidationResult.IsValid;
        }

     

        public List<string> PermissionIds { get; set; }
    }
}
