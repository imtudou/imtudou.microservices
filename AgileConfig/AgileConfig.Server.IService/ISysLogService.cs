using AgileConfig.Server.Data.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgileConfig.Server.IService
{
    public interface ISysLogService: IDisposable
    {
        Task<bool> AddSysLogAsync(SysLog log);

        Task<bool> AddRangeAsync(IEnumerable<SysLog> logs);


        Task<List<SysLog>> SearchPage(string appId, SysLogType? logType,DateTime? startTime, DateTime? endTime, int pageSize, int pageIndex);

        Task<long> Count(string appId, SysLogType? logType, DateTime? startTime, DateTime? endTime);

    }
}
