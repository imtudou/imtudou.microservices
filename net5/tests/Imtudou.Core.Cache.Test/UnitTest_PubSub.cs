using Imtudou.Core.Cache.StackExchangeRedis;

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace Imtudou.Core.Cache.Test
{
    public class UnitTest_PubSub
    {

        private readonly ITestOutputHelper testOutputHelper;
        private readonly IRedisCache redisHelper;

        public UnitTest_PubSub(ITestOutputHelper testOutput)
        {
            this.testOutputHelper = testOutput;
            this.redisHelper = new RedisCache(new RedisOptions("127.0.0.1:6379,allowadmin=true,connectTimeout=1000,connectRetry=1,syncTimeout=10000", 0));
        }


        [Fact]
        public async Task PubSubTestAsync()
        {
            const string channel = "message";
            const string message = "hi there";

            await redisHelper.SubscribeAsync(channel, (chn, msg) =>
            {
                Assert.Equal(channel, chn);
                Assert.Equal(message, msg);
            });

            await redisHelper.PublishAsync(channel, message);
        }

        [Fact]
        public async Task LockExecuteTestAsync()
        {
            const string key = "lockTest";

            var func = new Func<int, int, Task<int>>(async (a, b) =>
            {
                // _testOutputHelper.WriteLine(
                //     $"thread-{Thread.CurrentThread.ManagedThreadId.ToString()} get the lock.");
                await Task.Delay(1000);
                return await Task.FromResult(a + b);
            });

            var rdm = new Random();
            for (var i = 0; i < 3; i++)
            {
                new Thread(async () =>
                {
                    var success = redisHelper.LockExecute(key, Guid.NewGuid().ToString(), func,
                        out var result,
                        TimeSpan.MaxValue,
                        3000, rdm.Next(0, 10), 0);

                    if (success)
                    {
                        var res = await (result as Task<int>);
                        testOutputHelper.WriteLine(
                            $"result is {res}.\t{DateTime.Now.ToLongTimeString()}");
                    }
                    else
                        testOutputHelper.WriteLine(
                            $"failed to get lock.\t{DateTime.Now.ToLongTimeString()}");
                })
                { IsBackground = true }
                    .Start();
                await Task.Delay(500);
            }

            await Task.Delay(5000);
            testOutputHelper.WriteLine("all done");
        }


    }
}
