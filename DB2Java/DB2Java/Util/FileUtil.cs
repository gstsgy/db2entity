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
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DB2Java.Util
{
	/// <summary>
	/// 文件读写工具类
	/// </summary>
	public sealed class FileUtil
	{
		private FileUtil()
		{
		}
		
		/// <summary>
		/// 读取文件内容
		/// </summary>
		/// <param name="fileAllPath">文件全路径名</param>
		/// <param name="encoding">编码</param>
		/// <returns></returns>
		public static string ReadFile(string fileAllPath, Encoding encoding)
		{
			string content = "";
			StreamReader sr = null;
			try {
				if (encoding == null) {
					encoding = Encoding.Default;
				}
				sr = new StreamReader(fileAllPath, encoding);
				string lineStr;
				while ((lineStr = sr.ReadLine()) != null) {
					content = content + lineStr.TrimEnd() + "\n";
				}
			} catch (Exception ee) {

			} finally {
				if (sr != null) {
					sr.Close();
				}
			}

			return content;
		}
        /// <summary>
		/// 读取文件内容
		/// </summary>
		/// <param name="fileAllPath">文件全路径名</param>
		/// <param name="encoding">编码</param>
		/// <returns></returns>
		public static List<string> ReadFileLine(string fileAllPath, Encoding encoding)
        {
            List<string> content = new List<string>();
            StreamReader sr = null;
            try
            {
                if (encoding == null)
                {
                    encoding = Encoding.Default;
                }
                sr = new StreamReader(fileAllPath, encoding);
                string lineStr;
                while ((lineStr = sr.ReadLine()) != null)
                {
                    content.Add(lineStr);
                    // content = content + lineStr.Trim() + "\n";
                }
            }
            catch (Exception ee)
            {

            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }

            return content;
        }
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="fileAllPath">文件全路径名</param>
        /// <returns></returns>
        public static string ReadFile(string fileAllPath)
		{
			return ReadFile(fileAllPath, Encoding.Default);
		}

		/// <summary>
		/// 字符串写入文件
		/// </summary>
		/// <param name="fileAllPath">文件全路径名</param>
		/// <param name="data">要写入的字符</param>
		/// <param name="encoding">编码</param>
		/// <param name="fileMode">模式</param>
		public static string  WriteFile(string fileAllPath, string data, Encoding encoding, FileMode fileMode)
		{
			string errorMsg = "";
			FileStream fs = null;
			try {
				if (encoding == null) {
					encoding = Encoding.Default;
				}
				//这里的FileMode.create是创建这个文件,如果文件名存在则覆盖重新创建
				fs = new FileStream(fileAllPath, fileMode);
				//存储时时二进制,所以这里需要把我们的字符串转成二进制
				byte[] bytes = encoding.GetBytes(data);
				fs.Write(bytes, 0, bytes.Length);
			} catch (Exception ee) {
				errorMsg = ee.Message;
			} finally {
				if (fs != null) {
					fs.Close();
				}
			}
			
			return errorMsg;
		}
        public static void CreateDirectory()
        {
            string subPath = Directory.GetCurrentDirectory() + "/classes";
            if (false == Directory.Exists(subPath))
            {
                //创建pic文件夹
                Directory.CreateDirectory(subPath);
            }
        }

        /// <summary>
        /// 读取模板文件
        /// </summary>
        /// <returns></returns>
        public static string ReadTemplate()
        {
            string path = Directory.GetCurrentDirectory() + "/Template.conf";
            List<string> res = ReadFileLine(path,Encoding.UTF8);
            res.RemoveAt(res.Count - 1);
            string content = "";
            foreach(string str in res)
            {
                content += str + "\r\n";
            }
            return content.Replace("#",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString());
        }
    }
}
