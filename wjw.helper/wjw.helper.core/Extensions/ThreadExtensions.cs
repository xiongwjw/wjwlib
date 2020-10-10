
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wjw.helper.Extensions
{
    /// <summary>
    /// 线程（Thread）扩展
    /// </summary>
    public static class ThreadExtensions
    {
        #region CancelSleep(取消线程睡眠状态)
        /// <summary>
        /// 取消线程睡眠状态，继续线程
        /// </summary>
        /// <param name="thread">线程</param>
        public static void CancelSleep(this Thread thread)
        {
            if (thread.ThreadState != ThreadState.WaitSleepJoin)
            {
                return;
            }
            thread.Interrupt();
        }
        #endregion
        #region StartAndIgnoreAbort(启动线程)
        /// <summary>
        /// 启动线程，自动忽略停止线程时触发的ThreadAbortException异常
        /// </summary>
        /// <param name="thread">线程</param>
        /// <param name="failAction">引发非ThreadAbortException异常时执行的逻辑</param>
        public static void StartAndIgnoreAbort(this Thread thread, Action<Exception> failAction = null)
        {
            try
            {
                thread.Start();
            }
            catch (ThreadAbortException)
            { }
            catch (Exception e)
            {
                if (failAction != null)
                {
                    failAction(e);
                }
            }
        }
        #endregion
    }
}
