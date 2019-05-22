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
        private readonly DbSet<TEntity> entities;

        public BaseRepository(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            entities = context.Set<TEntity>();
        }
        public void Add(TEntity user)
        {
            entities.Add(user);
        }

        public TEntity Find(TKey Id)
        {
            return entities.Find(Id);
        }

        public TEntity Find(TKey Id, params Expression<Func<TEntity, object>>[] includes)
        {
            foreach(var include in includes)
            {
                entities.Include(include);
            }
            return entities.Find(Id);
        }
        public TEntity FirstOrDefualt(Expression<Func<TEntity, bool>> Predicate)
        {
            return entities.FirstOrDefault(Predicate);
        }

        public void Remove(TEntity entity)
        {
            entities.Remove(entity);
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> Predicate)
        {
            return entities.Where(Predicate);
        }
    }
}
