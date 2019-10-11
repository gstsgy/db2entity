/*
 * 由SharpDevelop创建。
 * 用户： guyue
 * 日期: 2018/8/7
 * 时间: 11:14
 *  * 
 * 修改者：guyue
 * 修改日期：2019-10-11
 * 修改内容：代码重构（格式方面）
 * 
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;

namespace DB2Entity.Entity.JavaEntity
{
	/// <summary>
    /// java实体对象模型
    /// </summary>
	public class JavaClass
	{
        /// <summary>
        /// 类修饰符
        /// </summary>
		public List<string> AccessModifier { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
		public string ClassKeyword;

        /// <summary>
        /// 
        /// </summary>
		public string name;

        /// <summary>
        /// 
        /// </summary>
		public List<JavaField> ljf = new List<JavaField>();

        /// <summary>
        /// 
        /// </summary>
		public List<JavaMethod> ljm = new List<JavaMethod>();
		
		public override string ToString()
		{
			string diff = " ";
			string tab = "    ";
			string ent = "\r\n";
			string str = "";
			foreach (string tmp in this.AccessModifier) {
				str += tmp + diff;
			}           
			str += ClassKeyword + diff + name + "{";
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
