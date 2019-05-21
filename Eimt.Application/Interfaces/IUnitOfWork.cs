using Eimt.Domain;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eimt.Application.Interfaces
{
    public interface IUnitOfWork
    {
         IUserRepository UserRepository { get;  }
         IRoleRepository RoleRepository { get; }
        IDbContextTransaction CreateTransaction();
        IRepository<TEntity,TKey> CreateRepository<TEntity,TKey >() where TEntity:class;
        int Commit();
    }
}
