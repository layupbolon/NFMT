/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInInvoiceDAL.cs
// 文件功能描述：收款分配至发票dbo.Fun_CashInInvoice_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年7月29日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Funds.Model;
using NFMT.DBUtility;
using NFMT.Funds.IDAL;
using NFMT.Common;
using System.Linq;

namespace NFMT.Funds.DAL
{
    /// <summary>
    /// 收款分配至发票dbo.Fun_CashInInvoice_Ref数据交互类。
    /// </summary>
    public partial class CashInInvoiceDAL : DetailOperate, ICashInInvoiceDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CashInInvoiceDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringNFMT;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            CashInInvoice fun_cashininvoice_ref = (CashInInvoice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_cashininvoice_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter corprefidpara = new SqlParameter("@CorpRefId", SqlDbType.Int, 4);
            corprefidpara.Value = fun_cashininvoice_ref.CorpRefId;
            paras.Add(corprefidpara);

            SqlParameter contractrefidpara = new SqlParameter("@ContractRefId", SqlDbType.Int, 4);
            contractrefidpara.Value = fun_cashininvoice_ref.ContractRefId;
            paras.Add(contractrefidpara);

            SqlParameter cashinidpara = new SqlParameter("@CashInId", SqlDbType.Int, 4);
            cashinidpara.Value = fun_cashininvoice_ref.CashInId;
            paras.Add(cashinidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = fun_cashininvoice_ref.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_cashininvoice_ref.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);

            SqlParameter fundslogidpara = new SqlParameter("@FundsLogId", SqlDbType.Int, 4);
            fundslogidpara.Value = fun_cashininvoice_ref.FundsLogId;
            paras.Add(fundslogidpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            CashInInvoice cashininvoice = new CashInInvoice();

            cashininvoice.RefId = Convert.ToInt32(dr["RefId"]);

            if (dr["AllotId"] != DBNull.Value)
            {
                cashininvoice.AllotId = Convert.ToInt32(dr["AllotId"]);
            }

            if (dr["CorpRefId"] != DBNull.Value)
            {
                cashininvoice.CorpRefId = Convert.ToInt32(dr["CorpRefId"]);
            }

            if (dr["ContractRefId"] != DBNull.Value)
            {
                cashininvoice.ContractRefId = Convert.ToInt32(dr["ContractRefId"]);
            }

            if (dr["CashInId"] != DBNull.Value)
            {
                cashininvoice.CashInId = Convert.ToInt32(dr["CashInId"]);
            }

            if (dr["InvoiceId"] != DBNull.Value)
            {
                cashininvoice.InvoiceId = Convert.ToInt32(dr["InvoiceId"]);
            }

            if (dr["AllotBala"] != DBNull.Value)
            {
                cashininvoice.AllotBala = Convert.ToDecimal(dr["AllotBala"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                cashininvoice.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }

            if (dr["FundsLogId"] != DBNull.Value)
            {
                cashininvoice.FundsLogId = Convert.ToInt32(dr["FundsLogId"]);
            }


            return cashininvoice;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            CashInInvoice cashininvoice = new CashInInvoice();

            int indexRefId = dr.GetOrdinal("RefId");
            cashininvoice.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexAllotId = dr.GetOrdinal("AllotId");
            if (dr["AllotId"] != DBNull.Value)
            {
                cashininvoice.AllotId = Convert.ToInt32(dr[indexAllotId]);
            }

            int indexCorpRefId = dr.GetOrdinal("CorpRefId");
            if (dr["CorpRefId"] != DBNull.Value)
            {
                cashininvoice.CorpRefId = Convert.ToInt32(dr[indexCorpRefId]);
            }

            int indexContractRefId = dr.GetOrdinal("ContractRefId");
            if (dr["ContractRefId"] != DBNull.Value)
            {
                cashininvoice.ContractRefId = Convert.ToInt32(dr[indexContractRefId]);
            }

            int indexCashInId = dr.GetOrdinal("CashInId");
            if (dr["CashInId"] != DBNull.Value)
            {
                cashininvoice.CashInId = Convert.ToInt32(dr[indexCashInId]);
            }

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            if (dr["InvoiceId"] != DBNull.Value)
            {
                cashininvoice.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
            }

            int indexAllotBala = dr.GetOrdinal("AllotBala");
            if (dr["AllotBala"] != DBNull.Value)
            {
                cashininvoice.AllotBala = Convert.ToDecimal(dr[indexAllotBala]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                cashininvoice.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexFundsLogId = dr.GetOrdinal("FundsLogId");
            if (dr["FundsLogId"] != DBNull.Value)
            {
                cashininvoice.FundsLogId = Convert.ToInt32(dr[indexFundsLogId]);
            }


            return cashininvoice;
        }

        public override string TableName
        {
            get
            {
                return "Fun_CashInInvoice_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CashInInvoice fun_cashininvoice_ref = (CashInInvoice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = fun_cashininvoice_ref.RefId;
            paras.Add(refidpara);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_cashininvoice_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter corprefidpara = new SqlParameter("@CorpRefId", SqlDbType.Int, 4);
            corprefidpara.Value = fun_cashininvoice_ref.CorpRefId;
            paras.Add(corprefidpara);

            SqlParameter contractrefidpara = new SqlParameter("@ContractRefId", SqlDbType.Int, 4);
            contractrefidpara.Value = fun_cashininvoice_ref.ContractRefId;
            paras.Add(contractrefidpara);

            SqlParameter cashinidpara = new SqlParameter("@CashInId", SqlDbType.Int, 4);
            cashinidpara.Value = fun_cashininvoice_ref.CashInId;
            paras.Add(cashinidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = fun_cashininvoice_ref.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_cashininvoice_ref.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = fun_cashininvoice_ref.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter fundslogidpara = new SqlParameter("@FundsLogId", SqlDbType.Int, 4);
            fundslogidpara.Value = fun_cashininvoice_ref.FundsLogId;
            paras.Add(fundslogidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetSIIdsbyAllotId(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select si.SIId from dbo.Invoice inv left join dbo.Inv_SI si on inv.InvoiceId = si.InvoiceId where inv.InvoiceId in (select InvoiceId from dbo.Fun_CashInInvoice_Ref where AllotId = {0} and DetailStatus >={1})", allotId, (int)Common.StatusEnum.已生效);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string resultStr = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        resultStr += dr["SIId"].ToString() + ",";
                    }
                    if (!string.IsNullOrEmpty(resultStr) && resultStr.IndexOf(',') > 0)
                        resultStr = resultStr.Substring(0, resultStr.Length - 1);
                    result.ReturnValue = resultStr;
                }
                result.ResultStatus = 0;
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel InvalidAll(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Fun_CashInInvoice_Ref set DetailStatus = {0} where AllotId = {1}", (int)Common.StatusEnum.已作废, allotId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                }
                else
                {
                    result.Message = "作废失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel LoadByAllot(UserModel user, int allotId, StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.Fun_CashInInvoice_Ref where AllotId ={0} and DetailStatus = {1}", allotId, (int)status);
            NFMT.Common.ResultModel result = Load<Model.CashInInvoice>(user, CommandType.Text, cmdText);
            List<Model.CashInInvoice> cashInInvoices = result.ReturnValue as List<Model.CashInInvoice>;
            if (cashInInvoices != null && cashInInvoices.Any())
                result.ResultStatus = 0;
            else
                result.ResultStatus = -1;
            return result;
        }

        #endregion
    }
}
