﻿namespace VinylExchange.Services.MemoryCache.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public interface IMemoryCacheManager
    {
        bool IsDisposed { get; }

        CancellationTokenSource Clear();

        void Dispose();

        IList<string> GetKeys();

        bool IsSet(string key);

        bool PerformActionWithLock(string key, TimeSpan expirationTime, Action action);

        void Remove(string key);

        void Set(string key, object data, int cacheTime);

        T Get<T>(string key, Func<T> acquire, int? cacheTime = null);
    }
}