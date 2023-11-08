
using Imtudou.Core.Cache.StackExchangeRedis;

using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace Imtudou.Core.Cache.Test
{
    public class UnitTest_Hash
    {

        private readonly ITestOutputHelper testOutputHelper;
        private readonly IRedisCache redisHelper;

        public UnitTest_Hash(ITestOutputHelper testOutput)
        {
            this.testOutputHelper = testOutput;
            this.redisHelper = new RedisCache(new RedisOptions("127.0.0.1:6379,allowadmin=true,connectTimeout=1000,connectRetry=1,syncTimeout=10000", 0));
        }


        [Fact]
        public async Task HashTestAsync()
        {
            const string key = "UnitTest_Hash_HashTestAsync";
            await redisHelper.HashSetAsync(key, new ConcurrentDictionary<string, string>
            {
                ["name"] = "colin",
                ["age"] = "18"
            });

            Assert.True(await redisHelper.HashDeleteFieldsAsync(key, new[] { "gender", "name" }));

            await redisHelper.HashSetFieldsAsync(key, new ConcurrentDictionary<string, string>
            {
                ["age"] = "20"
            });

            var dict = await redisHelper.HashGetFieldsAsync(key, new[] { "age" });
            Assert.Equal("20", dict["age"]);

            await redisHelper.HashDeleteAsync(key);
        }


    }
}
