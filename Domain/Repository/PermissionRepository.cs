using Domain.Interface.IRepository;
using Infrastructure.Entitys;
using Infrastructure.Interface.IFactory;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;

namespace Domain.Repository
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 17:01:44
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class PermissionRepository : SqlSugarRepository<ISqlSugarFactory>, IPermissionRepository
    {
        public PermissionRepository(ISqlSugarFactory factory) : base(factory)
        {
        }

        public bool AnyByParentId(string ParentId)
        {
            bool parentIdAny = false;
            Factory.GetDbContext((db) =>
            {
                parentIdAny = db.Queryable<Permission>()
                .Where(x => x.PermissionParentId == ParentId)
                .Any();
            });
            return parentIdAny;
        }

        public List<Permission> QueryAll()
        {
            List<Permission> res = new List<Permission>();
            Factory.GetDbContext((db) =>
            {
                res = db.Queryable<Permission>().ToList();
            });
            return res;
        }
        public List<Permission> QueryByParentId(string ParentId)
        {
            List<Permission> res = new List<Permission>();
            Factory.GetDbContext((db) =>
            {
                res = db.Queryable<Permission>()
                .Where(x => x.PermissionParentId == ParentId)
                .ToList();
            });
            return res;
        }
        public Permission QueryByNameTypeParentId(string nameValue, int typeValue, string parentIdValue)
        {
            Permission res = new Permission();
            Factory.GetDbContext((db) =>
            {
                res = db.Queryable<Permission>()
                .Where(x => x.PermissionName == nameValue
                && x.PermissionType == typeValue
                && x.PermissionParentId == parentIdValue)
                .First();
            });
            return res;
        }

        public bool CreatePermission(Infrastructure.Entitys.Permission m)
        {
            bool sqlExe = false;
            Factory.GetDbContext((db) =>
            {
                sqlExe = db.Insertable<Permission>(new
                {
                    m.PermissionId,
                    m.PermissionName,
                    m.PermissionType,
                    m.PermissionAction,
                    m.PermissionParentId,
                    m.IsValid
                })
                .IgnoreColumns(ignoreNullColumn: true)
                .ExecuteCommand() > 0;
            });
            return sqlExe;
        }

        public bool DeletePermission(string permissionId)
        {
            bool sqlExe = false;
            Factory.GetDbContext((db) =>
            {

                try
                {
                    db.BeginTran();
                    db.Deleteable<Permission>().Where(x => x.PermissionId == permissionId).ExecuteCommand();
                    db.Deleteable<Permission>().Where(x => x.PermissionParentId == permissionId).ExecuteCommand();
                    db.CommitTran();
                    sqlExe = true;
                }
                catch (Exception)
                {
                    db.RollbackTran();
                    sqlExe = false;
                }


            });
            return sqlExe;
        }

        public bool UpdatePermission(Permission m)
        {
            bool sqlExe = false;
            Factory.GetDbContext((db) =>
            {
                sqlExe = db.Updateable<Permission>(new
                {
                    m.PermissionName,
                    m.PermissionType,
                    m.PermissionAction,
                    m.PermissionParentId,
                    m.IsValid
                })
                .Where(x => x.PermissionId == m.PermissionId)
                .IgnoreColumns(ignoreAllNullColumns: true)
                .ExecuteCommand() > 0;
            });
            return sqlExe;
        }
    }
}
