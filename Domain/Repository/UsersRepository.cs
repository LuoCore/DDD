
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

        public bool CreateUser(Infrastructure.Entitys.User m)
        {
            bool sqlExe = false ;
            Factory.GetDbContext((db) =>
            {
                sqlExe = db.Insertable<User>(new
                {
                    m.UserId,
                    m.UserName,
                    m.Password,
                    m.Phone,
                    m.Email,
                    m.CreateTime,
                    m.CreateName
                })
                .IgnoreColumns(ignoreNullColumn: true)
                .ExecuteCommand() > 0;
            });
            return sqlExe;
        }
        public List<User> QueryAllUser()
        {
            List<User> res = new List<User>();
            Factory.GetDbContext((db) =>
            {
                res = db.Queryable<User>().ToList();
            });
            return res;
        }
        public User UserLogin(string userName,string userPassword)
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
        public User QueryUserName(string userName)
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



       
    }
}
