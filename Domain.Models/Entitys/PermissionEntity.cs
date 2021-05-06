using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Entitys
{
    public class PermissionEntity : BaseEntity
    {
        public PermissionEntity()
        {
            Permission = new Infrastructure.Entitys.Permission();
        }
        public PermissionEntity(Guid id, string permissionName, PermissionTypeEnum permissionType, string permissionAction, string permissionParentId,bool? isValid)
        {
            this.Id = id;
            IsValid = isValid;
            Permission = new Infrastructure.Entitys.Permission
            {
                PermissionId = id.ToString(),
                PermissionName = permissionName,
                PermissionType=(int)permissionType,
                PermissionAction= permissionAction,
                PermissionParentId= permissionParentId
            };
        }
        public Infrastructure.Entitys.Permission Permission { get; set; }
        public bool? IsValid { get; set; }
        public enum PermissionTypeEnum 
        {
            菜单=1,
            按钮=2
        }
    }
}
