using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.CommonEnum
{
    /// <summary>
    /// 审批状态
    /// </summary>
    public enum ApproveStatusEnum
    {
        待审批 = 1001,
        审批通过 = 1002,
        待处理 = 1003,
        审批驳回 = 1004,
        审批拒绝 = 1005,
    }
}
