
using Infrastructure.Entitys;
using Infrastructure.Interface.IRepository;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace Domain.Interface.IRepository
{
    public interface IUsersRepository : ISqlSugarRepository
    {

     
        public bool CreateUser(Models.Entitys.UserEntity m);
        public List<Models.Entitys.UserEntity> ReadUserAll(Models.Entitys.UserEntity m);
        public Models.Entitys.UserEntity ReadUser(Models.Entitys.UserEntity m);

        public Models.Entitys.PermissionEntity ReadPermission(Models.Entitys.PermissionEntity m);
        public List<Models.Entitys.PermissionEntity> ReadPermissionAll(Models.Entitys.PermissionEntity m);
        public bool CreatePermission(Models.Entitys.PermissionEntity m);


    }
}
