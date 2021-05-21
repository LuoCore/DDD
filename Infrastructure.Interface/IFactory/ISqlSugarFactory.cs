using SqlSugar;
using System;
using System.Collections.Generic;

namespace Infrastructure.Interface.IFactory
{
    public interface ISqlSugarFactory
    {
      
        SqlSugarClient GetDbContext(Action<string, SugarParameter[]> onExecutedEvent);
        SqlSugarClient GetDbContext(Func<string, SugarParameter[], KeyValuePair<string, SugarParameter[]>> onExecutingChangeSqlEvent);
        SqlSugarClient GetDbContext(Action<string, SugarParameter[]> onExecutedEvent = null, Func<string, SugarParameter[], KeyValuePair<string, SugarParameter[]>> onExecutingChangeSqlEvent = null, Action<Exception> onErrorEvent = null);
        void GetDbContext(Action<SqlSugar.SqlSugarClient> Func);
        public T GetDbContext<T>(Func<SqlSugarClient, T> Func);
    }
}
