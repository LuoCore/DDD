using System;
using System.Threading.Tasks;

namespace Application.Interface.IServices
{
    public interface IRoleService
    {
        public  Task<Boolean> Create(Application.Models.ViewModels.Role.CreateViewModel vm);
    }
}
