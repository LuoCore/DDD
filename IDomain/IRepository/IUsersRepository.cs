
using Infrastructure.Entitys;
using Infrastructure.Interface.IRepository;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace Domain.Interface.IRepository
{
    public interface IUsersRepository : ISqlSugarRepository
    {

        public List<User> ReadUserAll();
        bool CreateUser(User model);
        public User ReadUser(User model);

        public Infrastructure.Entitys.Permission ReadPermission(Infrastructure.Entitys.Permission m);
        public bool CreatePermission(Permission m);


    }
}
