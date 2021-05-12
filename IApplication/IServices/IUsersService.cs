using Application.Models.ViewModels;
using Application.Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IServices
{
    public interface IUsersService
    {

        public Task<Boolean> UserRegister(UserCreateViewModel vm);
        public Task<UserViewModel> UserLogin(UserLoginViewModel vm);

        public Task<Boolean> CreatePermission(PermissionCreateViewModel vm);
        public  Task<LayuiTableViewModel<PermissionViewModel>> QueryPermissionParentId(string parentId);
      
        Task<List<LayuiSelectViewModel>> GetPermissionSelect(string permissionParentId);
    }
}
