using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;


namespace MyProgramConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        #region 例子

        //只有子类型赋值过的父类型才能重新强制转化为子类型
        private static void Func1()
        {
            string str;
            Test test = new Test();
            TestChild testChild = new TestChild();
            test = testChild;
            testChild = (TestChild)test;
            if (testChild.X != 0)
            {
                str = testChild.X.ToString();
                Console.WriteLine(str);
            }
            Console.ReadKey();
        }

        //测试引用类型和值类型
        private static void Func2()
        {
            Test test = new Test();
            test.Str = "x";
            Getsomething(test);

            Console.WriteLine(test.Str);

            test.Str = "xxx";

            //Func<Test, bool> NewGetsomething = param => { param.Str = "xxxx"; return true; };

            NewGetsomething(test);

            Console.WriteLine(test.Str);

            Console.ReadKey();
        }

        //测试继承
        private static void Func3()
        {
            Test testA = new Test();
            testA.Str = "xxx";
            Console.WriteLine(testA.Str);
            Test testB = testA;
            testB.Str = "xxxxx";
            Console.WriteLine(testA.Str);
            Console.ReadKey();
        }

        //测试引用类型和值类型
        private static void Func4()
        {
            List<string> s1 = new List<string>() { "x", "xx", "xxx" };

            //List<string> s2 = new List<string>();
            //s2 = s1;

            List<string> s2 = s1;

            s2[0] = "y";
            Console.WriteLine(s1[0]);
            Console.ReadKey();
        }

        //测试引用类型和值类型
        private static void Func5()
        {
            List<string> s1 = new List<string>() { "x", "xx", "xxx" };
            ShowSomething(s1);
            Console.WriteLine(s1[0]);
            Console.ReadKey();
        }

        //编码相关
        private static void Func6()
        {
            string s = "1234567";
            string k = "嘶嘶声";

            s = s + new string(' ', 10 - Encoding.GetEncoding(0).GetBytes(s).Length);
            k = k + new string(' ', 10 - Encoding.GetEncoding(0).GetBytes(k).Length);
            s = s.PadRight(0);
            Console.WriteLine(s + "|");
            Console.WriteLine(k + "|");
            Console.ReadKey();
        }

        //不同文件中可以定义相同的命名空间，其中的类属于同一个命名空间域
        private static void Func7()
        {
            //xxx.class1 x = new class1();
            //xxx.class2 xx = new class2();

            //class2 x = new xxx.class2();
        }

        private static void Func8()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
        }

        //
        private static void Func9()
        {
            float a, b, c;
            a = 1.2f;
            b = 0.7f;
            c = a / b;

            Console.WriteLine(c.ToString());
        }

        private static void Func10()
        {

        }

        // 弱事件示例
        private static void WeakEvent()
        {
            var dealer = new CarDealer();
            var michael = new Consumer("Michael");

            WeakCarInfoEventManager.AddListener(dealer, michael);

            dealer.NewCar("Mercedes");

            Console.ReadKey();
        }

        //委托示例
        private static void DelegateTest()
        {
            var eventA = new EventDefine();

            eventA.MyEvent += new EventRespond().RespondAction;

            eventA.EventHappen();

            Console.ReadKey();
        }

        //继承相关测试
        public static void TestInherit()
        {
            BaseClass a = new BaseClass();
            BaseClass b = new ChildClass();
            ChildClass c = new ChildClass();
            a.Show();
            b.Show();
            c.Show();
            a.Show2();
            b.Show2();
            c.Show2();
            a.Show3();
            b.Show3();
            c.Show3();

            Console.WriteLine("------------");
            int counter = 100;
            while ((counter--) > 0)
            {
                b.Show3();
            }
        }

        //抽象工厂模式测试
        public static void AbstractFactory()
        {
            User user = new User();
            Department department = new Department();

            IUser iuser = DataAccess.CreateUser();

            IDepartment idepartment = DataAccess.CreateDepartment();

            iuser.Insert(user);
            iuser.GetUser(1);
            idepartment.Insert(department);
            idepartment.GetDepartment(1);

            Console.ReadKey();
        }

        //MyTest
        public static void MyTest()
        {
            MyAction<int> a = MyAction<int>.GetMyAction();


            IAction i = ActionChoice.GetAction();
            ActionB b = (ActionB)i;
            b.NoticeAgain += b.HereIsMessage;

            i.GetById(4);
            Console.ReadKey();
        }



        #endregion

        #region 私有函数

        private static bool Getsomething(Test test)
        {
            test.Str = "xx";
            return true;
        }

        private static bool NewGetsomething(Test test)
        {
            test = new Test();
            test.Str = "x";
            return true;
        }

        private static bool ShowSomething(List<string> str)
        {
            str = new List<string>();
            str.Add("y");
            Console.WriteLine(str[0]);
            return true;
        }

        #endregion

        #region 线程示例

        private static Boolean isCompleted = false;

        private static Boolean isStrated = false;

        public delegate int TakesAWhileDelegate(int data, int ms);

        public delegate string ReturnStringDelegate(int s);

        private static void TreadTest()
        {
            //*****创建线程简单示例
            //Console.ReadKey();
            //Thread t = new Thread(MainThread) { Name = "MainThread", IsBackground = false, Priority = ThreadPriority.Highest };
            //if (args.Length > 0)
            //    Console.WriteLine(args[0]);
            //t.Start();
            //Console.ReadKey();

            //ThreadStart ts = new ThreadStart(MainThread);

            //*****线程池方式创建线程
            //int nWorkrtThreads;
            //int nCompletionPortThreads;
            //ThreadPool.GetMaxThreads(out nWorkrtThreads, out nCompletionPortThreads);
            //Console.WriteLine("Max worker threads:{0}," + "I/O completion threads:{1}", nWorkrtThreads, nCompletionPortThreads);
            //for (int i = 0; i < 5; i++)
            //{
            //    ThreadPool.QueueUserWorkItem(JobForAThread);
            //}
            //Thread.Sleep(3000);


            //*****异步线程示例1
            TakesAWhileDelegate dl = TakesAWhile;
            dl.BeginInvoke(1, 1000, TakesAWhileCompleted, dl);
            while (true)
            {
                if (isCompleted)
                {
                    break;
                }
                if (isStrated)
                {
                    Console.Write(".");
                }
                Thread.Sleep(50);
            }
            Console.ReadKey();


            ////*****异步线程示例2
            //int a = 0;
            //TakesAWhileDelegate dl = TakesAWhile;
            //IAsyncResult ar = dl.BeginInvoke(1, 30000, null, null);
            //while (!ar.IsCompleted)
            //{
            //    a++;
            //    Console.Write("{0}-", a.ToString());
            //    Thread.Sleep(2500);
            //}
            //int result = dl.EndInvoke(ar);
            //Console.WriteLine("result: {0}", result);
            //Console.ReadKey();


            //*****创建带参数（1个、多个）的线程方法，利用结构体/匿名委托
            //Paramters p = new Paramters();
            //p.data = 1;
            //p.time = 3000;
            //Thread t = new Thread(new ParameterizedThreadStart(TakesAWhile));//这里需要把函数的参数设置为结构体类型
            //t.Start(p);
            //Thread t1 = new Thread(delegate() { TakesAWhile(1, 3000); });
            //t1.Start();

            ////验证使用委托创建线程的返回值就是方法的返回值
            //ReturnStringDelegate dl = ReturnString;
            //IAsyncResult ar = dl.BeginInvoke(100, null, null);
            //while (!ar.IsCompleted)
            //{
            //    Console.Write("-");
            //}
            //string s = dl.EndInvoke(ar);
            //Console.WriteLine(s);
            
        }

        public static void MainThread()
        {
            Console.WriteLine("Thread {0} is started!", Thread.CurrentThread.Name);
            Thread.Sleep(3000);
            Console.WriteLine("Thread {0} is completed!", Thread.CurrentThread.Name);
        }

        public static void JobForAThread(object state)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("loop {0},running inside pppled thread{1}", i, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(50);
            }
        }

        private static int TakesAWhile(int data, int ms)
        {
            isStrated = true;
            Thread.Sleep(50);
            Console.WriteLine("TakesAWhile started");
            Thread.Sleep(ms);
            return ++data;
        }

        private static void TakesAWhileCompleted(IAsyncResult ar)
        {
            isCompleted = true;
            if (ar == null)
                throw new ArgumentNullException("ar");
            TakesAWhileDelegate dl = ar.AsyncState as TakesAWhileDelegate;
            int result = dl.EndInvoke(ar);
            Console.WriteLine("\nTakesAWhile completed:result:{0}", result);      
        }
        
        private static string ReturnString(int s)
        {
            Thread.Sleep(3000);
            return s.ToString();
        }



        #endregion

        #region xml操作示例

        public static void CreateXmlFile()
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlDeclaration xmlDec = xmlDoc.CreateXmlDeclaration("1.0", "xxx", null);
            xmlDoc.AppendChild(xmlDec);

            XmlNode RootNode = xmlDoc.CreateElement("", "RootNode", "");
            xmlDoc.AppendChild(RootNode);

            for (int i = 0; i < 4; i++)
            {
                XmlNode xn = xmlDoc.SelectSingleNode("RootNode");

                XmlElement xe = xmlDoc.CreateElement("", "Person", "");
                xe.SetAttribute("name", "樱木花道");

                xn.AppendChild(xe);

                XmlElement xe1 = xmlDoc.CreateElement("Sex");
                xe1.InnerText = "Male";
                xe.AppendChild(xe1);
                XmlElement xe2 = xmlDoc.CreateElement("Age");
                xe2.InnerText = "18";
                xe.AppendChild(xe2);

                XmlNode xn1 = xmlDoc.CreateNode(XmlNodeType.Text, "", "");
                xn1.InnerText = "sssssssssss";
                xe.AppendChild(xn1);           
            }
            try
            {
                xmlDoc.Save("person.xml");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static bool LoadXmlFile()
        {
            if (!File.Exists("person.xml"))
                return false;
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("person.xml");

                XmlNode xmlNode = xmlDoc.SelectSingleNode("RootNode");
                XmlNodeList list = xmlNode.ChildNodes;
                foreach (XmlNode xn in list)
                {
                    foreach (XmlAttribute xa in xn.Attributes)
                    {
                        Console.WriteLine(xa.Value);
                    }

                    foreach (XmlNode xnode in xn.ChildNodes)
                    {
                        //XmlElement xe = (XmlElement)xnode;
                        //Console.WriteLine(xe.InnerText);
                        Console.WriteLine(xnode.InnerText);
                    }
                }
                return true;
            }
        }

        public static bool ModifyXmlFile()
        {
            if (!File.Exists("person.xml"))
                return false;
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("person.xml");

                XmlNode xmlNode = xmlDoc.SelectSingleNode("RootNode");
                XmlElement xe = (XmlElement)(xmlNode.ChildNodes[0]);
                xe.SetAttribute("name", "赤木晴子");
                foreach (XmlNode xn in xe.ChildNodes)
                {
                    XmlElement xe1 = (XmlElement)xn;
                    switch (xe1.LocalName)
                    {
                        case "Sex":
                            xe1.InnerText = "Female";
                            break;
                        case "Age":
                            xe1.InnerText = "17";
                            break;
                        default:
                            break;
                    }
                }

                //XmlNode xn = xmlDoc.DocumentElement;
                //xn.InnerText = "Text containing <markup/> will have char(<) and char(>) escaped.";
                //Console.WriteLine(xn.InnerXml);
                //xn.InnerXml = "Text containing <markup/>.";
                //Console.WriteLine(xn.InnerXml);

                xmlDoc.Save("person.xml");
                return true;
            }
        }

        #endregion

        #region 异常示例

        private static void ExceptionTest()
        {
            try
            {
                Do();
            }
            catch (Exception e)
            {

            }

            Console.ReadKey();
        }

        private static void Do()
        {
            Console.WriteLine("Please type in the name of the file containing the names of the people to be cold called >");
            string fileName = Console.ReadLine();

            ColdCallFileReader peopleToRing = new ColdCallFileReader();

            try
            {
                try
                {
                    peopleToRing.Open(fileName);
                    for (int i = 0; i < peopleToRing.NPeopleToRing; i++)
                    {
                        peopleToRing.PrecessNextPerson();
                    }
                    Console.WriteLine("All callers processed correctly");
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("The file {0} does not exist", fileName);
                    throw new Exception("这是测试抛出的异常！");//异常如果抛出来但是外层不catch的话，程序就会直接终止
                }
                catch (ColdCallFileFormatException ex)
                {
                    Console.WriteLine("The file {0} appears to have been corrupted", fileName);
                    Console.WriteLine("Details of  problem are : {0}", ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine("Inner exception was : {0}", ex.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception occurred:\n" + ex.Message);
                }
                finally
                {
                    peopleToRing.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("下面抛出来的异常");
                throw ex;
                try
                {
                    string s = "ljla";
                    int i = int.Parse(s);
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            }
            finally
            {
                peopleToRing.Dispose();
            }

            Console.ReadKey();
        }

        #endregion

        #region  递归和迭代

        /// <summary>
        /// 计算一张0.1毫米厚的纸对折100次的厚度
        /// </summary>
        /// <param name="index"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        private static double Recursion(int index ,double thickness)
        {
            if (index == 0)
                return thickness;
            return Recursion(--index, thickness * 2);
        }

        private static Person Create(int n)
        {
            Person s = new Person();
            while (n < 0)
            {
                Person p = new Person();
                Person l = new Person();
                Console.WriteLine("请输入一个正整数：");
                string strNum = Console.ReadLine();
                try
                {
                    int iNum = int.Parse(strNum);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("你输入的不是正整数！");
                }
                Person q = new Person();
            }
            return s;
        }

        private static void Ysf(int n,int k,int m)
        {
            for (int i = 0; i < m; i++)
            {

            }
        }

        #endregion

        #region 正则表达式

        private static void RegularExpression()
        {
            Regex.Match("", "");

        }

        #endregion

        #region 集合操作

        private static void CollectionTest()
        {
            List<int> listInt = new List<int>();
            List<string> listString = listInt.ConvertAll(r => r.ToString());

            Dictionary<string, string> dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            
        }
        #endregion

        #region 流操作
        private static void MyFileStream()
        {
            string path = "";
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write("0123456789");

        }

        private static void MyBufferStream()
        {
            MemoryStream ms = new MemoryStream();
            BufferedStream bs = new BufferedStream(ms, 4096);
            byte [] b=new  byte[10];
            for (int i = 0; i < 10; i++)
            {
                bs.WriteByte((byte)i);
            }
            bs.Position = 0;
            bs.Read(b, 0, 9);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("读的值是：{0}", b[i]);
            }
            Console.WriteLine("值是：{0}", bs.ReadByte());

        }

        private static void MyStringStream()
        {
            StringBuilder sb = new StringBuilder("111111");
            StringWriter sw = new StringWriter();
            sw.Write("222222");

            StringReader sr = new StringReader("333333");
        }
        #endregion
    }
}
