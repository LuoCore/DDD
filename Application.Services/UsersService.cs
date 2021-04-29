using Application.Interface;
using Application.Models.ViewModels;
using Domain.Interface.ICommandHandlers;
using Domain.Interface.IRepository;
using Infrastructure.Factory;
using Infrastructure.Repository;
using System;

namespace Application.Services
{
    public class UsersService : SqlSugarRepository<SqlSugarFactory, IUsersRepository>, IUsersService
    {
        // 中介者 总线
        private readonly IMediatorHandler Bus;
        // 事件源仓储
        private readonly IEventStoreRepository _eventStoreRepository;
        public UsersService(SqlSugarFactory factory, IUsersRepository repository) : base(factory)
        {

        }

        public UserViewModel GetById(Guid id)
        {
            var ddd = DbRepository.GetByEmail(id.ToString());
            UserViewModel userView = new UserViewModel()
            {
                Email = ddd.Email
            };
            return userView;
        }

        public void Register(UserViewModel userViewModel)
        {
            
            throw new NotImplementedException();
        }
    }
}
