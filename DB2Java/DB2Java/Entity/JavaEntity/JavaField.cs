/*
 * 由SharpDevelop创建。
 * 用户： guyue
 * 日期: 2018/8/7
 * 时间: 11:16
 * 
 * 
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
    /// Java字段实体对象模型
    /// </summary>
    public class JavaField
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
        /// 数据类型
        /// </summary>
		public string DataType { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// 字段注释
        /// </summary>
        public string Annotation { get; set; }

        /// <summary>
        /// 是否生成get，set方法
        /// </summary>
        public bool Met { get; set; } = true;

        /// <summary>
        /// 覆盖ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            StringBuilder str = new StringBuilder().
            Append(StrUtil.NewlineCharacter).
            Append(StrUtil.DoubleSeparator).
            Append(this.AccessModifier).
            Append(StrUtil.Separator);

            foreach (string tmp in this.OtherModifier)
            {
                str.Append(tmp + StrUtil.Separator);
            }
            str.Append(this.DataType + StrUtil.Separator + this.Name + ";");
            return str.ToString();
        }

        /// <summary>
        /// 生成get方法的方法
        /// </summary>
        /// <returns></returns>
		public string ToGetMethod()
        {
            StringBuilder str = new StringBuilder().
                Append(StrUtil.NewlineCharacter + StrUtil.DoubleSeparator + "public")
                .Append(StrUtil.Separator + this.DataType + StrUtil.Separator + "get" + StrUtil.InitUpper(this.Name) + "()")
                .Append(StrUtil.NewlineCharacter + StrUtil.DoubleSeparator + "{" + StrUtil.NewlineCharacter + StrUtil.DoubleSeparator)
                .Append(StrUtil.DoubleSeparator + "return" + StrUtil.Separator + "this." + this.Name + ";")
                .Append(StrUtil.NewlineCharacter + StrUtil.DoubleSeparator + "}");


            return str.ToString();
        }

        /// <summary>
        /// 生成set方法的方法
        /// </summary>
        /// <returns></returns>
		public string ToSetMethod()
        {
            StringBuilder str = new StringBuilder().
                Append(StrUtil.NewlineCharacter + StrUtil.DoubleSeparator + "public" + StrUtil.Separator + "void" + StrUtil.Separator)
                .Append("set" + StrUtil.InitUpper(this.Name) + "(" + this.DataType + StrUtil.Separator + this.Name + ")")
                .Append(StrUtil.NewlineCharacter + StrUtil.DoubleSeparator + "{" + StrUtil.NewlineCharacter + StrUtil.DoubleSeparator + StrUtil.DoubleSeparator)
                .Append("this." + this.Name + "=" + this.Name + ";")
                .Append(StrUtil.NewlineCharacter + StrUtil.DoubleSeparator + "}");

            return str.ToString();
        }
    }
}
