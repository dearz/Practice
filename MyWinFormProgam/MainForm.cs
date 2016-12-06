using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using MyProgram.GlobalTypes;
using MyProgram.GlobalFuncs;
using MyProgram.Reports;
using System.Data.OleDb;
using System.Configuration;

namespace MyProgram
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        protected override void OnLoad(EventArgs e)
        {
            ShowEnumOperator();
            WindowsMessage();
            ConfigurationManager.GetSection("");
            List<string> s = new List<string>();
            List<string>.Enumerator ss = s.GetEnumerator();
            Dictionary<string, string> d = new Dictionary<string, string>();
            Dictionary<string, string>.Enumerator dd = d.GetEnumerator();
        }

        #region 排序
        private void btnStart_Click(object sender, EventArgs e)
        {
            int[] intList = new int[this.richTextBox2.Text.Length];
            for (int i = 0; i < this.richTextBox2.Text.Length; i++)
            {
                intList[i] = int.Parse(this.richTextBox2.Text[i].ToString());
            }
            
            CommFunc.SortInt(intList);
            this.richTextBox2.Clear();
            foreach (int i in intList)
            {
                this.richTextBox2.Text += i.ToString();
            }
        }
        #endregion

        #region 文本读写

        private string openedFilePath = string.Empty;

        private const string fileFilter = "*.txt|*.txt|*.xml|*.xml|*.sql|*.sql";

        private void btnRead_Click(object sender, EventArgs e)
        {
            //ReadFile();
            ReadExeFile();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (openedFilePath == string.Empty)
                SaveAsFile();
            else
            {
                StreamWriter streamWriter = new StreamWriter(openedFilePath, false, Encoding.Unicode);
                try
                {
                    streamWriter.Write(this.richTextBox1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存失败！");
                }
                finally
                {
                    streamWriter.Close();
                }
                MessageBox.Show("保存成功！");
            }
        }

        private void btnSaveas_Click(object sender, EventArgs e)
        {
            SaveAsFile();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
        }

        private void ReadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = (openedFilePath == string.Empty ? Application.StartupPath : openedFilePath);
            //openFileDialog.Filter = fileFilter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                openedFilePath = openFileDialog.FileName;
                StreamReader streamReader = new StreamReader(openFileDialog.FileName, Encoding.Unicode);
                this.richTextBox1.Text = streamReader.ReadToEnd();

                //*****测试read和readblock的区别
                //char[] buffer = new char[5];
                //int startIndex = int.Parse(this.richTextBox1.Text);
                //int k = streamReader.Read(buffer, startIndex, 1);
                //int k = streamReader.ReadBlock(buffer, startIndex, 1);

                streamReader.Close();
            }
        }

        private void ReadExeFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = (openedFilePath == string.Empty ? Application.StartupPath : openedFilePath);
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                openedFilePath = openFileDialog.FileName;
                FileStream fs = new FileStream(openedFilePath, FileMode.Open, FileAccess.Read);
                byte[] b = new byte[1024];
                fs.Read(b, 0, 1024);
                string str = Encoding.Default.GetString(b);
                for (int i = 0; i < str.Length; i++)
                {
                    this.richTextBox1.Text += str[i].ToString();
                }
            }
        }

        private void SaveAsFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = (openedFilePath == string.Empty ? Application.StartupPath : openedFilePath);
            saveFileDialog.Filter = fileFilter;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName, false, Encoding.Unicode);
                streamWriter.Write(this.richTextBox1.Text);
                streamWriter.Close();
            }
        }

        #endregion

        #region 自定义枚举集合类的遍历使用

        public void ShowEnumOperator()
        {
            string all = string.Empty;
            string[] a = { "1", "2", "3", "4", "5", "6" };

            MyCollection s = new MyCollection(a);
            foreach (object x in s)
            {
                all += (x.ToString() + "-");
            }
        }

        #endregion

        #region new和vitual的测试

        public void ShowNewOrVitual()
        {
            BaseClass a = new BaseClass();
            BaseClass b = new ChildClass1();
            BaseClass c = new ChildClass2();
            ChildClass1 d = new ChildClass1();
            ChildClass1 e = new ChildClass2();
            ChildClass2 f = new ChildClass2();

            string all = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n", a.Show(), b.Show(), c.Show(), d.Show(), e.Show(), f.Show());
        }

        #endregion

        #region 子类和父类的转换测试
        public void TransformClass()
        {
            try
            {
                BaseClass bc = new BaseClass();
                ChildClass1 cc = new ChildClass1();
                bc = cc;//只有子类转化的基类才能重新强制转化为子类，直接转化无法进行
                ChildClass1 cc1 = (ChildClass1)bc;
            }
            catch(Exception ex)
            {
                
            }
        }
        #endregion

        #region StackTrace和反射应用

        public void BeginFunc()
        {
            ShowSomething();
            StackTrace st = new StackTrace();
            string methodNames = string.Empty;
            foreach (StackFrame sf in st.GetFrames())
            {
                methodNames += sf.GetMethod().Name + "||";
            }
        }

        public void ShowSomething()
        {
            StackTrace s = new StackTrace();
            string methodName = s.GetFrame(0).GetMethod().Name;
        }

        public void ShowReflect()
        {
            Type t = typeof(TestType);
            TestType tt = new TestType();
            tt.ID = "1234";
            tt.Detail = "zheshixiangxineirong";

            string propertyValue = string.Empty;
            foreach (PropertyInfo p in t.GetProperties())
            {
                propertyValue += p.GetValue(tt, null).ToString();
            }
        }

        #endregion

        #region 进程示例

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                StartProcess();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void StartProcess()
        {
            string fileName1 = @"..\../../MyProgramConsoleApplication/bin/Debug/MyProgramConsoleApplication.exe";
            string fileName2 = "E:/Personl Documents/Codes/Practice/MyProgramConsoleApplication/bin/Debug/MyProgramConsoleApplication.exe";
            string fileName3 = @"C:\Program Files\Internet Explorer\iexplore.exe";
            string fileName4 = @"E:\Personl Documents\Codes\Practice\MyException\bin\Debug\MyException.exe";
            string argument1 = "http://msdn.microsoft.com/zh-cn/library/system.diagnostics.process.aspx";
            string argument2 = "这事传递给线程程序的参数！";

            Process p = new Process();

            ProcessStartInfo ps = new ProcessStartInfo(fileName2, argument2);
            p.StartInfo = ps;
            try
            {
                p.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 字典示例

        public void ShowDictionary()
        {
            var employees = new Dictionary<EmployeeId, Employee>(31);

            var idKyle = new EmployeeId("T3755");
            var kyle = new Employee(idKyle, "Kyle Bush", 5443890.00m);
            employees.Add(idKyle, kyle);
            Console.WriteLine(kyle);

            var idCarl = new EmployeeId("F3547");
            var carl = new Employee(idCarl, "Carl Edwards", 5598120.00m);
            employees.Add(idCarl, carl);
            Console.WriteLine(carl);

            var idJimmie = new EmployeeId("C3386");
            var jimmie = new Employee(idJimmie, "Jimmie Johnson", 5024710.00m);
            employees.Add(idJimmie, jimmie);
            Console.WriteLine(kyle);

            var idDale = new EmployeeId("C3323");
            var dale = new Employee(idDale, "Dale Earhardt", 3522740.00m);
            employees[idDale] = dale;
            Console.WriteLine(dale);

            var idJeff = new EmployeeId("C3234");
            var jeff = new Employee(idJeff, "Jeff Burton", 3879540.00m);
            employees[idJeff] = jeff;
            Console.WriteLine(jeff);

            while (true)
            {
                Console.Write("Enter employee id (X to exit) >");
                var userInput = Console.ReadLine();
                userInput = userInput.ToUpper();
                if (userInput == "X")
                    break;

                EmployeeId id;
                try
                {
                    id = new EmployeeId(userInput);

                    Employee employee;
                    if (!employees.TryGetValue(id, out employee))
                    {
                        Console.WriteLine("Employee with id {0} does not exist", id);
                    }
                    else
                    {
                        Console.WriteLine(employee);
                    }
                }
                catch (EmployeeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        #endregion

        #region 抽象工厂模式

        public void ShowAbstractFactory()
        {
            TestType tt = new TestType();
            ISomething sm = SomethingFactory.CreateSomething();
            sm.InsertSomething(tt);
            sm.GetSomething("1234");
        }

        #endregion

        #region 代理模式
        public void ProxyTest()
        {
            int x = 1;
            int y = 1;
            MathProxy mp = new MathProxy();
            double result = mp.Add(x, y) + mp.Sub(x, y);
        }
        #endregion

        #region 静态构造函数测试
        public void ShowResult()
        {
            string str = "Today's color is :" + DayColor.todayColor.ToString();
        }
        #endregion

        #region 泛型类型
        public void GenericsTest()
        {

        }
        #endregion

        #region windows消息示例

        private MyRichTextBox r1 = new MyRichTextBox();
        private MyRichTextBox r2 = new MyRichTextBox();

        public void WindowsMessage()
        {
            r1.SendMessageEvent += new SendMessage(r2.Scroll);
            this.panel6.Controls.Add(r1);
            this.panel6.Controls.Add(r2);
            r2.Location = new Point(0, 100);
        }

        #endregion

        #region 套接字示例



        #endregion

        #region 报表示例

        private void bTnPrint_Click(object sender, EventArgs e)
        {
            GridPrint();
        }

        private void GridPrint()
        {
            ReportPrint reportPrint = new ReportPrint();
            reportPrint.SetReportFile("testReport", "打印测试");

            string strSql = "select distinct (r.triag_dept) as division," +
                "(select count(*) from pat_triage_info t where t.emergency_grade_amend = 'I级' and t.triag_dept = r.triag_dept) as one_level," +
                "(select count(*) from pat_triage_info t where t.emergency_grade_amend = 'II级' and t.triag_dept = r.triag_dept) as two_level," +
                "(select count(*) from pat_triage_info t where t.emergency_grade_amend = 'III级' and t.triag_dept = r.triag_dept) as three_level," +
                "(select count(*) from pat_triage_info t where t.emergency_grade_amend = 'IV级' and t.triag_dept = r.triag_dept) as four_level," +
                "count(*) as amount " + "from pat_triage_info r group by r.triag_dept ";
            string connstr = "Provider = MSDAORA;Data Source = czcs129;User id = ets;Password = ets;";
            OleDbConnection conn = new OleDbConnection(connstr);
            try
            {
                conn.Open();
                OleDbCommand cmmd = new OleDbCommand(strSql, conn);
                IDataReader dataSource = cmmd.ExecuteReader();
                reportPrint.SetDataSource(dataSource, DataSourceType.DataReader);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            reportPrint.PreviewReport();
            conn.Close();
        }

        #endregion

        #region 动态加载控件测试

        private bool AddRow()
        {
            this.tableLayoutPanel1.RowCount += 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            return true;
        }

        private bool AddColumn()
        {
            this.tableLayoutPanel1.ColumnCount += 1;
            return true;
        }

        private void InsertControls()
        {
            this.tableLayoutPanel1.Controls.Clear();
            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                for (int j = 0; j < this.tableLayoutPanel1.ColumnCount; j++)
                {
                    Button c = new Button();
                    c.Width = 125;
                    c.Height = 125;
                    if ((i + j) % 2 == 0)
                        c.BackColor = Color.Red;
                    else
                        c.BackColor = Color.Blue;
                    this.tableLayoutPanel1.Controls.Add(c);
                    this.tableLayoutPanel1.SetCellPosition(c, new TableLayoutPanelCellPosition(j, i));
                }
            }
        }

        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddRow();
            InsertControls();
        }

        #endregion

        #region 多线程优化UI展示
        private delegate void AddButtonDelegate();
        private void btnStartAdd_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                int i = 100;
                while (true)
                {
                    i++;

                    //这里使用invoke是因为不同的线程内，变量不能共享。
                    this.Invoke(new AddButtonDelegate(() =>
                    {
                        this.label1.Text = i.ToString();
                    }));
                }
            });
            thread.Start();
        }

        #endregion
    }
}
