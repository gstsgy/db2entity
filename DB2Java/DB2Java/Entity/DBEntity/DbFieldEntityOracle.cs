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
