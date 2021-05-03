
using Infrastructure.Entitys;
using Infrastructure.Interface.IRepository;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace Domain.Interface.IRepository
{
    public interface IUsersRepository : ISqlSugarRepository
    {
        public List<User> Read();

        bool Create(User model);
        public User ReadId(string userId);
        public User ReadName(string username);
        public User Login(string username, string pwd);
    }
}
