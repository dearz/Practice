using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.Remoting;
using System.Collections;
using System.Data;

namespace MyProgram.GlobalTypes
{
    #region 实验new和vitual

    public class BaseClass
    {
        public virtual string Show()
        {
            return "This is a virtual function!";
        }
    }

    public class ChildClass1 : BaseClass
    {
        public override string Show()
        {
            return "This is an override function!";
        }
    }

    public class ChildClass2 : ChildClass1
    {
        public new string Show()
        {
            return "This is a new function!";
        }
    }    

    #endregion

    public class TestType
    {
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string detail;
        public string Detail
        {
            get { return detail; }
            set { detail = value; }
        }

        private void DoSomething()
        {
        }

        public void GetSomething()
        {
        }
    }

    [Serializable]
    public class Employee
    {
        private string name;
        private decimal salary;
        private readonly EmployeeId id;

        public Employee(EmployeeId id, string name, decimal salary)
        {
            this.id = id;
            this.name = name;
            this.salary = salary;
        }

        public override string ToString()
        {
            return String.Format("{0}: {1,-20} {2:c}", id.ToString(), name, salary);
        }
    }

    [Serializable]
    public class EmployeeException : Exception
    {
        public EmployeeException(string message)
            : base(message)
        {
        }
    }

    [Serializable]
    public struct EmployeeId : IEquatable<EmployeeId>
    {
        private readonly char prefix;
        private readonly int number;

        public EmployeeId(string id)
        {
            if (id == null)
                throw new ArgumentNullException("id");
            prefix = (id.ToUpper())[0];
            int numLength = id.Length - 1;
            try
            {
                number = int.Parse(id.Substring(1, numLength > 6 ? 6 : numLength));
            }
            catch (FormatException)
            {
                throw new EmployeeException("Invalid EmployeeId format");
            }
        }

        public override string ToString()
        {
            return prefix.ToString() + string.Format("{0}", number);
        }

        public override int GetHashCode()
        {
            return (number ^ number << 16) * 0x15051505;
        }

        public bool Equals(EmployeeId other)
        {
            if (other == null)
                return false;
            return (prefix == other.prefix && number == other.number);
        }

        public override bool Equals(object obj)
        {
            return Equals((EmployeeId)obj);
        }

        public static bool operator ==(EmployeeId left, EmployeeId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EmployeeId left, EmployeeId right)
        {
            return !(left == right);
        }
    }

    public interface ISomething
    {
        bool InsertSomething(TestType testType);
        TestType GetSomething(string id);
    }

    public class ASomething : ISomething
    {
        public bool InsertSomething(TestType testType)
        {
            return true;
        }

        public TestType GetSomething(string id)
        {
            return new TestType();
        }
    }

    public class BSomething : ISomething
    {
        public bool InsertSomething(TestType testType)
        {
            return true;
        }

        public TestType GetSomething(string id)
        {
            return new TestType();
        }
    }

    #region 代理类
    public interface IMath
    {
        double Add(double x, double y);
        double Sub(double x, double y);
    }

    public class MathProxy : IMath
    {
        private Math math;

        public MathProxy()
        {
            AppDomain ad = AppDomain.CreateDomain("MathDomain", null, null);

            ObjectHandle obj = ad.CreateInstance("MyProxy", "MyProxy.Math");

            this.math = obj.Unwrap() as Math;
        }


        public double Add(double x, double y)
        {
            return math.Add(x, y);
        }

        public double Sub(double x, double y)
        {
            return math.Sub(x, y);
        }
    }

    public class Math : MarshalByRefObject, IMath
    {
        public double Add(double x, double y)
        {
            return x + y;
        }

        public double Sub(double x, double y)
        {
            return x - y;
        }
    }
    #endregion

    public class DayColor
    {
        public static readonly Color todayColor;//静态字段只能在定义的时候初始化或者在静态构造函数里初始化

        static DayColor()
        {
            DateTime dt = DateTime.Now;
            if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
            {
                todayColor = Color.Green;
            }
            else
            {
                todayColor = Color.Red;
            }
        }
    }

