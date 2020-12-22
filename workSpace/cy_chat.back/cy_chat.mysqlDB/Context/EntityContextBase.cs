using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace cy_chat.mysqlDB.Context
{
    public class EntityContextBase<TContext> : DbContext, IEntityContext where TContext : DbContext
    {
        public EntityContextBase(DbContextOptions<TContext> options) : base(options)
        {
        }
    }
}
