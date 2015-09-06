/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FinanceInvoiceDAL.cs
// 文件功能描述：财务发票dbo.Inv_FinanceInvoice数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年8月25日
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
    /// 财务发票dbo.Inv_FinanceInvoice数据交互类。
    /// </summary>
    public class FinanceInvoiceDAL : ExecOperate, IFinanceInvoiceDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FinanceInvoiceDAL()
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
            FinanceInvoice inv_financeinvoice = (FinanceInvoice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@FinanceInvoiceId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = inv_financeinvoice.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = inv_financeinvoice.AssetId;
            paras.Add(assetidpara);

            SqlParameter integeramountpara = new SqlParameter("@IntegerAmount", SqlDbType.Decimal, 9);
            integeramountpara.Value = inv_financeinvoice.IntegerAmount;
            paras.Add(integeramountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = inv_financeinvoice.NetAmount;
            paras.Add(netamountpara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = inv_financeinvoice.MUId;
            paras.Add(muidpara);

            SqlParameter vatratiopara = new SqlParameter("@VATRatio", SqlDbType.Decimal, 9);
            vatratiopara.Value = inv_financeinvoice.VATRatio;
            paras.Add(vatratiopara);

            SqlParameter vatbalapara = new SqlParameter("@VATBala", SqlDbType.Decimal, 9);
            vatbalapara.Value = inv_financeinvoice.VATBala;
            paras.Add(vatbalapara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            FinanceInvoice financeinvoice = new FinanceInvoice();

            int indexFinanceInvoiceId = dr.GetOrdinal("FinanceInvoiceId");
            financeinvoice.FinanceInvoiceId = Convert.ToInt32(dr[indexFinanceInvoiceId]);

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            if (dr["InvoiceId"] != DBNull.Value)
            {
                financeinvoice.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
            }

            int indexAssetId = dr.GetOrdinal("AssetId");
            if (dr["AssetId"] != DBNull.Value)
            {
                financeinvoice.AssetId = Convert.ToInt32(dr[indexAssetId]);
            }

            int indexIntegerAmount = dr.GetOrdinal("IntegerAmount");
            if (dr["IntegerAmount"] != DBNull.Value)
            {
                financeinvoice.IntegerAmount = Convert.ToDecimal(dr[indexIntegerAmount]);
            }

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            if (dr["NetAmount"] != DBNull.Value)
            {
                financeinvoice.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
            }

            int indexMUId = dr.GetOrdinal("MUId");
            if (dr["MUId"] != DBNull.Value)
            {
                financeinvoice.MUId = Convert.ToInt32(dr[indexMUId]);
            }

            int indexVATRatio = dr.GetOrdinal("VATRatio");
            if (dr["VATRatio"] != DBNull.Value)
            {
                financeinvoice.VATRatio = Convert.ToDecimal(dr[indexVATRatio]);
            }

            int indexVATBala = dr.GetOrdinal("VATBala");
            if (dr["VATBala"] != DBNull.Value)
            {
                financeinvoice.VATBala = Convert.ToDecimal(dr[indexVATBala]);
            }


            return financeinvoice;
        }

        public override string TableName
        {
            get
            {
                return "Inv_FinanceInvoice";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            FinanceInvoice inv_financeinvoice = (FinanceInvoice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter financeinvoiceidpara = new SqlParameter("@FinanceInvoiceId", SqlDbType.Int, 4);
            financeinvoiceidpara.Value = inv_financeinvoice.FinanceInvoiceId;
            paras.Add(financeinvoiceidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = inv_financeinvoice.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = inv_financeinvoice.AssetId;
            paras.Add(assetidpara);

            SqlParameter integeramountpara = new SqlParameter("@IntegerAmount", SqlDbType.Decimal, 9);
            integeramountpara.Value = inv_financeinvoice.IntegerAmount;
            paras.Add(integeramountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = inv_financeinvoice.NetAmount;
            paras.Add(netamountpara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = inv_financeinvoice.MUId;
            paras.Add(muidpara);

            SqlParameter vatratiopara = new SqlParameter("@VATRatio", SqlDbType.Decimal, 9);
            vatratiopara.Value = inv_financeinvoice.VATRatio;
            paras.Add(vatratiopara);

            SqlParameter vatbalapara = new SqlParameter("@VATBala", SqlDbType.Decimal, 9);
            vatbalapara.Value = inv_financeinvoice.VATBala;
            paras.Add(vatbalapara);


            return paras;
        }

        #endregion

        #region 新增方法

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "更新对象不能为null";
                    return result;
                }

                obj.LastModifyId = user.EmpId;

                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, string.Format("{0}Update", obj.TableName), CreateUpdateParameters(obj).ToArray());

                if (i == 1)
                {
                    result.Message = "更新成功";
                    result.ResultStatus = 0;
                }
                else
                    result.Message = "更新失败";

                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel GetByInvoiceId(UserModel user, int invoiceId)
        {
            ResultModel result = new ResultModel();

            if (invoiceId < 1)
            {
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@invoiceId", SqlDbType.Int, 4);
            para.Value = invoiceId;
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                string cmdText = "select * from dbo.Inv_FinanceInvoice where InvoiceId=@invoiceId";
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, paras.ToArray());

                FinanceInvoice financeinvoice = new FinanceInvoice();

                if (dr.Read())
                {
                    int indexFinanceInvoiceId = dr.GetOrdinal("FinanceInvoiceId");
                    financeinvoice.FinanceInvoiceId = Convert.ToInt32(dr[indexFinanceInvoiceId]);

                    int indexInvoiceId = dr.GetOrdinal("InvoiceId");
                    if (dr["InvoiceId"] != DBNull.Value)
                    {
                        financeinvoice.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
                    }

                    int indexAssetId = dr.GetOrdinal("AssetId");
                    if (dr["AssetId"] != DBNull.Value)
                    {
                        financeinvoice.AssetId = Convert.ToInt32(dr[indexAssetId]);
                    }

                    int indexIntegerAmount = dr.GetOrdinal("IntegerAmount");
                    if (dr["IntegerAmount"] != DBNull.Value)
                    {
                        financeinvoice.IntegerAmount = Convert.ToDecimal(dr[indexIntegerAmount]);
                    }

                    int indexNetAmount = dr.GetOrdinal("NetAmount");
                    if (dr["NetAmount"] != DBNull.Value)
                    {
                        financeinvoice.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
                    }

                    int indexMUId = dr.GetOrdinal("MUId");
                    if (dr["MUId"] != DBNull.Value)
                    {
                        financeinvoice.MUId = Convert.ToInt32(dr[indexMUId]);
                    }

                    int indexVATRatio = dr.GetOrdinal("VATRatio");
                    if (dr["VATRatio"] != DBNull.Value)
                    {
                        financeinvoice.VATRatio = Convert.ToDecimal(dr[indexVATRatio]);
                    }

                    int indexVATBala = dr.GetOrdinal("VATBala");
                    if (dr["VATBala"] != DBNull.Value)
                    {
                        financeinvoice.VATBala = Convert.ToDecimal(dr[indexVATBala]);
                    }

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = financeinvoice;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        public ResultModel GetAllotAmount(UserModel user, int fundsInvoiceId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select ISNULL(SUM(ISNULL(AllotBala,0)),0) from dbo.Inv_FinBusInvAllotDetail where FinanceInvoiceId = {0} and DetailStatus = {1}", fundsInvoiceId, (int)Common.StatusEnum.已完成);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                decimal allotAmount = 0;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && decimal.TryParse(obj.ToString(), out allotAmount))
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = "获取成功";
                    result.ReturnValue = allotAmount;
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

        public override IAuthority Authority
        {
            get
            {
                NFMT.Authority.CorpAuth auth = new Authority.CorpAuth();
                auth.AuthColumnNames.Add("case when inv.InvoiceDirection = 33 then inv.OutCorpId when inv.InvoiceDirection=34 then inv.InCorpId end");

                return auth;
            }
        }

        public override int MenuId
        {
            get
            {
                return 66;
            }
        }

        #endregion
    }
}
