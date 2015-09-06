/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractTypeDetailBLL.cs
// 文件功能描述：合约类型明细dbo.Con_ContractTypeDetail业务逻辑类。
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
    /// 合约类型明细dbo.Con_ContractTypeDetail业务逻辑类。
    /// </summary>
    public class ContractTypeDetailBLL : Common.ExecBLL
    {
        private ContractTypeDetailDAL contracttypedetailDAL = new ContractTypeDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ContractTypeDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public ContractTypeDetailBLL()
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
            get { return this.contracttypedetailDAL; }
        }
		
        #endregion

        #region 新增方法

        public ResultModel LoadContractTypesById(UserModel user, int contractId)
        {
            return this.contracttypedetailDAL.LoadContractTypesById(user, contractId);
        }

        #endregion
    }
}
