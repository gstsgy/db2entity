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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2Entity.Entity.DBEntity
{
    public abstract class DbConnection
    {
        
        /// <summary>
        /// sql执行方法
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns>List表</returns>
        public abstract IList<Dictionary<string,object>> ExecuteReader(string cmdText);
    }
}
