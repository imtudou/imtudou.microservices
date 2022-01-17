using Imtudou.Core.Logs.Serilog.Abstract;

using Serilog;
using Serilog.Core;
using Serilog.Events;

using System;
using System.IO;

namespace Imtudou.Core.Logs.Serilog
{
    public class Serilog2File : SerilogConfigurationAbstract, ISerilog
    {
        private readonly string LogFolderPath = Directory.GetParent(Directory.GetCurrentDirectory()) + @"\LogFiles\"; // API's Parent Path = LogHelper(Root Folder) + /LogFiles/
        private static readonly object fileLoglocks = new object();
        private Logger logger;

        public Serilog2File()
        {
            CreateDirectory(LogFolderPath);
        }
        public Serilog2File(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = LogFolderPath;
            }
            CreateDirectory(path);
        }

        protected override Logger LoggerManage()
        {
            if (logger == null)
            {
                lock (fileLoglocks) // thread safe, singleton
                {
                   logger = new LoggerConfiguration()
                                .WriteTo.Logger(s => s.Filter.ByIncludingOnly(error => error.Level == LogEventLevel.Error))
                                .WriteTo.File(string.Format(@"{0}\Error\error-.log", LogFolderPath), LogEventLevel.Error, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, fileSizeLimitBytes: 5000000, outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} | {Level}] {Message:lj}{NewLine}{Exception}")

                                .WriteTo.Logger(s => s.Filter.ByIncludingOnly(info => info.Level == LogEventLevel.Information))
                                .WriteTo.File(string.Format(@"{0}\Info\info-.log", LogFolderPath), LogEventLevel.Information, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, fileSizeLimitBytes: 5000000, outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} | {Level}] {Message:lj}{NewLine}{Exception}")

                                .WriteTo.Logger(s => s.Filter.ByExcluding(other => other.Level == LogEventLevel.Error || other.Level == LogEventLevel.Information))
                                .WriteTo.File(string.Format(@"{0}\Warning\warning-.log", LogFolderPath), LogEventLevel.Warning, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, fileSizeLimitBytes: 5000000, outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} | {Level}] {Message:lj}{NewLine}{Exception}")
                                .CreateLogger();

