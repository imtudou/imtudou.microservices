
using Microsoft.Extensions.Options;

using StackExchange.Redis;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

namespace Imtudou.Core.Cache.StackExchangeRedis
{

    public class RedisCache : IRedisCache
    {
        private readonly ConnectionMultiplexer conn;
        private readonly IDatabase db;

        public RedisCache(IOptionsMonitor<RedisOptions> options) : this(options.CurrentValue)
        {
        }

        public RedisCache(RedisOptions options)
        {
            var connectionString = options.ConnectionString;
            conn = ConnectionMultiplexer.Connect(connectionString);

            var dbNumber = options.DbNumber;
            db = conn.GetDatabase(dbNumber);
        }


        #region String

        public async Task<bool> StringSetAsync<T>(string key, T value) =>
            await db.StringSetAsync(key, value.ToRedisValue());

        public async Task<T> StringGetAsync<T>(string key) where T : class =>
            (await db.StringGetAsync(key)).ToObject<T>();

        public async Task<double> StringIncrementAsync(string key, int value = 1) =>
            await db.StringIncrementAsync(key, value);

        public async Task<double> StringDecrementAsync(string key, int value = 1) =>
            await db.StringDecrementAsync(key, value);

        #endregion

        #region List

        public async Task<long> EnqueueAsync<T>(string key, T value) =>
            await db.ListRightPushAsync(key, value.ToRedisValue());

        public async Task<T> DequeueAsync<T>(string key) where T : class =>
            (await db.ListLeftPopAsync(key)).ToObject<T>();

        public async Task<IEnumerable<T>> PeekRangeAsync<T>(string key, long start = 0, long stop = -1)
            where T : class =>
            (await db.ListRangeAsync(key, start, stop)).ToObjects<T>();

        #endregion

        #region Set

        public async Task<bool> SetAddAsync<T>(string key, T value) =>
            await db.SetAddAsync(key, value.ToRedisValue());

        public async Task<long> SetRemoveAsync<T>(string key, IEnumerable<T> values) =>
            await db.SetRemoveAsync(key, values.ToRedisValues());

        public async Task<IEnumerable<T>> SetMembersAsync<T>(string key) where T : class =>
            (await db.SetMembersAsync(key)).ToObjects<T>();

        public async Task<bool> SetContainsAsync<T>(string key, T value) =>
            await db.SetContainsAsync(key, value.ToRedisValue());

        #endregion

        #region Sortedset

        public async Task<bool> SortedSetAddAsync(string key, string member, double score) =>
            await db.SortedSetAddAsync(key, member, score);

        public async Task<long> SortedSetRemoveAsync(string key, IEnumerable<string> members) =>
            await db.SortedSetRemoveAsync(key, members.ToRedisValues());

        public async Task<double> SortedSetIncrementAsync(string key, string member, double value) =>
            await db.SortedSetIncrementAsync(key, member, value);

        public async Task<double> SortedSetDecrementAsync(string key, string member, double value) =>
            await db.SortedSetDecrementAsync(key, member, value);

        public async Task<ConcurrentDictionary<string, double>> SortedSetRangeByRankWithScoresAsync(string key,
            long start = 0,
            long stop = -1,
            Order order = Order.Ascending) =>
            (await db.SortedSetRangeByRankWithScoresAsync(key, start, stop, order)).ToConcurrentDictionary();

        public async Task<ConcurrentDictionary<string, double>> SortedSetRangeByScoreWithScoresAsync(string key,
            double start = double.NegativeInfinity, double stop = double.PositiveInfinity,
            Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1) =>
            (await db.SortedSetRangeByScoreWithScoresAsync(key, start, stop, exclude, order, skip, take))
            .ToConcurrentDictionary();

        #endregion

        #region Hash

        public async Task<ConcurrentDictionary<string, string>> HashGetAsync(string key) =>
            (await db.HashGetAllAsync(key)).ToConcurrentDictionary();

        public async Task<ConcurrentDictionary<string, string>> HashGetFieldsAsync(string key,
            IEnumerable<string> fields) =>
            (await db.HashGetAsync(key, fields.ToRedisValues())).ToConcurrentDictionary(fields);

        public async Task HashSetAsync(string key, ConcurrentDictionary<string, string> entries)
        {
            var val = entries.ToHashEntries();
            if (val != null)
                await db.HashSetAsync(key, val);
        }

        public async Task HashSetFieldsAsync(string key, ConcurrentDictionary<string, string> fields)
        {
            if (fields == null || !fields.Any())
                return;

            var hs = await HashGetAsync(key);
            foreach (var field in fields)
            {
                //if(!hs.ContainsKey(field.Key))

                //    continue;

                hs[field.Key] = field.Value;
            }

            await HashSetAsync(key, hs);
        }

