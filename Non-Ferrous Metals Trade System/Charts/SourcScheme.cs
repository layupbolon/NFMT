/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SourcScheme.cs
// 文件功能描述：数据源格式验证。
// 创建人：pekah.chow
// 创建时间： 2014-06-26
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Charts
{
    public class SourcScheme
    {
        /// <summary>
        /// 验证DataTable是否符合格式
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool CheckTableScheme(System.Data.DataTable table)
        {
            if (table == null)
                return false;

            if (table.Columns.Count < 2)
                return false;

            return true;
        }
    }
}
