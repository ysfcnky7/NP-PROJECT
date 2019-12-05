using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Autofac.Aspects.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Busines;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Business.Concrete
{
    //Sık sorulan sorulardan bir tanesi ==>
    //başka bir servisle çalışma durumunda nasıl bir yol izlicez         private IProductDal _productDal;
    //var mesela category ded bir control yapcaz  private ICategoryDal _categoryDal; mı yazcaz başlarına 
    //ürün eklerken category  ile bir sorgu yapcaz bir kural koycaz mesela o zaman nerden çekecez
    //ICategoryDal ı kullanmıyoruz çunku aynı category dala ıhtıyac duyulunca yıne yazmamız gerekıyor ?
    //bu yuzden bır servıs halıne getırıp oyle yazmamız gerekıyor yanı  
    //private ICategoryService _categoryService; bu şekilde yanı 

    public class ProductMenager : IProductService
    {
        private IProductDal _productDal;
        private ICategoryService _categoryService;
        public ProductMenager(IProductDal productDal, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _productDal = productDal;
        }
        public IDataResult<Product> GetById(int productId)
        {
            //Burada dönüş tipini IDataResult olarak ayarladığımız için ilk olarak hata verecektir
            //bu yuzden donus tipini new deyip SuccessDataResult tipinde seçtik buda şu anlama geliyor
            //Eğer data dönüyorsa bu zaten başarılı bir işlemdir demek anlamına geliyor.
            //Burda biz _productDal.Get(p => p.ProductId == productId) bununla datayı doldurduk 
            //eğer istese idik parametre olarak message ve success i de doldurabilirdik
            //tabi success otomatik true olarak geleceği için herhangi bir işleme gerek kalmıyor.
            //sadece diğer mesajı donduruyor.
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }
        [PerformanceAspect(5)] // 5 sanıyeden fazla olursa output da yazcak 
        public IDataResult<List<Product>> GetList()
        {
            Thread.Sleep(6000);
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        //[SecuredOperation("Product.List,Admin")] //admın veya list yetkısı olan 
        //[LogAspect(typeof(DatabaseLogger))]   //Veritabanına log kaydeder
        [LogAspect(typeof(FileLogger))]         //Dosyaya log kaydeder c:/log/log.json dosyasına 
        //MicroKnight.Log4net yuklendı
        //[CacheAspect(duration: 10)]
        //10 dakika boyunca cache den gelıcek 
        //[ExceptionLogAspect(typeof(FileLogger))] //Burası bizim backendimiz olduğu için ve burda yapılan 
        //tüm işlemlerde bu try catch kontrollerını hata kontrollerını yapmamız lazım fakat 
        //tek tek her şeyin basına yazmamız asla  doğru değil tek bir yerden yonetebiliyoruz olması lazım 
        //artı hepsıne yazmamız gerektiğinin de sebebı tahmın edemeycegımız bılmeyecegımız hataların 
        //oluşabilecek olması  bu yuzden boyle bır yapı kullanıyoruz. ama burda kullanmıcaz 
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }
        //ValidationAspect adında bir Aspect yazıcaz 
        //bu aspect şu işe yarıcak bu metod çağrıldığında product vlidatorun alacağı
        //parametre ne ıse o parametreyı gonderecek ve validasyon kontrolu yapacak
        //Autofac kullancaz autofac bir ioc containerdır 
        //ioc containerlerin hepsının bir interception özelliği var  araya girme özelliği yanı 
        //core utilities altında 
        //core utilities altında 
        [ValidationAspect(typeof(ProductValidator), Priority = 1)]
        [CacheRemoveAspect(pattern: "IProductService.Get")]
        //içerisinde IProductService.Get OLAN CACHE keylerını sılecek
        //[CacheRemoveAspect(pattern: "ICategoryService.Get")] 
        //2 tane alt alta yada daha fazla kullanılabılrı 
        public IResult Add(Product product)
        {
            //İşlemleri burada ozel yapıyorsun
            //Mesela eklenen bir ürün bir daha eklenmesin burada yapıyoruz. 
            if (_productDal.Get(p => p.ProductName == product.ProductName) != null)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName));
            //Buraya virgul deyıp istediğimiz kadar logic koyabiliyoruz
            //bu boyle ise şu şöyle ise boşsa falan filan 
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetList(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryIsEnabled()
        {
            var result = _categoryService.GetList();
            if (result.Data.Count < 10)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();
        }


        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        [TransactionScopeAspect] // bu şekilde çağırdık //controllerdan burayı cagıralım
        public IResult TransactionalOperation(Product product)
        {
            _productDal.Update(product);
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
