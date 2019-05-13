using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Caching
{
    /// <summary>
    /// Class extending DistributedCache
    /// </summary>
    public static class CachingExtension
    {
        /// <summary>
        /// Extension method to get cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key)
        {
            var cachedItem = await cache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(cachedItem))
            {
                return JsonConvert.DeserializeObject<T>(cachedItem);
            }
            return default(T);
        }

        /// <summary>
        /// Extension method to set cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task PutAsync<T>(this IDistributedCache cache, string key, T value, int expirationTime)
        {
            var options = new DistributedCacheEntryOptions();
            options.SetSlidingExpiration(TimeSpan.FromMinutes(expirationTime));
            await cache.SetStringAsync(key, JsonConvert.SerializeObject(value), options);
        }

        /// <summary>
        /// Extension method to remove cache
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task RemoveAsync(this IDistributedCache cache, string key)
        {
            await cache.RemoveAsync(key);
        }

        /// <summary>
        /// Sync method to get the cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this IDistributedCache cache, string key)
        {
            var cachedItem = cache.GetString(key);
            if (!string.IsNullOrEmpty(key))
            {
                return JsonConvert.DeserializeObject<T>(cachedItem);
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Sync method to set the cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Put<T>(this IDistributedCache cache, string key, T value, int expirationTime)
        {
            var options = new DistributedCacheEntryOptions();
            options.SetSlidingExpiration(TimeSpan.FromMinutes(10));
            cache.SetString(key, JsonConvert.SerializeObject(value), options);
        }

        /// <summary>
        /// Sync method to remove the cache
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        public static void Remove(this IDistributedCache cache, string key)
        {
             cache.Remove(key);
        }
    }
}

