using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;


namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        public string _pattern;
        public ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
            //Bunu yenı urun eklendıgınde guncellendıgınde falan calıstıracagız 
            //Add oldu ama success olmadı transaction mesela calısırsa sıkıntı bosu bosuna sılmıs oluruz 
            //sonucta gerı alıyor.
        }
    }
}
