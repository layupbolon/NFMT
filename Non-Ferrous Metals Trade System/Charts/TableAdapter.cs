/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：TableAdapter.cs
// 文件功能描述：满足数据格式的System.Data.DataTable，输出相应的Json字符串。
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
    public class TableAdapter : IChartAdapter
    {
        public string AdapterJson(object obj)
        {
            if (!(obj is System.Data.DataTable))
                throw new Exception("不是DataTable数据类型");

            System.Data.DataTable dt = obj as System.Data.DataTable;
            if (!SourcScheme.CheckTableScheme(dt))
                throw new Exception("数据源格式错误");

            System.Text.StringBuilder sb = new StringBuilder();
            sb.AppendLine(" [ ");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                System.Data.DataRow dr = dt.Rows[i];

                System.Text.StringBuilder builder = new StringBuilder();
                builder.Append(" { ");

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j == 0)
                    {
                        builder.Append(" \"name\":");
                        builder.AppendFormat("\"{0}\",", dr[j].ToString());
                    }
                    else if (j == 1)
                    {
                        builder.Append(" \"data\": [ ");
                        builder.AppendFormat("{0}", dr[j].ToString());
                    }
                    else if (j < dt.Columns.Count - 1)
                    {
                        builder.Append(",");
                        builder.AppendFormat("{0}", dr[j].ToString());
                    }
                }
                builder.Append(" ] ");

                builder.Append(" } ");

                if (i < dt.Rows.Count - 1)
                    builder.Append(",");

                sb.Append(builder);
            }

            sb.AppendLine(" ] ");
            return sb.ToString();
        }
    }
}
