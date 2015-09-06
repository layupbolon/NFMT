/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ChartJson.cs
// 文件功能描述：ChartModel实体输出Json，以满足HighCharts的要求。
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
    public class ChartJson
    {
        public static string CreateJosn(ChartModel chart)
        {
            System.Text.StringBuilder sb = new StringBuilder();

            //第一花括号
            sb.Append("{ ");

            //标题
            if(!string.IsNullOrEmpty(chart.Title))
                sb.AppendFormat("\"Title\":[\"{0}\"],",chart.Title);

            //副标题
            if (!string.IsNullOrEmpty(chart.SubTitle))
                sb.AppendFormat("\"SubTitle\":[\"{0}\"],", chart.SubTitle);

            //x坐标
            if (chart.XAxis != null)
            {
                sb.Append("\"XAxis\":{");
                //categories
                if (chart.XAxis.Categories == null)
                {
                    chart.XAxis.Categories = new List<string>();
                }

                if(chart.XAxis.Categories.Count ==0)
                {
                    if (SourcScheme.CheckTableScheme(chart.SourceTable))
                    {
                        foreach (System.Data.DataColumn column in chart.SourceTable.Columns)
                        {
                            chart.XAxis.Categories.Add(column.ColumnName);
                        }
                    }
                }

                sb.Append("\"Categories\":[");
                for (int i = 0; i < chart.XAxis.Categories.Count; i++)
                {
                    string category = chart.XAxis.Categories[i];
                    sb.AppendFormat("\"{0}\"", category);
                    if (i != chart.XAxis.Categories.Count - 1)
                        sb.Append(",");
                }
                sb.Append("]");

                sb.Append("},");
            }

            //y坐标
            if (chart.YAxis != null)
            {
                sb.Append("\"YAxis\":{");

                if (!string.IsNullOrEmpty(chart.YAxis.Title))
                    sb.AppendFormat("\"Title\":[\"{0}\"]",chart.YAxis.Title);

                sb.Append("},");
            }

            //Tooltip
            if (!string.IsNullOrEmpty(chart.Tooltip))
                sb.AppendFormat("\"Tooltip\":[\"{0}\"],",chart.Tooltip);

            //SourceTable
            if (chart.SourceTable != null && SourcScheme.CheckTableScheme(chart.SourceTable))
            {
                chart.SourceTable.TableName = "Series";                
                sb.Append("\"Series\":"); 
                TableAdapter adapter = new TableAdapter();
                sb.Append(adapter.AdapterJson(chart.SourceTable));
            }

            //最后花括号
            sb.Append("}");
            return sb.ToString();
        }
    }
}
