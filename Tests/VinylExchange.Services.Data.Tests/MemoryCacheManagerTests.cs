namespace VinylExchange.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using MemoryCache;
    using MemoryCache.Contracts;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    [CollectionDefinition("Non-Parallel Collection", DisableParallelization = true)]
    public class MemoryCacheManagerTests
    {
        private readonly IMemoryCacheManager cacheManager;


        public MemoryCacheManagerTests()
        {
            var services = new ServiceCollection();

            services.AddMemoryCache();

            var serviceProvider = services.BuildServiceProvider();

            var memoryCache = serviceProvider.GetService<IMemoryCache>();

            cacheManager = new MemoryCacheManager(memoryCache);
        }

        [Fact]
        public void ClearShouldDisposeCancelationToken()
        {
            var cancelationToken = cacheManager.Clear();

            Assert.True(cancelationToken.IsCancellationRequested);
        }

        [Fact]
        public void DisposeShouldDispose()
        {
            cacheManager.Dispose();

            Assert.True(cacheManager.IsDisposed);
        }

        [Fact]
        public void GetShouldGetSettedDataInMemoryCache()
        {
            var key = "0032323141";
            var obj = new List<int> {1, 2, 3};

            cacheManager.Set(key, obj, 1800);

            var returnedFromCacheObj = cacheManager.Get<List<int>>(key, null);

            Assert.True(obj.Count == returnedFromCacheObj.Count);
            Assert.Equal(string.Join(",", obj), string.Join(",", returnedFromCacheObj));
        }

        [Fact]
        public void PerformActionShouldLockCacheKey()
        {
            var key = "0032323141";

            var locked = cacheManager.PerformActionWithLock(key, TimeSpan.FromSeconds(200), () => { });

            Assert.True(locked);
        }

        [Fact]
        public void IsSetShouldReturnFalseIfKeyIsNotSet()
        {
            Assert.False(cacheManager.IsSet("testKey"));
        }

        [Fact]
        public void GetKeysShouldGetReturnEmptyListIfNoKeysArePresent()
        {
            Assert.True(cacheManager.GetKeys().Count == 0);
        }
    }
}