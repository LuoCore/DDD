using Application.Interface;
using Application.Interface.IServices;
using Application.Models.ViewModels;
using Application.Models.ViewModels.User;
using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using Domain.Models.User.CommandModels;
using Infrastructure.Entitys;
using Infrastructure.Factory;
using Infrastructure.Interface.IFactory;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Common;
using static Domain.Models.Entitys.PermissionEntity;

namespace Application.Services
{
    public class UsersService : SqlSugarRepository<ISqlSugarFactory, IUsersRepository>, IUsersService
    {
        // 中介者 总线
        private readonly IMediatorHandler Bus;
        // 事件源仓储
        private readonly IEventStoreRepository _eventStoreRepository;

        public UsersService(ISqlSugarFactory factory, IUsersRepository repository,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository
            ) : base(factory, repository)
        {
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }


        public async Task<Boolean> UserRegister(UserCreateViewModel vm)
        {
            //这里引入领域设计中的写命令 还没有实现
            //请注意这里如果是平时的写法，必须要引入Student领域模型，会造成污染
            var registerCommand = new UserCreateCommandModel(Guid.NewGuid(), vm.UserName, vm.Email, vm.Password, vm.Phone, "用户注册");
            return await Bus.SendCommand(registerCommand);
        }

        public async Task<UserViewModel> UserLogin(UserLoginViewModel vm)
        {

            return await Task.Run<UserViewModel>(() =>
            {
                UserViewModel userRes = new UserViewModel();
                try
                {
                    Domain.Models.Entitys.UserEntity userReq = new Domain.Models.Entitys.UserEntity(vm.UserName, vm.Password, null, null, null);

                    Domain.Models.Entitys.UserEntity userData = DbRepository.ReadUser(userReq);
                    if (userData != null)
                    {
                        userRes.UserId = userData.ENTITY_USER.UserId;
                        userRes.UserName = userData.ENTITY_USER.UserName;
                        userRes.Phone = userData.ENTITY_USER.Phone;
                        userRes.Email = userData.ENTITY_USER.Email;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                return userRes;

            });
        }



        public async Task<Boolean> CreatePermission(PermissionCreateViewModel vm)
        {


            var CreateCommand = new PermissionCreateCommandModel(Guid.NewGuid(), vm.PermissionName, vm.PermissionType.IntToEnum<Domain.Models.Entitys.PermissionEntity.PermissionTypeEnum>(), vm.PermissionAction, vm.PermissionParentId, vm.IsValid);
            return await Bus.SendCommand(CreateCommand);
        }


        public async Task<LayuiTableViewModel<PermissionViewModel>> QueryPermission(PermissionViewModel vm)
        {

            return await Task.Run(() =>
            {
                LayuiTableViewModel<PermissionViewModel> res = new LayuiTableViewModel<PermissionViewModel>();
                try
                {
                    Domain.Models.Entitys.PermissionEntity reqData = new Domain.Models.Entitys.PermissionEntity
                    (
                        vm.PermissionName,
                        vm.PermissionType.IntToEnum<Domain.Models.Entitys.PermissionEntity.PermissionTypeEnum>(),
                        vm.PermissionAction,
                        vm.PermissionParentId,
                        vm.IsValid
                     );

                    var resData = DbRepository.ReadPermissionAll(reqData);
                    res.data = new List<PermissionViewModel>();
                    resData.ForEach(x =>
                    {
                        res.data.Add(new PermissionViewModel()
                        {
                            PermissionId = x.ENTITY_PERMISSION.PermissionId,
                            PermissionName = x.ENTITY_PERMISSION.PermissionName,
                            PermissionAction = x.ENTITY_PERMISSION.PermissionAction,
                            PermissionParentId = x.ENTITY_PERMISSION.PermissionParentId,
                            PermissionType = x.ENTITY_PERMISSION.PermissionType,
                            IsValid = x.ENTITY_PERMISSION.IsValid
                        });
                    });

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

        public async Task<List<LayuiSelectViewModel>> GetPermissionSelect(string permissionParentId)
        {

            return await Task.Run(() =>
            {
                List<LayuiSelectViewModel> res = new List<LayuiSelectViewModel>();
                
                try
                {

                    


                    Domain.Models.Entitys.PermissionEntity reqData = new Domain.Models.Entitys.PermissionEntity(permissionParentId);
                    var resData = DbRepository.ReadPermissionAll(reqData);

                   

                    resData.ForEach(x =>
                    {
                        var selectModel = new LayuiSelectViewModel()
                        {
                            Name = x.ENTITY_PERMISSION.PermissionName,
                            value = x.ENTITY_PERMISSION.PermissionId
                        };
                        selectModel.disabled = !Convert.ToBoolean(x.IsValid);
                        var sm=  GetPermissionSelect(selectModel.value);
                        if (sm.Result != null && sm.Result.Count > 0) 
                        {
                            selectModel.children = new List<LayuiSelectViewModel>();
                            selectModel.children = sm.Result;
                            selectModel.disabled = true;
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


    }
}
