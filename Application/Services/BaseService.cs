using Domain.Interface.ICommandEventsHandler;
using Domain.Interface.IRepository;
using Infrastructure.Interface.IFactory;
using Infrastructure.Interface.IRepository;
using Infrastructure.Repository;

namespace Application.Services
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/21 10:19:48
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class BaseService<T> : SqlSugarRepository<ISqlSugarFactory, T> where T: ISqlSugarRepository
    {
        protected readonly IMediatorHandler Bus;
        protected readonly IEventStoreRepository _EVENTSTOREREPOSITORY;
        public BaseService(ISqlSugarFactory factory, T repository, IMediatorHandler bus,IEventStoreRepository eventStoreRepository) : base(factory, repository)
        {
            Bus = bus;
            _EVENTSTOREREPOSITORY = eventStoreRepository;
        }
    }
}
       
  

