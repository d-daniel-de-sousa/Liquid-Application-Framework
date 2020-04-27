using Liquid.Interfaces;
using Liquid.Runtime.Configuration.Base;
using Liquid.Runtime.Utils;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using System;
using System.Threading.Tasks;


namespace Liquid.OnAzure
{
    /// <summary>
    ///  Include support of AzureRedis, that processing data included on Configuration file.
    /// </summary>
    public class AzureRedis : ILightCache
    {
        private AzureRedisConfiguration config;
        private RedisCache _redisClient = null;
        private DistributedCacheEntryOptions _options = null;
        /// <summary>
        /// Initialize support of Cache and read file config
        /// </summary>
        public void Initialize()
        {
            config = LightConfigurator.Config<AzureRedisConfiguration>("AzureRedis");
            _redisClient = new RedisCache(new RedisCacheOptions()
            {
                Configuration = config.Configuration,
                InstanceName = config.InstanceName
            });

            _options = new DistributedCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromSeconds(config.SlidingExpirationSeconds),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(config.AbsoluteExpirationRelativeToNowSeconds)
            };
        }
        /// <summary>
        /// Get Key on the Azure Redis server cache
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="key">Key of object</param>
        /// <returns>object</returns>
        public T Get<T>(string key)
        {
            var data = _redisClient.Get(key);
            return CollectionTools.FromByteArray<T>(data);
        }
        /// <summary>
        /// Get Key Async on the Azure Redis server cache
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="key">Key of object</param>
        /// <returns>Task with object</returns>
        public async Task<T> GetAsync<T>(string key)
        {
            var data = await _redisClient.GetAsync(key);
            return CollectionTools.FromByteArray<T>(data);
        }
        /// <summary>
        /// Refresh key get on the Azure Redis server cache
        /// </summary>
        /// <param name="key">Key of object</param>
        public void Refresh(string key)
        {
            _redisClient.Refresh(key);
        }
        /// <summary>
        /// Refresh async key get on the Azure Redis server cache
        /// </summary>
        /// <param name="key">Key of object</param>
        /// <returns>Task</returns>
        public async Task RefreshAsync(string key)
        {
            await _redisClient.RefreshAsync(key);
        }
        /// <summary>
        ///  Remove key on the Azure Redis server cache
        /// </summary>
        /// <param name="key">Key of object</param>
        public void Remove(string key)
        {
            _redisClient.Remove(key);
        }
        /// <summary>
        ///  Remove async key on the Azure Redis server cache
        /// </summary>
        /// <param name="key">Key of object</param>
        /// <returns>Task</returns>
        public Task RemoveAsync(string key)
        {
            return _redisClient.RemoveAsync(key);
        }
        /// <summary>
        /// Set Key  and value on the Azure Redis server cache
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="key">Key of object</param>
        /// <returns>object</returns>
        public void Set<T>(string key, T value)
        {
            _redisClient.Set(key, CollectionTools.ToByteArray(value), _options);
        }
        /// <summary>
        /// Set Key and value Async on the Azure Redis server cache
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="key">Key of object</param>
        /// <returns>Task with object</returns>
        public async Task SetAsync<T>(string key, T value)
        {
            await _redisClient.SetAsync(key, CollectionTools.ToByteArray(value), _options);
        }

        

    }
}