    [Serializable]
    public class Racer : IComparable<Racer>, IFormattable 
    {
        public Racer(string firstName, string lastName, string country, int starts, int wins, IEnumerable<int> years, IEnumerable<string> cars)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Country = country;
            this.Starts = starts;
            this.Wins = wins;


            var yearsList = new List<int>();
            foreach (var year in years)
            {
                yearsList.Add(year);
            }
            this.Years = yearsList.ToArray();
            var carList = new List<string>();
            foreach (var car in cars)
            {
                carList.Add(car);
            }
            this.Cars = carList.ToArray();

        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public int Wins { get; set; }
        public int Starts { get; set; }
        public string[] Cars { get; private set; }
        public int[] Years { get; private set; }

        public override string ToString()
        {
            return String.Format("{0} {1}", FirstName, LastName);
        }

        public int CompareTo(Racer other)
        {
            if (other == null) throw new ArgumentNullException("other");

            return this.LastName.CompareTo(other.LastName);
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case null:
                case "N":
                    return ToString();
                case "F":
                    return FirstName;
                case "L":
                    return LastName;
                case "C":
                    return Country;
                case "S":
                    return Starts.ToString();
                case "W":
                    return Wins.ToString();
                case "A":
                    return String.Format("{0} {1}, {2}; starts: {3}, wins: {4}",
                          FirstName, LastName, Country, Starts, Wins);
                default:
                    throw new FormatException(String.Format("Format {0} not supported", format));
            }

           
        }
     }

    [Serializable]
    public class Team
    {
        public Team(string name, params int[] years)
        {
            this.Name = name;
            this.Years = years;
        }
        public string Name { get; private set; }
        public int[] Years { get; private set; }
    }

    public static class Formula1
    {
        private static List<Racer> racers;

        public static IList<Racer> GetChampions()
        {
            if (racers == null)
            {
                racers = new List<Racer>(40);
                racers.Add(new Racer("Nino", "Farina", "Italy", 33, 5, new int[] { 1950 }, new string[] { "Alfa Romeo" }));
                racers.Add(new Racer("Alberto", "Ascari", "Italy", 32, 10, new int[] { 1952, 1953 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("Juan Manuel", "Fangio", "Argentina", 51, 24, new int[] { 1951, 1954, 1955, 1956, 1957 }, new string[] { "Alfa Romeo", "Maserati", "Mercedes", "Ferrari" }));
                racers.Add(new Racer("Mike", "Hawthorn", "UK", 45, 3, new int[] { 1958 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("Phil", "Hill", "USA", 48, 3, new int[] { 1961 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("John", "Surtees", "UK", 111, 6, new int[] { 1964 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("Jim", "Clark", "UK", 72, 25, new int[] { 1963, 1965 }, new string[] { "Lotus" }));
                racers.Add(new Racer("Jack", "Brabham", "Australia", 125, 14, new int[] { 1959, 1960, 1966 }, new string[] { "Cooper", "Brabham" }));
                racers.Add(new Racer("Denny", "Hulme", "New Zealand", 112, 8, new int[] { 1967 }, new string[] { "Brabham" }));
                racers.Add(new Racer("Graham", "Hill", "UK", 176, 14, new int[] { 1962, 1968 }, new string[] { "BRM", "Lotus" }));
                racers.Add(new Racer("Jochen", "Rindt", "Austria", 60, 6, new int[] { 1970 }, new string[] { "Lotus" }));
                racers.Add(new Racer("Jackie", "Stewart", "UK", 99, 27, new int[] { 1969, 1971, 1973 }, new string[] { "Matra", "Tyrrell" }));
                racers.Add(new Racer("Emerson", "Fittipaldi", "Brazil", 143, 14, new int[] { 1972, 1974 }, new string[] { "Lotus", "McLaren" }));
                racers.Add(new Racer("James", "Hunt", "UK", 91, 10, new int[] { 1976 }, new string[] { "McLaren" }));
                racers.Add(new Racer("Mario", "Andretti", "USA", 128, 12, new int[] { 1978 }, new string[] { "Lotus" }));
                racers.Add(new Racer("Jody", "Scheckter", "South Africa", 112, 10, new int[] { 1979 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("Alan", "Jones", "Australia", 115, 12, new int[] { 1980 }, new string[] { "Williams" }));
                racers.Add(new Racer("Keke", "Rosberg", "Finland", 114, 5, new int[] { 1982 }, new string[] { "Williams" }));
                racers.Add(new Racer("Niki", "Lauda", "Austria", 173, 25, new int[] { 1975, 1977, 1984 }, new string[] { "Ferrari", "McLaren" }));
                racers.Add(new Racer("Nelson", "Piquet", "Brazil", 204, 23, new int[] { 1981, 1983, 1987 }, new string[] { "Brabham", "Williams" }));
                racers.Add(new Racer("Ayrton", "Senna", "Brazil", 161, 41, new int[] { 1988, 1990, 1991 }, new string[] { "McLaren" }));
                racers.Add(new Racer("Nigel", "Mansell", "UK", 187, 31, new int[] { 1992 }, new string[] { "Williams" }));
                racers.Add(new Racer("Alain", "Prost", "France", 197, 51, new int[] { 1985, 1986, 1989, 1993 }, new string[] { "McLaren", "Williams" }));
                racers.Add(new Racer("Damon", "Hill", "UK", 114, 22, new int[] { 1996 }, new string[] { "Williams" }));
                racers.Add(new Racer("Jacques", "Villeneuve", "Canada", 165, 11, new int[] { 1997 }, new string[] { "Williams" }));
                racers.Add(new Racer("Mika", "Hakkinen", "Finland", 160, 20, new int[] { 1998, 1999 }, new string[] { "McLaren" }));
                racers.Add(new Racer("Michael", "Schumacher", "Germany", 250, 91, new int[] { 1994, 1995, 2000, 2001, 2002, 2003, 2004 }, new string[] { "Benetton", "Ferrari" }));
                racers.Add(new Racer("Fernando", "Alonso", "Spain", 132, 21, new int[] { 2005, 2006 }, new string[] { "Renault" }));
                racers.Add(new Racer("Kimi", "Räikkönen", "Finland", 148, 17, new int[] { 2007 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("Lewis", "Hamilton", "UK", 44, 9, new int[] { 2008 }, new string[] { "McLaren" }));
            }

            return racers;
        }

        private static List<Team> teams;
        public static IList<Team> GetContructorChampions()
        {
            if (teams == null)
            {
                teams = new List<Team>()
                {
                    new Team("Vanwall", 1958),
                    new Team("Cooper", 1959, 1960),
                    new Team("Ferrari", 1961, 1964, 1975, 1976, 1977, 1979, 1982, 1983, 1999, 2000, 2001, 2002, 2003, 2004, 2007, 2008),
                    new Team("BRM", 1962),
                    new Team("Lotus", 1963, 1965, 1968, 1970, 1972, 1973, 1978),
                    new Team("Brabham", 1966, 1967),
                    new Team("Matra", 1969),
                    new Team("Tyrrell", 1971),
                    new Team("McLaren", 1974, 1984, 1985, 1988, 1989, 1990, 1991, 1998),
                    new Team("Williams", 1980, 1981, 1986, 1987, 1992, 1993, 1994, 1996, 1997),
                    new Team("Benetton", 1995),
                    new Team("Renault", 2005, 2006 )
                };
            }
            return teams;
        }
    }

    public class Generics<T> //where T : IComparable
    {
        private T field;

        public T Field
        {
            get { return field; }
            set { field = value; }
        }

        public T Connect<T>(Generics<T> latter)
        {
            return latter.field;
        }
    }

    #region 报表数据源结构
    public class TriageInfoDataTable : DataTable
    {
        public TriageInfoDataTable()
        {
            this.Columns.Add("DIVISION");
            this.Columns.Add("ONE_LEVEL");
            this.Columns.Add("TWO_LEVEL");
            this.Columns.Add("THREE_LEVEL");
            this.Columns.Add("FOUR_LEVEL");
            this.Columns.Add("AMOUNT");
        }
    }

    #endregion
}
