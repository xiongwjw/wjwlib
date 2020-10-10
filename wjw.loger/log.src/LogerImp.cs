using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System.Reflection;
using System.Diagnostics;

namespace wjw.loger
{
    public enum LogLevel
    {
        Debug = 0,
        Info,
        Warn,
        Error,
        Fatal
    }

    public class Log
    {
        private static LogerImp logerImp = new LogerImp();
        private const string DebugName = "[Debug]";
        private const string InfoName = "[Info]";
        private const string WarnName = "[Warn]";
        private const string ErrorName = "[Error]";
        private const string FatalName = "[Fatal]";

        private static int _loglevel = 0;
        public static LogLevel LogLevel
        {
            set
            {
                _loglevel = (int)value;
            }
        }
        public static string LogPath
        {
            set
            {
                logerImp.LogPath = value;
            }
        }

        public static int MaxLength
        {
            set
            {
                logerImp.MaxLength = value;
            }
        }
        

        public static string Prefix
        {
            set
            {
                logerImp.Prefix = value;
            }
        }

        public static bool IsSeperateFolder
        {
            set
            {
                logerImp.IsSeperateFolder = value;
            }
        }

        public static event Action<string> OnWriteLog = null;

        public static void Debug(string message)
        {
            if (_loglevel < 1)
                LogString(message, DebugName);
        }

        public static void DebugFormat(string message, params object[] args)
        {
            try
            {
                string msg = string.Format(message, args);
                if (_loglevel < 1)
                    LogString(msg, DebugName);
            }
            catch (System.Exception ex)
            {
                LogString(ex.Message, DebugName);
            }
        }

        public static void Debug(object obj)
        {
            if (_loglevel < 1)
                LogObject(obj, DebugName);
        }

        public static void Debug(string message, object obj)
        {
            if (_loglevel < 1)
                LogObject(message, obj, DebugName);
        }



        public static void Debug(Exception ex)
        {
            if (_loglevel < 1)
                LogExecption(ex, DebugName);

        }
        
        public static void Info(string message)
        {
            if (_loglevel < 2)
                LogString(message, InfoName);
        }

        public static void InfoFormat(string message, params object[] args)
        {
            try
            {
                string msg = string.Format(message, args);
                if (_loglevel < 2)
                    LogString(msg, InfoName);
            }
            catch (System.Exception ex)
            {
                LogString(ex.Message, InfoName);
            }
        }


        public static void Info(object obj)
        {
            if (_loglevel < 2)
                LogObject(obj, InfoName);
        }

        public static void Info(string message, object obj)
        {
            if (_loglevel < 2)
                LogObject(message, obj, InfoName);
        }

        public static void Info(Exception ex)
        {
            if (_loglevel < 2)
                LogExecption(ex, InfoName);

        }

        public static void Warn(string message)
        {
            if (_loglevel < 3)
                LogString(message, WarnName);
        }

        public static void WarnFormat(string message, params object[] args)
        {
            try
            {
                string msg = string.Format(message, args);
                if (_loglevel < 3)
                    LogString(msg, WarnName);
            }
            catch (System.Exception ex)
            {
                LogString(ex.Message, WarnName);
            }
        }


        public static void Warn(object obj)
        {
            if (_loglevel < 3)
                LogObject(obj, WarnName);
        }

        public static void Warn(string message, object obj)
        {
            if (_loglevel < 3)
                LogObject(message, obj, WarnName);
        }

        public static void Warn(Exception ex)
        {
            if (_loglevel < 3)
                LogExecption(ex, WarnName);

        }


        public static void Error(string message)
        {
            if (_loglevel < 4)
                LogString(message, ErrorName);
        }

        public static void ErrorFormat(string message, params object[] args)
        {
            try
            {
                string msg = string.Format(message, args);
                if (_loglevel < 4)
                    LogString(msg, ErrorName);
            }
            catch (System.Exception ex)
            {
                LogString(ex.Message, ErrorName);
            }
        }


        public static void Error(object obj)
        {
            if (_loglevel < 4)
                LogObject(obj, ErrorName);
        }

        public static void Error(string message, object obj)
        {
            if (_loglevel < 4)
                LogObject(message, obj, ErrorName);
        }

        public static void Error(Exception ex)
        {
            if (_loglevel < 4)
                LogExecption(ex, ErrorName);

        }

        public static void Fatal(string message)
        {
            LogString(message, FatalName);
        }

        public static void FatalFormat(string message, params object[] args)
        {
            try
            {
                string msg = string.Format(message, args);
                LogString(msg, FatalName);
            }
            catch (System.Exception ex)
            {
                LogString(ex.Message, FatalName);
            }
        }


        public static void Fatal(object obj)
        {
            LogObject(obj, FatalName);
        }

        public static void Fatal(string message, object obj)
        {
            LogObject(message, obj, FatalName);
        }

