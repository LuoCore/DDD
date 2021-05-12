using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Common;

namespace Domain.Models.Entitys
{
    public class PermissionEntity : BaseEntity
    {
     
        public PermissionEntity(Guid guid, string permissionName, PermissionTypeEnum permissionType, string permissionAction, string permissionParentId, bool isValid)
        {
            ENTITY_PERMISSION = new Infrastructure.Entitys.Permission() 
            {
                PermissionId= guid.ToString(),
                PermissionName=permissionName,
                PermissionType=permissionType.EnumToInt(),
                PermissionAction=permissionAction,
                PermissionParentId=permissionParentId,
                IsValid=isValid
            };

        }
        public Infrastructure.Entitys.Permission ENTITY_PERMISSION { get; protected set; }
       
    
        public enum PermissionTypeEnum
        {
            菜单=1,
            按钮=2
        }
    }
}
