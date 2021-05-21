using Application.Interface.IServices;
using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using Infrastructure.Interface.IFactory;
using System;
using System.Collections.Generic;
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

        public async Task<Models.ViewModels.Layui.TableViewModel<Application.Models.ViewModels.RoleViewModel>> GetAll()
        {
            return await Task.Run(() =>
            {
                Models.ViewModels.Layui.TableViewModel<Application.Models.ViewModels.RoleViewModel> models = new Models.ViewModels.Layui.TableViewModel<Application.Models.ViewModels.RoleViewModel>();
                models.data = new List<Models.ViewModels.RoleViewModel>();
                var roleList = DbRepository.QueryAll();
                roleList.ForEach(x =>
                {
                    models.data.Add(new Models.ViewModels.RoleViewModel()
                    {
                        RoleId = x.RoleId,
                        RoleName = x.RoleName,
                        RoleDescription = x.RoleDescription,
                        IsValid = x.IsValid
                    });
                });
                models.code = 0;
                models.count = models.data.Count;
                if (models.count < 1)
                {

                    models.code = -1;
                    models.msg = "没有数据！";
                }
                return models;
            });
        }
    }
}
