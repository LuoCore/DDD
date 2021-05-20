using Infrastructure.Entitys;
using Infrastructure.Interface.IRepository;
using System.Collections.Generic;

namespace Domain.Interface.IRepository
{
    public interface IUsersRepository : ISqlSugarRepository
    {


        public bool CreateUser(Infrastructure.Entitys.User m);
        public List<User> QueryAll();
        public User QueryByNamePassword(string userName, string userPassword);
        public User QueryByName(string userName);


    }
}
