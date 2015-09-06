/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SIDAL.cs
// 文件功能描述：价外票dbo.Inv_SI数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月1日
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
    /// 价外票dbo.Inv_SI数据交互类。
    /// </summary>
    public class SIDAL : ExecOperate, ISIDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SIDAL()
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
            SI inv_si = (SI)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@SIId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = inv_si.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter changecurrencyidpara = new SqlParameter("@ChangeCurrencyId", SqlDbType.Int, 4);
            changecurrencyidpara.Value = inv_si.ChangeCurrencyId;
            paras.Add(changecurrencyidpara);

            SqlParameter changeratepara = new SqlParameter("@ChangeRate", SqlDbType.Decimal, 9);
            changeratepara.Value = inv_si.ChangeRate;
            paras.Add(changeratepara);

            SqlParameter changebalapara = new SqlParameter("@ChangeBala", SqlDbType.Decimal, 9);
            changebalapara.Value = inv_si.ChangeBala;
            paras.Add(changebalapara);

            SqlParameter paydeptpara = new SqlParameter("@PayDept", SqlDbType.Int, 4);
            paydeptpara.Value = inv_si.PayDept;
            paras.Add(paydeptpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            SI si = new SI();

            int indexSIId = dr.GetOrdinal("SIId");
            si.SIId = Convert.ToInt32(dr[indexSIId]);

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            if (dr["InvoiceId"] != DBNull.Value)
            {
                si.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
            }

            int indexChangeCurrencyId = dr.GetOrdinal("ChangeCurrencyId");
            if (dr["ChangeCurrencyId"] != DBNull.Value)
            {
                si.ChangeCurrencyId = Convert.ToInt32(dr[indexChangeCurrencyId]);
            }

            int indexChangeRate = dr.GetOrdinal("ChangeRate");
            if (dr["ChangeRate"] != DBNull.Value)
            {
                si.ChangeRate = Convert.ToDecimal(dr[indexChangeRate]);
            }

            int indexChangeBala = dr.GetOrdinal("ChangeBala");
            if (dr["ChangeBala"] != DBNull.Value)
            {
                si.ChangeBala = Convert.ToDecimal(dr[indexChangeBala]);
            }

            int indexPayDept = dr.GetOrdinal("PayDept");
            if (dr["PayDept"] != DBNull.Value)
            {
                si.PayDept = Convert.ToInt32(dr[indexPayDept]);
            }


            return si;
        }

        public override string TableName
        {
            get
            {
                return "Inv_SI";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SI inv_si = (SI)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter siidpara = new SqlParameter("@SIId", SqlDbType.Int, 4);
            siidpara.Value = inv_si.SIId;
            paras.Add(siidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = inv_si.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter changecurrencyidpara = new SqlParameter("@ChangeCurrencyId", SqlDbType.Int, 4);
            changecurrencyidpara.Value = inv_si.ChangeCurrencyId;
            paras.Add(changecurrencyidpara);

            SqlParameter changeratepara = new SqlParameter("@ChangeRate", SqlDbType.Decimal, 9);
            changeratepara.Value = inv_si.ChangeRate;
            paras.Add(changeratepara);

            SqlParameter changebalapara = new SqlParameter("@ChangeBala", SqlDbType.Decimal, 9);
            changebalapara.Value = inv_si.ChangeBala;
            paras.Add(changebalapara);

            SqlParameter paydeptpara = new SqlParameter("@PayDept", SqlDbType.Int, 4);
            paydeptpara.Value = inv_si.PayDept;
            paras.Add(paydeptpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetSIbyInvoiceId(UserModel user, int invoiceId)
        {
            ResultModel result = new ResultModel();
            SqlDataReader dr = null;

            try
            {
                string sql = string.Format("select * from dbo.Inv_SI where InvoiceId = {0}", invoiceId);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, sql, null);

                SI si = new SI();

                if (dr.Read())
                {
                    int indexSIId = dr.GetOrdinal("SIId");
                    si.SIId = Convert.ToInt32(dr[indexSIId]);

                    int indexInvoiceId = dr.GetOrdinal("InvoiceId");
                    if (dr["InvoiceId"] != DBNull.Value)
                    {
                        si.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
                    }

                    int indexChangeCurrencyId = dr.GetOrdinal("ChangeCurrencyId");
                    if (dr["ChangeCurrencyId"] != DBNull.Value)
                    {
                        si.ChangeCurrencyId = Convert.ToInt32(dr[indexChangeCurrencyId]);
                    }

                    int indexChangeRate = dr.GetOrdinal("ChangeRate");
                    if (dr["ChangeRate"] != DBNull.Value)
                    {
                        si.ChangeRate = Convert.ToDecimal(dr[indexChangeRate]);
                    }

                    int indexChangeBala = dr.GetOrdinal("ChangeBala");
                    if (dr["ChangeBala"] != DBNull.Value)
                    {
                        si.ChangeBala = Convert.ToDecimal(dr[indexChangeBala]);
                    }

                    int indexPayDept = dr.GetOrdinal("PayDept");
                    if (dr["PayDept"] != DBNull.Value)
                    {
                        si.PayDept = Convert.ToInt32(dr[indexPayDept]);
                    }

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = si;
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

        public override IAuthority Authority
        {
            get
            {
                NFMT.Authority.CorpAuth auth = new Authority.CorpAuth();
                auth.AuthColumnNames.Add("case when inv.InvoiceDirection = 34 then inv.OutCorpId when inv.InvoiceDirection=33 then inv.InCorpId end");

                return auth;
            }
        }

        public override int MenuId
        {
            get
            {
                return 65;
            }
        }

        public ResultModel GetSIIdsByCustomCorpId(UserModel user, int customCorpId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select si.SIId from dbo.Inv_SI si inner join dbo.Invoice inv on si.InvoiceId = inv.InvoiceId where inv.InCorpId = {0} and inv.InvoiceStatus >= {1}", customCorpId, (int)Common.StatusEnum.已生效);
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
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        #endregion
    }
}
