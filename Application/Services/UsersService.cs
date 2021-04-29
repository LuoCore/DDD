using Application.EventSourcedNormalizers;
using Application.Interface;
using Application.Models.EventSourcedNormalizers;
using Application.Models.ViewModels;
using Domain.Interface.ICommandHandlers;
using Domain.Interface.IRepository;
using Infrastructure.Factory;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UsersService : SqlSugarRepository<SqlSugarFactory, IUsersRepository>, IUsersService
    {
        // 中介者 总线
        private readonly IMediatorHandler _Mediator;
        // 事件源仓储
        private readonly IEventStoreRepository _EventStoreRepository;
        public UsersService(SqlSugarFactory factory, IUsersRepository repository, IMediatorHandler mediator, IEventStoreRepository eventStoreRepository) : base(factory)
        {
            _Mediator = mediator;
            _EventStoreRepository = eventStoreRepository;
        }

        public async Task<IList<StudentHistoryData>> GetAllHistory(Guid id)
        {
         
            return StudentHistory.ToJavaScriptStudentHistory(await _EventStoreRepository.All(id));
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
            Domain.Commands.User.UserCreateCommand usercommandModel= new Domain.Commands.User.UserCreateCommand(userViewModel.Name, userViewModel.Email, userViewModel.BirthDate, userViewModel.Phone, userViewModel.Province, userViewModel.City, userViewModel.County, userViewModel.Street);

            _Mediator.SendCommand(usercommandModel);

            throw new NotImplementedException();
        }

        IList<StudentHistoryData> IUsersService.GetAllHistory(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
