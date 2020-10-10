using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wjw.helper.Extensions;
using wjw.helper.Text;
using wjw.helper;
using wjw.helper.DateTimes;
using wjw.helper.Logging;

namespace wjw.helper.test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //   Test();

            //   TestLog();

            TestCollection();
        }

        private void TestCollection()
        {
            List<int> testList = new List<int>() { 1, 2, 3, 4 };
            testList.ForEach((i) => { i++; });

        }

        private void TestLog()
        {
            Log.Debug("woding");
            TestClass t = new TestClass();
            t.Name = "wjw";
            Log.Debug(t);
            try
            {
                int c = 0;
                int b = 1 / c;
            }
            catch (System.Exception ex)
            {
                Log.Debug(ex.Message, ex);
                    
            }

            Log.SetLoger("mylog");
            Log.Debug("aaaaaa");

        }

        private void Test2()
        {
           object b=  DataCache.Get("bb");
            TestClass adf = new TestClass();
            adf.Name ="woding";
            DataCache.Set("bb", adf);


            TestClass ss = DataCache.Get<TestClass>("bb");
            TestClass ui = ss.Clone<TestClass>();
            ui.Name = "ffffffffffff";
          //  MessageBox.Show(ui.Name);

            string basdf = ui.ToXml();

            string dddddddddd = ui.ToJson();

        }

        private void Test()
        {


            int b = Conv.ToInt("23");
            //Singleton<TestClass>.Instance = new TestClass();
            //Singleton<TestClass>.Instance.Print();

            //Singleton<TestClass2>.Instance = new TestClass2();
            //Singleton<TestClass2>.Instance.Print();
            TestClass aa = new TestClass();
            aa.Name = "woding";
            TestClass bb = Sys.Clone<TestClass>(aa);
          //  TestClass bb = aa;
            bb.Name = "nihao";
            MessageBox.Show(aa.Name);
            MessageBox.Show(bb.Name);

            TestClass kdfsdf = Conv.To<TestClass>(aa);
            MessageBox.Show(kdfsdf.Name);
            if (Valid.IsIpAddress("fasdfasfd"))
                MessageBox.Show("ok");
            else
                MessageBox.Show("no");
            Valid.IsDate("fasfdaf");
        }
    }

    [Serializable]
    public class TestClass
        {
        public string Name { get; set; }
        public void Print()
        {
            MessageBox.Show("TestClass");
        }
        }
    public class TestClass2
    {

        public void Print()
        {
            MessageBox.Show("TestClass2");
        }
    }

}
