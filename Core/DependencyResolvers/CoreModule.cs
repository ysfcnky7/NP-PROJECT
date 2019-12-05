using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        //Merkezi dependency configurasyonları buraya uygulayacak 
        //Start up da services.add dediğim merkezı olayları arttık buraya eklıcem 
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            //mesela redise geçtik MemoryCacheMenager burası boyle degısmelı
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            ////services.AddSingleton<ICacheManager, RedisCacheManager>();
            services.AddSingleton<Stopwatch>();
        }
    }
}
