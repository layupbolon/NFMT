/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RecAllotDetailBLL.cs
// 文件功能描述：收款分配明细dbo.Fun_RecAllotDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Funds.Model;
using NFMT.Funds.DAL;
using NFMT.Funds.IDAL;
using NFMT.Common;

namespace NFMT.Funds.BLL
{
    /// <summary>
    /// 收款分配明细dbo.Fun_RecAllotDetail业务逻辑类。
    /// </summary>
    public class RecAllotDetailBLL : Common.ExecBLL
    {
        private RecAllotDetailDAL recallotdetailDAL = new RecAllotDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(RecAllotDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public RecAllotDetailBLL()
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
            get { return this.recallotdetailDAL; }
        }
		
        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int allotId, NFMT.Common.StatusEnum status = StatusEnum.已生效)
        {
            return this.recallotdetailDAL.Load(user, allotId, status);
        }

        #endregion
    }
}
