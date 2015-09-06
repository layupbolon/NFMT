/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractCorporationDetailBLL.cs
// 文件功能描述：合约抬头明细dbo.Con_ContractCorporationDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月16日
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
    /// 合约抬头明细dbo.Con_ContractCorporationDetail业务逻辑类。
    /// </summary>
    public class ContractCorporationDetailBLL:Common.ExecBLL
    {
        private ContractCorporationDetailDAL contractcorporationdetailDAL = new ContractCorporationDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ContractCorporationDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractCorporationDetailBLL()
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
            get { return this.contractcorporationdetailDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel LoadCorpListByContractId(UserModel user, int contractId, bool isSelf)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = contractcorporationdetailDAL.LoadCorpListByContractId(user, contractId, isSelf);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel LoadByContractId(UserModel user, int contractId, bool isSelf, StatusEnum status = StatusEnum.已生效)
        {
            return this.contractcorporationdetailDAL.LoadByContractId(user, contractId, isSelf, status);
        }

        #endregion
    }
}
