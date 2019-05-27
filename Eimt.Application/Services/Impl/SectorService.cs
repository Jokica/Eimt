using Eimt.Application.Interfaces;
using System.Collections.Generic;

namespace Eimt.Application.Services.Impl
{
    public class SectorService : ISectorService
    {
        private readonly IUnitOfWork unitOfWork;

        public SectorService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<string> GetSectorNames()
        {
            return unitOfWork.SectorRepository.GetSectorNames();
        }
    }
}
