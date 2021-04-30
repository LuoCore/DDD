
using Infrastructure.Interface.IFactory;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace Infrastructure.Factory
{
    public class SqlSugarFactory : ISqlSugarFactory
    {
        private readonly ConnectionConfig config;

        public SqlSugarFactory(ConnectionConfig _config)
        {

            this.config = _config;
        }
        //public SqlSugarClient GetDbContext(Action<Exception> onErrorEvent)
        //{
        //    return GetDbContext(null, null, onErrorEvent);
        //}

        public SqlSugarClient GetDbContext(Action<string, SugarParameter[]> onExecutedEvent)
        {
            return GetDbContext(onExecutedEvent);
        }

        public SqlSugarClient GetDbContext(Func<string, SugarParameter[], KeyValuePair<string, SugarParameter[]>> onExecutingChangeSqlEvent)
        {
            return GetDbContext(null, onExecutingChangeSqlEvent);
        }

        public SqlSugarClient GetDbContext(Action<string, SugarParameter[]> onExecutedEvent = null, Func<string, SugarParameter[], KeyValuePair<string, SugarParameter[]>> onExecutingChangeSqlEvent = null, Action<Exception> onErrorEvent = null)
        {

            SqlSugarClient db = new SqlSugarClient(config)
            {
                Aop =
                {
                      OnExecutingChangeSql = onExecutingChangeSqlEvent,
                      OnError = onErrorEvent ?? ((Exception ex) =>
                      {
                           Console.WriteLine($"ExecuteSql Error：【{ex}】");
                          //this._logger.LogError(ex, "ExecuteSql Error"); }
                      }),
                      OnLogExecuted = onExecutedEvent ?? ((string sql, SugarParameter[] pars) =>
                      {
                            var keyDic = new KeyValuePair<string, SugarParameter[]>(sql, pars);
                            Console.WriteLine($"ExecuteSql：【{keyDic}】");
                          //this._logger.LogInformation($"ExecuteSql：【{keyDic.ToJson()}】");
                      })
                  }
            };
            return db;
        }
        public void GetDbContext(Action<SqlSugarClient> Func)
        {

            using (SqlSugarClient db = new SqlSugarClient(this.config))
            {
                try
                {

                    db.Ado.IsEnableLogEvent = true;

                    db.Aop.OnLogExecuting = (sql, pars) =>//每次Sql执行前事件
                    {
                        //我可以在这里面写逻辑
                    };

                    db.Aop.OnExecutingChangeSql = (sql, pars) => //可以修改SQL和参数的值
                    {
                        return new KeyValuePair<string, SugarParameter[]>(sql, pars);
                    };
                    db.Aop.OnLogExecuted = (sql, pars) => //SQL执行完
                    {
                        Console.Write("time:" + db.Ado.SqlExecutionTime.ToString());//输出SQL执行时间

                        if (db.Ado.SqlExecutionTime.TotalSeconds > 1)//执行时间超过1秒
                        {
                            //代码CS文件名
                            var fileName = db.Ado.SqlStackTrace.FirstFileName;
                            //代码行数
                            var fileLine = db.Ado.SqlStackTrace.FirstLine;
                            //方法名
                            var FirstMethodName = db.Ado.SqlStackTrace.FirstMethodName;
                            //db.Ado.SqlStackTrace.MyStackTraceList[1].xxx 获取上层方法的信息
                        }
                    };

                    Func(db);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    db.Aop.OnError = (exp) =>//SQL报错
                    {
                        var dd = exp.Sql; //这样可以拿到错误SQL  
                        Console.Write("ExecSql:" + exp.Sql);
                    };
                }
            };
        }




    }
}
