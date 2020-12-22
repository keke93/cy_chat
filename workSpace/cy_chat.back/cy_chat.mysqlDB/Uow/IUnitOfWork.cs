using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using cy_chat.mysqlDB.Repositories;

namespace cy_chat.mysqlDB.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IRepository<TEntity> GetRepository<TEntity>();
        TRepository GetCustomRepository<TRepository>();
    }
}
