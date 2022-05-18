using Imtudou.Core.Cache.StackExchangeRedis;

using System;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace Imtudou.Core.Cache.Test
{
    public class UnitTest_List
    {

        private readonly ITestOutputHelper testOutputHelper;
        private readonly IRedisCache redisHelper;

        public UnitTest_List(ITestOutputHelper testOutput)
        {
            this.testOutputHelper = testOutput;
            this.redisHelper = new RedisCache(new RedisOptions("127.0.0.1:6379,allowadmin=true,connectTimeout=1000,connectRetry=1,syncTimeout=10000", 0));
        }


        [Fact]
        public async Task QqueueTestAsync()
        {
            this.testOutputHelper.WriteLine("EnqueueAsync:------------------");
            for (int i = 0; i < 10; i++)
            { 
                await this.redisHelper.EnqueueAsync<string>("UnitTest_List_EnqueueAsync", i.ToString());
                this.testOutputHelper.WriteLine(i.ToString());

                var output = await this.redisHelper.DequeueAsync<string>("UnitTest_List_EnqueueAsync");
                Assert.Equal(i.ToString(), output);
            }
        }


        [Fact]
        public async Task PeekRangeAsync()
        {
            this.testOutputHelper.WriteLine("PeekRangeAsync:------------------");
            for (int i = 0; i < 10; i++)
            {
                await this.redisHelper.EnqueueAsync<string>("UnitTest_List_PeekRangeAsync", i.ToString());
                this.testOutputHelper.WriteLine(i.ToString());

            }

            var output = await this.redisHelper.PeekRangeAsync<string>("UnitTest_List_PeekRangeAsync", 0, 10);
            Assert.NotNull(output);
            Assert.NotEmpty(output);
        }


    }
}
