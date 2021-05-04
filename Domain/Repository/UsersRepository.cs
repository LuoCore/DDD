
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

        public bool Create(User model)
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

        public User Login(string username,string pwd) 
        {
            User m = new User();
            Factory.GetDbContext((db) =>
            {
                m = db.Queryable<User>()
                .Where(x => x.UserName == username)
                .Where(x => x.Password == pwd)
                .First();
            });
            return m;
        }
    }
}
