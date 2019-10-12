using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2Java.Util
{
    public class MysqlConnection : SuperDb
    {
        public  string ORACLE_SERVER_IP;
        public  string ORACLE_SERVER_PORT;
        public  string ORACLE_SERVER_SERVICENAME;
        public  string ORACLE_USERID;
        public  string ORACLE_PASSWORD;
        public MysqlConnection ()
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
        private MySqlConnection GetConnection()
        {
            string connString = CreateConnString();
            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }
        public override List<List<string>> ExecuteReader(string cmdText)
        {
            List<List<string>> list = new List<List<string>>();
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
                Console.WriteLine();
                while (dataReader.Read())
                {
                    List<string> l = new List<string>();
                    for(int i = 0; i < 4; i++)
                    {
                        try
                        {
                            string str = dataReader.GetValue(i) == null ? "0" : dataReader.GetValue(i).ToString();
                            l.Add(str);
                        }
                        catch (Exception)
                        {
                            //list.Add(l);
                            break;
                        }
                    }
                    list.Add(l);
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
            return list;
        }
        private string CreateConnString()
        {
            StringBuilder str = new StringBuilder("server=");
            str.Append(ORACLE_SERVER_IP);
            str.Append(";port=");
            str.Append(ORACLE_SERVER_PORT);
            str.Append(";user=");
            str.Append(ORACLE_USERID);
            str.Append(";password=");
            str.Append(ORACLE_PASSWORD);
            str.Append(";database=");
            str.Append(ORACLE_SERVER_SERVICENAME);
            str.Append(";");
            str.Append("SslMode = none;");
            return str.ToString();
        }   
    }
}
