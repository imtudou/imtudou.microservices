using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.Cache
{
    // https://github.com/qq1206676756/RedisHelp/blob/master/RedisHelp/RedisHelper.cs
    public interface IRedisHelper
    {
        #region String
        /// <summary>
        /// StringSetAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> StringSetAsync<T>(string key, T value);

        /// <summary>
        ///  StringGetAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> StringGetAsync<T>(string key) where T : class;

        /// <summary>
        /// StringIncrementAsync
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<double> StringIncrementAsync(string key, int value = 1);

        /// <summary>
        /// StringDecrementAsync
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        Task<double> StringDecrementAsync(string key, int value = 1);
        #endregion

        #region List
        /// <summary>
        ///  入队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> EnqueueAsync<T>(string key, T value);

        /// <summary>
        ///  出对列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> DequeueAsync<T>(string key) where T : class;

        /// <summary>
        /// 从队列中读取数据而不出队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">起始位置</param>
        /// <param name="stop">结束位置</param>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>不指定 start、end 则获取所有数据</returns>
        Task<IEnumerable<T>> PeekRangeAsync<T>(string key, long start = 0, long stop = -1) where T : class;
        #endregion

        #region Set(无序集合)
        /// <summary>
        /// SetAddAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> SetAddAsync<T>(string key, T value);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        Task<long> SetRemoveAsync<T>(string key, IEnumerable<T> values);

        Task<IEnumerable<T>> SetMembersAsync<T>(string key) where T : class;

        Task<bool> SetContainsAsync<T>(string key, T value); 
        #endregion
    }
}
