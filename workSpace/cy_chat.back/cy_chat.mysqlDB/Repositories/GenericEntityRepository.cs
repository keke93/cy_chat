using cy_chat.mysqlDB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace cy_chat.mysqlDB.Repositories
{
    public class GenericEntityRepository<TEntity> : EntityRepositoryBase<DbContext, TEntity> where TEntity : EntityBase, new()
    {
        public GenericEntityRepository(ILogger<DataAccess> logger) : base(logger, null)
        { }
    }
}
