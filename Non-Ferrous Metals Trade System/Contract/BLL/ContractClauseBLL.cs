/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractClauseBLL.cs
// 文件功能描述：合约条款关联dbo.Con_ContractClause_Ref业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年1月13日
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
    /// 合约条款关联dbo.Con_ContractClause_Ref业务逻辑类。
    /// </summary>
    public class ContractClauseBLL : Common.ExecBLL
    {
        private ContractClauseDAL contractclauseDAL = new ContractClauseDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ContractClauseDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractClauseBLL()
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
            get { return this.contractclauseDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel LoadClauseByContractId(UserModel user, int contractId)
        {
            return contractclauseDAL.LoadClauseByContractId(user, contractId);
        }

        #endregion
    }
}
