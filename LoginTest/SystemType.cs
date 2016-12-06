using System;
using System.Collections.Generic;
using System.Text;

namespace LoginTest
{
    public struct ReturnValue
    {
        public const short OK = 1;
        public const short FAILED = 2;
    }

    public class UserInfo
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
