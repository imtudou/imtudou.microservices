using Imtudou.Core.Base;
using Imtudou.Core.CommonEnum;
using Imtudou.Core.Helpers;

using Microsoft.Extensions.Configuration;

using SqlSugar;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.Data.SqlSugar
{
    /// <summary>
    /// SqlSugar 仓储实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SqlSugarRepository<T, TKey> : ISqlSugarRepository<T, TKey> where T : class, IKey<TKey>, new()
    {
        private string connectionString;
        private DataBaseTypeEnum dataBaseType;
        public SqlSugarClient DbContext;

        public SqlSugarRepository()
        {
            this.connectionString = AppSettingsHelper.Configuration.GetConnectionString(nameof(DataBaseTypeEnum.SqlServer));
            this.dataBaseType = DataBaseTypeEnum.SqlServer;
            this.DbContext = GetSqlSugarClient();
        }

        public SqlSugarRepository(string connectionString, DataBaseTypeEnum dataBase)
        {
            this.connectionString = connectionString;
            this.dataBaseType = dataBase;
            this.DbContext = GetSqlSugarClient();
        }

        #region Query

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool Exists(params TKey[] param)
        {
            return this.DbContext.Queryable<T>().Any(s => param.Contains(s.Id));
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return this.DbContext.Queryable<T>().Any(predicate);
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(params TKey[] param)
        {
            return await this.DbContext.Queryable<T>().AnyAsync(s => param.Contains(s.Id));
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.DbContext.Queryable<T>().AnyAsync(predicate);
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return this.DbContext.Queryable<T>().First(predicate);
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.DbContext.Queryable<T>().FirstAsync(predicate);
        }

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T GetById(Expression<Func<T, bool>> predicate)
        {
            return this.DbContext.Queryable<T>().Single(predicate);
        }

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.DbContext.Queryable<T>().SingleAsync(predicate);
        }

        /// <summary>
        /// 根据多个ID查找
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<T> GetByIds(params TKey[] param)
        {
            return this.DbContext.Queryable<T>().Where(s => param.Contains(s.Id)).ToList();
        }

        /// <summary>
        /// 根据多个ID查找
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<T> GetByIds(IEnumerable<object> param)
        {
            return this.DbContext.Queryable<T>().Where(s => param.Contains(s.Id)).ToList();
        }

        /// <summary>
        /// 根据多个ID查找
        /// </summary>
        /// <param name="param">逗号分隔的标识列表，范例："1,2"</param>
        /// <returns></returns>
        public List<T> GetByIds(string param)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                throw new ArgumentNullException("param 参数不能为空");
            }
            var ids = param.Split(",").ToArray();
            return this.DbContext.Queryable<T>().Where(s => ids.Contains(s.Id.ToString())).ToList();
        }

        /// <summary>
        /// 根据多个ID查找
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<T>> GetByIdsAsync(params TKey[] param)
        {
            return await this.DbContext.Queryable<T>().Where(s => param.Contains(s.Id)).ToListAsync();
        }

        /// <summary>
        /// 根据多个ID查找
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<T>> GetByIdsAsync(IEnumerable<object> param)
        {
            return await this.DbContext.Queryable<T>().Where(s => param.Contains(s.Id)).ToListAsync();
        }

        /// <summary>
        /// 根据多个ID查找
        /// </summary>
        /// <param name="param">逗号分隔的标识列表，范例："1,2"</param>
        /// <returns></returns>
        public async Task<List<T>> GetByIdsAsync(string param)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                throw new ArgumentNullException("param 参数不能为空");
            }
            var ids = param.Split(",").ToArray();
            return await this.DbContext.Queryable<T>().Where(s => ids.Contains(s.Id.ToString())).ToListAsync();
        }

        /// <summary>
        /// 获取多个
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<T> GetList(Expression<Func<T, bool>> predicate)
        {
            return this.DbContext.Queryable<T>().Where(predicate).ToList();
        }

        /// <summary>
        /// 获取多个
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.DbContext.Queryable<T>().Where(predicate).ToListAsync();
        }

        /// <summary>
        ///  查找数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int GetCount()
        {
            return this.DbContext.Queryable<T>().Count();
        }

        /// <summary>
        ///  查找数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int GetCount(Expression<Func<T, bool>> predicate)
        {
            return this.DbContext.Queryable<T>().Count(predicate);
        }

        /// <summary>
        ///  查找数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<int> GetCountAsync()
        {
            return await this.DbContext.Queryable<T>().CountAsync();
        }

        /// <summary>
        /// 查找数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<int> GetCountAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.DbContext.Queryable<T>().CountAsync(predicate);
        }

        /// <summary>
        /// 分页查找
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="page">分页对象</param>
        /// <returns></returns>
        public List<T> GetPageList(Expression<Func<T, bool>> predicate, PageBaseModel page)
        {
            return this.DbContext.Queryable<T>().Where(predicate).ToPageList(page.PageIndex, page.PageSize);
        }

        /// <summary>
        /// 分页查找
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="page">分页对象</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>        
        public List<T> GetPageList(Expression<Func<T, bool>> predicate, PageBaseModel page, ref int total)
        {
            return this.DbContext.Queryable<T>().Where(predicate).ToPageList(page.PageIndex, page.PageSize, ref total);
        }

        /// <summary>
        /// 分页查找
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="page">分页对象</param>
        /// <returns></returns>        
        public async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> predicate, PageBaseModel page)
        {
            return await this.DbContext.Queryable<T>().Where(predicate).ToPageListAsync(page.PageIndex, page.PageSize);
        }

        /// <summary>
        /// 分页查找
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="page">分页对象</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>        
        public async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> predicate, PageBaseModel page, RefAsync<int> total)
        {
            return await this.DbContext.Queryable<T>().Where(predicate).ToPageListAsync(page.PageIndex, page.PageSize, total);
        }
        #endregion

        #region Insert

        /// <summary>
        /// 插入单条
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>返回自增列</returns>
        public int Insert(T entity)
        {
            return this.DbContext.Insertable(entity).ExecuteReturnIdentity();
        }

        /// <summary>
        /// 插入单条
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>返回自增列</returns>
        public async Task<int> InsertAsync(T entity)
        {
            return await this.DbContext.Insertable(entity).ExecuteReturnIdentityAsync();
        }

        /// <summary>
        /// 插入多条
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>
        public int Insert(List<T> entity)
        {
            return this.DbContext.Insertable(entity).ExecuteCommand();
        }

        /// <summary>
        /// 插入多条
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>
        public async Task<int> InsertAsync(List<T> entity)
        {
            return await this.DbContext.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>
        public int InsertBulk(List<T> entity)
        {
            return this.DbContext.Fastest<T>().BulkCopy(entity);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>
        public async Task<int> InsertBulkAsync(List<T> entity)
        {
            return await this.DbContext.Fastest<T>().BulkCopyAsync(entity);
        }

        #endregion

        #region Update

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>返回执行成功</returns>
        public bool Update(T entity)
        {
            return this.DbContext.Updateable(entity).ExecuteCommandHasChange();
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>返回执行成功</returns>
        public async Task<bool> UpdateAsync(T entity)
        {
            return await this.DbContext.Updateable(entity).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(List<T> entity)
        {
            return this.DbContext.Updateable(entity).ExecuteCommandHasChange();
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(List<T> entity)
        {
            return await this.DbContext.Updateable(entity).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>
        public bool UpdateBulk(List<T> entity)
        {
            return this.DbContext.Fastest<T>().BulkUpdate(entity) > 0;
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>
        public async Task<bool> UpdateBulkAsync(List<T> entity)
        {
            var result = await this.DbContext.Fastest<T>().BulkUpdateAsync(entity);
            return result > 0;
        }
        #endregion

        #region Delete

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>返回影响行数</returns>
        public bool Delete(T entity)
        {
           return this.DbContext.Deleteable(entity).ExecuteCommandHasChange();
        }

        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>返回影响行数</returns>
        public async Task<bool> DeleteAsync(T entity)
        {
            return await this.DbContext.Deleteable(entity).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 删除集合数据
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>

        public bool Delete(List<T> entity)
        {
            return this.DbContext.Deleteable(entity).ExecuteCommandHasChange();
        }

        /// <summary>
        /// 异步删除集合数据
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>

        public async Task<bool> DeleteAsync(List<T> entity)
        {
            return await this.DbContext.Deleteable(entity).ExecuteCommandHasChangeAsync();
        }
        #endregion

        public void Dispose()
        {
            this.DbContext.Dispose();
        }

        #region Private Method
        /// <summary>
        /// 获取 SqlSugarClient
        /// </summary>
        /// <returns></returns>
        private SqlSugarClient GetSqlSugarClient()
        {
            var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = this.connectionString,
                DbType = GetDbType(this.dataBaseType),
                IsAutoCloseConnection = true
            });

            // 验证连接是否成功
            if (!db.Ado.IsValidConnection())
            {
                throw new SqlSugarException("SqlSugarClient 连接失败，请检查连接字符串或者数据库类型是否正确");
            }

            return db;
        }

        /// <summary>
        /// 数据库类型转换
        /// </summary>
        /// <param name="dataBaseType"></param>
        /// <returns></returns>
        private DbType GetDbType(DataBaseTypeEnum dataBaseType)
        {
            switch (dataBaseType)
            {
                case DataBaseTypeEnum.MySql:
                    return DbType.MySql;
                case DataBaseTypeEnum.SqlServer:
                    return DbType.SqlServer;
                case DataBaseTypeEnum.Sqlite:
                    return DbType.Sqlite;
                case DataBaseTypeEnum.Oracle:
                    return DbType.Oracle;
                case DataBaseTypeEnum.PostgreSQL:
                    return DbType.PostgreSQL;
                default:
                    break;
            }
            return default;
        }
        #endregion
    }
}
