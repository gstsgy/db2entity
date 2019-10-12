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
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace DB2Entity.Entity.DBEntity
{
	/// <summary>
	/// Description of DbUtil.
	/// </summary>
	public class OracleConnection : DbConnection
    {
        /// <summary>
        /// 连接字符串字段
        /// </summary>
        public DbParamEntity DbParamEntity { get; set; }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="dBparm"></param>
        public OracleConnection (DbParamEntity dBparm)
		{
            this.DbParamEntity = dBparm;
        }

        /// <summary>
        /// 获取Oracle连接
        /// </summary>
        /// <returns></returns>
		private Oracle.ManagedDataAccess.Client.OracleConnection GetConnection()
		{
			string connString = CreateConnString();
            Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client. OracleConnection(connString);
			return conn;
		}

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
		public DataTable ExecuteDataTable(string sql)
		{
			try {
				using (Oracle.ManagedDataAccess.Client.OracleConnection conn = GetConnection()) {
					conn.Open();
					using (OracleCommand cmd = conn.CreateCommand()) {
						cmd.CommandText = sql;						
						OracleDataAdapter adapter = new OracleDataAdapter(cmd);
						DataTable datatable = new DataTable();
						adapter.Fill(datatable);
						return datatable;
					}
				}
			} catch (Exception ee) {
				MessageBox.Show(ee.Message);
				return null;
			}
		}

        /// <summary>
        /// 创建连接字符串
        /// </summary>
        /// <returns></returns>
		private string CreateConnString()
		{
			StringBuilder str = new StringBuilder("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=");
			str.Append(this.DbParamEntity.Ip);
			str.Append(")(PORT=");
			str.Append(this.DbParamEntity.Port);
			str.Append("))(CONNECT_DATA=(SERVICE_NAME=");
			str.Append(this.DbParamEntity.Database);
			str.Append(")));Persist Security Info=True;User ID=");
			str.Append(this.DbParamEntity.Username);
			str.Append(";Password=");
			str.Append(this.DbParamEntity.Password);
			str.Append(";");
			return str.ToString();
		}

     
        /// <summary>
        /// 解析dataset数据并返回
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public override IList<Dictionary<string, object>> ExecuteReader(string cmdText)
        {
            IList<Dictionary<string, object>> resList =new List<Dictionary<string, object>>();
            DataTable tb= ExecuteDataTable(cmdText);
            if (tb != null)
            {
                foreach(DataRow dr in tb.Rows)
                {
                    Dictionary<string, object> l = new Dictionary<string, object>();
                    
                   
                    for (int i = 0; i < tb.Columns.Count; i++)
                    {
                        l.Add(tb.Columns[i].ColumnName,dr[i]);
                    }
                    resList.Add(l);
                }
            }
            return resList;
        }
    }
}
