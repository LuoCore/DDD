using Application.Interface.IServices;
using Application.Models.ViewModels;
using Application.Models.ViewModels.User;
using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using Infrastructure.Common;
using Infrastructure.Interface.IFactory;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 18:31:51
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class PermissionService : SqlSugarRepository<ISqlSugarFactory, IPermissionRepository>, IPermissionService
    {
        private readonly IMediatorHandler Bus;
        // 事件源仓储
        private readonly IEventStoreRepository _eventStoreRepository;

        public PermissionService(ISqlSugarFactory factory, IPermissionRepository repository,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository
            ) : base(factory, repository)
        {
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

      

        public async Task<Boolean> Create(PermissionCreateViewModel vm)
        {
            var CreateCommand = new Domain.Models.CommandModels.Permission.CreatePermissionCommandModel(Guid.NewGuid(), vm.PermissionName, vm.PermissionType.IntToEnum<Domain.Models.CommandModels.PermissionCommandModel.PermissionTypeEnum>(), vm.PermissionAction, vm.PermissionParentId, vm.IsValid);
            return await Bus.SendCommand(CreateCommand);
        }

   


        private List<PermissionViewModel> RecursiveByParentId(string pid)
        {
            List<PermissionViewModel> res = new List<PermissionViewModel>();
            var resData = DbRepository.QueryByParentId(pid);
            resData.ForEach(x =>
            {
                PermissionViewModel model = new PermissionViewModel()
                {
                    PermissionId = x.PermissionId,
                    PermissionName = x.PermissionName,
                    PermissionAction = x.PermissionAction,
                    PermissionParentId = x.PermissionParentId,
                    PermissionType = x.PermissionType,
                    IsValid = x.IsValid
                };
                res.Add(model);
                res.AddRange(RecursiveByParentId(model.PermissionId));
            });
            return res;

        }


        public async Task<LayuiTableViewModel<PermissionViewModel>> GetByPerentId(string pid)
        {

            return await Task.Run(() =>
            {
                LayuiTableViewModel<PermissionViewModel> res = new LayuiTableViewModel<PermissionViewModel>();
                try
                {
                    res.data = new List<PermissionViewModel>();
                    res.data = RecursiveByParentId(pid);
                    res.code = 0;
                    res.count = res.data.Count;
                    if (res.count < 1)
                    {

                        res.code = -1;
                        res.msg = "没有数据！";
                    }
                }
                catch (Exception ex)
                {
                    res.code = -1;
                    res.msg = "异常错误：" + ex;
                }
                return res;

            });

        }


      
        public async Task<List<LayuiSelectViewModel>> SelectByParentId(string permissionParentId)
        {

            return await Task.Run(() =>
            {
                List<LayuiSelectViewModel> res = new List<LayuiSelectViewModel>();

                try
                {
                    var resData = DbRepository.QueryByParentId(permissionParentId);
                    resData.ForEach(x =>
                    {
                        var selectModel = new LayuiSelectViewModel()
                        {
                            Name = x.PermissionName,
                            value = x.PermissionId
                        };
                        selectModel.disabled = !Convert.ToBoolean(x.IsValid);
                        var sm = SelectByParentId(selectModel.value);
                        if (sm.Result != null && sm.Result.Count > 0)
                        {
                            selectModel.children = new List<LayuiSelectViewModel>();
                            selectModel.children = sm.Result;
                        }
                        res.Add(selectModel);
                    });
                }
                catch (Exception ex)
                {

                }
                return res;

            });
        }
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteById(string permissionId)
        {
            var CommandEvent = new Domain.Models.CommandModels.Permission.DeletePermissionCommandModel(permissionId.StringToGuid());
            return await Bus.SendCommand(CommandEvent);
        }
    }
}
