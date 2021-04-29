
using Infrastructure.Interface.IRepository;
using Infrastructure.Repository;

namespace Domain.Interface.IRepository
{
    public interface IUsersRepository:ISqlSugarRepository
    {
        //一些Student独有的接口
        Infrastructure.Entitys.User GetByEmail(string email);

        bool Create(Infrastructure.Entitys.User user);
    }
}
