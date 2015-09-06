/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BrandAssetBLL.cs
// 文件功能描述：dbo.BrandAsset业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年5月4日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Data.Model;
using NFMT.Data.DAL;
using NFMT.Data.IDAL;
using NFMT.Common;

namespace NFMT.Data.BLL
{
    /// <summary>
    /// dbo.BrandAsset业务逻辑类。
    /// </summary>
    public class BrandAssetBLL : Common.DataBLL
    {
        private BrandAssetDAL brandassetDAL = new BrandAssetDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(BrandAssetDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public BrandAssetBLL()
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
            get { return this.brandassetDAL; }
        }

        #endregion
    }
}
