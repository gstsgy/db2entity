/*
 * Created by SharpDevelop.
 * User: leibf
 * Date: 2018/6/19
 * Time: 16:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace DB2Java.Util
{
	/// <summary>
	/// Description of DbUtil.
	/// </summary>
	public class DbOracle: SuperDb
    {
		public string ORACLE_SERVER_IP;
		public string ORACLE_SERVER_PORT;
		public string ORACLE_SERVER_SERVICENAME;
		public string ORACLE_USERID;
		public string ORACLE_PASSWORD;

        public DbOracle()
		{
        }

        public override void InitDbInfo(DbParamEntity dBparm)
        {
			ORACLE_SERVER_IP = dBparm.ip;
			ORACLE_SERVER_PORT = dBparm.port;
			ORACLE_SERVER_SERVICENAME = dBparm.database;
			ORACLE_USERID = dBparm.username;
			ORACLE_PASSWORD = dBparm.password;
		}
		
		private OracleConnection GetConnection()
		{
			string connString = CreateConnString();
			OracleConnection conn = new OracleConnection(connString);
			return conn;
		}

		public DataTable ExecuteDataTable(string sql)
		{
			try {
				using (OracleConnection conn = GetConnection()) {
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
		private string CreateConnString()
		{
			StringBuilder str = new StringBuilder("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=");
			str.Append(ORACLE_SERVER_IP);
			str.Append(")(PORT=");
			str.Append(ORACLE_SERVER_PORT);
			str.Append("))(CONNECT_DATA=(SERVICE_NAME=");
			str.Append(ORACLE_SERVER_SERVICENAME);
			str.Append(")));Persist Security Info=True;User ID=");
			str.Append(ORACLE_USERID);
			str.Append(";Password=");
			str.Append(ORACLE_PASSWORD);
			str.Append(";");
			return str.ToString();
		}

     
        public override List<List<string>> ExecuteReader(string cmdText)
        {
            List<List<string>> list=new List<List<string>>();
            DataTable tb= ExecuteDataTable(cmdText);
            if (tb != null)
            {
                foreach(DataRow dr in tb.Rows)
                {
                    List<string> l = new List<string>();
                    
                   
                    for (int i = 0; i < tb.Columns.Count; i++)
                    {
                        l.Add(dr[i].ToString());
                    }
                    list.Add(l);
                }
            }
            return list;
        }
    }
}
