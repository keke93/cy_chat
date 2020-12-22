using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace cy_chat.mysqlDB.Uow
{

    public class UnitOfWork : UnitOfWorkBase<DbContext>, IUnitOfWork
    {
        public UnitOfWork(DbContext context, IServiceProvider provider) : base(context, provider)
        { }
    }
}
