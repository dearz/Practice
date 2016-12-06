using System;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Reflection;

namespace MyProgram.GlobalTypes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class SqlParameterAttribute : Attribute
    {
        private string name;

        public string Name
        {
            get { return string.IsNullOrEmpty(name) ? string.Empty : name; }
            set { name = value; }
        }

        public bool IsNameDefined
        {
            get
            {
                return !string.IsNullOrEmpty(name);
            }
        }

        private SqlDbType parameterType;

        public SqlDbType ParameterType
        {
            get { return parameterType; }
            set
            {
                parameterType = value;
                isParameterTypeDefined = true;
            }
        }

        private bool isParameterTypeDefined = false;

        public bool IsParameterTypeDefined
        {
            get { return isParameterTypeDefined; }
        }

        private int size;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public bool IsSizeDefined
        {
            get
            {
                return size != 0;
            }
        }

        private byte percision;

        public byte Percision
        {
            get { return percision; }
            set { percision = value; }
        }

        public bool IsPercisionDefined
        {
            get
            {
                return percision != 0;
            }
        }

        private byte scale;

        public byte Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public bool IsScaleDefined
        {
            get
            {
                return scale != 0;
            }
        }

        private ParameterDirection parameterDirection;

        public ParameterDirection ParameterDirection
        {
            get { return parameterDirection; }
            set
            {
                parameterDirection = value;
                isParameterDirectionDefined = true;
            }
        }

        private bool isParameterDirectionDefined = false;

        public bool IsParameterDirectionDefined
        {
            get { return isParameterDirectionDefined; }
        }

        public SqlParameterAttribute()
        {
        }

        public SqlParameterAttribute(string name)
        {
            this.name = name;
        }

        public SqlParameterAttribute(SqlDbType parameterType)
        {
            this.parameterType = parameterType;
        }

        public SqlParameterAttribute(int size)
        {
            this.size = size;
        }

        public SqlParameterAttribute(string name, SqlDbType parameterType, int size)
        {
            this.name = name;
            this.parameterType = parameterType;
            this.size = size;
        }

 
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class NotSqlParamterAttribute : Attribute
    {
        public NotSqlParamterAttribute()
        {
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class SqlCommandMethedAttribute : Attribute
    {
        private string commandText;

        public string CommandText
        {
            get { return commandText; }
            set { commandText = value; }
        }

        private CommandType commandType;

        public CommandType CommandType
        {
            get { return commandType; }
            set { commandType = value; }
        }

        public SqlCommandMethedAttribute(CommandType commandType)
            : this(commandType, null)
        {

        }

        public SqlCommandMethedAttribute(CommandType commandType, string commandText)
        {
            this.commandType = commandType;
            this.commandText = commandText;
        }

    }

    public class SqlCommandGenerator
    {
        public static readonly string ReturnValueParameterName = "RETURN_VALUE";

        public static readonly object[] NoValues = new object[] { };

        public static SqlCommand GenerateCommand(SqlConnection conn, MethodInfo method, object[] values)
        {
            if (method == null)
            {
                method = (MethodInfo)(new StackTrace()).GetFrame(1).GetMethod();
            }

            SqlCommand command = new SqlCommand();

            command.Connection = conn;

            SqlCommandMethedAttribute methodAttribute = (SqlCommandMethedAttribute)Attribute.GetCustomAttribute(method, typeof(SqlCommandMethedAttribute));

            command.CommandType = methodAttribute.CommandType;

            if (string.IsNullOrEmpty(methodAttribute.CommandText))
            {
                command.CommandText = method.Name;
            }
            else
            {
                command.CommandText = methodAttribute.CommandText;
            }

            GenerateCommandParameters(command, method, values);

            command.Parameters.Add(ReturnValueParameterName, SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            return command;

        }

        private static void GenerateCommandParameters(SqlCommand command, MethodInfo method, object[] values)
        {
            int parameterIndex = 0;
            ParameterInfo[] parameterInfos = method.GetParameters();

            foreach (ParameterInfo param in parameterInfos)
            {
                if (Attribute.IsDefined(param, typeof(NotSqlParamterAttribute)))
                {
                    continue;
                }

                SqlParameterAttribute sqlParameterAttribute = (SqlParameterAttribute)Attribute.GetCustomAttribute(param, typeof(SqlParameterAttribute));

                if (sqlParameterAttribute == null)
                {
                    sqlParameterAttribute = new SqlParameterAttribute();
                }

                SqlParameter sqlParameter = new SqlParameter();

                if (sqlParameterAttribute.IsNameDefined)
                {
                    sqlParameter.ParameterName = sqlParameterAttribute.Name;
                }
                else
                {
                    sqlParameter.ParameterName = param.Name;
                }

                if (!sqlParameter.ParameterName.StartsWith("@"))
                {
                    sqlParameter.ParameterName = "@" + sqlParameter.ParameterName;
                }

                if (sqlParameterAttribute.IsSizeDefined)
                    sqlParameter.Size = sqlParameterAttribute.Size;

                if (sqlParameterAttribute.IsPercisionDefined)
                    sqlParameter.Precision = sqlParameterAttribute.Percision;

                if (sqlParameterAttribute.IsSizeDefined)
                    sqlParameter.Scale = sqlParameterAttribute.Scale;

                if (sqlParameterAttribute.IsParameterTypeDefined)
                    sqlParameter.SqlDbType = sqlParameterAttribute.ParameterType;

                if (sqlParameterAttribute.IsParameterDirectionDefined)
                    sqlParameter.Direction = sqlParameterAttribute.ParameterDirection;
                else
                {
                    if (param.ParameterType.IsByRef)
                        sqlParameter.Direction = param.IsOut ? ParameterDirection.Input : ParameterDirection.InputOutput;
                    else
                        sqlParameter.Direction = ParameterDirection.Input;
                }

                sqlParameter.Value = values[parameterIndex];

                parameterIndex++;

                command.Parameters.Add(sqlParameter);
            }
        }
    }
}
