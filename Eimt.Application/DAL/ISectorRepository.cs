using Eimt.Application.Interfaces;
using Eimt.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eimt.Application.DAL
{
    public interface ISectorRepository:IRepository<Sector,long>
    {
        List<string> GetSectorNames();
    }
}
