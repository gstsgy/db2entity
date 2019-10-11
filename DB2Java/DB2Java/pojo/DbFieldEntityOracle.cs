using DB2Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strawberry.Util
{
    class DbFieldEntityOracle : DbFieldEntity
    {
        public override string TypeConversion()
        {
            if (this == null)
            {
                throw new Exception();
            }
            else if (this.dataType == "VARCHAR2" || this.dataType == "CHAR")
            {
                return "string";
            }
            else if (this.dataType == "NUMBER")
            {
                if (this.dataScale == 0)
                {
                    if (this.dataLength < 10)
                    {
                        return "int";
                    }
                    else if (this.dataLength < 19)
                    {
                        return "long";
                    }
                    else
                    {
                        return "BigDecimal";
                    }
                }
                else if (this.dataScale < 18)
                {
                    return "double";
                }
                else
                {
                    return "bigDecimal";
                }
            }
            else if (this.dataType == "DATE")
            {
                return "DateTime"; 
            }
            else
            {
                return "Object";
            }
        }
    }
}
