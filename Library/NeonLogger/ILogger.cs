using NLog;
using System;

namespace NeonLogger
{
    public interface ILogger
    {
        void WriteLog(LogLevel logLevel, string owner, string title, string message, Exception e = null);
        void WriteDbLog(LogLevel logLevel, string owner, string title, string message, Exception e = null);
    }
}