        public static void Fatal(Exception ex)
        {
            LogExecption(ex, FatalName);

        }

        private static void LogString(string message,string preName)
        {
            logerImp.Log(preName+ " " + message);
            OnWriteLog?.Invoke(message);
        }

        private static void LogExecption(Exception ex, string preName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(ex.Message);
            sb.AppendLine(ex.StackTrace);
            var stacktrace = new StackTrace();
            for (var i = 0; i < stacktrace.FrameCount; i++)
            {
                sb.AppendLine(stacktrace.GetFrame(i).GetFileName() + ":" + stacktrace.GetFrame(i).GetFileLineNumber() + ":" + stacktrace.GetFrame(i).GetMethod());
            }
            logerImp.Log(preName+ " " + sb.ToString());
            OnWriteLog?.Invoke(sb.ToString());
        }

        private static void LogObject(object obj, string preName)
        {
            string msg = ParseObject(obj);
            logerImp.Log(preName+" " + msg);
            OnWriteLog?.Invoke(msg);
        }

        private static void LogObject(string message,object obj, string preName)
        {
            string msg = message + ParseObject(obj);
            logerImp.Log(preName+" " + msg);
            OnWriteLog?.Invoke(msg);
        }

        private static string ParseObject(object obj)
        {
            if (obj is string)
                return obj.ToString();
            else
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendLine();
                foreach (PropertyInfo p in obj.GetType().GetProperties())
                {
                    msg.AppendFormat("{0} : {1}", p.Name, p.GetValue(obj)?.ToString());
                    msg.AppendLine();
                }
                return msg.ToString();
            }
        }

        private class LogerImp
        {
            public LogerImp()
            {
                IsSeperateFolder = false;
                Prefix = string.Empty;
                isRuning = true;
                ThreadPool.QueueUserWorkItem(WriteFile, s1.Token);
            }

            ~LogerImp()
            {
                if (logMessageQueue.Count > 0)// if still waiting write , give more 3 second to write
                {
                    queueEvent.Set();
                    Thread.Sleep(3000);
                }
                isRuning = false;
                s1.Cancel();
            }

            private bool isRuning = false;
            private CancellationTokenSource s1 = new CancellationTokenSource();

            private Queue<string> logMessageQueue = new Queue<string>();
            private AutoResetEvent queueEvent = new AutoResetEvent(false);

            private string _logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
            public string LogPath
            {
                get
                {
                    return _logPath;
                }
                set
                {
                    try
                    {
                        if (!Directory.Exists(value))
                            Directory.CreateDirectory(value);
                        _logPath = value;
                    }
                    catch { }
                }
            }
            public int MaxLength { get; set; } = 0;
            public bool IsSeperateFolder { get; set; }
            public string Prefix { get; set; }

            public void Log(string message)
            {
                if(MaxLength>0)
                {
                    if (message.Length > MaxLength)
                        message = message.Substring(MaxLength) + "...";
                }

                if (logMessageQueue.Count == 1999)
                    AddLogToQueue("messsage queue reach 2000,discard message");

                if (message != string.Empty && logMessageQueue.Count < 2000)
                {
                    AddLogToQueue(message);
                }
            }

            private void AddLogToQueue(string message)
            {
                logMessageQueue.Enqueue(message);
                queueEvent.Set();
            }

            private void WriteFile(object state)
            {
                StreamWriter sw = null;
                FileStream fs = null;
                try
                {
                    string message = string.Empty;
                    string fileName = string.Empty;

                    while (isRuning)
                    {
                        queueEvent.WaitOne();
                        while (logMessageQueue.Count > 0 && isRuning)// write all one time
                        {
                            message = logMessageQueue.Dequeue();
                            string folder_name = LogPath;
                            if (IsSeperateFolder)
                                folder_name = Path.Combine(LogPath, DateTime.Now.Year.ToString(), DateTime.Now.ToString("MM"));
                            string file_name = Path.Combine(folder_name, DateTime.Today.ToString("yyyyMMdd") + ".txt");
                            if (Prefix != string.Empty)
                                file_name = Path.Combine(folder_name, Prefix + DateTime.Today.ToString("yyyyMMdd") + ".txt");
                            if (!file_name.Equals(fileName) || fs == null || sw == null)// if we change to another file . renew the file and folder
                            {
                                fileName = file_name;
                                if (!Directory.Exists(folder_name))
                                    Directory.CreateDirectory(folder_name);
                                if (fs != null) fs.Close();
                                if (sw != null) sw.Close();
                                fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                                sw = new StreamWriter(fs, Encoding.UTF8);
                            }
                            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " : " + message);
                        }
                        sw.Flush();
                    }
                    if (sw != null) sw.Close();
                    if (fs != null) fs.Close();
                }
                catch { }
                finally
                {
                    if (sw != null) sw.Close();
                    if (fs != null) fs.Close();
                }
            }

        }

    }

}
