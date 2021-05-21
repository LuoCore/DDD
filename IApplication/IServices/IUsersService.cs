using Application.Models.ViewModels;
using Application.Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface.IServices
{
    public interface IUsersService
    {

        public  Task<Boolean> UserRegister(UserCreateViewModel vm);

        public  Task<UserViewModel> UserLogin(UserLoginViewModel vm);

        public IList<Domain.Models.HistoryModels.UserHistoryModel> GetAllHistory();
    }
}
