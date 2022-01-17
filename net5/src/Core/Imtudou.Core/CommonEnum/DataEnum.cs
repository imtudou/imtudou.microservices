using System.ComponentModel;

namespace Imtudou.Core.CommonEnum
{
    /// <summary>
    /// 数据类
    /// </summary>
    public enum DataEnum
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [Description("草稿")]
        草稿 = 9001,

        /// <summary>
        /// 已启用
        /// </summary>
        [Description("已启用")]
        已启用 = 9002,

        /// <summary>
        /// 已冻结
        /// </summary>
        [Description("已冻结")]
        已冻结 = 9003,

        /// <summary>
        /// 未发布
        /// </summary>
        [Description("未发布")]
        未发布 = 9004,

        /// <summary>
        /// 已发布
        /// </summary>
        [Description("已发布")]
        已发布 = 9005,

        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        已删除 = 9999,
    }
}
