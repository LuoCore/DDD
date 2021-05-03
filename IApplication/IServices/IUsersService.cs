using Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interface.IServices
{
    public interface IUsersService
    {
        void Register(Models.ViewModels.UserCreateViewModel userViewModel);
        public UserViewModel Login(UserLoginViewModel vm);
    }
}
