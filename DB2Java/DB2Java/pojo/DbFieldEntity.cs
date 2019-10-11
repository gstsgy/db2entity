/**
 * 
 *  数据库字段实体
 * 
 * 
 * 
 * 
 * */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2Java.Util
{
    abstract class DbFieldEntity
    {
        public string name;  // 字段名称

        public string dataType;   // 字段类型

        public int dataLength;    // 数据长度

        public int dataScale;    // 小数长度

        public string annotation; // 注释

        public abstract string TypeConversion(); // 类型转换
       

    }
}
