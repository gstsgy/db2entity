/*
 * 由SharpDevelop创建。
 * 用户： guyue
 * 日期: 2018/8/7
 * 时间: 11:14
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;

namespace DB2Java.Util
{
	/// <summary>
	/// Description of JavaClass.
	/// </summary>
	public class JavaClass
	{
		public List<string> st = new  List<string>();
		public string ty;
		public string name;
		public List<JavaField> ljf = new List<JavaField>();
		public List<JavaMethod> ljm = new List<JavaMethod>();
		public JavaClass()
		{
		}
		public override string ToString()
		{
			string diff = " ";
			string tab = "    ";
			string ent = "\r\n";
			string str = "";
			foreach (string tmp in this.st) {
				str += tmp + diff;
			}           
			str += ty + diff + name + "{";
			foreach (var tmp in ljf) {
				str += tmp.ToString();
			}
			foreach (var tmp in ljf) {
				if (tmp.met) {
					str += tmp.ToGetMethod();
					str += tmp.ToSetMethod();
				}
                
			}
			foreach (var tmp in ljm) {
				str += tmp.ToString();
			}
			str += ent + "}";
			return str;
		}
	}
}
