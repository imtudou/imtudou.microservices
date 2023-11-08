
using System;
using System.IO;

using Xunit;

namespace Imtudou.Core.Sample.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //c#文件流读文件 
            using (FileStream fsRead = new FileStream(@"D:\637802031304048100.xlsx", FileMode.Open))
            {
                //var npoi = new NpoiExcelUtility().UploadFileStream(fsRead);

            }

        }
    }
}
