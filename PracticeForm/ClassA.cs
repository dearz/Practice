using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace PracticeForm
{
    public enum week
    {
        Monday,
        Tuesday
    }

    public static class GlobalFunc
    {
        public static int CompareClassA(ClassA x, ClassA y)
        {
            return int.Parse(x.Id).CompareTo(int.Parse(y.Id));
        }

        public static object ObjectClone(object objSource)
        {
            BinaryFormatter BF = new BinaryFormatter();
            MemoryStream MS = new MemoryStream();

            BF.Serialize(MS, objSource);
            MS.Position = 0;

            return BF.Deserialize(MS);
        }

        public static object ObjectCloneFunc(object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            formatter.Serialize(stream, obj);
            stream.Seek(0, SeekOrigin.Begin);
            return formatter.Deserialize(stream);
        }
    }

    public class ClassA : IComparable<ClassA>, IFormattable
    {
        public ClassA(string name, string id)
        {
            this.id = id;
            this.name = name;
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public override string ToString()
        {
            return this.Name;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Empty;
        }

        public int CompareTo(ClassA a)
        {
            return int.Parse(id).CompareTo(int.Parse(a.Id));
        }
    }

    public class ComparerClassA : IComparer<ClassA>
    {
        public int Compare(ClassA x, ClassA y)
        {
            return x.CompareTo(y);
        }
    }

    public class FormatClassA : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(IFormatProvider))
                return this;
            else
                return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            switch(format)
            {
                case "a":
                    break;
                case "b":
                    break;
                case "c":
                    break;
                default:
                    break;
            }
            return string.Empty;
        }
    }


}
