using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using Domain.Models.CommandModels.Permission;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Common;
using Domain.Notifications;

namespace Domain.CommandEventsHandler.CommandHandlers
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/20 17:13:29
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class PermissionCommandHandler : CommandHandler,
                                            IRequestHandler<CreatePermissionCommandModel, bool>,
        IRequestHandler<UpdatePermissionCommandModel, bool>,
        IRequestHandler<DeletePermissionCommandModel, bool>
    {
        private readonly IPermissionRepository _Repository;
        private readonly IMediatorHandler Bus;
        public PermissionCommandHandler(IPermissionRepository permissionRepository, IMediatorHandler bus) : base(bus)
        {
            _Repository = permissionRepository;
            Bus = bus;
        }

        public Task<bool> Handle(CreatePermissionCommandModel request, CancellationToken cancellationToken)
        {
            // 命令验证
            if (!request.VerifyData())
            {
                // 错误信息收集
                NotifyValidationErrors(request);
                // 返回，结束当前线程
                return Task.FromResult(false);
            }

            Infrastructure.Entitys.Permission entity = new Infrastructure.Entitys.Permission() 
            {
                PermissionId=request.PermissionId.ToString(),
                PermissionName=request.PermissionName,
                PermissionAction=request.PermissionAction,
                PermissionType=request.PermissionType.EnumToInt(),
                PermissionParentId=request.PermissionParentId,
                IsValid=request.IsValid
            };

            var existingEntity = _Repository.QueryPermissionFirst(entity.PermissionName, entity.PermissionType, entity.PermissionParentId);
            if (existingEntity != null && !string.IsNullOrWhiteSpace(existingEntity.PermissionId))
            {
                Bus.RaiseEvent(new DomainNotification("Permission", "权限名称重复！"));
                return Task.FromResult(false);
            }
            //提交
            if (_Repository.CreatePermission(entity))
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等
                Bus.RaiseEvent(new Domain.Models.EventModels.Permission.PermissionCreateEventModel(entity.PermissionId.StringToGuid(),
                    entity.PermissionName,
                    entity.PermissionType.IntToEnum<Models.CommandModels.PermissionCommandModel.PermissionTypeEnum>(),
                    entity.PermissionAction,
                    entity.PermissionParentId,
                    entity.IsValid));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdatePermissionCommandModel request, CancellationToken cancellationToken)
        {
            if (!request.VerifyData())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);

            }

            Infrastructure.Entitys.Permission entity = new Infrastructure.Entitys.Permission()
            {
                PermissionId = request.PermissionId.ToString(),
                PermissionName = request.PermissionName,
                PermissionAction = request.PermissionAction,
                PermissionType = request.PermissionType.EnumToInt(),
                PermissionParentId = request.PermissionParentId,
                IsValid = request.IsValid
            };

            var existingEntity = _Repository.QueryPermissionFirst(entity.PermissionName, entity.PermissionType, entity.PermissionParentId);

            if (existingEntity != null && existingEntity.PermissionId == entity.PermissionId)
            {
                if (!existingEntity.Equals(entity))
                {

                    Bus.RaiseEvent(new DomainNotification("Permission", "权限的名称重复！"));
                    return Task.FromResult(false);

                }
            }

            if (_Repository.UpdatePermission(entity))
            {
                Bus.RaiseEvent(new Domain.Models.EventModels.Permission.PermissionUpdateEventModel(entity.PermissionId.StringToGuid(),
                   entity.PermissionName,
                   entity.PermissionType.IntToEnum<Models.CommandModels.PermissionCommandModel.PermissionTypeEnum>(),
                   entity.PermissionAction,
                   entity.PermissionParentId,
                   entity.IsValid));
            }
            else
            {
                Bus.RaiseEvent(new DomainNotification("Permission", "修改权限失败！"));
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(DeletePermissionCommandModel request, CancellationToken cancellationToken)
        {
            if (!request.VerifyData())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            if (_Repository.DeletePermission(request.PermissionId.ToString()))
            {
                Bus.RaiseEvent(new Domain.Models.EventModels.Permission.PermissionDeleteEventModel(request.PermissionId));
            }
            else
            {
                Bus.RaiseEvent(new DomainNotification("Permission", "删除失败！"));
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

       

      
    }
}
