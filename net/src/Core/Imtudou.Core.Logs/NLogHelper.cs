using Newtonsoft.Json;
using NLog;
using System;
using System.Threading.Tasks;

namespace Imtudou.Core.Logs
{
    /// <summary>
    /// NLogHelper
    /// </summary>
    public class NLogHelper
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="message">message</param>
        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void Info<T>(T param, string message)
        {
            string paramStr = param != null ? JsonConvert.SerializeObject(param) : string.Empty;
            logger.Info(message + paramStr);
        }

        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="message">message</param>
        public static void Warn(string message)
        {
            logger.Warn(message);
        }

        /// <summary>
        /// Warn
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="param">param</param>
        /// <param name="message">message</param>
        public static void Warn<T>(T param, string message)
        {
            string paramStr = param != null ? JsonConvert.SerializeObject(param) : string.Empty;
            logger.Warn(message + paramStr);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message">message</param>
        public static void Error(string message)
        {
            logger.Error(message);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="ex">ex</param>
        public static void Error(Exception ex)
        {
            logger.Error($"【{ex.Source}】【{ex.Message}】【{ex.InnerException}】\n\t【{ex.StackTrace}】");
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="ex">ex</param>
        /// <param name="message">message</param>
        public static void Error(Exception ex, string message)
        {
            logger.Error($"{message}|【{ex.Source}】【{ex.Message}】【{ex.InnerException}】\n\t【{ex.StackTrace}】");
        }
    }
}
