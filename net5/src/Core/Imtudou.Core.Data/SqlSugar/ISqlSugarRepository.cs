using Imtudou.Core.Base;

using SqlSugar;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.Data.SqlSugar
{
    public interface ISqlSugarRepository<T, TKey> : IRepository<T, TKey> where T : class, IKey<TKey>, new()
    {
        SqlSugarClient DbContext { get; set; }

        /// <summary>
        ///  查找数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int GetCount();

        /// <summary>
        ///  查找数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int GetCount(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///  查找数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// 分页查找
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="page">分页对象</param>
        /// <returns></returns>
        List<T> GetPageList(Expression<Func<T, bool>> predicate, PageBaseModel page);

        /// <summary>
        /// 分页查找
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="page">分页对象</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>        
        List<T> GetPageList(Expression<Func<T, bool>> predicate, PageBaseModel page,ref int total);

        /// <summary>
        /// 分页查找
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="page">分页对象</param>
        /// <returns></returns>        
        Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> predicate, PageBaseModel page);

        /// <summary>
        /// 分页查找
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="page">分页对象</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>        
        Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> predicate, PageBaseModel page, RefAsync<int> total);


    }
}
