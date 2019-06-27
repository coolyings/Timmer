using System;
using System.Diagnostics;
using Serilog;

namespace Timmers
{
    public class ServiceLog : Microsoft.Owin.Logging.ILogger
    {
        private static readonly string LogPath;
        static ServiceLog()
        {
            LogPath = AppDomain.CurrentDomain.BaseDirectory + "log";
        }
        private static ILogger _logger;
        public static ILogger Logger
        {
            get { return _logger ?? (_logger = CreateLogger()); }
            set { _logger = value; }
        }

        public static ILogger CreateLogger()
        {
            return new LoggerConfiguration().WriteTo.RollingFile(
                LogPath + "\\log-{Date}.txt", fileSizeLimitBytes: null
                ).CreateLogger();
        }

        public bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            var logged = false;
            switch (eventType)
            {
                case TraceEventType.Error:
                    Logger.Error(exception, formatter(state, exception));
                    logged = true;
                    break;
                case TraceEventType.Information:
                    Logger.Information(exception, formatter(state, exception));
                    logged = true;
                    break;
                case TraceEventType.Warning:
                    Logger.Warning(exception, formatter(state, exception));
                    logged = true;
                    break;
                default:
                    break;
            }
            return logged;
        }
    }

    public class ApiLoggerFactory : Microsoft.Owin.Logging.ILoggerFactory
    {
        public Microsoft.Owin.Logging.ILogger Create(string name)
        {
            return new ServiceLog();
        }
    }
}
