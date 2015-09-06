/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BDStyleDetailBLL.cs
// 文件功能描述：基础类型编码明细表dbo.BDStyleDetail业务逻辑类。
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
    /// 基础类型编码明细表dbo.BDStyleDetail业务逻辑类。
    /// </summary>
    public class BDStyleDetailBLL : Common.DataBLL
    {
        private BDStyleDetailDAL bdstyledetailDAL = new BDStyleDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(BDStyleDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public BDStyleDetailBLL()
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
            get { return this.bdstyledetailDAL; }
        }
        
        #endregion
    }
}
