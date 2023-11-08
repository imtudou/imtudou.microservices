using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.IdentityServer.Infrastructure.Enums
{
    /// <summary>
    /// 档案状态
    /// </summary>
    public enum ArchivesStatusEnum
    {
        [Description("正常")] 正常 = 1,
        [Description("简档")] 简档 = 2,
        [Description("迁出")] 迁出 = 3,
        [Description("失访")] 失访 = 4,
        [Description("已死亡")] 已死亡 = 5
    }
}
