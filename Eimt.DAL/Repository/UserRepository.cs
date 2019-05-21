using Eimt.Application.Interfaces;
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
                    .FirstOrDefault(x => x.Email == email);
        }
    }
}
