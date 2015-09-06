/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StocktAttachBLL.cs
// 文件功能描述：库存附件dbo.StocktAttach业务逻辑类。
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
    /// 库存附件dbo.StocktAttach业务逻辑类。
    /// </summary>
    public class StocktAttachBLL : Common.DataBLL
    {
        private StocktAttachDAL stocktattachDAL = new StocktAttachDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StocktAttachDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public StocktAttachBLL()
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
            get { return this.stocktattachDAL; }
        }

        #endregion
    }
}
