/*
 * 
 * User: guyue
 * Date: 2019/6/19
 * Time: 16:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
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
    /// mysql 与 java 数据类型转换
    /// </summary>
    class Mysql2CSharpEntity : DbDataTypeEntity
    {
        public override string TypeConversion()
        {
            if (this == null)
            {
                throw new Exception();
            }
            else if (this.DataType == "varchar" || this.DataType == "char" || this.DataType == "text")
            {
                return "string";
            }
            else if (this.DataType == "blob")
            {
                return "byte[]";
            }
            else if (this.DataType == "integer" || this.DataType == "id")
            {
                return "long";
            }
            else if (this.DataType == "tinyint" || this.DataType == "smllint" || this.DataType == "mediumint" || this.DataType == "int")
            {
                return "int";
            }
            else if (this.DataType == "bit")
            {
                return "bool";
            }
            else if (this.DataType == "bigint")
            {
                return "long";
            }
            else if (this.DataType == "float")
            {
                return "float";
            }
            else if (this.DataType == "double")
            {
                return "double";
            }
            else if (this.DataType == "decimal")
            {
                return "decimal";
            }
            else if (this.DataType == "date" || this.DataType == "year")
            {
                return "DateTime";
            }
            else if (this.DataType == "time")
            {
                return "DateTime";
            }
            else if (this.DataType == "datetime"|| this.DataType == "timestamp")
            {
                return "DateTime";
            }
            else
            {
                return "object";
            }
        }
    }
}
