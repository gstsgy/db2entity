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
using DB2Entity.Util;
using System;
using System.Collections.Generic;
using System.Text;

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
        /// class关键字
        /// </summary>
		public string ClassKeyword { get; set; } = "class";

        /// <summary>
        /// 类名称
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// 类字段
        /// </summary>
		public List<JavaField> Fields { get; set; } = new List<JavaField>();

        /// <summary>
        /// 类方法
        /// </summary>
		public List<JavaMethod> Methods { get; set; } = new List<JavaMethod>();


        /// <summary>
        /// 重写ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
		{
			
            StringBuilder str = new StringBuilder();

            foreach (string tmp in this.AccessModifier) {
                str .Append(tmp + StrUtil.Separator) ;
			}
            str.Append(this.ClassKeyword + StrUtil.Separator +this. Name + "{");
           
			foreach (var tmp in this.Fields) {
                str.Append(tmp .ToString());
			}
			foreach (var tmp in this.Fields) {
				if (tmp.Met) {
                    str.Append(tmp.ToGetMethod());
                    str.Append(tmp.ToSetMethod());
				}
                
			}
			foreach (var tmp in this.Methods ) {
                str.Append(tmp.ToString());
			}
            str.Append(StrUtil.NewlineCharacter + "}");

			return str.ToString();
		}
	}
}
