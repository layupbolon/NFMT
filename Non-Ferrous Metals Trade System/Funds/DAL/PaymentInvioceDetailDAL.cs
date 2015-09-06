/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PaymentInvioceDetailDAL.cs
// 文件功能描述：发票财务付款明细dbo.Fun_PaymentInvioceDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年12月23日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Funds.Model;
using NFMT.DBUtility;
using NFMT.Funds.IDAL;
using NFMT.Common;

namespace NFMT.Funds.DAL
{
    /// <summary>
    /// 发票财务付款明细dbo.Fun_PaymentInvioceDetail数据交互类。
    /// </summary>
    public class PaymentInvioceDetailDAL : ExecOperate, IPaymentInvioceDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PaymentInvioceDetailDAL()
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
            PaymentInvioceDetail fun_paymentinviocedetail = (PaymentInvioceDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter paymentidpara = new SqlParameter("@PaymentId", SqlDbType.Int, 4);
            paymentidpara.Value = fun_paymentinviocedetail.PaymentId;
            paras.Add(paymentidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = fun_paymentinviocedetail.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter payapplyidpara = new SqlParameter("@PayApplyId", SqlDbType.Int, 4);
            payapplyidpara.Value = fun_paymentinviocedetail.PayApplyId;
            paras.Add(payapplyidpara);

            SqlParameter payapplydetailidpara = new SqlParameter("@PayApplyDetailId", SqlDbType.Int, 4);
            payapplydetailidpara.Value = fun_paymentinviocedetail.PayApplyDetailId;
            paras.Add(payapplydetailidpara);

            SqlParameter paybalapara = new SqlParameter("@PayBala", SqlDbType.Decimal, 9);
            paybalapara.Value = fun_paymentinviocedetail.PayBala;
            paras.Add(paybalapara);

            SqlParameter fundsbalapara = new SqlParameter("@FundsBala", SqlDbType.Decimal, 9);
            fundsbalapara.Value = fun_paymentinviocedetail.FundsBala;
            paras.Add(fundsbalapara);

            SqlParameter virtualbalapara = new SqlParameter("@VirtualBala", SqlDbType.Decimal, 9);
            virtualbalapara.Value = fun_paymentinviocedetail.VirtualBala;
            paras.Add(virtualbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = fun_paymentinviocedetail.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PaymentInvioceDetail paymentinviocedetail = new PaymentInvioceDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            paymentinviocedetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexPaymentId = dr.GetOrdinal("PaymentId");
            paymentinviocedetail.PaymentId = Convert.ToInt32(dr[indexPaymentId]);

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            if (dr["InvoiceId"] != DBNull.Value)
            {
                paymentinviocedetail.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
            }

            int indexPayApplyId = dr.GetOrdinal("PayApplyId");
            if (dr["PayApplyId"] != DBNull.Value)
            {
                paymentinviocedetail.PayApplyId = Convert.ToInt32(dr[indexPayApplyId]);
            }

            int indexPayApplyDetailId = dr.GetOrdinal("PayApplyDetailId");
            if (dr["PayApplyDetailId"] != DBNull.Value)
            {
                paymentinviocedetail.PayApplyDetailId = Convert.ToInt32(dr[indexPayApplyDetailId]);
            }

            int indexPayBala = dr.GetOrdinal("PayBala");
            if (dr["PayBala"] != DBNull.Value)
            {
                paymentinviocedetail.PayBala = Convert.ToDecimal(dr[indexPayBala]);
            }

            int indexFundsBala = dr.GetOrdinal("FundsBala");
            if (dr["FundsBala"] != DBNull.Value)
            {
                paymentinviocedetail.FundsBala = Convert.ToDecimal(dr[indexFundsBala]);
            }

            int indexVirtualBala = dr.GetOrdinal("VirtualBala");
            if (dr["VirtualBala"] != DBNull.Value)
            {
                paymentinviocedetail.VirtualBala = Convert.ToDecimal(dr[indexVirtualBala]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                paymentinviocedetail.DetailStatus = (StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }


            return paymentinviocedetail;
        }

        public override string TableName
        {
            get
            {
                return "Fun_PaymentInvioceDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PaymentInvioceDetail fun_paymentinviocedetail = (PaymentInvioceDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = fun_paymentinviocedetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter paymentidpara = new SqlParameter("@PaymentId", SqlDbType.Int, 4);
            paymentidpara.Value = fun_paymentinviocedetail.PaymentId;
            paras.Add(paymentidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = fun_paymentinviocedetail.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter payapplyidpara = new SqlParameter("@PayApplyId", SqlDbType.Int, 4);
            payapplyidpara.Value = fun_paymentinviocedetail.PayApplyId;
            paras.Add(payapplyidpara);

            SqlParameter payapplydetailidpara = new SqlParameter("@PayApplyDetailId", SqlDbType.Int, 4);
            payapplydetailidpara.Value = fun_paymentinviocedetail.PayApplyDetailId;
            paras.Add(payapplydetailidpara);

            SqlParameter paybalapara = new SqlParameter("@PayBala", SqlDbType.Decimal, 9);
            paybalapara.Value = fun_paymentinviocedetail.PayBala;
            paras.Add(paybalapara);

            SqlParameter fundsbalapara = new SqlParameter("@FundsBala", SqlDbType.Decimal, 9);
            fundsbalapara.Value = fun_paymentinviocedetail.FundsBala;
            paras.Add(fundsbalapara);

            SqlParameter virtualbalapara = new SqlParameter("@VirtualBala", SqlDbType.Decimal, 9);
            virtualbalapara.Value = fun_paymentinviocedetail.VirtualBala;
            paras.Add(virtualbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = fun_paymentinviocedetail.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel LoadByInvoicePayApplyId(UserModel user, int invDetailId, StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                string cmdText = string.Format("select * from dbo.Fun_PaymentInvioceDetail where PayApplyDetailId = {0} and DetailStatus >={1}", invDetailId, (int)status);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, null);

                List<NFMT.Funds.Model.PaymentInvioceDetail> models = new List<NFMT.Funds.Model.PaymentInvioceDetail>();

                int i = 0;
                while (dr.Read())
                {
                    models.Add(CreateModel<NFMT.Funds.Model.PaymentInvioceDetail>(dr));
                    i++;
                }

                result.AffectCount = i;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = models;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }

            return result;
        }

        public ResultModel Load(UserModel user, int paymentId, StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                string cmdText = string.Format("select * from dbo.Fun_PaymentInvioceDetail where PaymentId = {0} and DetailStatus >={1}", paymentId, (int)status);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, null);

                List<NFMT.Funds.Model.PaymentInvioceDetail> models = new List<NFMT.Funds.Model.PaymentInvioceDetail>();

                int i = 0;
                while (dr.Read())
                {
                    models.Add(CreateModel<NFMT.Funds.Model.PaymentInvioceDetail>(dr));
                    i++;
                }

                result.AffectCount = i;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = models;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }

            return result;
        }

        public override ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate)
        {
            if (operate == OperateEnum.修改) 
            {
                ResultModel result = new ResultModel();
                result.ResultStatus = 0;
                return result;
            }

            return base.AllowOperate(user, obj, operate);
        }

        #endregion
    }
}
