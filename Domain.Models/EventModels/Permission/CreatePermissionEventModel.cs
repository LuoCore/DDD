using Infrastructure.CommandEventsHandler;
using System;


namespace Domain.Models.EventModels.Permission
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 17:18:28
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class CreatePermissionEventModel : PermissionEventModel
    {
        


        public CreatePermissionEventModel(Guid gid, string permissionname, CommandModels.PermissionCommandModel.PermissionTypeEnum permissiontype, string permissionaction, string permissionparentid, bool isvalid)
        {
            this.AggregateId = gid;
            this.PermissionId = gid;
            this.PermissionName = permissionname;
            this.PermissionType = permissiontype;
            this.PermissionAction = permissionaction;
            this.PermissionParentId = permissionparentid;
            this.IsValid = isvalid;
        }
    }
}
