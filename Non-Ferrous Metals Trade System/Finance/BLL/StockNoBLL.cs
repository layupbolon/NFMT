/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockNoBLL.cs
// 文件功能描述：融资单号表dbo.Fin_StockNo业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年5月14日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Finance.Model;
using NFMT.Finance.DAL;
using NFMT.Finance.IDAL;
using NFMT.Common;

namespace NFMT.Finance.BLL
{
    /// <summary>
    /// 融资单号表dbo.Fin_StockNo业务逻辑类。
    /// </summary>
    public class StockNoBLL : Common.ExecBLL
    {
        private StockNoDAL stocknoDAL = new StockNoDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StockNoDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public StockNoBLL()
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
            get { return this.stocknoDAL; }
        }
		
        #endregion
    }
}
