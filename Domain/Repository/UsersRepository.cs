
using Domain.Interface.IRepository;
using Infrastructure.Entitys;
using Infrastructure.Interface.IFactory;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using Infrastructure.Common;

namespace Domain.Repository
{
    public class UsersRepository : SqlSugarRepository<ISqlSugarFactory>, IUsersRepository
    {
        public UsersRepository(ISqlSugarFactory factory) : base(factory)
        {

        }

        public bool CreateUser(Models.Entitys.UserEntity m)
        {
            bool sqlExe = true;
            Factory.GetDbContext((db) =>
            {
                m.User.CreateTime = db.GetDate();
                sqlExe = db.Insertable<User>(new
                {
                    m.User.UserId,
                    m.User.UserName,
                    m.User.Password,
                    m.User.Phone,
                    m.User.Email,
                    m.User.CreateTime,
                    m.User.CreateName
                })
                .IgnoreColumns(ignoreNullColumn: true)
                .ExecuteCommand() > 0;
            });
            return sqlExe;
        }

        public List<Models.Entitys.UserEntity> ReadUserAll(Models.Entitys.UserEntity m)
        {
            List<Models.Entitys.UserEntity> resList = new List<Models.Entitys.UserEntity>();
            Factory.GetDbContext((db) =>
            {
               List<User> users= db.Queryable<User>()
                .WhereIF(!string.IsNullOrWhiteSpace(m.User.UserName),
                        x => x.UserName.Equals(m.User.UserName))
                .WhereIF(!string.IsNullOrWhiteSpace(m.User.Password),
                        x => x.Password.Equals(m.User.Password))
                .WhereIF(!string.IsNullOrWhiteSpace(m.User.Email),
                        x => x.Email.Equals(m.User.Email))
                .WhereIF(!string.IsNullOrWhiteSpace(m.User.Phone),
                        x => x.Phone.Equals(m.User.Phone))
                .ToList();
                users.ForEach(x =>
                {
                    resList.Add(new Models.Entitys.UserEntity()
                    {
                        User = x
                    });
                });
            });
            return resList;
        }

        public Models.Entitys.UserEntity ReadUser(Models.Entitys.UserEntity m)
        {
            Models.Entitys.UserEntity res = new Models.Entitys.UserEntity();
            Factory.GetDbContext((db) =>
            {
                res.User = db.Queryable<User>()
                .WhereIF(!string.IsNullOrWhiteSpace(m.User.UserName),
                        x => x.UserName.Equals(m.User.UserName))
                .WhereIF(!string.IsNullOrWhiteSpace(m.User.Password),
                        x => x.Password.Equals(m.User.Password))
                .WhereIF(!string.IsNullOrWhiteSpace(m.User.Email),
                        x => x.Email.Equals(m.User.Email))
                .WhereIF(!string.IsNullOrWhiteSpace(m.User.Phone),
                        x => x.Phone.Equals(m.User.Phone))
                .First();
            });
            return res;
        }


        public Permission ReadPermission(Models.Entitys.PermissionEntity m)
        {
            Permission res = new Permission();
            Factory.GetDbContext((db) =>
            {
                res = db.Queryable<Permission>()
                .WhereIF(!string.IsNullOrWhiteSpace(m.Permission.PermissionId), x => x.PermissionId.Equals(m.Permission.PermissionId))
                .WhereIF(!string.IsNullOrWhiteSpace(m.Permission.PermissionName), x => x.PermissionName.Contains(m.Permission.PermissionName))
                .WhereIF(!string.IsNullOrWhiteSpace(m.Permission.PermissionParentId), x => x.PermissionParentId.Equals(m.Permission.PermissionParentId))
                .WhereIF(m.Permission.PermissionType > 0, x => x.PermissionType.Equals(m.Permission.PermissionType))
                .WhereIF(m.IsValid == null, x => x.PermissionType.Equals(m.Permission.PermissionType))
                .First();
            });
            return res;
        }

        public List<Models.Entitys.PermissionEntity> ReadPermissionAll(Models.Entitys.PermissionEntity m)
        {
            List<Models.Entitys.PermissionEntity> res = new List<Models.Entitys.PermissionEntity>();
            Factory.GetDbContext((db) =>
            {
                var permissionList = db.Queryable<Permission>()
                .WhereIF(!string.IsNullOrWhiteSpace(m.Permission.PermissionId), x => x.PermissionId.Equals(m.Permission.PermissionId))
                .WhereIF(!string.IsNullOrWhiteSpace(m.Permission.PermissionName), x => x.PermissionName.Contains(m.Permission.PermissionName))
                .WhereIF(!string.IsNullOrWhiteSpace(m.Permission.PermissionParentId), x => x.PermissionParentId.Equals(m.Permission.PermissionParentId))
                .WhereIF(m.Permission.PermissionType > 0, x => x.PermissionType.Equals(m.Permission.PermissionType))
                .WhereIF(m.IsValid == null, x => x.PermissionType.Equals(m.Permission.PermissionType))
                .ToList();
                permissionList.ForEach(x =>
                {
                    res.Add(new Models.Entitys.PermissionEntity()
                    {
                        Permission = x
                    });
                });
            });
            return res;
        }

        public bool CreatePermission(Models.Entitys.PermissionEntity m)
        {
            bool sqlExe = true;
            Factory.GetDbContext((db) =>
            {
                sqlExe = db.Insertable<Permission>(new
                {
                    m.Permission.PermissionId,
                    m.Permission.PermissionName,
                    m.Permission.PermissionType,
                    m.Permission.PermissionAction,
                    m.Permission.PermissionParentId,
                    m.Permission.IsValid
                })
                .IgnoreColumns(ignoreNullColumn: true)
                .ExecuteCommand() > 0;
            });
            return sqlExe;
        }

    }
}
