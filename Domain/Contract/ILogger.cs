using System;
using Password.Domain.Contract.Enum;

namespace Password.Domain.Contract
{
    public interface ILogger
    {
        string Name { get; }

        void Log(LogLevel logLevel, string message, Exception exception = null);
    }
}