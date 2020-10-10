using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using wjw.helper.Extensions;
using wjw.helper.Exceptions;

namespace wjw.helper.Logging
{
    internal sealed class LogerImp : LogBase
    {
        public override bool IsDataLogging { get; } = true;

        public override bool IsTraceEnabled { get; } = true;

        public override bool IsDebugEnabled { get; } = true;

        public override bool IsInfoEnabled { get; } = true;

        public override bool IsWarnEnabled { get; } = true;

        public override bool IsErrorEnabled { get; } = true;

        public override bool IsFatalEnabled { get; } = true;

        private bool isRuning = false;
        private CancellationTokenSource s1 = new CancellationTokenSource();

        private Queue<string> logMessageQueue = new Queue<string>();
        private AutoResetEvent queueEvent = new AutoResetEvent(false);

        public int MaxLength { get; set; } = 0;
        public bool IsSeperateFolder { get; set; }
        public string Prefix { get; set; }


        protected override void Write(LogLevel level, object message, Exception exception, bool isData = false)
        {
            if (message == null)
                return;
            string msg = ParseObject(message);
            Log($"[{level.ToString()}]  {msg}");
            if (exception != null)
                Log($"[{level.ToString()}]  {exception.ToUnwrappedString()}");
        }

        private  string ParseObject(object obj)
        {
            if (obj is ValueType || obj is string)
                return obj.ToStr();
            else
            {
                return obj.ToJson();
            }
        }


        internal LogerImp(string name)
        {
            IsSeperateFolder = false;
            Prefix = name;
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

        private void Log(string message)
        {
            if (MaxLength > 0)
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
