
using Domain.Interface.IRepository;
using Infrastructure.Entitys;
using Infrastructure.Interface.IFactory;
using Infrastructure.Repository;


namespace Domain.Repository
{
    public class UsersRepository : SqlSugarRepository<ISqlSugarFactory>, IUsersRepository
    {
        public UsersRepository(ISqlSugarFactory factory) : base(factory)
        {

        }

        public bool Create(User model)
        {
            throw new System.NotImplementedException();
        }

        public User Read(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
