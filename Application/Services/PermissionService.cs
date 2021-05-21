using Application.Interface.IServices;
using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using Infrastructure.Common;
using Infrastructure.Interface.IFactory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 18:31:51
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class PermissionService : BaseService<IPermissionRepository>, IPermissionService
    {
        public PermissionService(ISqlSugarFactory factory, IPermissionRepository repository, IMediatorHandler bus, IEventStoreRepository eventStoreRepository) : base(factory, repository, bus, eventStoreRepository)
        {
        }

        public async Task<Boolean> Create(Application.Models.ViewModels.Permission.PermissionCreateViewModel vm)
        {
            var CreateCommand = new Domain.Models.CommandModels.Permission.CreateCommandModel(Guid.NewGuid(), vm.PermissionName, vm.PermissionType.IntToEnum<Domain.Models.CommandModels.PermissionCommandModel.PermissionTypeEnum>(), vm.PermissionAction, vm.PermissionParentId, vm.IsValid);
            return await Bus.SendCommand(CreateCommand);
        }

        public async Task<Boolean> Update(Application.Models.ViewModels.Permission.PermissionUpdateViewModel vm)
        {
            var CommandData = new Domain.Models.CommandModels.Permission.UpdateCommandModel(vm.PermissionId.StringToGuid(), vm.PermissionName, vm.PermissionType.IntToEnum<Domain.Models.CommandModels.PermissionCommandModel.PermissionTypeEnum>(), vm.PermissionAction, vm.PermissionParentId, vm.IsValid);
            return await Bus.SendCommand(CommandData);
        }


        private List<Application.Models.ViewModels.PermissionViewModel> RecursiveByParentId(string pid)
        {
            List<Application.Models.ViewModels.PermissionViewModel> res = new List<Application.Models.ViewModels.PermissionViewModel>();
            var resData = DbRepository.QueryByParentId(pid);
            resData.ForEach(x =>
            {
               
                Application.Models.ViewModels.PermissionViewModel model = new Application.Models.ViewModels.PermissionViewModel()
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


        public async Task<Models.ViewModels.Layui.TableViewModel<Application.Models.ViewModels.PermissionViewModel>> GetByPerentId(string pid)
        {

            return await Task.Run(() =>
            {
                Models.ViewModels.Layui.TableViewModel<Application.Models.ViewModels.PermissionViewModel> res = new Models.ViewModels.Layui.TableViewModel<Application.Models.ViewModels.PermissionViewModel>();
                try
                {
                    res.data = new List<Application.Models.ViewModels.PermissionViewModel>();
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


      
        public async Task<List<Models.ViewModels.Layui.SelectViewModel>> SelectByParentId(string permissionParentId)
        {

            return await Task.Run(() =>
            {
                List<Models.ViewModels.Layui.SelectViewModel> res = new List<Models.ViewModels.Layui.SelectViewModel>();

                try
                {
                    var resData = DbRepository.QueryByParentId(permissionParentId);
                    resData.ForEach(x =>
                    {
                        var selectModel = new Models.ViewModels.Layui.SelectViewModel()
                        {
                            Name = x.PermissionName,
                            value = x.PermissionId
                        };
                        selectModel.disabled = !Convert.ToBoolean(x.IsValid);
                        var sm = SelectByParentId(selectModel.value);
                        if (sm.Result != null && sm.Result.Count > 0)
                        {
                            selectModel.children = new List<Models.ViewModels.Layui.SelectViewModel>();
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
            var CommandEvent = new Domain.Models.CommandModels.Permission.DeleteCommandModel(permissionId.StringToGuid());
            return await Bus.SendCommand(CommandEvent);
        }
    }
}
