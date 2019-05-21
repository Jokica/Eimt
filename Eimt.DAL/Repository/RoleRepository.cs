using Eimt.Application.Interfaces;
using Eimt.Domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eimt.DAL.Repository
{
    public class RoleRepository : BaseRepository<Role, long>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }
    }
}
