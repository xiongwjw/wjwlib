
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wjw.helper.Extensions
{
    /// <summary>
    /// ReaderWriterLockSlim(读写锁)扩展
    /// </summary>
    public static class ReaderWriterLockSlimExtensions
    {
        #region ReadOnly(只读)
        /// <summary>
        /// 只读操作
        /// </summary>
        /// <param name="readerWriterLockSlim">读写锁</param>
        /// <param name="action">操作</param>
        public static void ReadOnly(this ReaderWriterLockSlim readerWriterLockSlim, Action action)
        {
            if (readerWriterLockSlim == null)
            {
                throw new ArgumentNullException("readerWriterLockSlim");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            readerWriterLockSlim.EnterReadLock();
            try
            {
                action();
            }
            finally
            {
                readerWriterLockSlim.ExitReadLock();
            }
        }
        /// <summary>
        /// 只读操作
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="readerWriterLockSlim">读写锁</param>
        /// <param name="function">Lambda表达式</param>
        /// <returns></returns>
        public static T ReadOnly<T>(this ReaderWriterLockSlim readerWriterLockSlim, Func<T> function)
        {
            if (readerWriterLockSlim == null)
            {
                throw new ArgumentNullException("readerWriterLockSlim");
            }
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }
            readerWriterLockSlim.EnterReadLock();
            try
            {
                return function();
            }
            finally
            {
                readerWriterLockSlim.ExitReadLock();
            }
        }
        #endregion
        #region WriteOnly(只写)
        /// <summary>
        /// 只写操作
        /// </summary>
        /// <param name="readerWriterLockSlim">读写锁</param>
        /// <param name="action">操作</param>
        public static void WriteOnly(this ReaderWriterLockSlim readerWriterLockSlim, Action action)
        {
            if (readerWriterLockSlim == null)
            {
                throw new ArgumentNullException("readerWriterLockSlim");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            readerWriterLockSlim.EnterWriteLock();
            try
            {
                action();
            }
            finally
            {
                readerWriterLockSlim.ExitWriteLock();
            }
        }
        /// <summary>
        /// 只写操作
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="readerWriterLockSlim">读写锁</param>
        /// <param name="function">Lambda表达式</param>
        /// <returns></returns>
        public static T WriteOnly<T>(this ReaderWriterLockSlim readerWriterLockSlim, Func<T> function)
        {
            if (readerWriterLockSlim == null)
            {
                throw new ArgumentNullException("readerWriterLockSlim");
            }
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }
            readerWriterLockSlim.EnterWriteLock();
            try
            {
                return function();
            }
            finally
            {
                readerWriterLockSlim.ExitWriteLock();
            }
        }

        #endregion
    }
}
