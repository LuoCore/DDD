using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using MediatR;
using Infrastructure.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandEventsHandler.CommandHandlers
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/21 16:20:44
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class RoleCommandHandler : CommandHandler<IRoleRepository>,
         IRequestHandler<Models.CommandModels.Role.CreateCommandModel, bool>,
        IRequestHandler<Models.CommandModels.Role.UpdateCommandModel, bool>,
        IRequestHandler<Models.CommandModels.Role.DeleteCommandModel, bool>
    {
        public RoleCommandHandler(IMediatorHandler bus, IRoleRepository repository) : base(bus, repository)
        {
        }

        public Task<bool> Handle(Models.CommandModels.Role.CreateCommandModel request, CancellationToken cancellationToken)
        {
            if (!request.VerifyData())
            {
                // 错误信息收集
                NotifyValidationErrors(request);
                // 返回，结束当前线程
                return Task.FromResult(false);
            }

            Infrastructure.Entitys.Role entity = new Infrastructure.Entitys.Role()
            {
                RoleId=request.RoleId.ToString(),
                RoleName=request.RoleName,
                RoleDescription=request.RoleDescription,
                IsValid=request.IsValid
            };

            var existingEntity = _REPOSITORY.QueryByName(entity.RoleName);
            if (existingEntity != null && !string.IsNullOrWhiteSpace(existingEntity.RoleId))
            {
                _Bus.RaiseEvent(new Notifications.DomainNotification("Role", "权限名称重复！"), "Role");
                return Task.FromResult(false);
            }
            //提交
            if (_REPOSITORY.Create(entity))
            {
                _Bus.RaiseEvent(new Domain.Models.EventModels.Role.CreateEventModel(entity.RoleId.StringToGuid(), entity.RoleName, entity.RoleDescription, entity.IsValid), "Role");
                return Task.FromResult(true);
            }
            else 
            {
                _Bus.RaiseEvent(new Notifications.DomainNotification("Role", "数据写入失败！"), "Role");
                return Task.FromResult(false);
            }

           
        }

        public Task<bool> Handle(Models.CommandModels.Role.UpdateCommandModel request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Handle(Models.CommandModels.Role.DeleteCommandModel request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
