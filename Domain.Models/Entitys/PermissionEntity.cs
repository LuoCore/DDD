﻿using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Common;

namespace Domain.Models.Entitys
{
    public class PermissionEntity : BaseEntity
    {
        public PermissionEntity(string permissionParentId)
        {
            ENTITY_PERMISSION = new Infrastructure.Entitys.Permission();
            ENTITY_PERMISSION.PermissionParentId = permissionParentId;
        }
        public PermissionEntity(string permissionName)
        {
           
            ENTITY_PERMISSION = new Infrastructure.Entitys.Permission();
            ENTITY_PERMISSION.PermissionName = permissionName;
            
        }
        public PermissionEntity(string permissionName, PermissionTypeEnum permissionType, string permissionAction, string permissionParentId, bool? isValid)
        {
            IsValid = isValid;
            ENTITY_PERMISSION = new Infrastructure.Entitys.Permission();
            ENTITY_PERMISSION.PermissionName = permissionName;
            ENTITY_PERMISSION.PermissionType = permissionType.EnumToInt<PermissionTypeEnum>();

            ENTITY_PERMISSION.PermissionAction = permissionAction;
            ENTITY_PERMISSION.PermissionParentId = permissionParentId;
        }

        public PermissionEntity(Guid id, string permissionName, PermissionTypeEnum permissionType, string permissionAction, string permissionParentId, bool? isValid)
        {
            this.Id = id;
            IsValid = isValid;
            ENTITY_PERMISSION = new Infrastructure.Entitys.Permission();
            ENTITY_PERMISSION.PermissionId = id.ToString();
            ENTITY_PERMISSION.PermissionName = permissionName;
            ENTITY_PERMISSION.PermissionType = (int)permissionType;
            ENTITY_PERMISSION.PermissionAction = permissionAction;
            ENTITY_PERMISSION.PermissionParentId = permissionParentId;
        }
        public Infrastructure.Entitys.Permission ENTITY_PERMISSION { get; protected set; }
        public bool? IsValid { get; set; }
    
        public enum PermissionTypeEnum
        {
            菜单,
            按钮
        }
    }
}
