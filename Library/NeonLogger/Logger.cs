using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace NeonLogger
{
    public class Logger : ILogger
    {
        public void WriteLog(LogLevel logLevel, string owner, string title, string message, Exception e = null)
        {
            var logger = LogManager.GetLogger(GetType().FullName);
            logger.Log(logLevel, e, message);
            //Console.WriteLine(message);
        }

        public void WriteDbLog(LogLevel logLevel, string owner, string title, string message, Exception e = null)
        {
            //var logger = LogManager.GetLogger("databaseLogger");
            //logger.Log(logLevel, e, message);
            Console.WriteLine(message);
        }
    }
}
