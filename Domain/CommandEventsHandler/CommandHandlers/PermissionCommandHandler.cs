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
    public class PermissionCommandHandler : CommandHandler<IPermissionRepository>,
        IRequestHandler<CreateCommandModel, bool>,
        IRequestHandler<UpdateCommandModel, bool>,
        IRequestHandler<DeleteCommandModel, bool>
    {
        public PermissionCommandHandler(IMediatorHandler bus, IPermissionRepository repository) : base(bus, repository)
        {
        }

        public Task<bool> Handle(CreateCommandModel request, CancellationToken cancellationToken)
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

            var existingEntity = _REPOSITORY.QueryByNameTypeParentId(entity.PermissionName, entity.PermissionType, entity.PermissionParentId);
            if (existingEntity != null && !string.IsNullOrWhiteSpace(existingEntity.PermissionId))
            {
                _Bus.RaiseEvent(new DomainNotification("Permission", "权限名称重复！"), "Permission");
                return Task.FromResult(false);
            }
            //提交
            if (_REPOSITORY.CreatePermission(entity))
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等
                _Bus.RaiseEvent(new Domain.Models.EventModels.Permission.CreatePermissionEventModel(entity.PermissionId.StringToGuid(),
                    entity.PermissionName,
                    entity.PermissionType.IntToEnum<Models.CommandModels.Permission.PermissionCommandModel.PermissionTypeEnum>(),
                    entity.PermissionAction,
                    entity.PermissionParentId,
                    entity.IsValid), "Permission");
                return Task.FromResult(true);
            }
            else
            {
                _Bus.RaiseEvent(new Notifications.DomainNotification("Permission", "数据写入失败！"), "Permission");
                return Task.FromResult(false);
            }

        }

        public Task<bool> Handle(UpdateCommandModel request, CancellationToken cancellationToken)
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

            var existingEntity = _REPOSITORY.QueryByNameTypeParentId(entity.PermissionName, entity.PermissionType, entity.PermissionParentId);

            if (existingEntity != null && existingEntity.PermissionId != entity.PermissionId)
            {
                if (!existingEntity.Equals(entity))
                {

                    _Bus.RaiseEvent(new DomainNotification("Permission", "不存在该权限的信息！"), "Permission");
                    return Task.FromResult(false);

                }
            }

            if (_REPOSITORY.UpdatePermission(entity))
            {
                _Bus.RaiseEvent(new Domain.Models.EventModels.Permission.UpdatePermissionEventModel(entity.PermissionId.StringToGuid(),
                   entity.PermissionName,
                   entity.PermissionType.IntToEnum<Models.CommandModels.Permission.PermissionCommandModel.PermissionTypeEnum>(),
                   entity.PermissionAction,
                   entity.PermissionParentId,
                   entity.IsValid), "Permission");
                return Task.FromResult(true);
            }
            else
            {
                _Bus.RaiseEvent(new DomainNotification("Permission", "修改权限失败！"), "Permission");
                return Task.FromResult(false);
            }

           
        }

        public Task<bool> Handle(DeleteCommandModel request, CancellationToken cancellationToken)
        {
            if (!request.VerifyData())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            if (_REPOSITORY.DeletePermission(request.PermissionId.ToString()))
            {
                _Bus.RaiseEvent(new Domain.Models.EventModels.Permission.DeletePermissionEventModel(request.PermissionId), "Permission");
                return Task.FromResult(true);
            }
            else
            {
                _Bus.RaiseEvent(new DomainNotification("Permission", "删除失败！"), "Permission");
                return Task.FromResult(false);
            }

            
        }

       

      
    }
}
