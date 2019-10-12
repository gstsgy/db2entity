/*
 * 由SharpDevelop创建。
 * 用户： guyue
 * 日期: 2018/8/7
 * 时间: 11:16
 * 
 *  * 
 * 修改者：guyue
 * 修改日期：2019-10-12
 * 修改内容：代码重构（格式方面）
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using DB2Entity.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB2Entity.Entity.ProgramEntity
{
	/// <summary>
	/// Java方法对应实体对象
	/// </summary>
	public class MethodEntity
	{
        /// <summary>
        /// 访问修饰符
        /// </summary>
		public string AccessModifier { get; set; }

        /// <summary>
        /// 其他修饰符
        /// </summary>
		public List<string> OtherModifier { get; set; } = new List<string>();

        /// <summary>
        /// 返回值类型
        /// </summary>
		public string ReturnType { get; set; }

        /// <summary>
        /// 方法名
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
		public List<string> ParameterTypes { get; set; } = new List<string>();

        /// <summary>
        /// 参数名成
        /// </summary>
		public List<string> ParameterNames { get; set; } = new List<string>();

        /// <summary>
        /// 方法体
        /// </summary>
		public string MethodContent { get; set; }


        /// <summary>
        /// 重写ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
		{
            StringBuilder str = new StringBuilder();

            str.Append(StrUtil.NewlineCharacter);

            str.Append(StrUtil.DoubleSeparator + this.AccessModifier + StrUtil.Separator);
                     
			foreach (string  tmp in this.OtherModifier) {
                str.Append(tmp + StrUtil.Separator);
			}

            str.Append(this.ReturnType + StrUtil.Separator + this.Name + StrUtil.Separator + "(");
            
			for (int i = 0; i < ParameterTypes .Count; i++) {
				if (i == ParameterTypes .Count - 1) {
                    str.Append(this.ParameterTypes[i] + StrUtil.Separator + this.ParameterNames[i]);
                   
				} else {
                    str.Append(this.ParameterTypes [i] + StrUtil.Separator + this.ParameterNames[i] + ",");
                    
				}
			}

            str.Append(")" + StrUtil.NewlineCharacter + StrUtil.DoubleSeparator + "{" + StrUtil.NewlineCharacter + StrUtil.DoubleSeparator 
                + StrUtil.DoubleSeparator + this.MethodContent + StrUtil.NewlineCharacter + StrUtil.DoubleSeparator + "}");
                       
			return str.ToString();
		}
		
	}
}
