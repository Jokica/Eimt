using Eimt.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Eimt.DAL.Repository
{
    public class BaseRepository<TEntity, TKey> : IRepository<TEntity,TKey> where TEntity : class
    {
        private readonly DbContext context;

        public BaseRepository(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Add(TEntity user)
        {
            context.Add(user);
        }

        public TEntity Find(TKey Id)
        {
            return context.Set<TEntity>().Find(Id);
        }

        public TEntity Find(TKey Id, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = context.Set<TEntity>();
            foreach(var include in includes)
            {
                result.Include(include);
            }
            return result.Find(Id);
        }
        public TEntity FirstOrDefualt(Expression<Func<TEntity, bool>> Predicate)
        {
            return context.Set<TEntity>().FirstOrDefault(Predicate);
        }

        public void Remove(TEntity entity)
        {
             context.Set<TEntity>().Remove(entity);
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> Predicate)
        {
            return context.Set<TEntity>().Where(Predicate);
        }
    }
}
