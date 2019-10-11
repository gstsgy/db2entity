using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2Entity.Entity.DBEntity
{
    /// <summary>
    /// 数据连接参数
    /// </summary>
     public class DbParamEntity
    {
        /// <summary>
        /// ip
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 数据库名
        /// </summary>
        public string Database { get; set; }

    }
}
