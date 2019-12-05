using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, Context>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            //Bir kullanıcının rollerini çekmek istiyorum 
            //Kullanıcı ile ilgili bir işlem olduğu için burda topluyorum.
            //Burda bir joın yazmam  lazım 2 tablonun joın edilecek
            using (var context = new Context())
            {
                var result = from operationClaim in context.OperationClaims 
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name,
                             };
                return result.ToList();  //Burda Result Iquareable doner biz bunu liste çevirmiş olduk 
            }
        }
    }
}
