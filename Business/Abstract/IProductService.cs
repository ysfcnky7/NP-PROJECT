using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<Product> GetById(int productId);
        IDataResult<List<Product>> GetList();
        IDataResult<List<Product>> GetListByCategory(int categoryId);
        //Burda normalde IResult Yerine void kullanıyorduk
        //alttaki düzenleme ile IResult donduruyoruz.
        //Bu şu anlama geliyor.Herhangi bir şey donmicek data donmicek anlamına gelıyor.
        IResult Add(Product product);
        IResult Delete(Product product);
        IResult Update(Product product);
        IResult TransactionalOperation(Product product);

    }
}
