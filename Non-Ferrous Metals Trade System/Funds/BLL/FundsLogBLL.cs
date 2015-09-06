/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FundsLogBLL.cs
// 文件功能描述：资金流水dbo.Fun_FundsLog业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年12月11日
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
    /// 资金流水dbo.Fun_FundsLog业务逻辑类。
    /// </summary>
    public class FundsLogBLL : Common.ExecBLL
    {
        private FundsLogDAL fundslogDAL = new FundsLogDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(FundsLogDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FundsLogBLL()
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
            get { return this.fundslogDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int subId,Common.StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.Fun_FundsLog where SubId = {0} and LogStatus >={1}",subId,(int)status);
            ResultModel result = this.fundslogDAL.Load<Model.FundsLog>(user, CommandType.Text, cmdText);
            return result;
        }

        #endregion

        #region report

        public SelectModel GetFundsLogReportSelect(int pageIndex, int pageSize, string orderStr, DateTime startDate, DateTime endDate,int inCorpId,int outCorpId,int logType)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "fl.FundsLogId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("fl.FundsLogId,fl.LogDate,fl.InCorpId,inCorp.CorpName as InCorpName,fl.InBankId,inBank.BankName as InBankName,fl.InAccountId");
            sb.Append(",inAcc.AccountNo as InAccountNo,fl.OutCorpId,outCorp.CorpName as OutCorpName,fl.OutBankId,outBank.BankName as OutBankName");
            sb.Append(",fl.OutAccountId,outAcc.AccountNo as OutAccountNo,fl.LogType,lt.DetailName as LogTypeName,cur.CurrencyName");
            sb.Append(",fl.FundsBala,fl.IsVirtualPay,fl.PayMode,pm.DetailName as PayModeName");

            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT.dbo.Fun_FundsLog fl ");
            sb.Append(" left join NFMT_User.dbo.Corporation inCorp on inCorp.CorpId = fl.InCorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank inBank on inBank.BankId = fl.InBankId ");
            sb.Append(" left join NFMT_Basic.dbo.BankAccount inAcc on inAcc.BankAccId = fl.InAccountId ");
            sb.Append(" left join NFMT_User.dbo.Corporation outCorp on outCorp.CorpId = fl.OutCorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank outBank on outBank.BankId = fl.OutBankId ");
            sb.Append(" left join NFMT_Basic.dbo.BankAccount outAcc on outAcc.BankAccId = fl.OutAccountId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail lt on lt.StyleDetailId = fl.LogType ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on fl.CurrencyId = cur.CurrencyId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail pm on pm.StyleDetailId = fl.PayMode ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" fl.LogStatus>=50 ");
            if (startDate > Common.DefaultValue.DefaultTime && endDate > startDate)
                sb.AppendFormat(" and fl.LogDate between '{0}' and '{1}' ", startDate.ToString(), endDate.ToString());
            if (inCorpId > 0)
                sb.AppendFormat(" and fl.InCorpId = {0} ", inCorpId);
            if (outCorpId > 0)
                sb.AppendFormat(" and fl.OutCorpId = {0} ", outCorpId);
            if (logType > 0)
                sb.AppendFormat(" and fl.LogType = {0} ", logType);
            select.WhereStr = sb.ToString();

            return select;
        }

        //public override string[,] SetExcelRangeData(DataTable source)
        //{
        //    string[,] objData = new string[source.Rows.Count, 12];

        //    for (int i = 0; i < source.Rows.Count; i++)
        //    {
        //        System.Data.DataRow dr = source.Rows[i];

        //        objData[i, 0] = ((DateTime)dr["LogDate"]).ToString("yyyy-MM-dd");
        //        objData[i, 1] = dr["LogTypeName"].ToString();
        //        objData[i, 2] = dr["InCorpName"].ToString();
        //        objData[i, 3] = dr["InBankName"].ToString();
        //        objData[i, 4] = dr["InAccountNo"].ToString();
        //        objData[i, 5] = dr["OutCorpName"].ToString();
        //        objData[i, 6] = dr["OutBankName"].ToString();
        //        objData[i, 7] = dr["OutAccountNo"].ToString();
        //        objData[i, 8] = dr["PayModeName"].ToString();
        //        objData[i, 9] = dr["FundsBala"].ToString();
        //        objData[i, 10] = dr["CurrencyName"].ToString();
        //        objData[i, 11] = dr["IsVirtualPay"].ToString();
        //    }

        //    return objData;
        //}

        public override DataTable SetExcelRangeData(DataTable source)
        {
            string[] strs = new string[] { "LogDate", "LogTypeName", "InCorpName", "InBankName", "InAccountNo", "OutCorpName", "OutBankName", "OutAccountNo", "PayModeName", "FundsBala", "CurrencyName", "IsVirtualPay" };

            return source.ConvertDataTable(strs);
        }

        #endregion
    }
}
