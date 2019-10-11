/*
 * Created by SharpDevelop.
 * User: leibf
 * Date: 2018/6/19
 * Time: 16:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace DB2Java.Util
{
	/// <summary>
	/// Description of ConfUtil.
	/// </summary>
	public sealed class ConfUtil
	{
		 private static string INIFileName =
            Path.Combine(Directory.GetCurrentDirectory(), "config.ini");
		 private const string SECTION = "ATTENDANCE";
		 
		private ConfUtil()
		{
		}
		
		public static string GetProfileString(string key)
        {
            if (!File.Exists(INIFileName))
            {
                return "";
            }
            else
            {
                StringBuilder temp = new StringBuilder(3000);
                GetPrivateProfileString(SECTION, key, "", temp, 3000, INIFileName);

                return temp.ToString();
            }
        }
		
		/// <summary>
		/// 属性写入ini文件
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
        public static void WritePrivateProfileString(string key, string value)
        {
            if (!File.Exists(INIFileName))
            {
                throw new Exception();
            }

            WritePrivateProfileString(SECTION, key, value, INIFileName);

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
