using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Reflection;
using MyProgram.GlobalTypes;

namespace MyProgram.GlobalFuncs
{
    public class SomethingFactory
    {
        private static string assemblyName = "MyProgram";
        private static string kind = ConfigurationSettings.AppSettings["Kind"];

        public static ISomething CreateSomething()
        {
            string className = "MyProgram.GlobalTypes." + kind + "Something";
            return (ISomething)Assembly.Load(assemblyName).CreateInstance(className);
        }
    }
}
