using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyProgram.GlobalTypes
{
    /// <summary>
    /// 实现了接口IEnumerable可以通过foreach进行遍历循环
    /// </summary>
    public class MyCollection : IEnumerable
    {
        private object[] items;
        public MyCollection(object[] items)
        {
            this.items = items;
        }
        public IEnumerator GetEnumerator()
        {
            return new MyEnumerator(items);
        }
    }

    /// <summary>
    /// 自定义的枚举数，实现IEnumerator即可
    /// </summary>
    public class MyEnumerator : IEnumerator
    {
        private object[] items;

        private int index = -1;

        public MyEnumerator()
        {
        }

        public MyEnumerator(int capacity)
        {
            items = new object[capacity];
        }

        public MyEnumerator(object[] obj)
        {
            int length = obj.Length;
            items = new object[length];
            for (int i = 0; i < length; i++)
            {
                items[i] = obj[i];
            }
        }

        #region 接口实现

        public object Current
        {
            get
            {
                if (index < 0)
                    throw new InvalidOperationException();
                else if (index > items.Length)
                    throw new InvalidOperationException();
                else
                    return items[index];
            }
        }

        public bool MoveNext()
        {
            if (index < items.Length - 1)
            {
                index++;
                return true;
            }
            else
                return false;
        }

        public void Reset()
        {
            index = -1;
        }

        #endregion
    }
}
