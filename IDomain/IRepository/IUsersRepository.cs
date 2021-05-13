
using Infrastructure.Entitys;
using Infrastructure.Interface.IRepository;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace Domain.Interface.IRepository
{
    public interface IUsersRepository : ISqlSugarRepository
    {


        public bool CreateUser(Models.Entitys.UserEntity m);

        public List<User> ReadUserAll();

        public User ReadUserLogin(string userName, string userPassword);
        public User ReadUserName(string userName);



        public bool ReadPermissionParentIdAny(string ParentId);
        public List<Permission> ReadPermissionParentIdList(string ParentId);

        public Permission ReadPermissionNameType(string nameValue, int type, string pid);
        public List<Permission> ReadPermissionAll();

        public bool CreatePermission(Models.Entitys.PermissionEntity m);


    }
}
