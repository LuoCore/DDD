using Infrastructure.Entitys;
using Infrastructure.Interface.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interface.IRepository
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 17:10:20
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public interface IPermissionRepository : ISqlSugarRepository
    {
        public bool AnyByParentId(string ParentId);

        public List<Permission> QueryAll();
        public List<Permission> QueryByParentId(string ParentId);
        public Permission QueryByNameTypeParentId(string nameValue, int typeValue, string parentIdValue);

        public bool CreatePermission(Infrastructure.Entitys.Permission m);

        public bool DeletePermission(string permissionId);

        public bool UpdatePermission(Permission m);
    }
}
