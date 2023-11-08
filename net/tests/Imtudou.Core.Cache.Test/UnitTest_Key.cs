
using Imtudou.Core.Cache.StackExchangeRedis;

using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace Imtudou.Core.Cache.Test
{
    public class UnitTest_Key
    {

        private readonly ITestOutputHelper testOutputHelper;
        private readonly IRedisCache redisHelper;

        public UnitTest_Key(ITestOutputHelper testOutput)
        {
            this.testOutputHelper = testOutput;
            this.redisHelper = new RedisCache(new RedisOptions("127.0.0.1:6379,allowadmin=true,connectTimeout=1000,connectRetry=1,syncTimeout=10000", 0));
        }



        [Fact]
        public async Task KeyExpiryTestAsync()
        {
            const string key = "expirytest";
            const string value = "haha";

            await redisHelper.StringSetAsync(key, value);
            await redisHelper.KeyExpireAsync(key, TimeSpan.FromSeconds(3));

            Assert.Equal(value, await redisHelper.StringGetAsync<string>(key));
            await Task.Delay(3000);
            Assert.Null(await redisHelper.StringGetAsync<string>(key));
        }

        [Fact]
        public void GetAllKeys()
        {
            var keys = redisHelper.GetAllKeys();
            foreach (var key in keys)
                testOutputHelper.WriteLine(key);
        }


    }
}
