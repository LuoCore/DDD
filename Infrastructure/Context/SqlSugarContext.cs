

using Infrastructure.Interface.IContext;
using Infrastructure.Interface.IFactory;

namespace Infrastructure.Context
{
    public class SqlSugarContext<TFactory, TIRepository> : ISqlSugarContext where TFactory : ISqlSugarFactory where TIRepository : ISqlSugarContext
    {
        protected readonly TFactory Factory;
        protected readonly TIRepository DbRepository;
        protected TFactory DbContext => this.Factory;
        public SqlSugarContext(TFactory factory)
        {
            Factory = factory;
        }

        public SqlSugarContext(TFactory factory, TIRepository repository) : this(factory)
        {
            DbRepository = repository;
        }
    }

    public class SqlSugarContext<TFactory> : ISqlSugarContext where TFactory : ISqlSugarFactory
    {
        protected readonly TFactory Factory;
        protected TFactory DbContext
        {
            get
            {
                return this.Factory;
            }
        }

        public SqlSugarContext(TFactory factory)
        {
            Factory = factory;
        }
    }
}
