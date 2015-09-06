/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SubTypeDetailBLL.cs
// 文件功能描述：子合约类型明细dbo.Con_SubTypeDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Contract.Model;
using NFMT.Contract.DAL;
using NFMT.Contract.IDAL;
using NFMT.Common;

namespace NFMT.Contract.BLL
{
    /// <summary>
    /// 子合约类型明细dbo.Con_SubTypeDetail业务逻辑类。
    /// </summary>
    public class SubTypeDetailBLL : Common.ExecBLL
    {
        private SubTypeDetailDAL subtypedetailDAL = new SubTypeDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(SubTypeDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public SubTypeDetailBLL()
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
            get { return this.subtypedetailDAL; }
        }
		
        #endregion

        #region 新增方法
        public ResultModel LoadSubTypesById(UserModel user, int subId)
        {
            return this.subtypedetailDAL.LoadSubTypesById(user, subId);
        }
        #endregion 
    }
}
