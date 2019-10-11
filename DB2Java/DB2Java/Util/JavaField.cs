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
	/// Description of JavaField.
	/// </summary>
	public class JavaField
	{
		public string power;
		public List<string> javaStatic = new List<string>();
		public string javaType;
		public string name;
        public string annotation;

        public bool met;
		public JavaField()
		{
		}
		public override string ToString()
		{
			string diff = " ";
			string tab = "    ";
			string ent = "\r\n";
			string str = ent;
			str += tab+this.power + diff;           
			foreach (string tmp in this.javaStatic) {
				str += tmp + diff;
			}            
			str += this.javaType + diff + this.name + ";";
			return str;
		}
		public string ToGetMethod()
		{
			string diff = " ";
			string tab = "    ";
			string ent = "\r\n";
			string str = ent +tab+ "public" + diff + this.javaType + diff + "get" + InitUpper(this.name) + "()" + ent +tab+ "{" + ent +tab+ tab + "return" + diff + "this." + this.name + ";";
			str += ent +tab+ "}";
			return str;
		}
		public string ToSetMethod()
		{
			string diff = " ";
			string tab = "    ";
			string ent = "\r\n";
			string str = ent +tab+ "public" + diff + "void" + diff + "set" + InitUpper(this.name) + "(" + this.javaType + diff + this.name + ")" + ent +tab+ "{" + ent +tab+ tab + "this." + this.name + "=" + this.name + ";";
			str += ent +tab+ "}";
			return str;
		}
		public string InitUpper(string str)
		{
			if (str == null || str == "") {
				return"";
			} else if (str.Length == 1) {
				return str.ToUpper();
			} else {
				return str[0].ToString().ToUpper() + str.Substring(1, str.Length - 1).ToLower();
			}           
		}
	}
}
