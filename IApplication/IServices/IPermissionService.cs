using Application.Models.ViewModels;
using Application.Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IServices
{
    public interface IPermissionService
    {
        public  Task<LayuiTableViewModel<PermissionViewModel>> GetByPerentId(string pid);
        public  Task<bool> DeleteById(string permissionId);
        public  Task<List<LayuiSelectViewModel>> SelectByParentId(string permissionParentId);
        public  Task<Boolean> Create(PermissionCreateViewModel vm);
    }
}
