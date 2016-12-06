using System;
using System.Collections.Generic;
using System.Text;

namespace LoginTest
{
    public class DataService
    {
        private static DataService instance = null;

        public static DataService Instance
        {
            get
            {
                if (instance == null)
                    return new DataService();
                return instance;
            }
        }

        private DataService()
        {
        }

        public short GetUserInfo(string userId, ref UserInfo userInfo)
        {
            return SystemContext.Instance.DataAccess.GetUserInfo(userId,ref userInfo);
        }

        public short IsUserValid(string userId, string password)
        {
            return SystemContext.Instance.DataAccess.IsUserValid(userId, password);
        }

        public bool SaveTextFiles(string stringFile)
        {
            return SystemContext.Instance.DataAccess.SaveTextFiles(stringFile);
        }
    }
}
