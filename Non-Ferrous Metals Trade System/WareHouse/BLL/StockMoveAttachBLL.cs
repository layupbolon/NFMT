/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockMoveAttachBLL.cs
// 文件功能描述：移库附件dbo.StockMoveAttach业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.WareHouse.Model;
using NFMT.WareHouse.DAL;
using NFMT.WareHouse.IDAL;
using NFMT.Common;

namespace NFMT.WareHouse.BLL
{
    /// <summary>
    /// 移库附件dbo.StockMoveAttach业务逻辑类。
    /// </summary>
    public class StockMoveAttachBLL : Common.DataBLL
    {
        private StockMoveAttachDAL stockmoveattachDAL = new StockMoveAttachDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StockMoveAttachDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public StockMoveAttachBLL()
		{
		}
        
		#endregion

        #region 数据库操作

        protected override log4net.ILog Log
        {
            get { return this.log; }
        }

        public override IOperate Operate
        {
            get { return this.stockmoveattachDAL; }
        }

        #endregion
    }
}
