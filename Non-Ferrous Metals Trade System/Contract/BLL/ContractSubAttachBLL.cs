/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractSubAttachBLL.cs
// 文件功能描述：子合约附件dbo.Con_ContractSubAttach业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
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
    /// 子合约附件dbo.Con_ContractSubAttach业务逻辑类。
    /// </summary>
    public class ContractSubAttachBLL : Common.ExecBLL
    {
        private ContractSubAttachDAL contractsubattachDAL = new ContractSubAttachDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ContractSubAttachDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractSubAttachBLL()
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
            get { return this.contractsubattachDAL; }
        }
        #endregion
    }
}
