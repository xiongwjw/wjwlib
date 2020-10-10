using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace wjw.loger
{

    public class Log4Net
    {

        private static Log4NetImp logImp = new Log4NetImp();

        public static void Debug(object message)
        {
            logImp.Debug(message);
        }

        public static void Debug(object message, Exception exception)
        {
            logImp.Debug(message, exception);
        }

        public static void DebugInfo(string format, params object[] args)
        {
            logImp.Debug(format, args);
        }

        public static void Info(object message)
        {
            logImp.Info(message);
        }

        public static void Info(object message, Exception exception)
        {
            logImp.Info(message, exception);
        }

        public static void InfoInfo(string format, params object[] args)
        {
            logImp.Info(format, args);
        }

        public static void Warn(object message)
        {
            logImp.Warn(message);
        }

        public static void Warn(object message, Exception exception)
        {
            logImp.Warn(message, exception);
        }

        public static void WarnInfo(string format, params object[] args)
        {
            logImp.Warn(format, args);
        }

        public static void Error(object message)
        {
            logImp.Error(message);
        }

        public static void Error(object message, Exception exception)
        {
            logImp.Error(message, exception);
        }

        public static void ErrorInfo(string format, params object[] args)
        {
            logImp.Error(format, args);
        }

        public static void Fatal(object message)
        {
            logImp.Fatal(message);
        }

        public static void Fatal(object message, Exception exception)
        {
            logImp.Fatal(message, exception);
        }

        public static void FatalInfo(string format, params object[] args)
        {
            logImp.Fatal(format, args);
        }


        private class Log4NetImp
        {
            
            private readonly log4net.ILog loger = log4net.LogManager.GetLogger("RootLog");

            public Log4NetImp()
            {
                string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
                if (File.Exists(configFile))
                {
                    FileInfo fi = new FileInfo(configFile);
                    log4net.Config.XmlConfigurator.Configure(fi);
                }
            }

            public void Debug(object message)
            {
                if (loger != null && loger.IsDebugEnabled)
                    loger.Debug(message);
            }
            public void Debug(object message, Exception exception)
            {

                if (loger != null && loger.IsDebugEnabled)
                {
                    loger.Debug(message, exception);
                }

            }

            public void Debug(string format, params object[] args)
            {

                if (loger != null && loger.IsDebugEnabled)
                {
                    loger.DebugFormat(format, args);
                }

            }

            public void Info(object message)
            {

                if (loger != null && loger.IsInfoEnabled)
                {
                    loger.Info(message);
                }

            }
            public void Info(object message, Exception exception)
            {

                if (loger != null && loger.IsInfoEnabled)
                {
                    loger.Info(message, exception);
                }

            }

            public void Info(string format, params object[] args)
            {

                if (loger != null && loger.IsInfoEnabled)
                {
                    loger.InfoFormat(format, args);
                }

            }

            public void Warn(object message)
            {

                if (loger != null && loger.IsWarnEnabled)
                {
                    loger.Warn(message);
                }

            }
            public void Warn(object message, Exception exception)
            {

                if (loger != null && loger.IsWarnEnabled)
                {
                    loger.Warn(message, exception);
                }

            }

            public void Warn(string format, params object[] args)
            {

                if (loger != null && loger.IsWarnEnabled)
                {
                    loger.WarnFormat(format, args);
                }

            }

            public void Error(object message)
            {

                if (loger != null && loger.IsErrorEnabled)
                {
                    loger.Error(message);
                }

            }
            public void Error(object message, Exception exception)
            {

                if (loger != null && loger.IsErrorEnabled)
                {
                    loger.Error(message, exception);
                }

            }

            public void Error(string format, params object[] args)
            {

                if (loger != null && loger.IsErrorEnabled)
                {
                    loger.ErrorFormat(format, args);
                }

            }

            public void Fatal(object message)
            {

                if (loger != null && loger.IsFatalEnabled)
                {
                    loger.Fatal(message);
                }

            }
            public void Fatal(object message, Exception exception)
            {

                if (loger != null && loger.IsFatalEnabled)
                {
                    loger.Fatal(message, exception);
                }

            }

            public void Fatal(string format, params object[] args)
            {

                if (loger != null && loger.IsFatalEnabled)
                {
                    loger.FatalFormat(format, args);
                }

            }


        }
    }

}
