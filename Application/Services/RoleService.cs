using Application.Interface.IServices;
using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using Infrastructure.Interface.IFactory;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/21 10:19:05
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class RoleService : BaseService<IRoleRepository>, IRoleService
    {
        public RoleService(ISqlSugarFactory factory, IRoleRepository repository, IMediatorHandler bus, IEventStoreRepository eventStoreRepository) : base(factory, repository, bus, eventStoreRepository)
        {
        }

        public async Task<Boolean> Create(Application.Models.ViewModels.Role.CreateViewModel vm)
        {
            var CreateCommand = new Domain.Models.CommandModels.Role.CreateCommandModel(Guid.NewGuid(), vm.RoleName,vm.RoleDescription,vm.IsValid);
            return await Bus.SendCommand(CreateCommand);
        }
    }
}
