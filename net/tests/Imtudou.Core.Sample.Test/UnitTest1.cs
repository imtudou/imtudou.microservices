
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
            //c#�ļ������ļ� 
            using (FileStream fsRead = new FileStream(@"D:\637802031304048100.xlsx", FileMode.Open))
            {
                //var npoi = new NpoiExcelUtility().UploadFileStream(fsRead);

            }

        }
    }
}
