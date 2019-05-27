using System;
using System.Collections;
using System.Text;

namespace Eimt.Application.Services
{
    public interface IDocumentService
    {
         byte[] ToDocument(IList obj);
    }
}
