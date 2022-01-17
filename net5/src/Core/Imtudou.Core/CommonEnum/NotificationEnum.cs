using System.ComponentModel;

namespace Imtudou.Core.CommonEnum
{
    /// <summary>
    /// 通知类
    /// </summary>
    public enum NotificationEnum
    {
        /// <summary>
        /// 已通知        
        /// </summary>
        [Description("已通知")]
        已通知 = 3001,

        /// <summary>
        /// 未通知        
        /// </summary>
        [Description("未通知")]
        未通知 = 3002,

        /// <summary>
        /// 已反馈        
        /// </summary>
        [Description("已反馈")]
        已反馈 = 3003,

        /// <summary>
        /// 未反馈        
        /// </summary>
        [Description("未反馈")]
        未反馈 = 3004,
    }
}
