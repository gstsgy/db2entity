/*
 * 由SharpDevelop创建。
 * 用户： guyue
 * 日期: 2018/8/7
 * 时间: 11:16
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;

namespace DB2Java.Util
{
	/// <summary>
	/// Description of JavaMethod.
	/// </summary>
	public class JavaMethod
	{
		public string power;
		public List<string> javaStatic = new List<string>();
		public string javaRtype;
		public string name;
		public List<string> javaCType = new List<string>();
		public List<string> javaCname = new List<string>();
		public string methodContent;
		public JavaMethod()
		{
		}
		public override string ToString()
		{

			string diff = " ";
			string tab = "    ";
			string ent = "\r\n";
			string str = ent;
			str +=tab+ this.power + diff;            
			foreach (string  tmp in this.javaStatic) {
				str += tmp + diff;
			}    
			str += this.javaRtype + diff + this.name + diff + "(";
			for (int i = 0; i < javaCType.Count; i++) {
				if (i == javaCType.Count - 1) {
					str += this.javaCType[i] + diff + this.javaCname[i];
				} else {
					str += this.javaCType[i] + diff + this.javaCname[i] + ",";
				}
			}          
			str += ")" + ent +tab+ "{" + ent +tab+ tab + this.methodContent + ent +tab+ "}";            
			return str;
		}
		
	}
}
