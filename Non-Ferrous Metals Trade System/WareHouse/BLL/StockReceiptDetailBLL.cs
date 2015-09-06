/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockReceiptDetailBLL.cs
// 文件功能描述：回执明细dbo.St_StockReceiptDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年9月3日
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
    /// 回执明细dbo.St_StockReceiptDetail业务逻辑类。
    /// </summary>
    public class StockReceiptDetailBLL : Common.ExecBLL
    {
        private StockReceiptDetailDAL stockreceiptdetailDAL = new StockReceiptDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StockReceiptDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public StockReceiptDetailBLL()
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
            get { return this.stockreceiptdetailDAL; }
        }
		
        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int receiptId,Common.StatusEnum status = StatusEnum.已生效)
        {
            return stockreceiptdetailDAL.Load(user, receiptId,status);
        }

        #endregion
    }
}
