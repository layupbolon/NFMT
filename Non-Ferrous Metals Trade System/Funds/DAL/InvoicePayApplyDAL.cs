/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoicePayApplyDAL.cs
// 文件功能描述：发票付款申请dbo.Fun_InvoicePayApply_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月27日
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
    /// 发票付款申请dbo.Fun_InvoicePayApply_Ref数据交互类。
    /// </summary>
    public class InvoicePayApplyDAL : ApplyOperate, IInvoicePayApplyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InvoicePayApplyDAL()
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
            InvoicePayApply fun_invoicepayapply_ref = (InvoicePayApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter payapplyidpara = new SqlParameter("@PayApplyId", SqlDbType.Int, 4);
            payapplyidpara.Value = fun_invoicepayapply_ref.PayApplyId;
            paras.Add(payapplyidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = fun_invoicepayapply_ref.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter siidpara = new SqlParameter("@SIId", SqlDbType.Int, 4);
            siidpara.Value = fun_invoicepayapply_ref.SIId;
            paras.Add(siidpara);

            SqlParameter applybalapara = new SqlParameter("@ApplyBala", SqlDbType.Decimal, 9);
            applybalapara.Value = fun_invoicepayapply_ref.ApplyBala;
            paras.Add(applybalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = fun_invoicepayapply_ref.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            InvoicePayApply invoicepayapply = new InvoicePayApply();

            int indexRefId = dr.GetOrdinal("RefId");
            invoicepayapply.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexPayApplyId = dr.GetOrdinal("PayApplyId");
            if (dr["PayApplyId"] != DBNull.Value)
            {
                invoicepayapply.PayApplyId = Convert.ToInt32(dr[indexPayApplyId]);
            }

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            if (dr["InvoiceId"] != DBNull.Value)
            {
                invoicepayapply.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
            }

            int indexSIId = dr.GetOrdinal("SIId");
            if (dr["SIId"] != DBNull.Value)
            {
                invoicepayapply.SIId = Convert.ToInt32(dr[indexSIId]);
            }

            int indexApplyBala = dr.GetOrdinal("ApplyBala");
            if (dr["ApplyBala"] != DBNull.Value)
            {
                invoicepayapply.ApplyBala = Convert.ToDecimal(dr[indexApplyBala]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                invoicepayapply.DetailStatus = (StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }


            return invoicepayapply;
        }

        public override string TableName
        {
            get
            {
                return "Fun_InvoicePayApply_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            InvoicePayApply fun_invoicepayapply_ref = (InvoicePayApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = fun_invoicepayapply_ref.RefId;
            paras.Add(refidpara);

            SqlParameter payapplyidpara = new SqlParameter("@PayApplyId", SqlDbType.Int, 4);
            payapplyidpara.Value = fun_invoicepayapply_ref.PayApplyId;
            paras.Add(payapplyidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = fun_invoicepayapply_ref.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter siidpara = new SqlParameter("@SIId", SqlDbType.Int, 4);
            siidpara.Value = fun_invoicepayapply_ref.SIId;
            paras.Add(siidpara);

            SqlParameter applybalapara = new SqlParameter("@ApplyBala", SqlDbType.Decimal, 9);
            applybalapara.Value = fun_invoicepayapply_ref.ApplyBala;
            paras.Add(applybalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = fun_invoicepayapply_ref.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel LoadByInvoice(UserModel user, int invoiceId, StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();        
            SqlDataReader dr = null;
            try
            {
                string cmdText = string.Format("select * from dbo.Fun_InvoicePayApply_Ref where InvoiceId = {0} and DetailStatus >={1}",invoiceId,(int)status);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, null);                

                List<NFMT.Funds.Model.InvoicePayApply> models = new List<NFMT.Funds.Model.InvoicePayApply>();

                int i = 0;
                while (dr.Read())
                {
                    models.Add(CreateModel<NFMT.Funds.Model.InvoicePayApply>(dr));
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

        public ResultModel Load(UserModel user, int payApplyId, StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            SqlDataReader dr = null;
            try
            {
                string cmdText = string.Format("select * from dbo.Fun_InvoicePayApply_Ref where PayApplyId = {0} and DetailStatus >={1}", payApplyId, (int)status);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, null);

                List<NFMT.Funds.Model.InvoicePayApply> models = new List<NFMT.Funds.Model.InvoicePayApply>();

                int i = 0;
                while (dr.Read())
                {
                    models.Add(CreateModel<NFMT.Funds.Model.InvoicePayApply>(dr));
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
        #endregion
    }
}
