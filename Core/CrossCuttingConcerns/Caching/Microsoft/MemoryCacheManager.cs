using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    //ICasheManager i implemente ediyoruz burda
    //implementasyon farklı tekniklerle olarabilir 
    //MemoryCashe vasıtasıyle olabilir yada redis kullanılabilir memory cashe implemente ettik
    //bu yuzden microsoft klasorune bunu attık

    public class MemoryCacheManager : ICacheManager
    {
        private IMemoryCache _memoryCache;
        public MemoryCacheManager()
        {
            //IMemoryCache ==> Microsoft.Extensions.Caching.Memory tipinde 
            //.net core ekibinin memory cashe implementasyonunu yapıyoruz 
            //services.AddMemoryCashe() kısmını burda yapıyoruz.
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }
        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }
        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }
        public void Add(string key, object data, int duration)
        {
            _memoryCache.Set(key, data, TimeSpan.FromMinutes(duration)); //Dakika
        }
        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _); //out _); ?????
        }
        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
        public void RemoveByPattern(string pattern)
        {
            //Butun cache içeriisnde benim gonderdıgım patterne uygun olan keylerı almak ıcın 
            //yazılmıs bır metod 
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();
            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled |
                RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString()))
                .Select(d => d.Key).ToList();
        }
    }
}
