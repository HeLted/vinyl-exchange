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
        public MemoryCacheManagerTests()
        {
            var services = new ServiceCollection();

            services.AddMemoryCache();

            var serviceProvider = services.BuildServiceProvider();

            var memoryCache = serviceProvider.GetService<IMemoryCache>();

            this.cacheManager = new MemoryCacheManager(memoryCache);
        }

        private readonly IMemoryCacheManager cacheManager;

        [Fact]
        public void ClearShouldDisposeCancelationToken()
        {
            var cancelationToken = this.cacheManager.Clear();

            Assert.True(cancelationToken.IsCancellationRequested);
        }

        [Fact]
        public void DisposeShouldDispose()
        {
            this.cacheManager.Dispose();

            Assert.True(this.cacheManager.IsDisposed);
        }

        [Fact]
        public void GetKeysShouldGetReturnEmptyListIfNoKeysArePresent()
        {
            Assert.True(this.cacheManager.GetKeys().Count == 0);
        }

        [Fact]
        public void GetShouldGetSettedDataInMemoryCache()
        {
            var key = "0032323141";
            var obj = new List<int> {1, 2, 3};

            this.cacheManager.Set(key, obj, 1800);

            var returnedFromCacheObj = this.cacheManager.Get<List<int>>(key, null);

            Assert.True(obj.Count == returnedFromCacheObj.Count);
            Assert.Equal(string.Join(",", obj), string.Join(",", returnedFromCacheObj));
        }

        [Fact]
        public void IsSetShouldReturnFalseIfKeyIsNotSet()
        {
            Assert.False(this.cacheManager.IsSet("testKey"));
        }

        [Fact]
        public void PerformActionShouldLockCacheKey()
        {
            var key = "0032323141";

            var locked = this.cacheManager.PerformActionWithLock(key, TimeSpan.FromSeconds(200), () => { });

            Assert.True(locked);
        }
    }
}