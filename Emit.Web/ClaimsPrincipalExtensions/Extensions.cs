using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Emit.Web.ClaimsPrincipalExtensions
{
    public static class Extensions
    {
        public static bool IsAdmin(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.IsInRole("Admin");
        }
        public static bool IsMenager(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.IsInRole("Menager");
        }
        public static string GetSector(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "Sector")?.Value;
        }
        public static Tkey GetId<Tkey>(this ClaimsPrincipal claimsPrincipal) where Tkey : IComparable
        {
            var id = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new ArgumentNullException("key");
            return (Tkey)Convert.ChangeType(id, typeof(Tkey));
        }

    }
}
