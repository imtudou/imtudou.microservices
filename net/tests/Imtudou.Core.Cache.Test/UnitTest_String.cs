

using Imtudou.Core.Cache.StackExchangeRedis;

using System;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace Imtudou.Core.Cache.Test
{
    public class UnitTest_String
    {

        private readonly ITestOutputHelper testOutputHelper;
        private readonly IRedisCache redisHelper;

        public UnitTest_String(ITestOutputHelper testOutput)
        {
            this.testOutputHelper = testOutput;
            this.redisHelper = new RedisCache(new RedisOptions("127.0.0.1:6379,allowadmin=true,connectTimeout=1000,connectRetry=1,syncTimeout=10000", 0));
        }


        [Fact]
        public async Task StringSetAsync()
        {
            this.testOutputHelper.WriteLine("Arrage");
            this.testOutputHelper.WriteLine("act");
            this.testOutputHelper.WriteLine("Assert");

            await this.redisHelper.StringSetAsync("UnitTest_String_CurrentUserKey", "11111");
            var output = await this.redisHelper.StringGetAsync<string>("UnitTest_String_CurrentUserKey");
            
            Assert.NotNull(output);
            Assert.Equal("11111", output);
        }

        [Fact]
        public async Task StringGetAsync()
        {
            var output = await this.redisHelper.StringGetAsync<string>("UnitTest_String_CurrentUserKey");
            Assert.NotNull(output);
            Assert.Equal("11111", output);
        }

        [Fact]
        public async Task StringIncrementAsync()
        {
            var output = await this.redisHelper.StringIncrementAsync("UnitTest_String_StringIncrementAsync", 1);
            Assert.True(output > 0);
        }

        [Fact]
        public async Task StringDecrementAsync()
        {
            var output = await this.redisHelper.StringIncrementAsync("UnitTest_String_StringIncrementAsync", 1);
            Assert.True(output > 0);
        }

    }
}
