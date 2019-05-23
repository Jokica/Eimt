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
        
    }
}
