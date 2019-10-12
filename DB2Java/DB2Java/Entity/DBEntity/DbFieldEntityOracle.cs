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





using DB2Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2Entity.Entity.DBEntity
{
    /// <summary>
    /// Oracle 与 Java 数据类型转换
    /// </summary>
    class DbFieldEntityOracle : DbFieldEntity
    {
        public override string TypeConversion()
        {
            if (this == null)
            {
                throw new Exception();
            }
            else if (this.DataType == "VARCHAR2" || this.DataType == "CHAR")
            {
                return "String";
            }
            else if (this.DataType == "NUMBER")
            {
                if (this.DataScale == 0)
                {
                    if (this.DataLength < 10)
                    {
                        return "int";
                    }
                    else if (this.DataLength < 19)
                    {
                        return "long";
                    }
                    else
                    {
                        return "BigDecimal";
                    }
                }
                else if (this.DataScale < 18)
                {
                    return "double";
                }
                else
                {
                    return "BigDecimal";
                }
            }
            else if (this.DataType == "DATE")
            {
                return "Time"; 
            }
            else
            {
                return "Object";
            }
        }
    }
}
