/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorpFundsAllotContractBLL.cs
// 文件功能描述：集团或公司款分配至合约dbo.Fun_CorpFundsAllotContract_Ref业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
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
    /// 集团或公司款分配至合约dbo.Fun_CorpFundsAllotContract_Ref业务逻辑类。
    /// </summary>
    public class CorpFundsAllotContractBLL : Common.DataBLL
    {
        private CorpFundsAllotContractDAL corpfundsallotcontractDAL = new CorpFundsAllotContractDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CorpFundsAllotContractDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public CorpFundsAllotContractBLL()
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
            get { return this.corpfundsallotcontractDAL; }
        }
        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int empId, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ra.ReceivableAllotId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" ra.ReceivableAllotId,ra.AllotTime,ra.AllotDesc,CONVERT(varchar,ra.AllotBala) + c.CurrencyName as AllotBala,e.Name,ra.AllotStatus,bd.StatusName,sub.SubNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_ReceivableAllot ra ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on ra.CurrencyId = c.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on ra.EmpId =e.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on ra.AllotStatus = bd.DetailId and StatusId = {0} ", statusId);
            sb.Append(" right join (select distinct AllotId,SubContractId from dbo.Fun_CorpFundsAllotContract_Ref ) as ref  on ref.AllotId = ra.ReceivableAllotId ");
            sb.Append(" left join dbo.Con_ContractSub sub on sub.SubId = ref.SubContractId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1");
            if (status > 0)
                sb.AppendFormat(" and ra.AllotStatus = {0} ", status);
            if (empId > 0)
                sb.AppendFormat(" and ra.EmpId = {0} ", empId);

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion
    }
}
