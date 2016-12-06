using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace TestProgram
{
    public static class MyContainer
    {
        private static Dictionary<Type, object> _stores = null;

        private static Dictionary<Type, object> Stores
        {
            get
            {
                if (_stores == null)
                    _stores = new Dictionary<Type, object>();
                return _stores;
            }
        }

        private static Dictionary<string, object> CreateConstructorParameter(Type targetType)
        {
            Dictionary<string, object> paramArray = new Dictionary<string, object>();

            ConstructorInfo[] cis = targetType.GetConstructors();
            if (cis.Length > 1)
                throw new Exception("target object has more then one constructor,container can't peek one for you.");

            foreach (ParameterInfo pi in cis[0].GetParameters())
            {
                if (Stores.ContainsKey(pi.ParameterType))
                    paramArray.Add(pi.Name, GetInstance(pi.ParameterType));
            }
            return paramArray;
        }

        public static object GetInstance(Type t)
        {
            if (Stores.ContainsKey(t))
            {
                ConstructorInfo[] cis = t.GetConstructors();
                if (cis.Length != 0)
                {
                    Dictionary<string, object> paramArray = CreateConstructorParameter(t);
                    List<object> cArray = new List<object>();
                    foreach (ParameterInfo pi in cis[0].GetParameters())
                    {
                        if (paramArray.ContainsKey(pi.Name))
                            cArray.Add(paramArray[pi.Name]);
                        else
                            cArray.Add(null);
                    }
                    //在这里完成了对构造函数的调用，而构造函数的传入参数是通过在容器中查找匹配类型的实例得到的，
                    //所以被称为构造器注入。
                    return cis[0].Invoke(cArray.ToArray());
                }
                else if (Stores[t] != null)
                    return Stores[t];
                else
                    return Activator.CreateInstance(t, false);
            }
            return Activator.CreateInstance(t, false);
        }

        //向容器中加入ServiceProvider的实例
        public static void RegisterImplement(Type t, object impl)
        {
            if (Stores.ContainsKey(t))
                Stores[t] = impl;
            else
                Stores.Add(t, impl);
        }

        //向容器中加入ServiceUser的类型，类型的构造器将在容器中被调用
        public static void RegisterImplement(Type t)
        {
            if (!Stores.ContainsKey(t))
                Stores.Add(t, null);
        }
    }

    public class User
    {
        private MyProvider p;
        public User(MyProvider p)
        {
            this.p = p;
        }
    }

    public class MyProvider
    {
        private Y y;
        public MyProvider(Y y)
        {
            this.y = y;
        }
    }

    public class Y
    {
        private string name;
        public Y(string name)
        {
            this.name = name;
        }
    }
}
