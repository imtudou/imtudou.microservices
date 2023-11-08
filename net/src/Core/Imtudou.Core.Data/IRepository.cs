using Imtudou.Core.Base;
using Imtudou.Core.CommonEnum;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.Data
{
    /// <summary>
    /// 基础仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T, TKey> : IDisposable where T : class, IKey<TKey>, new()
    {
        /// <summary>
        /// 实例化
        /// </summary>
        IRepository<T, TKey> Instance();

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        IRepository<T, TKey> Instance(SqlOptions options);

        #region Query

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool Exists(params TKey[] param);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(params TKey[] param);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
          
        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T GetById(TKey param);

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(TKey param);

        /// <summary>
        /// 根据多个ID查找
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<T> GetByIds(params TKey[] param);

        /// <summary>
        /// 根据多个ID查找
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<List<T>> GetByIdsAsync(params TKey[] param);

        /// <summary>
        /// 根据多个ID查找
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<T> GetByIds(IEnumerable<object> param);

        /// <summary>
        /// 根据多个ID查找
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<List<T>> GetByIdsAsync(IEnumerable<object> param);

        /// <summary>
        /// 根据多个ID查找
        /// </summary>
        /// <param name="param">逗号分隔的标识列表，范例："1,2"</param>
        /// <returns></returns>
        List<T> GetByIds(string param);

        /// <summary>
        /// 根据多个ID查找
        /// </summary>
        /// <param name="param">逗号分隔的标识列表，范例："1,2"</param>
        /// <returns></returns>
        Task<List<T>> GetByIdsAsync(string param);

        /// <summary>
        /// 获取多个
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<T> GetList(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 获取多个
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        #endregion

        #region Insert

        /// <summary>
        /// 插入单条
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>返回自增列</returns>
        int Insert(T entity);

        /// <summary>
        /// 插入单条
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>返回自增列</returns>
        Task<int> InsertAsync(T entity);

        /// <summary>
        /// 插入多条
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>
        int Insert(List<T> entity);

        /// <summary>
        /// 插入多条
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>
        Task<int> InsertAsync(List<T> entity);

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>
        int InsertBulk(List<T> entity);

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>
        Task<int> InsertBulkAsync(List<T> entity);
            
        #endregion

        #region Update

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>返回执行成功</returns>
        bool Update(T entity);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>返回执行成功</returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(List<T> entity);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(List<T> entity);

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>
        bool UpdateBulk(List<T> entity);

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>
        Task<bool> UpdateBulkAsync(List<T> entity);
        #endregion

        #region Delete

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool DeleteById(TKey param);

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<bool> DeleteByIdAsync(TKey param);

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool DeleteByIds(params TKey[] param);

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<bool> DeleteByIdsAsync(params TKey[] param);

        /// <summary>
        /// 根据多个ID删除
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool DeleteByIds(IEnumerable<object> param);

        /// <summary>
        /// 根据多个ID删除
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<bool> DeleteByIdsAsync(IEnumerable<object> param);

        /// <summary>
        /// 根据多个ID删除
        /// </summary>
        /// <param name="param">逗号分隔的标识列表，范例："1,2"</param>
        /// <returns></returns>
        bool DeleteByIds(string param);

        /// <summary>
        /// 根据多个ID删除
        /// </summary>
        /// <param name="param">逗号分隔的标识列表，范例："1,2"</param>
        /// <returns></returns>
        Task<bool> DeleteByIdsAsync(string param);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>返回影响行数</returns>
        bool Delete(T entity);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>返回影响行数</returns>
        Task<bool> DeleteAsync(T entity);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>

        bool Delete(List<T> entity);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>

        Task<bool> DeleteAsync(List<T> entity);
        #endregion
    }
}
