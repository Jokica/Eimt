using Eimt.Application.DAL;
using Eimt.Domain.DomainModels;
using Eimt.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eimt.DAL.Repository
{
    public class SectorRepository : BaseRepository<Sector, long>, ISectorRepository
    {
        private readonly EiMTDbContext context;

        public SectorRepository(EiMTDbContext context) : base(context)
        {
            this.context = context;
        }

        public List<string> GetSectorNames()
        {
          return context.Sectors.Select(x => x.Name).ToList();
        }
    }
}
