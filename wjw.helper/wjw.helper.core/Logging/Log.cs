using System;
using System.Collections.Generic;
using System.Text;

namespace wjw.helper.Logging
{
    public class Log
    {
        private static ILogger _log = null;
        private static ILogger log
        {
            get
            {
                if (_log == null)
                    _log = LogManager.GetLogger <Log>();
                return _log;
            }

            set
            {
                _log = value;
            }
        }

        public static void SetLoger(string name)
        {
            log = LogManager.GetLogger(name);
        }
            

        public static void SetEntryInfo(bool enabled, LogLevel entryLevel)
        {
            LogManager.SetEntryInfo(enabled, entryLevel);
        }

        public static void Debug<T>(T message)
        {
            log.Debug<T>(message);
        }

        public static void Debug(string format, params object[] args)
        {
            log.Debug(format, args);
        }

        public static void Error<T>(T message)
        {
            log.Error<T>(message);
        }

        public static void Error(string format, params object[] args)
        {
            log.Error(format, args);
        }

        public static void Error<T>(T message, Exception exception)
        {
            log.Error<T>(message, exception);
        }

        public static void Error(string format, Exception exception, params object[] args)
        {
            log.Error(format, exception, args);
        }

        public static void Fatal<T>(T message)
        {
            log.Fatal<T>(message);
        }

        public static void Fatal(string format, params object[] args)
        {
            log.Fatal(format, args);
        }

        public static void Fatal<T>(T message, Exception exception)
        {
            log.Fatal<T>(message, exception);
        }

        public static void Fatal(string format, Exception exception, params object[] args)
        {
            log.Fatal(format, exception, args);
        }

        public static void Info<T>(T message, bool isData)
        {
            log.Info<T>(message, isData);
        }

        public static void Info(string format, params object[] args)
        {
            log.Info(format, args);
        }

        public static void Trace<T>(T message)
        {
            log.Trace<T>(message);
        }

        public static void Trace(string format, params object[] args)
        {
            log.Trace(format, args);
        }

        public static void Warn<T>(T message)
        {
            log.Warn<T>(message);
        }

        public static void Warn(string format, params object[] args)
        {
            log.Warn(format, args);
        }
    }
}
