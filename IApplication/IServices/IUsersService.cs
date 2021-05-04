using Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IServices
{
    public interface IUsersService
    {
        void Register(Models.ViewModels.UserCreateViewModel userViewModel);
        public  Task<UserViewModel> Login(UserLoginViewModel vm);
    }
}
