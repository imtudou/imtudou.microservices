using Imtudou.Core.Cache.StackExchangeRedis;

using StackExchange.Redis;

using System;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace Imtudou.Core.Cache.Test
{
    public class UnitTest_SortedSet
    {

        private readonly ITestOutputHelper testOutputHelper;
        private readonly IRedisCache redisHelper;

        public UnitTest_SortedSet(ITestOutputHelper testOutput)
        {
            this.testOutputHelper = testOutput;
            this.redisHelper = new RedisCache(new RedisOptions("127.0.0.1:6379,allowadmin=true,connectTimeout=1000,connectRetry=1,syncTimeout=10000", 0));
        }


        [Fact]
        public async Task SortedSetTestAsync()
        {
            const string key = "UnitTest_SortedSet_SortedSetTestAsync";

            Assert.True(await redisHelper.SortedSetAddAsync(key, "colin", 8));
            var score0 = await redisHelper.SortedSetIncrementAsync(key, "colin", 1);
            var score1 = await redisHelper.SortedSetDecrementAsync(key, "colin", 1);
            Assert.Equal(1, score0 - score1);

            Assert.True(await redisHelper.SortedSetAddAsync(key, "robin", 6));
            Assert.True(await redisHelper.SortedSetAddAsync(key, "tom", 7));
            Assert.True(await redisHelper.SortedSetAddAsync(key, "bob", 5));
            Assert.True(await redisHelper.SortedSetAddAsync(key, "elle", 5));
            Assert.True(await redisHelper.SortedSetAddAsync(key, "helen", 5));

            //返回排名前五，无论分数多少
            var top5 = await redisHelper.SortedSetRangeByRankWithScoresAsync(key, 0, 4, Order.Descending);
            foreach (var (k, v) in top5)
                testOutputHelper.WriteLine($"{k}\t{v}");

            testOutputHelper.WriteLine("---------------");

            //返回6-10分之间前五
            var highScore =
                await redisHelper.SortedSetRangeByScoreWithScoresAsync(key, 6, 10, order: Order.Descending, take: 5);
            foreach (var (k, v) in highScore)
                testOutputHelper.WriteLine($"{k}\t{v}");

            await redisHelper.KeyDeleteAsync(new[] { key });
        }

    }
}
