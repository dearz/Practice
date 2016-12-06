using System;
using System.Collections.Generic;
using System.Text;

namespace MyProgram.GlobalFuncs
{
    public class CommFunc
    {
        /// <summary>
        /// 排序一个整数数组
        /// </summary>
        /// <param name="list"></param>
        public static void SortInt(int[] list)
        {
            int index = 0;
            int tempData = 0;
            for (int i = 0; i < list.Length-1; i++)
            {
                index = i;
                for (int j = i; j < list.Length; j++)
                {
                    if (list[j] < list[index])
                        index = j;
                }
                tempData = list[i];
                list[i] = list[index];
                list[index] = tempData;
            }
        }

        /// <summary>
        ///字符串右侧填充指定数目字节数字符（中英文混写有效）
        /// </summary>
        /// <param name="str"></param>
        /// <param name="totalByteCount"></param>
        /// <returns></returns>
        private string padRightEx(string str, int totalByteCount)
        {
            Encoding coding = Encoding.GetEncoding(0);
            int dcount = 0;
            int strCount = coding.GetByteCount(str);
            if (strCount > totalByteCount)
            {
                int i = 0;
                int n = 0;
                char[] charArrary = str.ToCharArray();
                for (i = 0; i < charArrary.Length; i++)
                {
                    n = n + coding.GetByteCount(charArrary[i].ToString());
                    if (n > totalByteCount)
                        break;
                }
                str = str.Substring(0, i - 1);
            }
            foreach (char ch in str.ToCharArray())
            {
                if (coding.GetByteCount(ch.ToString()) == 2)
                    dcount++;
            }
            string w = str.PadRight(totalByteCount - dcount);
            return w;
        }
    }
}
