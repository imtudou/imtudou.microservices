using Imtudou.Core.Cache.StackExchangeRedis;

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace Imtudou.Core.Cache.Test
{
    public class UnitTest_Batch
    {

        private readonly ITestOutputHelper testOutputHelper;
        private readonly IRedisCache redisHelper;

        public UnitTest_Batch(ITestOutputHelper testOutput)
        {
            this.testOutputHelper = testOutput;
            this.redisHelper = new RedisCache(new RedisOptions("127.0.0.1:6379,allowadmin=true,connectTimeout=1000,connectRetry=1,syncTimeout=10000", 0));
        }



        [Fact]
        public async Task BatchExecuteAsync()
        {
            await redisHelper.ExecuteBatchAsync(
                async () => await redisHelper.StringSetAsync("name", "colin"),
                async () => await redisHelper.SetAddAsync("guys", "robin")
            );

            Assert.Equal("colin", await redisHelper.StringGetAsync<string>("name"));
            Assert.Equal("robin", (await redisHelper.SetMembersAsync<string>("guys")).FirstOrDefault());

            await redisHelper.KeyDeleteAsync(new[] { "name", "guys" });
        }


    }
}
