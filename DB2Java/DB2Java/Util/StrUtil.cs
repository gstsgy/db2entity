/*
 * 由SharpDevelop创建。
 * 用户： guyue
 * 日期: 2019-10-11
 * 时间: 11:14
 *  * 
 * 修改者：guyue
 * 修改日期：2019-10-12
 * 修改内容：代码重构（格式方面）
 * 
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2Entity.Util
{
    /// <summary>
    /// toString 帮助类
    /// </summary>
   public static class StrUtil
    {
        /// <summary>
        /// 分割符（1个空格）
        /// </summary>
        public static readonly string Separator  = " ";

        /// <summary>
        /// 分割符（四个空格）
        /// </summary>
        public static readonly string DoubleSeparator = "    ";

        /// <summary>
        /// 换行符
        /// </summary>
        public static readonly string NewlineCharacter = "\r\n";

        /// <summary>
        /// 单词首字母大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
		public static string InitUpper(string str)
        {
            if (str == null || str == "")
            {
                return "";
            }
            //else if (str.Length == 1)
            //{
            //    return str.ToUpper();
            //}
            else
            {
                return str[0].ToString().ToUpper() + str.Substring(1, str.Length - 1);
            }
        }
    }
}
