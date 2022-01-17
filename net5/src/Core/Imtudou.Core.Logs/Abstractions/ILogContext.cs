using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.Logs.Abstractions
{
    /// <summary>
    /// 日志上下文
    /// </summary>
    public interface ILogContext
    {
        /// <summary>
        /// 日志标识
        /// </summary>
        string LogID { get; }

        /// <summary>
        /// 跟踪号
        /// </summary>
        string TraceID { get; }

        /// <summary>
        /// 计时器
        /// </summary>
        Stopwatch Stopwatch { get; }

        /// <summary>
        /// IP
        /// </summary>
        string Ip { get; }

        /// <summary>
        /// 主机
        /// </summary>
        string Host { get; }

        /// <summary>
        /// 浏览器
        /// </summary>
        string Browser { get; }

        /// <summary>
        /// 请求地址
        /// </summary>
        string Url { get; }
    }
}
