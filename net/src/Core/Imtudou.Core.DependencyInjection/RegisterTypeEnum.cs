using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.DependencyInjection
{
    public enum RegisterTypeEnum
    {
        [Description("Service")] Service = 1,
        [Description("Repository")] Repository = 2,
    }
}
