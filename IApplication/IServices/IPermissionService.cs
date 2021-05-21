using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface.IServices
{
    public interface IPermissionService
    {
        public  Task<Models.ViewModels.Layui.TableViewModel<Application.Models.ViewModels.PermissionViewModel>> GetByPerentId(string pid);
        public  Task<bool> DeleteById(string permissionId);
        public  Task<List<Models.ViewModels.Layui.SelectViewModel>> SelectByParentId(string permissionParentId);
        public  Task<Boolean> Create(Application.Models.ViewModels.Permission.PermissionCreateViewModel vm);
        public Task<Boolean> Update(Application.Models.ViewModels.Permission.PermissionUpdateViewModel vm);
    }
}
