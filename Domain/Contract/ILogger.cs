using System;

namespace Password.Domain.Contract
{
    public interface ILogger
    {
        string Name { get; }

        void Debug(string message, Exception exception = null);

        void Info(string message, Exception exception = null);

        void Warn(string message, Exception exception = null);

        void Error(string message, Exception exception = null);

        void Fatal(string message, Exception exception = null);
    }
}