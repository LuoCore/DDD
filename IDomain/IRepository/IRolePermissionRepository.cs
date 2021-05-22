using Infrastructure.Interface.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interface.IRepository
{
    public interface IRolePermissionRepository : ISqlSugarRepository
    {
        public bool Create(Infrastructure.Entitys.Role_Permission m);
        public bool CreateBatch(List<Infrastructure.Entitys.Role_Permission> models);
        public bool AnyByRoleIdPermissionId(string RoleId, string PermissionId);
    }
}
