using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.IdentityServer.Infrastructure.Enums
{
    public enum GB_GenderEnum
    {
        [Description("未知的性别")]
        Unknown = 0,
        [Description("男性")]
        Man = 1,
        [Description("女性")]
        Women = 2,
        [Description("未说明的性别")]
        Unspecified = 9
    }
}
