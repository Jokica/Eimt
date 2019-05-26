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
        private List<Expression<Func<TEntity, object>>> includes;

        public BaseRepository(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            entities = context.Set<TEntity>();
            includes = new List<Expression<Func<TEntity, object>>>();
        }
        public void Add(TEntity user)
        {
            entities.Add(user);
        }

        public TEntity Find(TKey Id)
        {
            return entities.Find(Id);
        }
        public IRepository<TEntity,TKey> Include(Expression<Func<TEntity, object>> include)
        {
            includes.Add(include);
            return this;
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

        public IEnumerable<TEntity> GetAll()
        {
            return entities;
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
