using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMT.Common
{
    public static class Extention
    {
        /// <summary>
        /// 处理datatable，整理成导出excel需要的格式
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static System.Data.DataTable ConvertDataTable(this System.Data.DataTable dt, string[] strs)
        {
            //将不再Strs里的列删除
            for (int j = dt.Columns.Count - 1; j >= 0; j--)
            {
                bool isIn = false;
                for (int i = 0; i < strs.Length; i++)
                {
                    if (dt.Columns[j].ColumnName == strs[i])
                    {
                        isIn = true;
                        break;
                    }
                }
                if (!isIn)
                    dt.Columns.Remove(dt.Columns[j].ColumnName);
            }

            //调整列的位置
            for (int k = 0; k < strs.Length; k++)
            {
                dt.Columns[strs[k]].SetOrdinal(k);
            }
            return dt;
        }
    }
}