using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        //Mevcut kullanıcımıza karşılık geliyor burası  Principali gorunce bu demek mevcut kullanıcı 
        //Bu extension ile kullanıcı rollerını çekicez tumden 
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimsType)
        {
            //claimsPrincipal? ile claimsPrincipal var mı sorusu soruyoruz.
            //olmayabilir ? bu işlemi var mı devam et varsa gibi 
            //claimsType ların key value pairlerini almak için 
            var result = claimsPrincipal?.FindAll(claimsType)?.Select(x => x.Value).ToList();
            return result;
        }
        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            //claim rolleri 
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}
