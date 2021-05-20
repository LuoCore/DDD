using Infrastructure.Entitys;
using Infrastructure.Interface.IRepository;
using System.Collections.Generic;

namespace Domain.Interface.IRepository
{
    public interface IUsersRepository : ISqlSugarRepository
    {


        public bool CreateUser(Infrastructure.Entitys.User m);
        public List<User> QueryAllUser();
        public User UserLogin(string userName, string userPassword);
        public User QueryUserName(string userName);


    }
}
