using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Eimt.Application.Services.Impl;
using Eimt.Domain;

namespace Eimt.Application.Interfaces
{
    public interface IRepository<TEntity,TKey> where TEntity : class
    {
        TEntity Find(TKey Id);
        TEntity Find(TKey Id,params Expression<Func<TEntity,object>>[] includes);
        TEntity FirstOrDefualt(Expression<Func<TEntity, bool>> Predicate);
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> Predicate);
        void Add(TEntity user);
        void Remove(TEntity entity);
    }
}