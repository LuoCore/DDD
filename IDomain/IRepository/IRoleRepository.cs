using Infrastructure.Interface.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interface.IRepository
{
    public interface IRoleRepository : ISqlSugarRepository
    {
        public List<Infrastructure.Entitys.Role> QueryAll();
        public Infrastructure.Entitys.Role QueryByName(string namevalue);
        public bool Create(Infrastructure.Entitys.Role m);
    }
}
