using System;
using System.Collections.Generic;
using System.Text;
using grproLib;

namespace MyProgram.GlobalTypes
{
    /// <summary>
    /// 报表
    /// </summary>
    public struct MatchFieldPairType
    {
        public IGRField field;//报表字段
        public int MatchColumnIndex;//报表字段对应的数据源的列下标
    }
}