                    // LogEventLevel: Verbose < Debug < Information < Warning < Error < Fatal
                }
            }

            return logger;
        }

        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!Directory.Exists(path + @"\Debug\"))
            {
                Directory.CreateDirectory(path + @"\Debug\");
            }

            if (!Directory.Exists(path + @"\Info\"))
            {
                Directory.CreateDirectory(path + @"\Info\");
            }

            if (!Directory.Exists(path + @"\Error\"))
            {
                Directory.CreateDirectory(path + @"\Error\");
            } 

            if (!Directory.Exists(path + @"\Fatal\"))
            {
                Directory.CreateDirectory(path + @"\Fatal\");
            }

            if (!Directory.Exists(path + @"\Warning\"))
            {
                Directory.CreateDirectory(path + @"\Warning\");
            }
            
        }

        // Information
        public void Information(string messageTemplate) => LoggerManage().Information(messageTemplate);
        public void Information(string messageTemplate, params object[] propertyValues) => LoggerManage().Information(messageTemplate, propertyValues);
        public void Information(Exception exception, string messageTemplate) => LoggerManage().Information(exception, messageTemplate);
        public void Information(Exception exception, string messageTemplate, params object[] propertyValues) => LoggerManage().Information(exception, messageTemplate, propertyValues);
        public void Information<T>(string messageTemplate, T propertyValue) => LoggerManage().Information(messageTemplate, propertyValue);
        public void Information<T>(Exception exception, string messageTemplate, T propertyValue) => LoggerManage().Information(exception, messageTemplate, propertyValue);
        public void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => LoggerManage().Information(messageTemplate, propertyValue0, propertyValue1);
        public void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => LoggerManage().Error(exception, messageTemplate, propertyValue0, propertyValue1);

        // Error
        public void Error(string messageTemplate) => LoggerManage().Error(messageTemplate);
        public void Error(string messageTemplate, params object[] propertyValues) => LoggerManage().Error(messageTemplate, propertyValues);
        public void Error(Exception exception, string messageTemplate) => LoggerManage().Error(exception, messageTemplate);
        public void Error(Exception exception, string messageTemplate, params object[] propertyValues) => LoggerManage().Error(exception, messageTemplate, propertyValues);
        public void Error<T>(string messageTemplate, T propertyValue) => LoggerManage().Error(messageTemplate, propertyValue);
        public void Error<T>(Exception exception, string messageTemplate, T propertyValue) => LoggerManage().Error(exception, messageTemplate, propertyValue);
        public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => LoggerManage().Error(messageTemplate, propertyValue0, propertyValue1);
        public void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => LoggerManage().Error(exception, messageTemplate, propertyValue0, propertyValue1);

        // Fatal
        public void Fatal(string messageTemplate) => LoggerManage().Fatal(messageTemplate);
        public void Fatal(string messageTemplate, params object[] propertyValues) => LoggerManage().Fatal(messageTemplate, propertyValues);
        public void Fatal(Exception exception, string messageTemplate) => LoggerManage().Fatal(exception, messageTemplate);
        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues) => LoggerManage().Fatal(exception, messageTemplate, propertyValues);
        public void Fatal<T>(string messageTemplate, T propertyValue) => LoggerManage().Fatal(messageTemplate, propertyValue);
        public void Fatal<T>(Exception exception, string messageTemplate, T propertyValue) => LoggerManage().Fatal(exception, messageTemplate, propertyValue);
        public void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => LoggerManage().Fatal(messageTemplate, propertyValue0, propertyValue1);
        public void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => LoggerManage().Fatal(exception, messageTemplate, propertyValue0, propertyValue1);

        // Warning
        public void Warning(string messageTemplate) => LoggerManage().Warning(messageTemplate);
        public void Warning(string messageTemplate, params object[] propertyValues) => LoggerManage().Warning(messageTemplate, propertyValues);
        public void Warning(Exception exception, string messageTemplate) => LoggerManage().Warning(exception, messageTemplate);
        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues) => LoggerManage().Warning(exception, messageTemplate, propertyValues);
        public void Warning<T>(string messageTemplate, T propertyValue) => LoggerManage().Warning(messageTemplate, propertyValue);
        public void Warning<T>(Exception exception, string messageTemplate, T propertyValue) => LoggerManage().Warning(exception, messageTemplate, propertyValue);
        public void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => LoggerManage().Warning(messageTemplate, propertyValue0, propertyValue1);
        public void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => LoggerManage().Warning(exception, messageTemplate, propertyValue0, propertyValue1);

        // Debug
        public void Debug(string messageTemplate) => LoggerManage().Debug(messageTemplate);
        public void Debug(string messageTemplate, params object[] propertyValues) => LoggerManage().Debug(messageTemplate, propertyValues);
        public void Debug(Exception exception, string messageTemplate) => LoggerManage().Debug(exception, messageTemplate);
        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues) => LoggerManage().Debug(exception, messageTemplate, propertyValues);
        public void Debug<T>(string messageTemplate, T propertyValue) => LoggerManage().Debug(messageTemplate, propertyValue);
        public void Debug<T>(Exception exception, string messageTemplate, T propertyValue) => LoggerManage().Debug(exception, messageTemplate, propertyValue);
        public void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => LoggerManage().Debug(messageTemplate, propertyValue0, propertyValue1);
        public void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => LoggerManage().Debug(exception, messageTemplate, propertyValue0, propertyValue1);

        // Verbose
        public void Verbose(string messageTemplate) => LoggerManage().Verbose(messageTemplate);
        public void Verbose(string messageTemplate, params object[] propertyValues) => LoggerManage().Debug(messageTemplate, propertyValues);
        public void Verbose(Exception exception, string messageTemplate) => LoggerManage().Debug(exception, messageTemplate);
        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues) => LoggerManage().Debug(exception, messageTemplate, propertyValues);
        public void Verbose<T>(string messageTemplate, T propertyValue) => LoggerManage().Verbose(messageTemplate, propertyValue);
        public void Verbose<T>(Exception exception, string messageTemplate, T propertyValue) => LoggerManage().Debug(exception, messageTemplate, propertyValue);
        public void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => LoggerManage().Debug(messageTemplate, propertyValue0, propertyValue1);
        public void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => LoggerManage().Debug(exception, messageTemplate, propertyValue0, propertyValue1);
    }
}
