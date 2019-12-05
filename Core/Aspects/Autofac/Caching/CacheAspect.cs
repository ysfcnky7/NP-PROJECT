using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        public int _duration; //Kullanıcıdan alcaz // süresi 
        public ICacheManager _cacheManager; //Hangi cashe menagerı kullancaz 

        public CacheAspect(int duration = 60) //default 60 dakika 
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
            //cashe menagerım burdan gelıyor hangısını kullacagımızı burdan alıcaz 
        }
        //Override diyerek invocation yaptık 
        //ProductMenager.GetByCategory
        public override void Intercept(IInvocation invocation)
        {
            //Buradaki mantık şu olacak bak once cache de var mı varsa cache den getır  
            //yoksa ekle key degerı bızım ıcın burda metod bılgısı olacak 
            //Business.Abstract.IProductService.GetListByCategory metod name boyle bır sey gelıyor calısınca
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //s2v20
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            //Parametre var mı bak varsa parametreleri yaz yoksa Null olarak yaz 
            //her bir parametre için eğer parametre varsa onu stringe çevir aksi taktirde 
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key); // varsa cache de gonder gerı 
                return;
            }
            invocation.Proceed(); //Yoksa metodu çalıştır 
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
