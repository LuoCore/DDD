
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
                m.USER.CreateTime = db.GetDate();
                sqlExe = db.Insertable<User>(new
                {
                    m.USER.UserId,
                    m.USER.UserName,
                    m.USER.Password,
                    m.USER.Phone,
                    m.USER.Email,
                    m.USER.CreateTime,
                    m.USER.CreateName
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
                List<User> users = db.Queryable<User>()
                 .WhereIF(!string.IsNullOrWhiteSpace(m.USER.UserName),
                         x => x.UserName.Equals(m.USER.UserName))
                 .WhereIF(!string.IsNullOrWhiteSpace(m.USER.Password),
                         x => x.Password.Equals(m.USER.Password))
                 .WhereIF(!string.IsNullOrWhiteSpace(m.USER.Email),
                         x => x.Email.Equals(m.USER.Email))
                 .WhereIF(!string.IsNullOrWhiteSpace(m.USER.Phone),
                         x => x.Phone.Equals(m.USER.Phone))
                 .ToList();
                users.ForEach(x =>
                {
                    resList.Add(new Models.Entitys.UserEntity()
                    {
                        USER = x
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
                res.USER = db.Queryable<User>()
                .WhereIF(!string.IsNullOrWhiteSpace(m.USER.UserName),
                        x => x.UserName.Equals(m.USER.UserName))
                .WhereIF(!string.IsNullOrWhiteSpace(m.USER.Password),
                        x => x.Password.Equals(m.USER.Password))
                .WhereIF(!string.IsNullOrWhiteSpace(m.USER.Email),
                        x => x.Email.Equals(m.USER.Email))
                .WhereIF(!string.IsNullOrWhiteSpace(m.USER.Phone),
                        x => x.Phone.Equals(m.USER.Phone))
                .First();
            });
            return res;
        }


        public Models.Entitys.PermissionEntity ReadPermission(Models.Entitys.PermissionEntity m)
        {
            Models.Entitys.PermissionEntity res = new Models.Entitys.PermissionEntity();
            Factory.GetDbContext((db) =>
            {
                res.PERMISSION = db.Queryable<Permission>()
                .WhereIF(!string.IsNullOrWhiteSpace(m.PERMISSION.PermissionId),
                    x => x.PermissionId.Equals(m.PERMISSION.PermissionId))
                .WhereIF(!string.IsNullOrWhiteSpace(m.PERMISSION.PermissionName),
                    x => x.PermissionName.Contains(m.PERMISSION.PermissionName))
                .WhereIF(!string.IsNullOrWhiteSpace(m.PERMISSION.PermissionParentId),
                    x => x.PermissionParentId.Equals(m.PERMISSION.PermissionParentId))
                .WhereIF(m.PERMISSION.PermissionType > 0,
                    x => x.PermissionType.Equals(m.PERMISSION.PermissionType))
                .WhereIF(m.IsValid == null, x => x.PermissionType.Equals(m.PERMISSION.PermissionType))
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
               .WhereIF(!string.IsNullOrWhiteSpace(m.PERMISSION.PermissionId),
                    x => x.PermissionId.Equals(m.PERMISSION.PermissionId))
                .WhereIF(!string.IsNullOrWhiteSpace(m.PERMISSION.PermissionName),
                    x => x.PermissionName.Contains(m.PERMISSION.PermissionName))
                .WhereIF(!string.IsNullOrWhiteSpace(m.PERMISSION.PermissionParentId),
                    x => x.PermissionParentId.Equals(m.PERMISSION.PermissionParentId))
                .WhereIF(m.PERMISSION.PermissionType > 0,
                    x => x.PermissionType.Equals(m.PERMISSION.PermissionType))
                .WhereIF(m.IsValid == null,
                    x => x.PermissionType.Equals(m.PERMISSION.PermissionType))
                .ToList();
                permissionList.ForEach(x =>
                {
                    res.Add(new Models.Entitys.PermissionEntity()
                    {
                        PERMISSION = x
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
                    m.PERMISSION.PermissionId,
                    m.PERMISSION.PermissionName,
                    m.PERMISSION.PermissionType,
                    m.PERMISSION.PermissionAction,
                    m.PERMISSION.PermissionParentId,
                    m.PERMISSION.IsValid
                })
                .IgnoreColumns(ignoreNullColumn: true)
                .ExecuteCommand() > 0;
            });
            return sqlExe;
        }

    }
}
