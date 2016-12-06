using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyProgramConsoleApplication
{
    #region 其他

    public class Test
    {
        private string str;

        public string Str
        {
            get { return str; }
            set { str = value; }
        }

        private string[] s;

        public string this[int index]
        {
            get { return s[index]; }
            set { s[index] = value; }
        }
    }

    public class TestChild : Test
    {
        private int x;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
    }

    public struct TestStruct
    {
        public const string identifier = "xx";

        public struct User
        {
            public const string name = "zhangsan";
        }
    }

    public struct Person
    {
        int num;
        //Person next;
    }

    public class MyController : Controller
    {
        
    }

    #endregion

    #region 线程

    public struct Paramters
    {
        public int data;
        public int time;
    }

    #endregion

    #region 弱事件

    public class CarInfoEventArgs : EventArgs
    {
        public string Car { get; private set; }
        public CarInfoEventArgs(string car)
        {
            this.Car = car;
        }
    }

    public class CarDealer
    {
        public event EventHandler<CarInfoEventArgs> NewCarInfo;
        public void NewCar(string car)
        {
            Console.WriteLine("CarDealer : new car {0}", car);
            if (NewCarInfo != null)
            {
                NewCarInfo(this, new CarInfoEventArgs(car));
            }
        }
    }

    public class WeakCarInfoEventManager : WeakEventManager
    {
        public static void AddListener(object source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedAddListener(source, listener);
        }

        public static void RemoveListener(object source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedRemoveListener(source, listener);
        }

        public static WeakCarInfoEventManager CurrentManager
        {
            get
            {
                WeakCarInfoEventManager manager = GetCurrentManager(typeof(WeakCarInfoEventManager)) as WeakCarInfoEventManager;
                if (manager == null)
                {
                    manager = new WeakCarInfoEventManager();
                    SetCurrentManager(typeof(WeakCarInfoEventManager), manager);
                }
                return manager;
            }
        }

        protected override void StartListening(object source)
        {
            (source as CarDealer).NewCarInfo += CarDealer_NewCarInfo;
        }

        protected override void StopListening(object source)
        {
            (source as CarDealer).NewCarInfo -= CarDealer_NewCarInfo;
        }

        private void CarDealer_NewCarInfo(object sender, CarInfoEventArgs e)
        {
            DeliverEvent(sender, e);
        }
    }

    public class Consumer : IWeakEventListener
    {
        private string name;
        public Consumer(string name)
        {
            this.name = name;
        }

        public void NewCarIsHere(object sender, CarInfoEventArgs e)
        {
            Console.WriteLine("{0} : car {1} is new", name, e.Car);
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)//为什么这里一定要加类名引用
        {
            NewCarIsHere(sender, e as CarInfoEventArgs);
            return true;
        }
    }

    #endregion

    #region 委托示例

    public class EventDefine
    {
        public event EventHandler MyEvent;
        public void EventHappen()
        {
            Console.WriteLine("An event is happened!");
            MyEvent(this, new EventArgs());
        }
    }

    public class EventRespond
    {
        public void RespondAction(object sender, EventArgs e)
        {
            Console.WriteLine("This is an action to the event!");
        }
    }

    #endregion

    #region 异常

    public class ColdCallFileReader : IDisposable
    {
        FileStream fs;
        StreamReader sr;
        uint nPeopleToRing;
        bool isDisposed = false;
        bool isOpen = false;

        public void Open(string fileName)
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException("peopleToRing");
            }
            fs = new FileStream(fileName, FileMode.Open);
            sr = new StreamReader(fs);

            try
            {
                string firstLine = sr.ReadLine();
                nPeopleToRing = uint.Parse(firstLine);
                isOpen = true;
            }
            catch (FormatException ex)
            {
                throw new ColdCallFileFormatException("First line isn't an integer", ex);
            }
        }

        public void PrecessNextPerson()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException("peopleToRing");
            }
            if (!isOpen)
            {
                throw new UnexpectedException("Attempted to access coldcall file that is not open");
            }
            try
            {
                string name;
                name = sr.ReadLine();
                if (name == null)
                    throw new ColdCallFileFormatException("Not enough names");
                if (name[0] == 'B')
                {
                    throw new SalesSpyFoundException(name);
                }
                Console.WriteLine(name);
            }
            catch (SalesSpyFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        public uint NPeopleToRing
        {
            get
            {
                if (isDisposed)
                    throw new ObjectDisposedException("peopleToRing");
                if (!isOpen)
                    throw new UnexpectedException("Attempted to aeecss cold-call file that is not open");
                return nPeopleToRing;
            }
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            isDisposed = true;
            isOpen = false;
            if (fs != null)
            {
                fs.Close();
                fs = null;
            }
        }
    }

    public class SalesSpyFoundException : ApplicationException
    {
        public SalesSpyFoundException(string spyName)
            : base("Sales spy found,with name " + spyName)
        {
        }

        public SalesSpyFoundException(string spyName, Exception innerException)
            : base("Sales spy found,with name " + spyName, innerException)
        {
        }
    }

    public class ColdCallFileFormatException : ApplicationException
    {
        public ColdCallFileFormatException(string message)
            : base(message)
        {
        }

        public ColdCallFileFormatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class UnexpectedException : ApplicationException
    {
        public UnexpectedException(string message)
            : base(message)
        {
        }
        public UnexpectedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    #endregion

    #region 继承相关测试

    public class BaseClass
    {
        public virtual void Show()
        {
            Console.WriteLine("this is BaseClass1!");
        }

        public void Show2()
        {
            Console.WriteLine("this is BaseClass2!");
        }

        public void Show3()
        {
            Console.WriteLine("this is BaseClass3!");
        }

        public virtual void Show4()
        {
            Console.WriteLine("this is BaseClass4!");
        }
    }

    public class ChildClass : BaseClass
    {
        public override void Show()
        {
            Console.WriteLine("this is ChildClass1!");
        }

        public new void Show2()
        {
            Console.WriteLine("this is ChildClass2!");
        }

        public void Show3()
        {
            Console.WriteLine("this is ChildClass3!");
        }
    }

    public class ChildClass2 : ChildClass
    {

    }

    #endregion

    #region 抽象工厂相关涉及的类

    public class User
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

    }

    public class Department
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }

    public interface IUser
    {
        void Insert(User user);
        void GetUser(int id);
    }

    public interface IDepartment
    {
        void Insert(Department department);
        void GetDepartment(int id);
    }

    public class SqlserverUser : IUser
    {
        public void Insert(User user)
        {
            Console.WriteLine("根据user插入了一条用户记录！");
        }
        public void GetUser(int id)
        {
            Console.WriteLine("根据id读取了一条用户记录！");
        }
    }

    public class AccessUser : IUser
    {
        public void Insert(User user)
        {
            Console.WriteLine("根据id插入了一条用户记录！");
        }
        public void GetUser(int id)
        {
            Console.WriteLine("根据name插入了一条用户记录！");
        }
    }

    public class SqlserverDepartment : IDepartment
    {
        public void Insert(Department department)
        {
            Console.WriteLine("根据department插入了一条用户记录！");
        }
        public void GetDepartment(int id)
        {
            Console.WriteLine("根据id读取了一条用户记录！");
        }
    }

    public class AccessDepartment : IDepartment
    {
        public void Insert(Department department)
        {
            Console.WriteLine("根据department插入了一条用户记录！");
        }
        public void GetDepartment(int id)
        {
            Console.WriteLine("根据id读取了一条用户记录！");
        }
    }

    public class DataAccess
    {
        private static readonly string assmblyName = "MyAbstractFactory";
        private static readonly string db = ConfigurationManager.AppSettings["DB"];

        public static IUser CreateUser()
        {
            string classname = "MyAbstractFactory." + db + "User";
            return (IUser)Assembly.Load(assmblyName).CreateInstance(classname);
        }

        public static IDepartment CreateDepartment()
        {
            string classname = "MyAbstractFactory." + db + "Department";
            return (IDepartment)Assembly.Load(assmblyName).CreateInstance(classname);
        }
    }

    #endregion

    #region MyTest

    public delegate void MyEventHandler(MyEventArgs e);

    public class Entity
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Entity(int i, string name)
        {
            Id = i;
            Name = name;
        }

        public Entity(int i)
            : this(i, "Zsh")
        {
        }
    }

    public interface IAction
    {
        Entity GetById(int i);
        void HereIsMessage(MyEventArgs e);
    }

    public class MyAction<T>
    {
        private static readonly object obj = new object();
        private static MyAction<T> instance;

        public static MyAction<T> GetMyAction()
        {
            if (instance == null)
            {
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = new MyAction<T>();
                    }
                }
            }
            return instance;
        }

        public virtual void Show(T s)
        {
            Console.WriteLine(s.ToString() + "这是基类的virtual函数！");
        }
    }

    public class ActionA : MyAction<int>, IAction
    {
        private Entity instance;
        public event MyEventHandler NoticeAgain;

        public override void Show(int s)
        {
            Console.WriteLine(s.ToString() + "这是子类A的override函数！");
        }

        public Entity GetById(int i)
        {
            if (i == 4)
            {
                instance = new Entity(i);
                Console.WriteLine("这个数字我喜欢，对象A的实体创建成功！");
                NoticeAgain(new MyEventArgs(i.ToString()));
                return instance;
            }
            else
            {
                Console.WriteLine("这个数字我不喜欢，对象A的实体创建失败");
                return null;
            }
        }

        public void HereIsMessage(MyEventArgs e)
        {
            if (instance == null)
            {
                Console.WriteLine("对象A的实体没有创建成功，没话可说！");
            }
            else
            {
                Console.WriteLine("对象A的实体创建成功，名字叫" + instance.Name + "A");
            }
        }
    }

    public class ActionB : MyAction<int>, IAction
    {
        private string a;

        public string A
        {
            get { return a; }
            set { a = value; }
        }

        private Entity instance;
        public event MyEventHandler NoticeAgain;

        public override void Show(int s)
        {
            Console.WriteLine(s.ToString() + "这是子类B的override函数！");
        }

        public Entity GetById(int i)
        {
            if (i == 4)
            {
                instance = new Entity(i);
                Console.WriteLine("这个数字我喜欢，对象B的实体创建成功！");
                NoticeAgain(new MyEventArgs(A));
                return instance;
            }
            else
            {
                Console.WriteLine("这个数字我不喜欢，对象B的实体创建失败");
                return null;
            }
        }

        public void HereIsMessage(MyEventArgs e)
        {
            if (instance == null)
            {
                Console.WriteLine("对象B的实体没有创建成功，没话可说！");
            }
            else
            {
                Console.WriteLine("对象B的实体创建成功，名字叫" + instance.Name + "B");
            }
        }
    }

    public class ActionChoice
    {
        private static readonly string assemblyName = "MyTest";
        private static readonly string kind = ConfigurationManager.AppSettings["kind"];

        public static IAction GetAction()
        {
            string classname = assemblyName + ".Action" + kind;
            return (IAction)Assembly.Load(assemblyName).CreateInstance(classname);
        }
    }

    public class MyEventArgs
    {
        private string mark;

        public string Mark
        {
            get { return mark; }
            set { mark = value; }
        }

        public MyEventArgs(string mark)
        {
            Mark = mark;
        }

        public void ShowMark()
        {
            Console.WriteLine(mark);
        }
    }

    #endregion

    [Bind(Include="FieldA")]
    [MetadataType(typeof(Cookie_Validation))]
    public partial class Cookie
    {
        public void Func()
        {
            this.FieldA = "123";
        }
    }

    public partial class Cookie
    {
        public string FieldA { get; set; }
    }

    public class Cookie_Validation
    {
        [Required(ErrorMessage = "FieldA is Required")]
        [StringLength(256, ErrorMessage = "字符串长度必须小于256字符")]
        public string FieldA { get; set; }
    }

}
