using Domain.Interface.IRepository;
using Infrastructure.Interface.IFactory;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Domain.Repository
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/22 14:51:55
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class RolePermissionRepository : SqlSugarRepository<ISqlSugarFactory>, IRolePermissionRepository
    {
        public RolePermissionRepository(ISqlSugarFactory factory) : base(factory)
        {
        }

        public bool Create(Infrastructure.Entitys.Role_Permission m)
        {
            return Factory.GetDbContext((db) =>
            {
                return db.Insertable<Infrastructure.Entitys.Role_Permission>(new
                {
                    m.RolePermissionId,
                    m.RoleId,
                    m.PermissionId
                }).IgnoreColumns(true).ExecuteCommand() > 0;
            });
        }

        public bool CreateBatch(List<Infrastructure.Entitys.Role_Permission> models)
        {
            return Factory.GetDbContext((db) =>
            {
                try
                {
                    db.BeginTran();
                    foreach (var m in models)
                    {
                        db.Insertable<Infrastructure.Entitys.Role_Permission>(new
                        {
                            m.RolePermissionId,
                            m.RoleId,
                            m.PermissionId
                        }).IgnoreColumns(true)
                        .ExecuteCommand();
                    }

                    db.CommitTran();
                    return true;
                }
                catch (Exception ex)
                {
                    db.RollbackTran();
                    Debug.WriteLine(ex);
                }

                return false;
            });
        }


        public bool AnyByRoleIdPermissionId(string RoleId, string PermissionId)
        {
            return Factory.GetDbContext((db) =>
            {
                return db.Queryable<Infrastructure.Entitys.Role_Permission>()
                .Where(x => x.RoleId == RoleId && x.PermissionId == PermissionId)
                .Any();
            });
        }

    }
}
