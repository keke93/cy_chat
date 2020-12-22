using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using cy_chat.mysqlDB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;

namespace cy_chat.mysqlDB.Repositories
{
    public abstract class EntityRepositoryBase<TContext, TEntity> : RepositoryBase<TContext>, IRepository<TEntity> where TContext : DbContext where TEntity : EntityBase, new()
    {
        protected EntityRepositoryBase(ILogger<DataAccess> logger, TContext context) : base(logger, context)
        { }

        public virtual IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(null, orderBy, includes);
            return result.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(null, orderBy, includes);
            return await result.ToListAsync();
        }

        public virtual void Load(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(null, orderBy, includes);
            result.Load();
        }

        public virtual async Task LoadAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(null, orderBy, includes);
            await result.LoadAsync();
        }

        public virtual IEnumerable<TEntity> GetPage(int startRow, int pageLength, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(null, orderBy, includes);
            return result.Skip(startRow).Take(pageLength).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetPageAsync(int startRow, int pageLength, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(null, orderBy, includes);
            return await result.Skip(startRow).Take(pageLength).ToListAsync();
        }

        public virtual TEntity Get(object id)
        {
            return Context.Set<TEntity>().Find(id);
        }


        public virtual ValueTask<TEntity> GetAsync(object id)
        {
            return Context.Set<TEntity>().FindAsync(id);
        }

        public virtual IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(filter, orderBy, includes);
            return result.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(filter, orderBy, includes);
            return await result.ToListAsync();
        }

        public virtual void Load(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(filter, orderBy, includes);
            result.Load();
        }

        public virtual async Task LoadAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(filter, orderBy, includes);
            await result.LoadAsync();
        }

        public virtual IEnumerable<TEntity> QueryPage(int startRow, int pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(filter, orderBy, includes);
            return result.Skip(startRow).Take(pageLength).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> QueryPageAsync(int startRow, int pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(filter, orderBy, includes);
            return await result.Skip(startRow).Take(pageLength).ToListAsync();
        }

        public virtual void Add(TEntity entity)
        {
            if (entity == null) throw new InvalidOperationException("Unable to add a null entity to the repository.");
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void AddBatch(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new InvalidOperationException("Unable to add a null entities to the repository.");

            Context.Set<TEntity>().AddRange(entities);
        }

        public virtual TEntity Update(TEntity entity)
        {
            return Context.Set<TEntity>().Update(entity).Entity;
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Deleted;
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void Remove(object id)
        {
            var entity = Context.Set<TEntity>().Find(id);

            this.Remove(entity);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Any();
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.AnyAsync();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.CountAsync();
        }

        protected IQueryable<TEntity> QueryDb(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public void SetUnchanged(TEntity entity)
        {
            base.Context.Entry<TEntity>(entity).State = EntityState.Unchanged;
        }

        public List<TEntity> GetList(string strSql, bool sqlExe,  params object[] parameters)
        {
            return SqlQuery(Context, strSql, string.Empty, parameters).ToList();
        }

        public List<TEntity> GetList(string strSql, bool sqlExe, string propertyNameStr, params object[] parameters)
        {
            return SqlQuery(Context, strSql, propertyNameStr, parameters).ToList();
        }
        
        public TEntity Get(string strSql, bool sqlExe, params object[] parameters)
        {
            return SqlQuery(Context, strSql, string.Empty, parameters).FirstOrDefault();
        }

        public TEntity Get(string strSql, bool sqlExe, string propertyNameStr, params object[] parameters)
        {
            return SqlQuery(Context, strSql, propertyNameStr, parameters).FirstOrDefault();
        }

        private IList<TEntity> SqlQuery(DbContext db, string sql, string propertyNameStr, params object[] parameters)
        {
            //注意：不要对GetDbConnection获取到的conn进行using或者调用Dispose，否则DbContext后续不能再进行使用了，会抛异常 
            var conn = db.Database.GetDbConnection();
            try
            {
                conn.Open();

                string[] propertyNameList = null;
                if (!string.IsNullOrEmpty(propertyNameStr))
                {
                    propertyNameList = propertyNameStr.Split(',');
                }
                   
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Parameters.AddRange(parameters);
                   
                    var propts = typeof(TEntity).GetProperties();
                    var rtnList = new List<TEntity>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var model = new TEntity();
                            foreach (var l in propts)
                            {
                                if (l.CanWrite && !l.PropertyType.IsArray && !l.PropertyType.Namespace.Equals("System.Collections.Generic") && !l.PropertyType.Namespace.Equals(typeof(TEntity).Namespace))
                                {
                                    if (propertyNameList != null)
                                    {
                                        if (propertyNameList.Contains(l.Name))
                                        {
                                            continue;
                                        }
                                    }

                                    var val = reader[l.Name];

                                    l.SetValue(model, val == DBNull.Value ? null : val);
                                }
                            }
                            rtnList.Add(model);
                        }
                    }
                    return rtnList;
                }
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
