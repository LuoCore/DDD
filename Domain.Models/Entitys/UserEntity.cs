using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Entitys
{
    public class UserEntity : BaseEntity
    {
        public UserEntity()
        {
            USER = new Infrastructure.Entitys.User();
        }
        public UserEntity(Guid userId, string usetName,string password, string email, string phone,string createName)
        {
            USER = new Infrastructure.Entitys.User
            {
                UserId=userId.ToString(),
                UserName= usetName,
                Password= password,
                Phone = phone,
                Email=email,
                CreateName= createName
            };
        }
        public Infrastructure.Entitys.User USER { get; set; }
    }
}
