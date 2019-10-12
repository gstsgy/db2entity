/*
 * 
 * User: guyue
 * Date: 2019/6/19
 * Time: 16:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2Entity.Entity.DBEntity
{
    /// <summary>
    /// 数据库字段实体
    /// </summary>
    abstract class DbDataTypeEntity
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength { get; set; }

        /// <summary>
        /// 小数点长度
        /// </summary>
        public int DataScale { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string Annotation { get; set; }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <returns></returns>
        public abstract string TypeConversion();
       

    }
}
