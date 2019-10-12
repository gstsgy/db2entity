/*
 * 由SharpDevelop创建。
 * 用户： guyue
 * 日期: 2018/8/7
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
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace DB2Entity.Util
{
    /// <summary>
    /// 配置文件读写工具类
    /// </summary>
    public sealed class ConfUtil
    {
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private static string FilePath =
           Path.Combine(Directory.GetCurrentDirectory(), "config.ini");

        /// <summary>
        /// 配置文件session
        /// </summary>
        private const string Session = "DB2Entity";
		 
        /// <summary>
        /// 私有构造方法 
        /// </summary>
		private ConfUtil()
		{
		}
		
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="key">key值</param>
        /// <returns>value值</returns>
		public static string GetProfile(string key)
        {
            if (!File.Exists(FilePath))
            {
                return "";
            }
            else
            {
                StringBuilder temp = new StringBuilder();
                GetPrivateProfileString(Session, key, "", temp, int.MaxValue, FilePath);

                return temp.ToString();
            }
        }
		
		/// <summary>
		/// 属性写入ini文件
		/// </summary>
		/// <param name="key">key值</param>
		/// <param name="value">value值</param>
        public static void WriteProfile(string key, string value)
        {
            if (!File.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath); 
            }

            WritePrivateProfileString(Session, key, value, FilePath);

            return;
        }
		
		[DllImport("kernel32 ")]
        private static extern int GetPrivateProfileString(
            string section,
            string key,
            string p,
            StringBuilder temp,
            int size,
            string FileName);
		
		[DllImport("kernel32")]
        private static extern int WritePrivateProfileString(
          string strSection,
          string strKey,
          string strValue,
          string FileName
          );
	}
}
