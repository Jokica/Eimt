using System;
using System.Collections.Generic;
using System.Text;

namespace Eimt.Application.Services
{
    public interface ISectorService
    {
        IEnumerable<string> GetSectorNames();
    }
}
