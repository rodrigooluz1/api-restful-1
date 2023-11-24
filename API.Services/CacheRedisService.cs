﻿using System;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace API.Services
{
    public class CacheRedisService : ICacheService
	{
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;

        public CacheRedisService(IDistributedCache cache)
		{
            _cache = cache;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120) //cache irá expirar em 2 minutos
            };
		}

        public T Get<T>(string key)
        {
            var cache = _cache.Get(key);

            if (cache is null)
                return default;

            var result = JsonSerializer.Deserialize<T>(cache);

            return result;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Set<T>(string key, T content)
        {
            var contentAsString = JsonSerializer.Serialize(content);
            _cache.SetString(key, contentAsString, _options);
        }
    }
}

