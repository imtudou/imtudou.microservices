using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.CommonEnum
{
    /// <summary>
    /// 任务类
    /// </summary>
    public enum TaskEnum
    {
        未开始 = 8000,
        未执行 = 8001,
        已执行 = 8002,
        部分已执行 = 8003,
        超时未执行 = 8004,
        超时已执行 = 8005,
        已终止 = 8006,
    }
}
