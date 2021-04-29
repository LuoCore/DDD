using Application.Models.EventSourcedNormalizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interface
{
    public interface IUsersService
    {
        void Register(Models.ViewModels.UserViewModel userViewModel);
        IList<StudentHistoryData> GetAllHistory(Guid id);
    }
}
