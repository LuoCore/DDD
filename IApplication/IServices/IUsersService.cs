using Application.Models.ViewModels;
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
    }
}
