/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SelectModel.cs
// 文件功能描述：分页查询实体。
// 创建人：pekah.chow
// 创建时间： 2014-04-29
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace NFMT.Common
{
    [Serializable()]
    public class SelectModel
    {
        private int pageIndex = 1;
        /// <summary>
        /// 当前页索引
        /// </summary>
        public int PageIndex 
        { 
            get { return this.pageIndex; } 
            set { this.pageIndex = value; } 
        }

        private int pageSize = 10;
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize 
        { 
            get { return this.pageSize; } 
            set { this.pageSize = value; } 
        }

        private string columnName = string.Empty;
        /// <summary>
        /// 查询，不包含 select 关键字
        /// </summary>
        public string ColumnName
        { 
            get { return this.columnName; } 
            set { this.columnName = value; } 
        }

        private string tableName = string.Empty;
        /// <summary>
        /// 查询表，不包含 from
        /// </summary>
        public string TableName
        {
            get { return this.tableName; }
            set { this.tableName = value; }
        }

        private string whereStr = string.Empty;
        /// <summary>
        /// 查询条件，不包含 where 关键字
        /// </summary>
        public string WhereStr
        {
            get { return this.whereStr; }
            set { this.whereStr = value; }
        }

        private string orderStr = string.Empty;
        /// <summary>
        /// 排序，不包含 order by 关键字，包含 desc/asc
        /// </summary>
        public string OrderStr
        {
            get { return this.orderStr; }
            set { this.orderStr = value; }
        }
    }
}
