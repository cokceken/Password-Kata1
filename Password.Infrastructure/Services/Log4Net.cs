using System;
using log4net;
using Password.Domain.Contract;

namespace Password.Infrastructure.Services
{
    public class Log4Net : ILogger
    {
        private readonly ILog _log;

        public Log4Net()
        {
            Name = "log4net";
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public string Name { get; }

        public void Debug(string message, Exception exception)
        {
            _log.Debug(message, exception);
        }

        public void Info(string message, Exception exception)
        {
            _log.Info(message, exception);
        }

        public void Warn(string message, Exception exception)
        {
            _log.Warn(message, exception);
        }

        public void Error(string message, Exception exception)
        {
            _log.Error(message, exception);
        }

        public void Fatal(string message, Exception exception)
        {
            _log.Fatal(message, exception);
        }
    }
}