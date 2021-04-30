using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interface.IServices
{
    public interface IUsersService
    {
        void Register(Models.ViewModels.UserViewModel userViewModel);
    }
}