        public async Task<bool> HashDeleteAsync(string key) =>
            await KeyDeleteAsync(new string[] { key }) > 0;

        public async Task<bool> HashDeleteFieldsAsync(string key, IEnumerable<string> fields)
        {
            if (fields == null || !fields.Any())
                return false;

            var success = true;
            foreach (var field in fields)
            {
                if (!await db.HashDeleteAsync(key, field))
                    success = false;
            }

            return success;
        }

        #endregion

        #region Key

        public IEnumerable<string> GetAllKeys() =>
            conn.GetEndPoints().Select(endPoint => conn.GetServer(endPoint))
                .SelectMany(server => server.Keys().ToStrings());

        public IEnumerable<string> GetAllKeys(EndPoint endPoint) =>
            conn.GetServer(endPoint).Keys().ToStrings();

        public async Task<bool> KeyExistsAsync(string key) =>
            await db.KeyExistsAsync(key);

        public async Task<long> KeyDeleteAsync(IEnumerable<string> keys) =>
            await db.KeyDeleteAsync(keys.Select(k => (RedisKey)k).ToArray());


        public async Task<bool> KeyExpireAsync(string key, TimeSpan? expiry) => await db.KeyExpireAsync(key, expiry);

        public async Task<bool> KeyExpireAsync(string key, DateTime? expiry) => await db.KeyExpireAsync(key, expiry);

        #endregion

        #region Advanced

        public async Task<long> PublishAsync(string channel, string msg) =>
            await conn.GetSubscriber().PublishAsync(channel, msg);

        public async Task SubscribeAsync(string channel, Action<string, string> handler) =>
            await conn.GetSubscriber().SubscribeAsync(channel, (chn, msg) => handler(chn, msg));

        public Task ExecuteBatchAsync(params Action[] operations) =>
            Task.Run(() =>
            {
                var batch = db.CreateBatch();

                foreach (var operation in operations)
                    operation();

                batch.Execute();
            });


        public async Task<(bool, object)> LockExecuteAsync(string key, string value, Delegate del,
            TimeSpan expiry, params object[] args)
        {
            if (!await db.LockTakeAsync(key, value, expiry))
                return (false, null);

            try
            {
                return (true, del.DynamicInvoke(args));
            }
            finally
            {
                db.LockRelease(key, value);
            }
        }


        public bool LockExecute(string key, string value, Delegate del, out object result, TimeSpan expiry,
            int timeout = 0, params object[] args)
        {
            result = null;
            if (!GetLock(key, value, expiry, timeout))
                return false;

            try
            {
                result = del.DynamicInvoke(args);
                return true;
            }
            finally
            {
                db.LockRelease(key, value);
            }
        }

        public bool LockExecute(string key, string value, Action action, TimeSpan expiry, int timeout = 0)
        {
            return LockExecute(key, value, action, out var _, expiry, timeout);
        }

        public bool LockExecute<T>(string key, string value, Action<T> action, T arg, TimeSpan expiry, int timeout = 0)
        {
            return LockExecute(key, value, action, out var _, expiry, timeout, arg);
        }

        public bool LockExecute<T>(string key, string value, Func<T> func, out T result, TimeSpan expiry,
            int timeout = 0)
        {
            result = default;
            if (!GetLock(key, value, expiry, timeout))
                return false;
            try
            {
                result = func();
                return true;
            }
            finally
            {
                db.LockRelease(key, value);
            }
        }

        public bool LockExecute<T, TResult>(string key, string value, Func<T, TResult> func, T arg, out TResult result,
            TimeSpan expiry, int timeout = 0)
        {
            result = default;
            if (!GetLock(key, value, expiry, timeout))
                return false;
            try
            {
                result = func(arg);
                return true;
            }
            finally
            {
                db.LockRelease(key, value);
            }
        }

        private bool GetLock(string key, string value, TimeSpan expiry, int timeout)
        {
            using (var waitHandle = new AutoResetEvent(false))
            {
                var timer = new System.Timers.Timer(1000);
                timer.Elapsed += (s, e) =>
                {
                    if (!db.LockTake(key, value, expiry))
                        return;
                    try
                    {
                        waitHandle.Set();
                        timer.Stop();
                    }
                    catch
                    {
                    }
                };
                timer.Start();


                if (timeout > 0)
                    waitHandle.WaitOne(timeout);
                else
                    waitHandle.WaitOne();

                timer.Stop();
                timer.Close();
                timer.Dispose();

                return db.LockQuery(key) == value;
            }
        }

        #endregion
    }
}
