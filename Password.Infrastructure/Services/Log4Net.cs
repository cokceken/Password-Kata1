using System;
using System.IO;
using log4net;
using Password.Domain.Contract;
using Password.Domain.Contract.Enum;

namespace Password.Infrastructure.Services
{
    public class Log4Net : ILogger
    {
        private readonly ILog _log;

        public Log4Net()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(
                AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory,
                "log4net.config")));

            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public string Name { get; } = "log4net";

        public void Log(LogLevel logLevel, string message, Exception exception = null)
        {
            switch (logLevel)
            {
                case LogLevel.Info:
                    _log.Info(message, exception);
                    break;
                case LogLevel.Debug:
                    _log.Debug(message, exception);
                    break;
                case LogLevel.Warn:
                    _log.Warn(message, exception);
                    break;
                case LogLevel.Error:
                    _log.Error(message, exception);
                    break;
                case LogLevel.Fatal:
                    _log.Fatal(message, exception);
                    break;
                default:
                    throw new NotSupportedException($"{Name} does not support log level:{logLevel}");
            }
        }
    }
}