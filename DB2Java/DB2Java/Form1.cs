using DB2Java.Util;
using Newtonsoft.Json;
using Strawberry.Util;
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

namespace DB2Java
{
    public partial class Form1 : Form
    {
        private SuperDb db = null;
        private DbFieldEntity d = null;
        private string sql1;
        private string sql2;
        private List<List<string>> l = new List<List<string>>();
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
                    string tsql = sql2 + checkedListBox1.GetItemText(checkedListBox1.Items[i]) + "'";
                    //MessageBox.Show(sql);
                    CSharpClass j = CreatCSharp(db.ExecuteReader(tsql));
                    j.ty = "class";
                    j.st.Add("public");
                    j.name = InitUpper(checkedListBox1.GetItemText(checkedListBox1.Items[i]));
                    string filename = Path.Combine(Directory.GetCurrentDirectory() + "\\classes", j.name);
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
        private JavaClass CreatJava(List<List<string>> l)
        {
            if (l == null || l.Count == 0)
            {
                return null;
            }
            JavaClass j = new JavaClass();
            foreach(List<string> tmp in l)
            {               
                d.Name = tmp[0];
                d.DataType = tmp[1];
                d.DataLength = (tmp[2]==null|| tmp[2] == "")?0:int.Parse(tmp[2]);
                d.DataScale= (tmp[3] == null || tmp[3] == "") ? 0 : int.Parse(tmp[3]);
                JavaField jf = new JavaField();
                jf.javaType = d.TypeConversion();
                jf.power = "private";
                jf.name = d.Name.ToLower();
                jf.met = true;
                j.ljf.Add(jf);              
            }
            return j;
        }
        private CSharpClass CreatCSharp(List<List<string>> l)
        {
            if (l == null || l.Count == 0)
            {
                return null;
            }
            CSharpClass j = new CSharpClass();
            foreach (List<string> tmp in l)
            {
                d.Name = tmp[0];
                d.DataType = tmp[1];
                d.DataLength = (tmp[2] == null || tmp[2] == "") ? 0 : int.Parse(tmp[2]);
                d.DataScale = (tmp[3] == null || tmp[3] == "") ? 0 : int.Parse(tmp[3]);
                d.Annotation = tmp[4];
                JavaField jf = new JavaField();
                jf.javaType = d.TypeConversion();
                jf.power = "private";
                jf.name = InitUpper(d.Name.ToLower());
                jf.met = true;
                j.ljf.Add(jf);
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
                    string tsql = sql2 + checkedListBox1.GetItemText(checkedListBox1.Items[i]) + "'";
                    //MessageBox.Show(sql);
                    CSharpClass j = CreatCSharp(db.ExecuteReader(tsql));
                    j.ty = "class";
                    j.st.Add("public");
                    j.name = InitUpper(checkedListBox1.GetItemText(checkedListBox1.Items[i]));
                    string filename = Path.Combine(Directory.GetCurrentDirectory() + "\\classes", j.name);
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
            dbParams["Oracle"].ip = textBox1.Text.Trim();
            dbParams["Oracle"].port = textBox2.Text.Trim();
            dbParams["Oracle"].username = textBox3.Text.Trim();
            dbParams["Oracle"].password = textBox4.Text.Trim();
            dbParams["Oracle"].database = textBox5.Text.Trim();
           // string jsonData = JsonConvert.SerializeObject(oneList);
            string path = Directory.GetCurrentDirectory() + "/db.conf";
            FileUtil.WriteFile(path,JsonConvert.SerializeObject(dbParams),null,FileMode.Create);

            sql1 = "select table_name from user_tables";
            db = new DbOracle();
            db.InitDbInfo(dbParams["Oracle"]); 
            try
            {
                l = db.ExecuteReader(sql1);
            }
            catch (Exception e1)
            {
                MessageBox.Show("参数有误------>" + e1.Message);
            }
            GetTable(l);
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
            dbParams["MySql"].ip = textBox10.Text.Trim();
            dbParams["MySql"].port = textBox9.Text.Trim();
            dbParams["MySql"].username = textBox8.Text.Trim();
            dbParams["MySql"].password = textBox7.Text.Trim();
            dbParams["MySql"].database = textBox6.Text.Trim();
            string path = Directory.GetCurrentDirectory() + "/db.conf";
            FileUtil.WriteFile(path, JsonConvert.SerializeObject(dbParams), null, FileMode.Create);
              sql1 = "select table_name from information_schema.tables where table_schema = '" + dbParams["MySql"].database + "'"; ;
               db = new MysqlConnection ();
           
            db.InitDbInfo(dbParams["Oracle"]);
            try
            {
                l = db.ExecuteReader(sql1);
            }
            catch (Exception e1)
            {
                MessageBox.Show("参数有误------>" + e1.Message);
            }
            GetTable(l);
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
        
        void GetTable(List<List<string>> l)
        {
            this.checkedListBox1.Items.Clear();
            if (l != null)
            {
                foreach (var tmp in l)
                {
                    this.checkedListBox1.Items.Add(tmp[0]);
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                sql1 = "select table_name from user_tables";
                
                
                db = new DbOracle();
                if (dbParams.ContainsKey("Oracle"))
                {
                    db.InitDbInfo(dbParams["Oracle"]);
                }
                
                d = new DbFieldEntityOracle();
                sql2 = "select A.COLUMN_NAME,A.DATA_TYPE,A.DATA_LENGTH,A.DATA_SCALE,b.comments from user_tab_columns A,user_col_comments b" +
                    "  where a.COLUMN_NAME=b.COLUMN_NAME and a.TABLE_NAME = b.TABLE_NAME  and  a.TABLE_NAME = '";
            }
            else if(tabControl1.SelectedIndex == 1)
            {

                db = new MysqlConnection ();
                if (dbParams.ContainsKey("MySql"))
                {
                    db.InitDbInfo(dbParams["MySql"]);
                }
                
                d = new DbFieldEntityMysql();
                sql2 = "select tmp.COLUMN_NAME,tmp.DATA_TYPE,'0',tmp.NUMERIC_SCALE from information_schema.columns tmp where table_name='";
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
                this.textBox10.Text = dbParams["MySql"].ip;
                this.textBox9.Text = dbParams["MySql"].port;
                this.textBox6.Text = dbParams["MySql"].database;
                this.textBox8.Text = dbParams["MySql"].username;
                this.textBox7.Text = dbParams["MySql"].password;
            }
            if (dbParams.ContainsKey("Oracle"))
            {
                this.textBox1.Text = dbParams["Oracle"].ip;
                this.textBox2.Text = dbParams["Oracle"].port;
                this.textBox3.Text = dbParams["Oracle"].database;
                this.textBox4.Text = dbParams["Oracle"].username;
                this.textBox5.Text = dbParams["Oracle"].password;
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            List<List<string>> tmp = new List<List<string>>();
            string str = textBox11.Text.Trim();
            foreach(List<string> list in l)
            {
                if (list[0].IndexOf(str) != -1)
                {
                    tmp.Add(list);
                }
            }
            GetTable(tmp);
        }

       
    }
}
