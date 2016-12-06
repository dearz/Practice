using System;
using System.Collections.Generic;
using System.Text;

namespace LoginTest
{
    public class SystemContext
    {
        private static SystemContext instance = null;
 
        public static SystemContext Instance
        {
            get
            {
                if (instance == null)
                    return new SystemContext();
                return instance;
            }
        }

        private UserInfo loginUser = null;

        public UserInfo LoginUser
        {
            get
            {
                return loginUser;
            }
            set
            {
                loginUser = value;
            }
        }

        private DataAccess dataAccess = null;

        public DataAccess DataAccess
        {
            get
            {
                if (dataAccess == null)
                    return new DataAccess();
                return dataAccess;
            }
        }
    }
}
