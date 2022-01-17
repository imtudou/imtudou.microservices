using System.ComponentModel;

namespace Imtudou.Core.CommonEnum
{
    /// <summary>
    /// 审批状态
    /// </summary>
    public enum ApproveStatusEnum
    {
        /// <summary>
        /// 待审批
        /// </summary>
        [Description("待审批")]
        待审批 = 1001,

        /// <summary>
        /// 审批通过
        /// </summary>
        [Description("审批通过")]
        审批通过 = 1002,

        /// <summary>
        /// 待处理
        /// </summary>
        [Description("待处理")]
        待处理 = 1003,

        /// <summary>
        /// 审批驳回
        /// </summary>
        [Description("审批驳回")]
        审批驳回 = 1004,

        /// <summary>
        /// 审批拒绝
        /// </summary>
        [Description("审批拒绝")]
        审批拒绝 = 1005,
    }
}
