using Eimt.Application.DAL;
using Eimt.Application.Interfaces;
using Eimt.DAL.Repository;
using Eimt.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eimt.DAL.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private EiMTDbContext _context;
        public IUserRepository UserRepository { get ;  private set; }

        public IRoleRepository RoleRepository { get; private set; }

        public ISectorRepository SectorRepository { get; private set; }

        public UnitOfWork(EiMTDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            UserRepository = new UserRepository(context);
            RoleRepository = new RoleRepository(context);
            SectorRepository = new SectorRepository(context);
        }
        public int Commit()
        {
          return _context.SaveChanges();
        }

        public IDbContextTransaction CreateTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<TEntity, TKey> CreateRepository<TEntity, TKey>() where TEntity : class
        {
            return new BaseRepository<TEntity, TKey>(_context);
        }
    }
}
