using Eimt.Application.Interfaces;
using Eimt.Application.Services.ViewModels;
using Eimt.Domain.DomainModels;
using Eimt.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eimt.DAL.Repository
{
    public class UserRepository : BaseRepository<User, long>, IUserRepository
    {
        private readonly EiMTDbContext context;

        public UserRepository(EiMTDbContext context) : base(context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public User FindFull(long Id)
        {
            return context.Users
                 .Include(x => x.Roles)
                 .ThenInclude(x => x.Role)
                 .Include(x => x.Sector)
                 .FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<User> FindSectorUsersWithRoles(string sector)
        {
            return context.Users
                 .Include(x => x.Roles)
                 .ThenInclude(x=>x.Role)
                 .Include(x=>x.Sector)
                 .Where(x=>x.Sector.Name == sector);
        }

        public User FindUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(x => x.Email == email);

        }
        public User FindUserByEmailincludeToken(string email)
        {
            return context.Users
                .Include(x=>x.Token)
                .FirstOrDefault(x => x.Email == email);
        }

        public User FindUserByEmailWithRoles(string email)
        {
            return context.Users
                    .Include(x => x.Roles)
                    .ThenInclude(x=>x.Role)
                    .Include(x=>x.ManagesSector)
                    .FirstOrDefault(x => x.Email == email);
        }

        public User FindWithRoles(long id)
        {
            return context.Users
                    .Include(x => x.Roles)
                    .ThenInclude(x => x.Role)
                    .FirstOrDefault(x=>x.Id ==id);
        }

        IEnumerable<User> IUserRepository.FindAllUsersWithRoles()
        {
          return context.Users
                    .Include(x => x.Roles)
                    .ThenInclude(x=>x.Role)
                    .Include(x=>x.Sector);
        }
    }
}
