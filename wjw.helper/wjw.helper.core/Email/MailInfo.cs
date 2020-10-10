
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wjw.helper.Text;

namespace wjw.helper.Email
{
    /// <summary>
    /// 发送邮件的信息
    /// </summary>
    public class MailInfo
    {
        /// <summary>
        /// 标题
        /// </summary>
        private string _subject;

        /// <summary>
        /// 接收者名字
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 接收者邮箱（多个用英文","号分割）
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject
        {
            get
            {
                if (_subject.IsNullOrEmpty() && _subject.Length > 15)
                {
                    return Body.Substring(0, 15);
                }
                return _subject;
            }
            set { _subject = value; }
        }

        /// <summary>
        /// 正文内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 抄送人集合（多个用英文","分割）
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// 回复地址
        /// </summary>
        public string Replay { get; set; }
    }
}
