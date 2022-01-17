
using Serilog.Core;

namespace Imtudou.Core.Logs.Serilog.Abstract
{
    public abstract class SerilogConfigurationAbstract
    {
        public static string ConnectionString { get; set; }

        protected abstract Logger LoggerManage();

        public Logger InstanceLogger()
        {
            return default;
        }
    }
}
