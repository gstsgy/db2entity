using DB2Entity.Entity.DBEntity;
using DB2Entity.Entity.ProgramEntity;
using DB2Java.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB2Entity
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        private DbConnection connection = null;

        /// <summary>
        /// 数据库字段转换成程序字段
        /// </summary>
        private DbDataTypeEntity dbDataTypeEntity = null;

        /// <summary>
        /// 查找用户所有数据表信息sql
        /// </summary>
        private string getTablesSql;

        /// <summary>
        /// 查询表字段详细数据信息sql
        /// </summary>
        private string getTableInfosSql;

        /// <summary>
        /// 数据结果集
        /// </summary>
        private IList<Dictionary<string, object>> resList = new List<Dictionary<string, object>>();

        /// <summary>
        /// 数据库连接字符串参数
        /// </summary>
        Dictionary<string, DbParamEntity> dbParams = new Dictionary<string, DbParamEntity>();
        public Form1()
        {
            InitializeComponent();
            Init();
            FileUtil.CreateDirectory();
        }
       
     

       
        private void button2_Click(object sender, EventArgs e)
        {
           
            if (checkedListBox1.Items.Count == 0)
            {
                MessageBox.Show("没有数据表信息！");
                return;
            }
            bool choice=false;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    string tsql = getTableInfosSql + checkedListBox1.GetItemText(checkedListBox1.Items[i]) + "'";
                    //MessageBox.Show(sql);
                    CSharpEntity j = CreatCSharp(connection.ExecuteReader(tsql));
                    
                    j.AccessModifier.Add("public");
                    j.Name = InitUpper(checkedListBox1.GetItemText(checkedListBox1.Items[i]));
                    string filename = Path.Combine(Directory.GetCurrentDirectory() + "\\classes", j.Name);
                    filename += ".cs";
                    string str = j.ToString();
                    //str = str.Replace("#", "\r\n\t");
                    File.WriteAllText(filename, str);
                    choice = true;
                }
 
                
                
            }
            if (!choice)
            {
                MessageBox.Show("没有选中表");
            }
            else
            {
                MessageBox.Show("保存成功");
            }
        }
        private JavaEntity CreatJava(IList<Dictionary<string, object>> l)
        {
            if (l == null || l.Count == 0)
            {
                return null;
            }
            JavaEntity j = new JavaEntity();
            foreach(Dictionary<string, object> tmp in l)
            {               
                dbDataTypeEntity.Name = tmp["NAME"].ToString();
                dbDataTypeEntity.DataType = tmp["TYPE"].ToString();
                dbDataTypeEntity.DataLength = (tmp["LENGTH"]== null|| tmp["LENGTH"].ToString() == "")?0:int.Parse(tmp["LENGTH"].ToString());
                dbDataTypeEntity.DataScale= (tmp["SCALE"] == null || tmp["SCALE"].ToString()== "") ? 0 : int.Parse(tmp["SCALE"].ToString());
                FieldEntity jf = new FieldEntity();
                jf.DataType = dbDataTypeEntity.TypeConversion();
                jf.AccessModifier = "private";
                jf.Name = dbDataTypeEntity.Name.ToLower();
                jf.Annotation = tmp["COMMENTS"].ToString();
                j.Fields.Add(jf);              
            }
            return j;
        }
        private CSharpEntity CreatCSharp(IList<Dictionary<string, object>> l)
        {
            if (l == null || l.Count == 0)
            {
                return null;
            }
            CSharpEntity j = new CSharpEntity();
            foreach (Dictionary<string, object> tmp in l)
            {
                dbDataTypeEntity.Name = tmp["NAME"].ToString();
                dbDataTypeEntity.DataType = tmp["TYPE"].ToString();
                dbDataTypeEntity.DataLength = (tmp["LENGTH"] == null || tmp["LENGTH"].ToString() == "") ? 0 : int.Parse(tmp["LENGTH"].ToString());
                dbDataTypeEntity.DataScale = (tmp["SCALE"] == null || tmp["SCALE"].ToString() == "") ? 0 : int.Parse(tmp["SCALE"].ToString());
                FieldEntity jf = new FieldEntity();
                jf.DataType = dbDataTypeEntity.TypeConversion();
                jf.AccessModifier = "private";
                jf.Name = dbDataTypeEntity.Name.ToLower();
                jf.Annotation = tmp["COMMENTS"].ToString();
                j.Fields.Add(jf);
            }
            return j;
        }
        private string InitUpper(string str)
        {
            if (str == null || str == "")
            {
                return "";
            }
            else if (str.Length == 1)
            {
                return str.ToUpper();
            }
            else
            {
                return str[0].ToString().ToUpper() + str.Substring(1, str.Length - 1).ToLower();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.Items.Count == 0)
            {
                MessageBox.Show("没有数据表信息！");
                return;
            }
            bool choice = false;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    string tsql = getTableInfosSql + checkedListBox1.GetItemText(checkedListBox1.Items[i]) + "'";
                   // MessageBox.Show(tsql);
                    CSharpEntity j = CreatCSharp(connection.ExecuteReader(tsql));
                    
                    j.AccessModifier.Add("public");
                    j.Name = InitUpper(checkedListBox1.GetItemText(checkedListBox1.Items[i]));
                    string filename = Path.Combine(Directory.GetCurrentDirectory() + "\\classes", j.Name);
                    filename += ".cs";
                    string str = j.ToString();
                    //str = str.Replace("#", "\r\n\t");
                    // File.WriteAllText(filename, str);
                    MessageBox.Show(str);
                    choice = true;
                }
            }
            if (!choice)
            {
                MessageBox.Show("没有选中表");
            }
            else
            {
                MessageBox.Show("预览结束");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //空校验
            foreach (Control tmp in this.groupBox1.Controls)
            {
                if (tmp is TextBox)
                {
                    if (tmp.Text == "" || tmp.Text == null)
                    {
                        MessageBox.Show("参数不能为空！");
                        return;
                    }
                }
            }
            if (!dbParams.ContainsKey("Oracle"))
            {
                dbParams.Add("Oracle", new DbParamEntity());
            }
            dbParams["Oracle"].Ip = textBox1.Text.Trim();
            dbParams["Oracle"].Port = textBox2.Text.Trim();
            dbParams["Oracle"].Username = textBox3.Text.Trim();
            dbParams["Oracle"].Password = textBox4.Text.Trim();
            dbParams["Oracle"].Database = textBox5.Text.Trim();
           // string jsonData = JsonConvert.SerializeObject(oneList);
            string path = Directory.GetCurrentDirectory() + "/db.conf";
            FileUtil.WriteFile(path,JsonConvert.SerializeObject(dbParams),null,FileMode.Create);

            getTablesSql = "select table_name name from user_tables";
            connection = new OracleConnection(dbParams["Oracle"]);
           
            try
            {
                resList = connection.ExecuteReader(getTablesSql);
            }
            catch (Exception e1)
            {
                MessageBox.Show("参数有误------>" + e1.Message);
            }
            GetTable(resList);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //空校验
            foreach (Control tmp in this.groupBox2.Controls)
            {
                if (tmp is TextBox)
                {
                    if (tmp.Text == "" || tmp.Text == null)
                    {
                        MessageBox.Show("参数不能为空！");
                        return;
                    }
                }
            }
            if (!dbParams.ContainsKey("MySql"))
            {
                dbParams.Add("MySql", new DbParamEntity());
            }
            dbParams["MySql"].Ip = textBox10.Text.Trim();
            dbParams["MySql"].Port = textBox9.Text.Trim();
            dbParams["MySql"].Username = textBox8.Text.Trim();
            dbParams["MySql"].Password = textBox7.Text.Trim();
            dbParams["MySql"].Database = textBox6.Text.Trim();
            string path = Directory.GetCurrentDirectory() + "/db.conf";
            FileUtil.WriteFile(path, JsonConvert.SerializeObject(dbParams), null, FileMode.Create);
              getTablesSql = "select table_name NAME from information_schema.tables where table_schema = '" + dbParams["MySql"].Database + "'"; ;
               connection = new MysqlConnection (dbParams["Oracle"]);
           
          
            try
            {
                resList = connection.ExecuteReader(getTablesSql);
            }
            catch (Exception e1)
            {
                MessageBox.Show("参数有误------>" + e1.Message);
            }
            GetTable(resList);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox7.PasswordChar = new char();
                this.checkBox2.Text = "隐藏密码";
            }
            else
            {
                textBox7.PasswordChar = '*';
                this.checkBox2.Text = "显示密码";
            }
        }
        
        /// <summary>
        /// 密码框的隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox4.PasswordChar = new char();
                this.checkBox1.Text = "隐藏密码";
            }
            else
            {
                textBox4.PasswordChar = '*';
                this.checkBox1.Text = "显示密码";
            }
        }
        
        void GetTable(IList<Dictionary<string, object>> list)
        {
            this.checkedListBox1.Items.Clear();
            if (list != null)
            {
                foreach (var tmp in list)
                {
                    this.checkedListBox1.Items.Add(tmp["NAME"]);
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                getTablesSql = "select table_name from user_tables";
                
                
                connection = new OracleConnection(dbParams["Oracle"]);
                //if (dbParams.ContainsKey("Oracle"))
                //{
                //    connection.InitDbInfo(dbParams["Oracle"]);
                //}
                
                dbDataTypeEntity = new Oracle2CSharpEntity();
                getTableInfosSql = "select A.COLUMN_NAME name,A.DATA_TYPE type,A.DATA_LENGTH length,A.DATA_SCALE scale,b.comments comments from user_tab_columns A,user_col_comments b" +
                    "  where a.COLUMN_NAME=b.COLUMN_NAME and a.TABLE_NAME = b.TABLE_NAME  and  a.TABLE_NAME = '";
            }
            else if(tabControl1.SelectedIndex == 1)
            {

                connection = new MysqlConnection(dbParams["MySql"]);
                //if (dbParams.ContainsKey("MySql"))
                //{
                //    connection.InitDbInfo();
                //}
                
                dbDataTypeEntity = new Mysql2CSharpEntity();
                getTableInfosSql = "select tmp.COLUMN_NAME NAME,tmp.DATA_TYPE TYPE,'0' LENGTH,tmp.NUMERIC_SCALE SCALE,tmp.COLUMN_COMMENT COMMENTS from information_schema.columns tmp where table_name='";
            }
           
        }

        void Init()
        {
            string path = Directory.GetCurrentDirectory() + "/db.conf";
            string paramStr = FileUtil.ReadFile(path);
            if (paramStr != "")
            {
                dbParams = JsonConvert.DeserializeObject<Dictionary<string, DbParamEntity>>(paramStr);
            }
            if (dbParams.ContainsKey("MySql"))
            {
                this.textBox10.Text = dbParams["MySql"].Ip;
                this.textBox9.Text = dbParams["MySql"].Port;
                this.textBox6.Text = dbParams["MySql"].Database;
                this.textBox8.Text = dbParams["MySql"].Username;
                this.textBox7.Text = dbParams["MySql"].Password;
            }
            if (dbParams.ContainsKey("Oracle"))
            {
                this.textBox1.Text = dbParams["Oracle"].Ip;
                this.textBox2.Text = dbParams["Oracle"].Port;
                this.textBox3.Text = dbParams["Oracle"].Database;
                this.textBox4.Text = dbParams["Oracle"].Username;
                this.textBox5.Text = dbParams["Oracle"].Password;
            }

            getTablesSql = "select table_name from user_tables";


            connection = new OracleConnection(dbParams["Oracle"]);
            //if (dbParams.ContainsKey("Oracle"))
            //{
            //    connection.InitDbInfo(dbParams["Oracle"]);
            //}

            dbDataTypeEntity = new Oracle2CSharpEntity();
            getTableInfosSql = "select A.COLUMN_NAME name,A.DATA_TYPE type,A.DATA_LENGTH length,A.DATA_SCALE scale,b.comments comments from user_tab_columns A,user_col_comments b" +
                "  where a.COLUMN_NAME=b.COLUMN_NAME and a.TABLE_NAME = b.TABLE_NAME  and  a.TABLE_NAME = '";
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            IList<Dictionary<string, object>>  tmp = new List<Dictionary<string, object>>();
            string str = textBox11.Text.Trim().ToUpper();
            foreach(Dictionary<string, object> list in resList)
            {
                if (list["NAME"].ToString().IndexOf(str) != -1)
                {
                    tmp.Add(list);
                }
            }
            GetTable(tmp);
        }

       
    }
}
