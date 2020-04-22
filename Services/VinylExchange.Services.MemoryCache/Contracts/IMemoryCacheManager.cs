using System;
using System.Collections.Generic;

namespace VinylExchange.Services.MemoryCache.Contracts
{
    public interface  IMemoryCacheManager
    {
        void Clear();

        void Dispose();

        IList<string> GetKeys();

        bool IsSet(string key);

        bool PerformActionWithLock(string key, TimeSpan expirationTime, Action action);

        void Remove(string key);

        void Set(string key, object data, int cacheTime);

        T Get<T>(string key, Func<T> acquire, int? cacheTime = null);
    }
}
