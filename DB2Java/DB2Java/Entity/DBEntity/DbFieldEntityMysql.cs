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
    class DbFieldEntityMysql : DbFieldEntity
    {
        public override string TypeConversion()
        {
            if (this == null)
            {
                throw new Exception();
            }
            else if (this.DataType == "varchar" || this.DataType == "char" || this.DataType == "text")
            {
                return "String";
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
                return "Boolean";
            }
            else if (this.DataType == "bigint")
            {
                return "BigInteger";
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
                return "BigDecimal";
            }
            else if (this.DataType == "date" || this.DataType == "year")
            {
                return "Date";
            }
            else if (this.DataType == "time")
            {
                return "Time";
            }
            else if (this.DataType == "datetime"|| this.DataType == "timestamp")
            {
                return "Timestamp";
            }
            else
            {
                return "Object";
            }
        }
    }
}
