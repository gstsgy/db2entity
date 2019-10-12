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


using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB2Entity.Entity.DBEntity
{
    public class MysqlConnection : DbConnection
    {
        /// <summary>
        /// 连接字符串字段
        /// </summary>
        public DbParamEntity DbParamEntity { get; set; }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="dBparm"></param>
        public MysqlConnection (DbParamEntity dBparm)
        {
            this.DbParamEntity = dBparm;
        }
       
        /// <summary>
        /// 获取MySQL连接
        /// </summary>
        /// <returns></returns>
        private MySqlConnection GetConnection()
        {
            string connString = CreateConnString();
            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public override IList<Dictionary<string, object>> ExecuteReader(string cmdText)
        {
            //查询结果
            IList<Dictionary<string, object>> resList = new List<Dictionary<string, object>>();

            //创建一个MySqlCommand对象
            MySqlCommand cmd = new MySqlCommand();

            //创建一个MySqlConnection对象
            MySqlConnection conn = GetConnection(); 
            
            conn.Open();

            MySqlDataReader dataReader = null;

            MySqlCommand command = null;

            try
            {
                command = conn.CreateCommand();
                command.CommandText = cmdText;
                dataReader = command.ExecuteReader();
               
                while (dataReader.Read())
                {
                    Dictionary<string, object> l = new Dictionary<string, object>();

                   // dataReader.GetFieldValue<>();


                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        try
                        {
                            string str = dataReader.GetValue(i) == null ? "" : dataReader.GetValue(i).ToString();
                            l.Add(dataReader.GetName(i).Trim(),dataReader.GetValue(i));
                            //MessageBox.Show(dataReader.GetName(i).Trim());
                        }
                        catch (Exception)
                        {
                            //list.Add(l);
                            break;
                        }
                    }
                    resList.Add(l);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (!dataReader.IsClosed)
                {
                    dataReader.Close();
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return resList;
        }

        /// <summary>
        /// 创建连接字符串
        /// </summary>
        /// <returns>连接字符串</returns>
        private string CreateConnString()
        {
            StringBuilder str = new StringBuilder("server=");
            str.Append(this.DbParamEntity.Ip);
            str.Append(";port=");
            str.Append(this.DbParamEntity.Port);
            str.Append(";user=");
            str.Append(this.DbParamEntity.Username);
            str.Append(";password=");
            str.Append(this.DbParamEntity.Password);
            str.Append(";database=");
            str.Append(this.DbParamEntity.Database);
            str.Append(";");
            str.Append("SslMode = none;");
            return str.ToString();
        }   
    }
}
