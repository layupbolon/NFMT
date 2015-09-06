/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FinBusInvAllotDetailDAL.cs
// 文件功能描述：业务发票财务发票分配明细dbo.Inv_FinBusInvAllotDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月24日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Invoice.Model;
using NFMT.DBUtility;
using NFMT.Invoice.IDAL;
using NFMT.Common;

namespace NFMT.Invoice.DAL
{
    /// <summary>
    /// 业务发票财务发票分配明细dbo.Inv_FinBusInvAllotDetail数据交互类。
    /// </summary>
    public class FinBusInvAllotDetailDAL : ExecOperate, IFinBusInvAllotDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FinBusInvAllotDetailDAL()
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
            FinBusInvAllotDetail inv_finbusinvallotdetail = (FinBusInvAllotDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = inv_finbusinvallotdetail.AllotId;
            paras.Add(allotidpara);

            SqlParameter businessinvoiceidpara = new SqlParameter("@BusinessInvoiceId", SqlDbType.Int, 4);
            businessinvoiceidpara.Value = inv_finbusinvallotdetail.BusinessInvoiceId;
            paras.Add(businessinvoiceidpara);

            SqlParameter financeinvoiceidpara = new SqlParameter("@FinanceInvoiceId", SqlDbType.Int, 4);
            financeinvoiceidpara.Value = inv_finbusinvallotdetail.FinanceInvoiceId;
            paras.Add(financeinvoiceidpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = inv_finbusinvallotdetail.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            FinBusInvAllotDetail finbusinvallotdetail = new FinBusInvAllotDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            finbusinvallotdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexAllotId = dr.GetOrdinal("AllotId");
            if (dr["AllotId"] != DBNull.Value)
            {
                finbusinvallotdetail.AllotId = Convert.ToInt32(dr[indexAllotId]);
            }

            int indexBusinessInvoiceId = dr.GetOrdinal("BusinessInvoiceId");
            if (dr["BusinessInvoiceId"] != DBNull.Value)
            {
                finbusinvallotdetail.BusinessInvoiceId = Convert.ToInt32(dr[indexBusinessInvoiceId]);
            }

            int indexFinanceInvoiceId = dr.GetOrdinal("FinanceInvoiceId");
            if (dr["FinanceInvoiceId"] != DBNull.Value)
            {
                finbusinvallotdetail.FinanceInvoiceId = Convert.ToInt32(dr[indexFinanceInvoiceId]);
            }

            int indexAllotBala = dr.GetOrdinal("AllotBala");
            if (dr["AllotBala"] != DBNull.Value)
            {
                finbusinvallotdetail.AllotBala = Convert.ToDecimal(dr[indexAllotBala]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                finbusinvallotdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }


            return finbusinvallotdetail;
        }

        public override string TableName
        {
            get
            {
                return "Inv_FinBusInvAllotDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            FinBusInvAllotDetail inv_finbusinvallotdetail = (FinBusInvAllotDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = inv_finbusinvallotdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = inv_finbusinvallotdetail.AllotId;
            paras.Add(allotidpara);

            SqlParameter businessinvoiceidpara = new SqlParameter("@BusinessInvoiceId", SqlDbType.Int, 4);
            businessinvoiceidpara.Value = inv_finbusinvallotdetail.BusinessInvoiceId;
            paras.Add(businessinvoiceidpara);

            SqlParameter financeinvoiceidpara = new SqlParameter("@FinanceInvoiceId", SqlDbType.Int, 4);
            financeinvoiceidpara.Value = inv_finbusinvallotdetail.FinanceInvoiceId;
            paras.Add(financeinvoiceidpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = inv_finbusinvallotdetail.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = inv_finbusinvallotdetail.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetJson(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(" select allotDetail.BusinessInvoiceId,inv.InvoiceDate,inv.InvoiceNo,inv.InvoiceName,bd.DetailName,cur.CurrencyName,inv.OutCorpName,inv.InCorpName,bus.VATRatio,bus.VATBala,inv.InvoiceBala,allotDetail.AllotBala ");
                sb.Append(" from dbo.Inv_FinBusInvAllotDetail allotDetail ");
                sb.Append(" inner join dbo.Inv_BusinessInvoice bus on allotDetail.BusinessInvoiceId = bus.BusinessInvoiceId ");
                sb.Append(" inner join dbo.Invoice inv on bus.InvoiceId = inv.InvoiceId ");
                sb.Append(" left join NFMT_Basic.dbo.Currency cur on inv.CurrencyId = cur.CurrencyId ");
                sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail bd on inv.InvoiceDirection = bd.StyleDetailId and bd.BDStyleId ={0} ", (int)Data.StyleEnum.InvoiceDirection);
                sb.AppendFormat(" where allotDetail.AllotId = {0} and allotDetail.DetailStatus >= {1} ", allotId, (int)Common.StatusEnum.已生效);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = dt;
                }
                else
                {
                    result.ResultStatus = 0;
                    result.Message = "获取失败";
                }
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
                string sql = string.Format("update dbo.Inv_FinBusInvAllotDetail set DetailStatus = {0} where AllotId = {1} ", (int)Common.StatusEnum.已作废, allotId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                    result.AffectCount = i;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "作废失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel Load(UserModel user, int allotId, Common.StatusEnum status)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select * from dbo.Inv_FinBusInvAllotDetail where AllotId = {0} and DetailStatus = {1}", allotId, (int)status);

                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, sql, null, CommandType.Text);

                List<FinBusInvAllotDetail> finBusInvAllotDetails = new List<FinBusInvAllotDetail>();

                foreach (DataRow dr in dt.Rows)
                {
                    FinBusInvAllotDetail finbusinvallotdetail = new FinBusInvAllotDetail();
                    finbusinvallotdetail.DetailId = Convert.ToInt32(dr["DetailId"]);

                    if (dr["AllotId"] != DBNull.Value)
                    {
                        finbusinvallotdetail.AllotId = Convert.ToInt32(dr["AllotId"]);
                    }
                    if (dr["BusinessInvoiceId"] != DBNull.Value)
                    {
                        finbusinvallotdetail.BusinessInvoiceId = Convert.ToInt32(dr["BusinessInvoiceId"]);
                    }
                    if (dr["FinanceInvoiceId"] != DBNull.Value)
                    {
                        finbusinvallotdetail.FinanceInvoiceId = Convert.ToInt32(dr["FinanceInvoiceId"]);
                    }
                    if (dr["AllotBala"] != DBNull.Value)
                    {
                        finbusinvallotdetail.AllotBala = Convert.ToDecimal(dr["AllotBala"]);
                    }
                    if (dr["DetailStatus"] != DBNull.Value)
                    {
                        finbusinvallotdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
                    }
                    finBusInvAllotDetails.Add(finbusinvallotdetail);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = finBusInvAllotDetails;
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel GetBIds(UserModel user, int fId)
        {
            ResultModel result = new ResultModel();
            try
            {
                string sql = string.Format("select BusinessInvoiceId from dbo.Inv_FinBusInvAllotDetail where FinanceInvoiceId = {0} and DetailStatus = {1}", fId, (int)Common.StatusEnum.已生效);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ReturnValue = dt;
                    result.ResultStatus = 0;
                }
                else
                {
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel GetAllotIdByFid(UserModel user, int fId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select top 1 AllotId from dbo.Inv_FinBusInvAllotDetail where FinanceInvoiceId = {0} and DetailStatus = {1}", fId, (int)Common.StatusEnum.已生效);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                int allotId = 0;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && int.TryParse(obj.ToString(), out allotId))
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = allotId;
                    result.Message = "获取成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel InvalidAllByFinInvoiceId(UserModel user, int finInvoiceId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql =
                    string.Format(
                        "update dbo.Inv_FinBusInvAllotDetail set DetailStatus = {0} where FinanceInvoiceId = {1}",
                        (int)Common.StatusEnum.已生效, finInvoiceId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "作废失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        #endregion
    }
}
