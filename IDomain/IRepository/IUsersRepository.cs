
using Infrastructure.Entitys;
using Infrastructure.Interface.IRepository;
using Infrastructure.Repository;

namespace Domain.Interface.IRepository
{
    public interface IUsersRepository : ISqlSugarRepository
    {
         User Read(string id);

        bool Create(User model);
    }
}
