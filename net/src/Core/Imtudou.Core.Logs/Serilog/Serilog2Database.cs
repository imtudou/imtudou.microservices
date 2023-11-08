
using Imtudou.Core.Logs.Serilog.Abstract;

using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using System;
using System.IO;

namespace Imtudou.Core.Logs.Serilog
{
    public class Serilog2Database : SerilogConfigurationAbstract, ISerilog
    {
        public Serilog2Database() { }
        public Serilog2Database(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override Logger LoggerManage()
        {
            IConfigurationRoot configuration = null; 
            if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json")))
            {
                configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();
            }

            if (string.IsNullOrEmpty(ConnectionString))
            {
                ConnectionString = configuration.GetConnectionString("ConnectionString");
            }

            return new LoggerConfiguration()
                .WriteTo
                .MSSqlServer(
                    connectionString: ConnectionString,
                    tableName: "Logs",
                    period: TimeSpan.FromSeconds(5),
                    autoCreateSqlTable: true
                )
                .CreateLogger();
        }

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Information level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        public void Information(string messageTemplate) => this.LoggerManage().Information(messageTemplate);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Information level and
        //     associated exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        public void Information(Exception exception, string messageTemplate) => this.LoggerManage().Information(exception, messageTemplate);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Information level and
        //     associated exception.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValues:
        //     Objects positionally formatted into the message template.
        public void Information(string messageTemplate, params object[] propertyValues) => this.LoggerManage().Information(messageTemplate, propertyValues);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Information level and
        //     associated exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValues:
        //     Objects positionally formatted into the message template.
        public void Information(Exception exception, string messageTemplate, params object[] propertyValues) => this.LoggerManage().Information(exception, messageTemplate, propertyValues);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Information level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue:
        //     Object positionally formatted into the message template.
        public void Information<T>(string messageTemplate, T propertyValue) => this.LoggerManage().Information(messageTemplate, propertyValue);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Information level and
        //     associated exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue:
        //     Object positionally formatted into the message template.
        public void Information<T>(Exception exception, string messageTemplate, T propertyValue) => this.LoggerManage().Information(exception, messageTemplate, propertyValue);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Information level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue0:
        //     Object positionally formatted into the message template.
        //
        //   propertyValue1:
        //     Object positionally formatted into the message template.
        public void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => this.LoggerManage().Information(messageTemplate, propertyValue0, propertyValue1);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Information level and
        //     associated exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue0:
        //     Object positionally formatted into the message template.
        //
        //   propertyValue1:
        //     Object positionally formatted into the message template.
        public void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => this.LoggerManage().Information(exception, messageTemplate, propertyValue0, propertyValue1);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Error level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        public void Error(string messageTemplate) => this.LoggerManage().Information(messageTemplate);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Error level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        public void Error(Exception exception, string messageTemplate) => this.LoggerManage().Information(exception, messageTemplate);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Error level and associated
        //     exception.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValues:
        //     Objects positionally formatted into the message template.
        public void Error(string messageTemplate, params object[] propertyValues) => this.LoggerManage().Error(messageTemplate, propertyValues);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Error level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValues:
        //     Objects positionally formatted into the message template.
        public void Error(Exception exception, string messageTemplate, params object[] propertyValues) => this.LoggerManage().Error(exception, messageTemplate, propertyValues);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Error level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue:
        //     Object positionally formatted into the message template.
        public void Error<T>(string messageTemplate, T propertyValue) => this.LoggerManage().Error(messageTemplate, propertyValue);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Error level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue:
        //     Object positionally formatted into the message template.
        public void Error<T>(Exception exception, string messageTemplate, T propertyValue) => this.LoggerManage().Error(exception, messageTemplate, propertyValue);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Error level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue0:
        //     Object positionally formatted into the message template.
        //
        //   propertyValue1:
        //     Object positionally formatted into the message template.
        public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => this.LoggerManage().Error(messageTemplate, propertyValue0, propertyValue1);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Error level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue0:
        //     Object positionally formatted into the message template.
        //
        //   propertyValue1:
        //     Object positionally formatted into the message template.
        public void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => this.LoggerManage().Error(exception, messageTemplate, propertyValue0, propertyValue1);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Fatal level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        public void Fatal(string messageTemplate) => this.LoggerManage().Fatal(messageTemplate);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Fatal level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        public void Fatal(Exception exception, string messageTemplate) => this.LoggerManage().Fatal(exception, messageTemplate);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Fatal level and associated
        //     exception.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValues:
        //     Objects positionally formatted into the message template.
        public void Fatal(string messageTemplate, params object[] propertyValues) => this.LoggerManage().Fatal(messageTemplate, propertyValues);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Fatal level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValues:
        //     Objects positionally formatted into the message template.
        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues) => this.LoggerManage().Fatal(exception, messageTemplate, propertyValues);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Fatal level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue:
        //     Object positionally formatted into the message template.
        public void Fatal<T>(string messageTemplate, T propertyValue) => this.LoggerManage().Fatal(messageTemplate, propertyValue);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Fatal level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue:
        //     Object positionally formatted into the message template.
        public void Fatal<T>(Exception exception, string messageTemplate, T propertyValue) => this.LoggerManage().Fatal(exception, messageTemplate, propertyValue);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Fatal level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue0:
        //     Object positionally formatted into the message template.
        //
        //   propertyValue1:
        //     Object positionally formatted into the message template.
        public void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => this.LoggerManage().Fatal(messageTemplate, propertyValue0, propertyValue1);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Fatal level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue0:
        //     Object positionally formatted into the message template.
        //
        //   propertyValue1:
        //     Object positionally formatted into the message template.
        public void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => this.LoggerManage().Fatal(exception, messageTemplate, propertyValue0, propertyValue1);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Warning level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        public void Warning(string messageTemplate) => this.LoggerManage().Warning(messageTemplate);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Warning level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        public void Warning(Exception exception, string messageTemplate) => this.LoggerManage().Warning(exception, messageTemplate);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Warning level and associated
        //     exception.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValues:
        //     Objects positionally formatted into the message template.
        public void Warning(string messageTemplate, params object[] propertyValues) => this.LoggerManage().Warning( messageTemplate, propertyValues);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Warning level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValues:
        //     Objects positionally formatted into the message template.
        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues) => this.LoggerManage().Warning(exception, messageTemplate, propertyValues);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Warning level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue:
        //     Object positionally formatted into the message template.
        public void Warning<T>(string messageTemplate, T propertyValue) => this.LoggerManage().Warning(messageTemplate, propertyValue);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Warning level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue:
        //     Object positionally formatted into the message template.
        public void Warning<T>(Exception exception, string messageTemplate, T propertyValue) => this.LoggerManage().Warning(exception, messageTemplate, propertyValue);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Warning level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue0:
        //     Object positionally formatted into the message template.
        //
        //   propertyValue1:
        //     Object positionally formatted into the message template.
        public void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => this.LoggerManage().Warning(messageTemplate, propertyValue0, propertyValue1);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Warning level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue0:
        //     Object positionally formatted into the message template.
        //
        //   propertyValue1:
        //     Object positionally formatted into the message template.
        public void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => this.LoggerManage().Warning(exception, messageTemplate, propertyValue0, propertyValue1);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Debug level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        public void Debug(string messageTemplate) => this.LoggerManage().Debug(messageTemplate);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Debug level and associated
        //     exception.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValues:
        //     Objects positionally formatted into the message template.
        public void Debug(string messageTemplate, params object[] propertyValues) => this.LoggerManage().Debug(messageTemplate, propertyValues);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Debug level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        public void Debug(Exception exception, string messageTemplate) => this.LoggerManage().Debug(exception, messageTemplate);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Debug level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValues:
        //     Objects positionally formatted into the message template.
        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues) => this.LoggerManage().Debug(exception, messageTemplate, propertyValues);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Debug level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue:
        //     Object positionally formatted into the message template.
        public void Debug<T>(string messageTemplate, T propertyValue) => this.LoggerManage().Debug(messageTemplate, propertyValue);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Debug level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue:
        //     Object positionally formatted into the message template.
        public void Debug<T>(Exception exception, string messageTemplate, T propertyValue) => this.LoggerManage().Debug(exception, messageTemplate, propertyValue);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Debug level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue0:
        //     Object positionally formatted into the message template.
        //
        //   propertyValue1:
        //     Object positionally formatted into the message template.
        public void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => this.LoggerManage().Debug(messageTemplate, propertyValue0, propertyValue1);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Debug level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue0:
        //     Object positionally formatted into the message template.
        //
        //   propertyValue1:
        //     Object positionally formatted into the message template.
        public void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => this.LoggerManage().Debug(exception, messageTemplate, propertyValue0, propertyValue1);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Verbose level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        public void Verbose(string messageTemplate) => this.LoggerManage().Verbose(messageTemplate);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Verbose level and associated
        //     exception.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValues:
        //     Objects positionally formatted into the message template.
        public void Verbose(string messageTemplate, params object[] propertyValues) => this.LoggerManage().Verbose(messageTemplate, propertyValues);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Verbose level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        public void Verbose(Exception exception, string messageTemplate) => this.LoggerManage().Verbose(exception, messageTemplate);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Verbose level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValues:
        //     Objects positionally formatted into the message template.
        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues) => this.LoggerManage().Verbose(exception, messageTemplate, propertyValues);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Verbose level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue:
        //     Object positionally formatted into the message template.
        public void Verbose<T>(string messageTemplate, T propertyValue) => this.LoggerManage().Verbose(messageTemplate, propertyValue);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Verbose level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue:
        //     Object positionally formatted into the message template.
        public void Verbose<T>(Exception exception, string messageTemplate, T propertyValue) => this.LoggerManage().Verbose(exception, messageTemplate, propertyValue);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Verbose level.
        //
        // Parameters:
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue0:
        //     Object positionally formatted into the message template.
        //
        //   propertyValue1:
        //     Object positionally formatted into the message template.
        public void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => this.LoggerManage().Verbose(messageTemplate, propertyValue0, propertyValue1);

        // Summary:
        //     Write a log event with the Serilog.Events.LogEventLevel.Verbose level and associated
        //     exception.
        //
        // Parameters:
        //   exception:
        //     Exception related to the event.
        //
        //   messageTemplate:
        //     Message template describing the event.
        //
        //   propertyValue0:
        //     Object positionally formatted into the message template.
        //
        //   propertyValue1:
        //     Object positionally formatted into the message template.
        public void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => this.LoggerManage().Verbose(exception, messageTemplate, propertyValue0, propertyValue1);
    }
}
