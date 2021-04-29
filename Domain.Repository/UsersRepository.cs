
using Domain.Interface.IRepository;
using Infrastructure.Entitys;
using Infrastructure.Factory;
using Infrastructure.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class UsersRepository : SqlSugarRepository<SqlSugarFactory>, IUsersRepository
    {
        public UsersRepository(SqlSugarFactory factory) : base(factory)
        {

        }

    

        public bool Create(User user)
        {
            throw new System.NotImplementedException();
        }

        public User GetByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
