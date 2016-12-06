using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoginTest
{
    public class DataAccess
    {
        public short GetUserInfo(string userId, ref UserInfo userInfo)
        {
            if (userId == "123")
            {
                if (userInfo == null)
                    userInfo = new UserInfo();
                userInfo.Id = "123";
                userInfo.Name = "zhangsan";
                return ReturnValue.OK;
            }
            else if(userId=="1234")
            {
                if (userInfo == null)
                    userInfo = new UserInfo();
                userInfo.Id = "1234";
                userInfo.Name = "lisi";
                return ReturnValue.OK;
            }
            else
            {
                return ReturnValue.FAILED;
            }
        }

        public short IsUserValid(string userId, string password)
        {
            if (password == "123")
                return ReturnValue.OK;
            else
                return ReturnValue.FAILED;
        }

        public bool SaveTextFiles(string stringFile)
        {
            string filePath = @".\userFile\userInfo.txt";
            FileStream fs;
            if (!File.Exists(filePath))
                fs = File.Create(filePath);
            else
                fs = File.Open(filePath, FileMode.Append, FileAccess.Write);
            
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.WriteLine(stringFile);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sw.Close();
                fs.Close();
            }          
        }
    }
}
