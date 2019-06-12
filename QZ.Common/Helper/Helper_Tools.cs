using System;
using System.Collections.Generic;
using System.Text;

namespace QZ.Common.Helper
{
    public static class Helper_Tools
    {
        /// <summary>
        /// 通过总行数得到总页数
        /// </summary>
        /// <param name="PageCount">总行数</param>
        /// <param name="size">每页显示行数</param>
        /// <returns></returns>
        public static int GetPageCount(this int PageCount, int size)
        {
            int count = PageCount / size;
            if (PageCount % size > 0)
            {
                count++;
            }
            return count;
        }
    }
}
