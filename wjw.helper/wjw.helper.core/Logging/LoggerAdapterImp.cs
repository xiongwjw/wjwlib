using System;
using System.Collections.Generic;
using System.Text;

namespace wjw.helper.Logging
{
    internal sealed class LoggerAdapterImp : LoggerAdapterBase
    {
        protected override ILog CreateLogger(string name)
        {
            return new LogerImp(name);
        }
    }
}
