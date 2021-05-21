using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface.IServices
{
    public interface IRoleService
    {
        public  Task<Boolean> Create(Application.Models.ViewModels.Role.CreateViewModel vm);
        public  Task<Models.ViewModels.Layui.TableViewModel<Application.Models.ViewModels.RoleViewModel>> GetAll();
    }
}
