using Domain.Models.CommandModels.Permission;
using System;

namespace Domain.Models.CommandModels.Permission
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 16:09:54
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class CreateCommandModel: PermissionCommandModel
    {

        public CreateCommandModel(Guid gid, string permissionname,PermissionTypeEnum permissiontype,string permissionaction,string permissionparentid,bool isvalid)
        {
            this.PermissionId = gid;
            this.PermissionName = permissionname;
            this.PermissionType = permissiontype;
            this.PermissionAction = permissionaction;
            this.PermissionParentId = permissionparentid;
            this.IsValid = isvalid;
        }
    }
}
