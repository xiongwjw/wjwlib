using wjw.loger;
using System;

namespace wjw.loger.test.core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Log4Net.Debug("test");
            Log.Debug("woding");

        }
    }
}
