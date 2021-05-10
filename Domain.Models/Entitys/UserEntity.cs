using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Entitys
{
    public class UserEntity : BaseEntity
    {

        public UserEntity(string usetName)
        {

            ENTITY_USER = new Infrastructure.Entitys.User
            {
                UserName = usetName,
              
            };
        }
        public UserEntity(string usetName, string password)
        {

            ENTITY_USER = new Infrastructure.Entitys.User
            {
                UserName = usetName,
                Password=password

            };
        }
        public UserEntity(string usetName, string password, string email, string phone, string createName)
        {
           
            ENTITY_USER = new Infrastructure.Entitys.User
            {
                UserName = usetName,
                Password = password,
                Phone = phone,
                Email = email,
                CreateName = createName
            };
        }

        public UserEntity(Guid userId, string usetName,string password, string email, string phone,string createName)
        {
            Id = userId;
            ENTITY_USER = new Infrastructure.Entitys.User
            {
                UserId= Id.ToString(),
                UserName= usetName,
                Password= password,
                Phone = phone,
                Email=email,
                CreateName= createName
            };
        }
        public Infrastructure.Entitys.User ENTITY_USER { get;protected set; }
    }
}
