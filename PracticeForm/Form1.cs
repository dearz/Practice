#define XX //123

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Runtime;

namespace PracticeForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.tabControl2.SelectedTab = tabPage5;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RunFunction();
        }

        public void Func()
        {
             
        }

        /// <summary>
        /// 对象的HashCode的问题
        /// </summary>
        public void Func13()
        {
            string a = "123";
            string b = "123";

            int aa = a.GetHashCode();
            int bb = b.GetHashCode();
        }

        /// <summary>
        /// 测试类型的比较方式以及排序
        /// </summary>
        public void Func12()
        {
            List<ClassA> aa = new List<ClassA>() { new ClassA("c", "3"), new ClassA("a", "1"), new ClassA("d", "4"), new ClassA("b", "2") };
            try
            {
                aa.Sort();
                //Comparison<ClassA> dd = new Comparison<ClassA>(GlobalFunc.CompareClassA);
                //aa.Sort(dd);
                //aa.Sort(new ComparerClassA());
                foreach (ClassA a in aa)
                {
                    richTextBox2.Text += a.Name;
                }
            }
            catch (Exception ex)
            {
                richTextBox2.Text = ex.Message;
            }
        }

        /// <summary>
        /// 编码测试
        /// </summary>
        public void Func11()
        {
            byte[] b1 = Encoding.ASCII.GetBytes(new char[] { 'a' });
            byte[] b2 = Encoding.UTF32.GetBytes(new char[] { 'a' });
            byte[] b3 = Encoding.UTF7.GetBytes(new char[] { 'a' });
            byte[] b4 = Encoding.UTF8.GetBytes(new char[] { 'a' });
            byte[] b5 = Encoding.Unicode.GetBytes(new char[] { 'a' });
            byte[] b6 = Encoding.GetEncoding("GBK").GetBytes(new char[] { 'a' });

            EncodingInfo[] ee = Encoding.GetEncodings();
            foreach (EncodingInfo e in ee)
            {
                this.comboBox2.Items.Add(e.Name);
            }
        }

        public void Func10()
        {
            Hashtable hashtable = new Hashtable();
            hashtable["1"] = 1;
            hashtable["2"] = 2;
            hashtable["3"] = 3;
            foreach (DictionaryEntry dd in hashtable)
            {
            }
            Dictionary<int, string> d = new Dictionary<int, string>();
            IDictionaryEnumerator de = hashtable.GetEnumerator();
            while (de.MoveNext())
            {
            }
            Dictionary<int, string>.Enumerator en = d.GetEnumerator();
            int[] k = new int[] { 1, 2, 3, 4 };
            object obj = hashtable[6];

            foreach (KeyValuePair<int, string> ddd in d)
            {
            }

            Form f = new Form();
            
        }

        public void Func1()
        {
            try
            {
                string sss = "ab";
                MessageBox.Show(double.Parse(sss).ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.comboBox1.DropDownWidth = int.Parse(textBox1.Text);
            UserControl control = new UserControl();
            AutoResetEvent ss = new AutoResetEvent(false);
            Object obj = new object();
            lock (obj)
            {
            }
            List<string> s = new List<string>() { "1", "2", "3", "4" };
            MessageBox.Show(s.Find(x => x == "5"));
            foreach (var k in s)
            {
                if (!string.IsNullOrEmpty(k))
                    MessageBox.Show(k);
            }
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("1", 1);
            dict.Add("2", 2);
            dict.Add("3", 3);
        }

        public void Func2()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("a");
            dt.Columns.Add("b");
            dt.Columns.Add("c");
            for (int i = 0; i < 4; i++)
            {
                DataRow dr = dt.NewRow();
                dr["a"] = i;
                dr["b"] = i;
                dr["c"] = i;
                dt.Rows.Add(dr);
            }
            DataRow[] dtFind = dt.Select("a = '5'");
            if (dtFind != null)
            {
                DataRow ddr = dtFind[0];
                ddr["a"] = "x";
                ddr["b"] = "x";
                ddr["c"] = "x";
            }
        }

        public void Func4()
        {
            //-1
            //List<int> s = new List<int>() { 3, 2, 8, 4, 5, 6 };

            //var list1 = s.OrderByDescending(k => k);

            //this.richTextBox1.Clear();
            //foreach (int i in list1)
            //{
            //    this.richTextBox1.Text += i.ToString();
            //}


            //-2
            //Dictionary<int, string> d = new Dictionary<int, string>() { { 1, "a" }, { 2, "b" }, { 3, "c" }, { 4, "d" } };

            //var list2 = d.OrderBy(k => k.Key);
            //this.richTextBox1.Clear();
            //foreach (KeyValuePair<int, string> h in list2)
            //{
            //    this.richTextBox1.Text += h.Value;
            //}


            //-3
            //List<string> s = new List<string>() { "3", "2", "8", "6", "5", "1" };

            //var list3 = s.ToDictionary<string, int>(k => int.Parse(k));
            
            //foreach (KeyValuePair<int, string> h in list3)
            //{
            //    this.richTextBox1.Text += h.Key;
            //}

            //-4  Dictionary对象没有ToList()方法
            //Dictionary<int, string> d = new Dictionary<int, string>() { { 1, "a" }, { 2, "b" }, { 3, "c" }, { 4, "d" } };
            //var list4 = d.ToList<string>();

            //-5
            //int[] num = new int[] { 3, 2, 8, 4, 5, 6 };

            //Array a = Array.CreateInstance(typeof(int), 6);

            //ArrayList al = new ArrayList() { 1, "2", 3, 4, 5, 6, 7 };

            //a = al.ToArray();
            
            //var list5 = num.ToList<int>();
            
            //int count = 0;
            //foreach (int i in list5)
            //{
            //    count += i;
            //}
            //this.richTextBox1.Text = count.ToString();          
        }

        public void Func5()
        {
            OleDbConnection conn = new OleDbConnection("provider=msdaora.1;data source=S3EMRSTEST;user id=NURDOC;password=NURDOC;");
            conn.Open();
            string sqlStr = "select order_text from nur_first_order where visit_id = '2379988' ";
            OleDbCommand cmd = new OleDbCommand(sqlStr, conn);
            //OleDbDataReader reader = cmd.ExecuteReader();
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            if (ds == null || ds.Tables == null || ds.Tables.Count <= 0)
                return;
            string s = ds.Tables[0].Rows[0][0].ToString();
            s = s.Replace("          ", "");
            MessageBox.Show("Hr.Comm.Constant.String.CRLF +");
        }

        public void Func6()
        {
            int index = 0;
            for (int i = 0; i < 10; i++)
            {
                index = this.dataGridView3.Rows.Add();
                this.dataGridView3.Rows[index].Cells[0].Value = i.ToString();
            }
        }

        /// <summary>
        /// linq语法测试
        /// </summary>
        public void Func7()
        {
            List<string> list = new List<string>() { "3", "2", "1", "4" };
            var l = from k in list where !string.IsNullOrEmpty(k) orderby int.Parse(k) select k;
            this.richTextBox1.Clear();
            foreach (string s in l)
            {
                this.richTextBox1.Text += s;
            }
        }

        /// <summary>
        /// 数据库用dataset更新，必须在查询的时候指定主键的测试
        /// </summary>
        public void Func8()
        {
            //DataSet ds = new DataSet();
            //DataSet ds1 = new DataSet();
            //string sql1 = "select * from dept_dict";
            //OracleConnection conn = new OracleConnection();
            //conn.ConnectionString = "data source=czcs129;user id=ETS;password=ETS;";
            //conn.Open();
            //OracleCommand cmd = new OracleCommand(sql1, conn);
            //OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            //try
            //{
            //    //adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            //    //adapter.Fill(ds);
            //    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            //    adapter.Fill(ds1);
            //    Console.WriteLine(ds1.Tables.Count.ToString() + "success!");
            //    ds1.Tables[0].Rows[0][6] = "0";
            //    OracleCommandBuilder cmdBuilder = new OracleCommandBuilder(adapter);

            //    cmdBuilder.ConflictOption = ConflictOption.OverwriteChanges;

            //    int result = adapter.Update(ds1);
            //---------------------------------------------------
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            string sql1 = "select * from dept_dict";
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=MSDAORA;data source=czcs129;user id=ETS;password=ETS;";
            conn.Open();
            OleDbCommand cmd = new OleDbCommand(sql1, conn);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            try
            {
                //adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                //adapter.Fill(ds);
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Fill(ds1);
                Console.WriteLine(ds1.Tables.Count.ToString() + "success!");
                //ds1.Tables[0].Rows[0][6] = "0";
                //OleDbCommandBuilder cmdBuilder = new OleDbCommandBuilder(adapter);

                //cmdBuilder.ConflictOption = ConflictOption.OverwriteChanges;

                //int result = adapter.Update(ds1);

            }
            catch (Exception ex)
            {
                Console.WriteLine("failed");
            }
            Console.ReadKey();
        }

        public void Func9()
        {
            Queue qq = new Queue();
            Queue<int> q = new Queue<int>();
        }

        private void RunFunction()
        {
            MethodInfo methodInfo = this.GetType().GetMethod("Func" + this.textBox1.Text);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);
            else
                MessageBox.Show("没有该方法！");
           
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //int index =this.dataGridView2.CurrentRow.Index;
            //if (keyData == Keys.Enter)
            //{
            //    if (this.dataGridView2.CurrentCell == this.dataGridView2.Rows[index].Cells[this.Column6.Index])
            //    {
            //        this.dataGridView2.CurrentCell = this.dataGridView2.Rows[index].Cells[this.Column7.Index];
            //    }
            //    else if (this.dataGridView2.CurrentCell == this.dataGridView2.Rows[index].Cells[this.Column7.Index])
            //    {
            //        this.dataGridView2.CurrentCell = this.dataGridView2.Rows[index].Cells[this.Column8.Index];
            //    }
            //    else if (this.dataGridView2.CurrentCell == this.dataGridView2.Rows[index].Cells[this.Column8.Index])
            //    {
            //        this.dataGridView2.CurrentCell = this.dataGridView2.Rows[index].Cells[this.Column9.Index];
            //    }
            //    else if (this.dataGridView2.CurrentCell == this.dataGridView2.Rows[index].Cells[this.Column9.Index])
            //    {
            //        this.dataGridView2.CurrentCell = this.dataGridView2.Rows[index].Cells[this.Column10.Index];
            //    }
            //    else if (this.dataGridView2.CurrentCell == this.dataGridView2.Rows[index].Cells[this.Column10.Index])
            //    {
            //        this.dataGridView2.CurrentCell = this.dataGridView2.Rows[index].Cells[this.Column11.Index];
            //    }
            //    else if (this.dataGridView2.CurrentCell == this.dataGridView2.Rows[index].Cells[this.Column11.Index])
            //    {
            //        this.dataGridView2.CurrentCell = this.dataGridView2.Rows[index].Cells[this.Column12.Index];
            //    }
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public Form GetFormInDll(string dllName, string typeName)
        {
            Form result;
            try
            {
                if (Path.GetDirectoryName(dllName).Length == 0)
                {
                    dllName = Path.Combine(Application.StartupPath, dllName);
                }
                Assembly asmAssembly = Assembly.LoadFrom(dllName);
                Type typeToLoad = asmAssembly.GetType(typeName);
                object GenericInstance = Activator.CreateInstance(typeToLoad);
                Form formToLoad = new Form();
                formToLoad = (Form)GenericInstance;
                result = formToLoad;
            }
            catch (Exception ex)
            {
                string msg = string.Concat(new string[]
				{
					"DLL名称: ",
					dllName,
					" 类名:",
					typeName,
					" 没被发现! "
				});
                throw new Exception(msg + ex.Message);
            }
            return result;
        }

        public void GetSomething(string param, out object obj)
        {
            if (param == "string")
            {
                obj = "123";
            }
            else if (param == "int")
            {
                obj = 123;
            }
            else
            {
                obj = "meiyouzhi";
            }
        }

        public string padRightEx(string str, int totalByteCount)
        {
            Encoding coding = Encoding.GetEncoding(0);
            int dcount = 0;
            int strCount = coding.GetByteCount(str);
            if (strCount > totalByteCount)
            {
                int i = 0;
                int n = 0;
                char[] charArrary = str.ToCharArray();
                for (i = 0; i < charArrary.Length; i++)
                {
                    n = n + coding.GetByteCount(charArrary[i].ToString());
                    if (n > totalByteCount)
                        break;
                }
                str = str.Substring(0, i);
            }
            MessageBox.Show("||" + str + "||");
            foreach (char ch in str.ToCharArray())
            {
                if (coding.GetByteCount(ch.ToString()) == 2)
                    dcount++;
            }
            string w = str.PadRight(totalByteCount - dcount);
            return w;
        }

        public bool StartProcess()
        {
            string fileName1 = @"..\../../MyThread/bin/Debug/MyThread.exe";
            string fileName2 = "E:/Personl Documents/Codes/Practice/MyThread/bin/Debug/MyThread.exe";
            string fileName3 = @"C:\Program Files\Internet Explorer\iexplore.exe";
            string argument1 = "http://msdn.microsoft.com/zh-cn/library/system.diagnostics.process.aspx";
            Process p = new Process();
            p.StartInfo.FileName = fileName3;
            p.StartInfo.Arguments = argument1;
            try
            {
                p.Start();
                return true;
            }
            catch (Exception e)
            {
                throw e;
                return false;
            }
        }     
    }
}
