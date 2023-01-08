using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace GC.MFI.Helpers
{
    public class CacheHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public string GetSessionData(string key)
        {
            return _session.GetString(key);
        }
        public static void SetCache(IMemoryCache memoryCache, string key, string value)
        {
            memoryCache.Set(key, value);
        }
        public static object GetCache(IMemoryCache memoryCache, string key)
        {
            return memoryCache.Get(key);
        }
        public static void SetCacheWithExpiration(IMemoryCache memoryCache, string key, string value)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(20));
            // Save data in cache.
            // memoryCache.Set(CacheKeys.Dynamics365Token, cacheEntry, cacheEntryOptions);
            memoryCache.Set(key, value, cacheEntryOptions);
        }
    }

    public class CacheKeys
    {
        public const string Dynamics365Token = "Dynamics365Token";
    }
}
