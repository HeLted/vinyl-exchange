using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Services.MemoryCache;
using VinylExchange.Services.MemoryCache.Contracts;
using Xunit;


namespace VinylExchange.Services.Data.Tests
{

   
    [CollectionDefinition("Non-Parallel Collection", DisableParallelization = true)]
    public class MemoryCacheManagerTests
    {
        private IMemoryCacheManager cacheManager;

        
        public MemoryCacheManagerTests()
        {

            var services = new ServiceCollection();

            services.AddMemoryCache();

            var serviceProvider = services.BuildServiceProvider();

            var memoryCache = serviceProvider.GetService<IMemoryCache>();

            this.cacheManager = new MemoryCacheManager(memoryCache);
        }

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
        public void GetShouldGetSettedDataInMemoryCache()
        {
            var key = "0032323141";
            var obj = new List<int>(){1,2,3};

            this.cacheManager.Set(key, obj, 1800);

            var returnedFromCacheObj = this.cacheManager.Get<List<int>>(key, null);

            Assert.True(obj.Count == returnedFromCacheObj.Count);
            Assert.Equal(string.Join(",",obj),string.Join(",",returnedFromCacheObj));

        }

        [Fact]
        public void PerformActionShouldLockCacheKey()
        {
            var key = "0032323141";

            var locked = this.cacheManager.PerformActionWithLock(key,TimeSpan.FromSeconds(200),()=>{});

            Assert.True(locked);
        }

    }
}
