using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Imtudou.Core.Data.Test.SqlSugar
{
    public class GetStarted
    {
        public GetStarted()
        {
        }

        [Fact]
        public void GetSplit()
        {
            string ids = "1,2,3";
            var cc = ids.Split(",").ToArray();

        }
    }
}
