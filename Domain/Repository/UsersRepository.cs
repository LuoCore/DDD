
using Domain.Interface.IRepository;
using Infrastructure.Entitys;
using Infrastructure.Interface.IFactory;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;

namespace Domain.Repository
{
    public class UsersRepository : SqlSugarRepository<ISqlSugarFactory>, IUsersRepository
    {
        public UsersRepository(ISqlSugarFactory factory) : base(factory)
        {

        }

        public bool Create(User model)
        {
            bool sqlExe = true;
            Factory.GetDbContext((db) =>
            {
                sqlExe = db.Insertable<User>(model)
                .IgnoreColumns(ignoreNullColumn: true)
                .ExecuteCommand()>0;
            });
            return sqlExe;
        }

        public List<User> Read()
        {
            List<User> models = new List<User>();
            Factory.GetDbContext((db) =>
            {
                models = db.Queryable<User>().ToList();
            });
            return models;
        }

        public User ReadId(string userId)
        {
            User m=new User();
            Factory.GetDbContext((db) =>
            {
                m = db.Queryable<User>().Where(x=>x.UserId==userId).First();
            });
            return m;
        }
        public User ReadName(string username)
        {
            User m = new User();
            Factory.GetDbContext((db) =>
            {
                m = db.Queryable<User>().Where(x=>x.UserName==username).First();
            });
            return m;
        }
    }
}
