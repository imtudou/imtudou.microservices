using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.Data.UnitOfWorks
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 提交,返回影响的行数
        /// </summary>
        int Commit();

        /// <summary>
        /// 提交,返回影响的行数
        /// </summary>
        Task<int> CommitAsync();
    }
}
