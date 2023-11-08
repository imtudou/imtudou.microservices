using Imtudou.Core.Base;
using Imtudou.Core.CommonEnum;
using Imtudou.Core.Helpers;
using Imtudou.Core.Logs;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using NLog;
using NLog.Web;

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
        private string _connectionString;
        private DataBaseTypeEnum _dataBaseType;

        public SqlSugarClient DbContext { get;  set; }
        public SqlSugarRepository()
        {
            if (LogManager.LogFactory.Configuration != null)
            {
                NLogBuilder.ConfigureNLog("Configs\\NLog.config");
            }
        }
        public SqlSugarRepository(SqlOptions options)
        {
            _connectionString = options.ConnectionString;
            _dataBaseType = options.DataBaseType;
            this.DbContext = this.GetSqlSugarClient();// 验证客户端
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
        public T GetById(TKey param)
        {
            return this.DbContext.Queryable<T>().Single(s => s.Id.Equals(param));
        }

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(TKey param)
        {
            return await this.DbContext.Queryable<T>().SingleAsync(s => s.Id.Equals(param));
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
            var cc = this.DbContext.Queryable<T>().Where(predicate)
                .OrderByIF(string.IsNullOrEmpty(page.OrderFileds), $"{page.OrderFileds}{page.OrderByType}").ToSql();

            return this.DbContext.Queryable<T>().Where(predicate)
                .OrderByIF(string.IsNullOrEmpty(page.OrderFileds), $"{page.OrderFileds}{page.OrderByType}")
                .ToPageList(page.PageIndex, page.PageSize);
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
            return this.DbContext.Queryable<T>().Where(predicate).OrderByIF(!string.IsNullOrEmpty(page.OrderFileds), $"{page.OrderFileds} {page.OrderByType}").ToPageList(page.PageIndex, page.PageSize, ref total);
        }

        /// <summary>
        /// 分页查找
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="page">分页对象</param>
        /// <returns></returns>        
        public async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> predicate, PageBaseModel page)
        {
            return await this.DbContext.Queryable<T>().Where(predicate).OrderByIF(!string.IsNullOrEmpty(page.OrderFileds), $"{page.OrderFileds} {page.OrderByType}").ToPageListAsync(page.PageIndex, page.PageSize);
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
            return await this.DbContext.Queryable<T>().Where(predicate).OrderByIF(!string.IsNullOrEmpty(page.OrderFileds), $"{page.OrderFileds} {page.OrderByType}").ToPageListAsync(page.PageIndex, page.PageSize, total);
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
        /// 根据ID删除
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool DeleteById(TKey param)
        {
            return this.DbContext.Deleteable<T>().In(param).ExecuteCommandHasChange();
        }

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync(TKey param)
        {
            return await this.DbContext.Deleteable<T>().In(param).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool DeleteByIds(params TKey[] param)
        {
            return this.DbContext.Deleteable<T>().In(param).ExecuteCommandHasChange();
        }

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdsAsync(params TKey[] param)
        {
            return await this.DbContext.Deleteable<T>().In(param).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 根据多个ID删除
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool DeleteByIds(IEnumerable<object> param)
        {
            return this.DbContext.Deleteable<T>().In(param).ExecuteCommandHasChange();
        }

        /// <summary>
        /// 根据多个ID删除
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdsAsync(IEnumerable<object> param)
        {
            return await this.DbContext.Deleteable<T>().In(param).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 根据多个ID删除
        /// </summary>
        /// <param name="param">逗号分隔的标识列表，范例："1,2"</param>
        /// <returns></returns>
        public bool DeleteByIds(string param)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                throw new ArgumentNullException("param 参数不能为空");
            }
            var ids = param.Split(",").ToArray();
            return this.DbContext.Deleteable<T>().In(ids).ExecuteCommandHasChange();
        }

        /// <summary>
        /// 根据多个ID删除
        /// </summary>
        /// <param name="param">逗号分隔的标识列表，范例："1,2"</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdsAsync(string param)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                throw new ArgumentNullException("param 参数不能为空");
            }
            var ids = param.Split(",").ToArray();
            return await this.DbContext.Deleteable<T>().In(ids).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<T, bool>> predicate)
        {
            return this.DbContext.Deleteable<T>().Where(predicate).ExecuteCommandHasChange();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.DbContext.Deleteable<T>().Where(predicate).ExecuteCommandHasChangeAsync();
        }

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
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>返回影响行数</returns>
        public async Task<bool> DeleteAsync(T entity)
        {
            return await this.DbContext.Deleteable(entity).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>

        public bool Delete(List<T> entity)
        {
            return this.DbContext.Deleteable(entity).ExecuteCommandHasChange();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体模型集合</param>
        /// <returns>返回影响行数</returns>

        public async Task<bool> DeleteAsync(List<T> entity)
        {
            return await this.DbContext.Deleteable(entity).ExecuteCommandHasChangeAsync();
        }
        #endregion

        #region Instance

        /// <summary>
        /// 实例化
        /// </summary>
        public IRepository<T, TKey> Instance()
        {
            this._connectionString = AppSettingsHelper.Configuration.GetConnectionString(nameof(DataBaseTypeEnum.SqlServer));
            this._dataBaseType = DataBaseTypeEnum.SqlServer;
            this.DbContext = this.GetSqlSugarClient();// 验证客户端
            return this;
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public IRepository<T, TKey> Instance(SqlOptions options)
        {
            this._connectionString = options.ConnectionString;
            this._dataBaseType = options.DataBaseType;
            this.DbContext = this.GetSqlSugarClient();// 验证客户端
            return this;
        }
        #endregion

        public void Dispose()
        {
        }

        #region Private Method    
        /// <summary>
        /// 获取 SqlSugarClient
        /// </summary>
        /// <returns></returns>
        private SqlSugarClient GetSqlSugarClient()
        {
            if (string.IsNullOrEmpty(this._connectionString))
            {
                throw new SqlSugarException("接字符串不能为空");
            }
            var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = this._connectionString,
                DbType = GetDbType(this._dataBaseType),
                IsAutoCloseConnection = true
            });

            // 验证连接是否成功
            if (!db.Ado.IsValidConnection())
            {
                throw new SqlSugarException("SqlSugarClient 连接失败，请检查连接字符串或者数据库类型是否正确");
            }

            //如果不存在创建数据库
            db.DbMaintenance.CreateDatabase(); //个别数据库不支持

            //SQL执行完
            db.Aop.OnLogExecuted = (sql, pars) =>
            {
                //执行完了可以输出SQL执行时间 (OnLogExecutedDelegate) 
                //Console.Write("time:" + db.Ado.SqlExecutionTime.ToString());

                //执行时间超过5秒
                //if (db.Ado.SqlExecutionTime.TotalSeconds > 1)
                //{
                //NLogHelper.Warn("【-----------Begin-------------】");
                //NLogHelper.Warn($"【Sql】:{sql}");
                //NLogHelper.Warn($"【Parametres】:{pars}");
                ////代码CS文件名
                //var fileName = db.Ado.SqlStackTrace.FirstFileName;
                //NLogHelper.Warn($"代码CS文件名:{fileName}");
                ////代码行数
                //var fileLine = db.Ado.SqlStackTrace.FirstLine;
                //NLogHelper.Warn($"代码行数:{fileLine}");
                ////方法名
                //var FirstMethodName = db.Ado.SqlStackTrace.FirstMethodName;
                //NLogHelper.Warn($"方法名:{FirstMethodName}");
                ////db.Ado.SqlStackTrace.MyStackTraceList[1].xxx 获取上层方法的信息
                //NLogHelper.Warn(JsonConvert.SerializeObject(db.Ado.SqlStackTrace));
                //NLogHelper.Warn("【-----------End-------------】");
                //}
            };
            db.Aop.OnLogExecuting = (sql, pars) => //SQL执行前
            {

            };
            db.Aop.OnError = (exp) =>//SQL报错
            {
                //NLogHelper.Error("【-----------Begin-------------】");
                ////exp.sql 这样可以拿到错误SQL
                //NLogHelper.Error($"Sql:{exp.Sql}");
                //NLogHelper.Error($"Parametres:{exp.Parametres}");

                ////代码CS文件名
                //var fileName = db.Ado.SqlStackTrace.FirstFileName;
                //NLogHelper.Error($"代码CS文件名:{fileName}");
                ////代码行数
                //var fileLine = db.Ado.SqlStackTrace.FirstLine;
                //NLogHelper.Error($"代码行数:{fileLine}");
                ////方法名
                //var FirstMethodName = db.Ado.SqlStackTrace.FirstMethodName;
                //NLogHelper.Error($"方法名:{FirstMethodName}");
                //NLogHelper.Error("【-----------End-------------】");
            };
            //db.Aop.OnExecutingChangeSql = (sql, pars) => //可以修改SQL和参数的值
            //{
            //    //sql=newsql
            //    //foreach(var p in pars) //修改
            //    //return new KeyValuePair<string, SugarParameter[]>(sql, pars);
            //};
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

        /// <summary>
        /// 排序字段类型转换
        /// </summary>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        private OrderByType GetOrderByType(OrderByTypeEnum orderByType)
        {
            switch (orderByType)
            {
                case OrderByTypeEnum.Asc:
                    break;
                case OrderByTypeEnum.Desc:
                    break;
                default:
                    break;
            }
            return default;
        }
        #endregion
    }
}

