
using Domain.Interface.IRepository;
using Infrastructure.Entitys;
using Infrastructure.Interface.IFactory;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class UsersRepository : SqlSugarRepository<ISqlSugarFactory>, IUsersRepository
    {
        public UsersRepository(ISqlSugarFactory factory) : base(factory)
        {

        }

        public bool CreateUser(User model)
        {
            bool sqlExe = true;
            Factory.GetDbContext((db) =>
            {
                sqlExe = db.Insertable<User>(new 
                {
                    UserId=model.UserId,
                    UserName=model.UserName,
                    Password=model.Password,
                    Email=model.Email,
                    Phone=model.Phone,
                    CreateTime=DateTime.Now,
                    CreateName=model.CreateName
                })
                .IgnoreColumns(ignoreNullColumn: true)
                .ExecuteCommand()>0;
            });
            return sqlExe;
        }

        public List<User> ReadUserAll()
        {
            List<User> models = new List<User>();
            Factory.GetDbContext((db) =>
            {
                models = db.Queryable<User>().ToList();
            });
            return models;
        }

        public User ReadUser(User model) 
        {
            User m = new User();
            Factory.GetDbContext((db) =>
            {
                m = db.Queryable<User>()
                .WhereIF(!string.IsNullOrWhiteSpace(model.UserName),x => x.UserName == model.UserName)
                .WhereIF(!string.IsNullOrWhiteSpace(model.Password), x => x.Password == model.Password)
                .WhereIF(!string.IsNullOrWhiteSpace(model.Email), x => x.Email == model.Email)
                .WhereIF(!string.IsNullOrWhiteSpace(model.Phone), x => x.Phone == model.Phone)
                .First();
            });
            return m;
        }


        public Infrastructure.Entitys.Permission ReadPermission(Infrastructure.Entitys.Permission m) 
        {
            Permission res = new Permission();
            Factory.GetDbContext((db) =>
            {
                res = db.Queryable<Permission>()
                .WhereIF(!string.IsNullOrWhiteSpace(m.PermissionName), x => x.PermissionName == m.PermissionName)
                .First();
            });
            return m;
        }

        public bool CreatePermission(Permission m)
        {
            bool sqlExe = true;
            Factory.GetDbContext((db) =>
            {
                sqlExe = db.Insertable<Permission>(new 
                {
                   m.PermissionId,
                    m.PermissionName,
                })
                .IgnoreColumns(ignoreNullColumn: true)
                .ExecuteCommand() > 0;
            });
            return sqlExe;
        }

    }
}
