using Core.DataAccess;
using Core.Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        //Ek olarak bir kullanıcının claimlerini çekmek için bu şekilde bir şey yazıyoruz.
        //Burası bizim için bir join operasyonu olacak
        //
        List<OperationClaim> GetClaims(User user);
    }
}
