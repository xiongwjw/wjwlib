using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wjw.loger;


namespace wjw.loger.test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitLog();
            Test();
            TestLog4Net();
        }

        private void InitLog()
        {
            Log.Prefix = "wjw";
            Log.LogLevel = LogLevel.Info;
            Log.MaxLength = 20;
            Log.OnWriteLog += WriteToEditor;
        }

        private void TestLog4Net()
        {
            Log4Net.Debug("debug log");
            Log4Net.Info("info log");
            Log4Net.Warn("Warn log");
            Log4Net.Error("Error log");
            Log4Net.Fatal("Fatal log");
            Log4Net.DebugInfo("Debug log{0}", 2);
        }

        private void WriteToEditor(string message)
        {
            rtLog.AppendText(message);
        }

        private void Test()
        {
            Log.Debug("fasfasdfasf");
            Log.Info("aaaa");
        }
    }
}
