using Domain.Interface.IRepository;
using Infrastructure.Interface.IFactory;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repository
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/21 13:58:34
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class RoleRepository : SqlSugarRepository<ISqlSugarFactory>, IRoleRepository
    {
        public RoleRepository(ISqlSugarFactory factory) : base(factory)
        {
        }

        public List<Infrastructure.Entitys.Role> QueryAll()
        {
            return Factory.GetDbContext((db) =>
             {
                 return db.Queryable<Infrastructure.Entitys.Role>().ToList();
             });

        }
        public Infrastructure.Entitys.Role QueryByName(string namevalue) 
        {
            return Factory.GetDbContext((db) =>
            {
                return db.Queryable<Infrastructure.Entitys.Role>().Where(x=>x.RoleName== namevalue).First();
            });
        }
        public bool Create(Infrastructure.Entitys.Role m)
        {
            return Factory.GetDbContext((db) =>
            {
                return db.Insertable<Infrastructure.Entitys.Role>(new
                {
                    m.RoleId,
                    m.RoleName,
                    m.RoleDescription,
                    m.IsValid
                })
                .IgnoreColumns(true)
                .ExecuteCommand()>0;
            });
        }
    }
}
