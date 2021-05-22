using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using Domain.Models.CommandModels.RolePermission;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandEventsHandler.CommandHandlers
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/22 15:15:46
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class RolePermissionCommandHandler : CommandHandler<Domain.Interface.IRepository.IRolePermissionRepository>,
        MediatR.IRequestHandler<Models.CommandModels.RolePermission.CreateCommandModel, bool>,
        MediatR.IRequestHandler<Models.CommandModels.RolePermission.CreateBatchCommandModel, bool>
    {
        public RolePermissionCommandHandler(IMediatorHandler bus, IRolePermissionRepository repository) : base(bus, repository)
        {
        }

        public Task<bool> Handle(CreateCommandModel request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Handle(CreateBatchCommandModel request, CancellationToken cancellationToken)
        {
            if (!request.VerifyData())
            {
                // 错误信息收集
                NotifyValidationErrors(request);
                // 返回，结束当前线程
                return Task.FromResult(false);
            }

            List<Infrastructure.Entitys.Role_Permission> entitys = new List<Infrastructure.Entitys.Role_Permission>();
            foreach (string s in request.PermissionIds)
            {
                if (!_REPOSITORY.AnyByRoleIdPermissionId(request.RoleId, s)) 
                {
                    entitys.Add(new Infrastructure.Entitys.Role_Permission()
                    {
                        RolePermissionId = request.RolePermissionId.ToString(),
                        RoleId = request.RoleId,
                        PermissionId = s
                    });
                }
                
            }
           
            //提交
            if (_REPOSITORY.CreateBatch(entitys))
            {
                _Bus.RaiseEvent(new Domain.Models.EventModels.RolePermission.CreateBatchEventModel(request.RolePermissionId,request.RoleId,request.PermissionIds), "Role_Permission");
            }
            return Task.FromResult(true);
        }
    }
}
