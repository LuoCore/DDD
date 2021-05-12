
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
            bool sqlExe = false ;
            Factory.GetDbContext((db) =>
            {
                m.ENTITY_USER.CreateTime = db.GetDate();
                sqlExe = db.Insertable<User>(new
                {
                    m.ENTITY_USER.UserId,
                    m.ENTITY_USER.UserName,
                    m.ENTITY_USER.Password,
                    m.ENTITY_USER.Phone,
                    m.ENTITY_USER.Email,
                    m.ENTITY_USER.CreateTime,
                    m.ENTITY_USER.CreateName
                })
                .IgnoreColumns(ignoreNullColumn: true)
                .ExecuteCommand() > 0;
            });
            return sqlExe;
        }

        public List<User> ReadUserAll()
        {
            List<User> res = new List<User>();
            Factory.GetDbContext((db) =>
            {
                res = db.Queryable<User>().ToList();
            });
            return res;
        }

        public User ReadUserLogin(string userName,string userPassword)
        {
            User res = new User() ;
            Factory.GetDbContext((db) =>
            {
                res = db.Queryable<User>()
                .Where(x=>x.UserName==userName&&x.Password==userPassword)
                 .First();
                
            });
            return res;
        }
        public User ReadUserName(string userName)
        {
            User res = new User();
            Factory.GetDbContext((db) =>
            {
                res = db.Queryable<User>()
                .Where(x => x.UserName == userName)
                 .First();

            });
            return res;
        }



        public bool ReadPermissionParentIdAny(string ParentId)
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
        public List<Permission> ReadPermissionParentIdList(string ParentId)
        {
            List<Permission> res = new List<Permission>();
            Factory.GetDbContext((db) =>
            {
                res = db.Queryable<Permission>()
                .Where(x=>x.PermissionParentId==ParentId)
                .ToList();
            });
            return res;
        }

        public Permission ReadPermissionNameType(string nameValue,int type,string pid)
        {
            Permission res = new Permission();
            Factory.GetDbContext((db) =>
            {
                res = db.Queryable<Permission>()
                .Where(x => x.PermissionName == nameValue
                &&x.PermissionType== type
                &&x.PermissionParentId==pid)
                .First();
            });
            return res;
        }

        public List<Permission> ReadPermissionAll()
        {
            List<Permission> res = new List<Permission>();
            Factory.GetDbContext((db) =>
            {
                res = db.Queryable<Permission>().ToList();
            });
            return res;
        }

        public bool CreatePermission(Models.Entitys.PermissionEntity m)
        {
            bool sqlExe = false;
            Factory.GetDbContext((db) =>
            {
              
                sqlExe = db.Insertable<Permission>(new
                {
                    m.ENTITY_PERMISSION.PermissionId,
                    m.ENTITY_PERMISSION.PermissionName,
                    m.ENTITY_PERMISSION.PermissionType,
                    m.ENTITY_PERMISSION.PermissionAction,
                    m.ENTITY_PERMISSION.PermissionParentId,
                    m.ENTITY_PERMISSION.IsValid
                })
                .IgnoreColumns(ignoreNullColumn: true)
                .ExecuteCommand() > 0;
            });
            return sqlExe;
        }

    }
}
