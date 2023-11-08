using Imtudou.Core.Logs.Serilog;

using System;

using Xunit;

namespace Imtudou.Core.Log.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var connectionString = "Data Source=.;database=DemoForEverything;Uid=sa;Password=12345;";
            var path = "";
            var message = new LogContent {BusinessName = "≤‚ ‘1", BusinessNo = "≤‚ ‘1", ModuleName = "≤‚ ‘1", ModuleNo = "≤‚ ‘1", ServiceName = "≤‚ ‘1", ServiceNo = "≤‚ ‘1" };
            SerilogFactory.DatabaseLogManager(connectionString).Information("Test1", message);
            SerilogFactory.FileLogManager(path).Information("Test1", message);

            try
                                                                                                                                        {
                if (message != null)
                {
                    throw new Exception("Sample Error");
                }
            }
            catch (Exception e)
            {
                SerilogFactory.DatabaseLogManager().Error(e, "Test1", message, e.Message);
                SerilogFactory.FileLogManager().Error(e, "Test1", message, e.Message);
            }
        }


    }

    public class LogContent
    {
        public string ServiceNo { get; set; }
        public string ServiceName { get; set; }

        public string ModuleNo { get; set; }

        public string ModuleName { get; set; }

        public string BusinessNo { get; set; }
        public string BusinessName { get; set; }

    }
}
