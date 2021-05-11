
using Domain.Interface.IRepository;
using Infrastructure.Entitys;
using Infrastructure.Interface.IFactory;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using Infrastructure.Common;
using static Domain.Models.Entitys.PermissionEntity;

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

        public List<Models.Entitys.UserEntity> ReadUserAll(Models.Entitys.UserEntity m)
        {
            List<Models.Entitys.UserEntity> resList = new List<Models.Entitys.UserEntity>();
            Factory.GetDbContext((db) =>
            {
                List<User> users = db.Queryable<User>()
                 .WhereIF(!string.IsNullOrWhiteSpace(m.ENTITY_USER.UserName),
                         x => x.UserName.Equals(m.ENTITY_USER.UserName))
                 .WhereIF(!string.IsNullOrWhiteSpace(m.ENTITY_USER.Password),
                         x => x.Password.Equals(m.ENTITY_USER.Password))
                 .WhereIF(!string.IsNullOrWhiteSpace(m.ENTITY_USER.Email),
                         x => x.Email.Equals(m.ENTITY_USER.Email))
                 .WhereIF(!string.IsNullOrWhiteSpace(m.ENTITY_USER.Phone),
                         x => x.Phone.Equals(m.ENTITY_USER.Phone))
                 .ToList();
                users.ForEach(x =>
                {
                    resList.Add(new Models.Entitys.UserEntity(x.UserId.StringToGuid(), x.UserName, x.Password, x.Email, x.Phone, x.CreateName));
                });
            });
            return resList;
        }

        public Models.Entitys.UserEntity ReadUser(Models.Entitys.UserEntity m)
        {
            Models.Entitys.UserEntity res =null;
            Factory.GetDbContext((db) =>
            {
               var data = db.Queryable<User>()
                .WhereIF(!string.IsNullOrWhiteSpace(m.ENTITY_USER.UserName),
                        x => x.UserName.Equals(m.ENTITY_USER.UserName))
                .WhereIF(!string.IsNullOrWhiteSpace(m.ENTITY_USER.Password),
                        x => x.Password.Equals(m.ENTITY_USER.Password))
                .WhereIF(!string.IsNullOrWhiteSpace(m.ENTITY_USER.Email),
                        x => x.Email.Equals(m.ENTITY_USER.Email))
                .WhereIF(!string.IsNullOrWhiteSpace(m.ENTITY_USER.Phone),
                        x => x.Phone.Equals(m.ENTITY_USER.Phone))
                .First();
                res = new Models.Entitys.UserEntity(data.UserId.StringToGuid(), data.UserName, data.Password, data.Email, data.Phone, data.CreateName);
            });
            return res;
        }


        public Models.Entitys.PermissionEntity ReadPermission(Models.Entitys.PermissionEntity m)
        {
            Models.Entitys.PermissionEntity res = null;
            Factory.GetDbContext((db) =>
            {
                var data = db.Queryable<Permission>()
                .WhereIF(!string.IsNullOrWhiteSpace(m.ENTITY_PERMISSION.PermissionId),
                    x => x.PermissionId.Equals(m.ENTITY_PERMISSION.PermissionId))
                .WhereIF(!string.IsNullOrWhiteSpace(m.ENTITY_PERMISSION.PermissionName),
                    x => x.PermissionName.Contains(m.ENTITY_PERMISSION.PermissionName))
                .WhereIF(!string.IsNullOrWhiteSpace(m.ENTITY_PERMISSION.PermissionParentId),
                    x => x.PermissionParentId.Equals(m.ENTITY_PERMISSION.PermissionParentId))
                .WhereIF(m.ENTITY_PERMISSION.PermissionType > 0,
                    x => x.PermissionType.Equals(m.ENTITY_PERMISSION.PermissionType))
                .WhereIF(m.IsValid == null,
                    x => x.PermissionType.Equals(m.ENTITY_PERMISSION.PermissionType))
                .First();
                res = new Models.Entitys.PermissionEntity(data.PermissionId.StringToGuid(),data.PermissionName,data.PermissionType.IntToEnum<PermissionTypeEnum>(),data.PermissionAction,data.PermissionParentId,data.IsValid);
            });
            return res;
        }

        public List<Models.Entitys.PermissionEntity> ReadPermissionAll(Models.Entitys.PermissionEntity m)
        {
            List<Models.Entitys.PermissionEntity> res = new List<Models.Entitys.PermissionEntity>();
            Factory.GetDbContext((db) =>
            {
                var dataBase = db.Queryable<Permission>()
                 .WhereIF(!string.IsNullOrWhiteSpace(m.ENTITY_PERMISSION.PermissionId),
                      x => x.PermissionId.Equals(m.ENTITY_PERMISSION.PermissionId))
                  .WhereIF(!string.IsNullOrWhiteSpace(m.ENTITY_PERMISSION.PermissionName),
                      x => x.PermissionName.Contains(m.ENTITY_PERMISSION.PermissionName));

                if (m.ENTITY_PERMISSION.PermissionParentId == null)
                {
                    dataBase.Where(x => x.PermissionParentId.Equals("")||x.PermissionParentId.Equals("0"));
                }
                else 
                {
                    dataBase.WhereIF(!string.IsNullOrWhiteSpace(m.ENTITY_PERMISSION.PermissionParentId),
                    x => x.PermissionParentId.Equals(m.ENTITY_PERMISSION.PermissionParentId));
                }
                

                dataBase.WhereIF(m.ENTITY_PERMISSION.PermissionType > 0,
                    x => x.PermissionType.Equals(m.ENTITY_PERMISSION.PermissionType))
                .WhereIF(m.IsValid != null,
                    x => x.PermissionType.Equals(m.ENTITY_PERMISSION.IsValid));

                var datas = dataBase.ToList();
                datas.ForEach(x =>
                {
                    res.Add(new Models.Entitys.PermissionEntity(x.PermissionId.StringToGuid(),x.PermissionName,x.PermissionType.IntToEnum<PermissionTypeEnum>(),x.PermissionAction,x.PermissionParentId,x.IsValid));
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
