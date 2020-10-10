
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Email
{
    /// <summary>
    /// 邮件服务设置
    /// </summary>
    public class EmailServiceSettings
    {
        /// <summary>
        /// 主机名，如：smtp.163.com
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口号，如：25
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 是否使用默认端口，如果设为false，默认25端口
        /// </summary>

        public bool UsePort { get; set; }
        
        /// <summary>
        /// 是否启用SSL，默认：false
        /// 如果启用，端口号要改为加密方式发送
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// 是否包含Html代码
        /// </summary>
        public bool IsHtml { get; set; }

        /// <summary>
        /// 发送者显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 初始化一个<see cref="EmailServiceSettings"/>类型的实例
        /// </summary>

        public EmailServiceSettings()
        {
            UsePort = false;
        }

        /// <summary>
        /// 初始化一个<see cref="EmailServiceSettings"/>类型的实例
        /// </summary>
        /// <param name="host">主机名，如：smtp.163.com</param>
        /// <param name="port">端口号</param>
        public EmailServiceSettings(string host, int port)
        {
            Host = host;
            Port = port;
        }
    }
}
