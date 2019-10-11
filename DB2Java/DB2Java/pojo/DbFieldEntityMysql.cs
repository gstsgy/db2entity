using DB2Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strawberry.Util
{
    class DbFieldEntityMysql : DbFieldEntity
    {
        public override string TypeConversion()
        {
            if (this == null)
            {
                throw new Exception();
            }
            else if (this.dataType == "varchar" || this.dataType == "char" || this.dataType == "text")
            {
                return "String";
            }
            else if (this.dataType == "blob")
            {
                return "byte[]";
            }
            else if (this.dataType == "integer" || this.dataType == "id")
            {
                return "long";
            }
            else if (this.dataType == "tinyint" || this.dataType == "smllint" || this.dataType == "mediumint" || this.dataType == "int")
            {
                return "int";
            }
            else if (this.dataType == "bit")
            {
                return "Boolean";
            }
            else if (this.dataType == "bigint")
            {
                return "BigInteger";
            }
            else if (this.dataType == "float")
            {
                return "float";
            }
            else if (this.dataType == "double")
            {
                return "double";
            }
            else if (this.dataType == "decimal")
            {
                return "BigDecimal";
            }
            else if (this.dataType == "date" || this.dataType == "year")
            {
                return "Date";
            }
            else if (this.dataType == "time")
            {
                return "Time";
            }
            else if (this.dataType == "datetime"|| this.dataType == "timestamp")
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
