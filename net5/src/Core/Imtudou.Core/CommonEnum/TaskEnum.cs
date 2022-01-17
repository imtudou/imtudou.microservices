using System.ComponentModel;

namespace Imtudou.Core.CommonEnum
{
    /// <summary>
    /// 任务类
    /// </summary>
    public enum TaskEnum
    {
        /// <summary>
        /// 未开始
        /// </summary>
        [Description("未开始")]
        未开始 = 8000,

        /// <summary>
        /// 未执行
        /// </summary>
        [Description("未执行")]
        未执行 = 8001,

        /// <summary>
        /// 已执行
        /// </summary>
        [Description("已执行")]
        已执行 = 8002,

        /// <summary>
        /// 部分已执行
        /// </summary>
        [Description("部分已执行")]
        部分已执行 = 8003,

        /// <summary>
        /// 超时未执行
        /// </summary>
        [Description("超时未执行")]
        超时未执行 = 8004,

        /// <summary>
        /// 超时已执行
        /// </summary>
        [Description("超时已执行")]
        超时已执行 = 8005,

        /// <summary>
        /// 已终止
        /// </summary>
        [Description("已终止")]
        已终止 = 8006,
    }
}
