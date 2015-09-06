/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PaymentContractDetailDAL.cs
// 文件功能描述：合约财务付款明细dbo.Fun_PaymentContractDetail数据交互类。
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
    /// 合约财务付款明细dbo.Fun_PaymentContractDetail数据交互类。
    /// </summary>
    public class PaymentContractDetailDAL : ExecOperate , IPaymentContractDetailDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public PaymentContractDetailDAL()
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
            PaymentContractDetail fun_paymentcontractdetail = (PaymentContractDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
                          returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName ="@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

			    SqlParameter paymentidpara = new SqlParameter("@PaymentId",SqlDbType.Int,4);
            paymentidpara.Value = fun_paymentcontractdetail.PaymentId;
            paras.Add(paymentidpara);

			    SqlParameter contractidpara = new SqlParameter("@ContractId",SqlDbType.Int,4);
            contractidpara.Value = fun_paymentcontractdetail.ContractId;
            paras.Add(contractidpara);

			    SqlParameter contractsubidpara = new SqlParameter("@ContractSubId",SqlDbType.Int,4);
            contractsubidpara.Value = fun_paymentcontractdetail.ContractSubId;
            paras.Add(contractsubidpara);

			    SqlParameter payapplyidpara = new SqlParameter("@PayApplyId",SqlDbType.Int,4);
            payapplyidpara.Value = fun_paymentcontractdetail.PayApplyId;
            paras.Add(payapplyidpara);

			    SqlParameter payapplydetailidpara = new SqlParameter("@PayApplyDetailId",SqlDbType.Int,4);
            payapplydetailidpara.Value = fun_paymentcontractdetail.PayApplyDetailId;
            paras.Add(payapplydetailidpara);

			    SqlParameter paybalapara = new SqlParameter("@PayBala",SqlDbType.Decimal,9);
            paybalapara.Value = fun_paymentcontractdetail.PayBala;
            paras.Add(paybalapara);

			    SqlParameter fundsbalapara = new SqlParameter("@FundsBala",SqlDbType.Decimal,9);
            fundsbalapara.Value = fun_paymentcontractdetail.FundsBala;
            paras.Add(fundsbalapara);

			    SqlParameter virtualbalapara = new SqlParameter("@VirtualBala",SqlDbType.Decimal,9);
            virtualbalapara.Value = fun_paymentcontractdetail.VirtualBala;
            paras.Add(virtualbalapara);


            return paras;
        }
        
		public override IModel CreateModel(SqlDataReader dr)
        {
            PaymentContractDetail paymentcontractdetail = new PaymentContractDetail();

                    int indexDetailId = dr.GetOrdinal("DetailId");
                    paymentcontractdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);
                    
                    int indexPaymentId = dr.GetOrdinal("PaymentId");
                    paymentcontractdetail.PaymentId = Convert.ToInt32(dr[indexPaymentId]);
                    
                    int indexContractId = dr.GetOrdinal("ContractId");
                    if(dr["ContractId"] != DBNull.Value)
                    {
                    paymentcontractdetail.ContractId = Convert.ToInt32(dr[indexContractId]);
                    }
                    
                    int indexContractSubId = dr.GetOrdinal("ContractSubId");
                    if(dr["ContractSubId"] != DBNull.Value)
                    {
                    paymentcontractdetail.ContractSubId = Convert.ToInt32(dr[indexContractSubId]);
                    }
                    
                    int indexPayApplyId = dr.GetOrdinal("PayApplyId");
                    if(dr["PayApplyId"] != DBNull.Value)
                    {
                    paymentcontractdetail.PayApplyId = Convert.ToInt32(dr[indexPayApplyId]);
                    }
                    
                    int indexPayApplyDetailId = dr.GetOrdinal("PayApplyDetailId");
                    if(dr["PayApplyDetailId"] != DBNull.Value)
                    {
                    paymentcontractdetail.PayApplyDetailId = Convert.ToInt32(dr[indexPayApplyDetailId]);
                    }
                    
                    int indexPayBala = dr.GetOrdinal("PayBala");
                    if(dr["PayBala"] != DBNull.Value)
                    {
                    paymentcontractdetail.PayBala = Convert.ToDecimal(dr[indexPayBala]);
                    }
                    
                    int indexFundsBala = dr.GetOrdinal("FundsBala");
                    if(dr["FundsBala"] != DBNull.Value)
                    {
                    paymentcontractdetail.FundsBala = Convert.ToDecimal(dr[indexFundsBala]);
                    }
                    
                    int indexVirtualBala = dr.GetOrdinal("VirtualBala");
                    if(dr["VirtualBala"] != DBNull.Value)
                    {
                    paymentcontractdetail.VirtualBala = Convert.ToDecimal(dr[indexVirtualBala]);
                    }
                    

            return paymentcontractdetail;
        }

        public override string TableName
        {
            get
            {
                return "Fun_PaymentContractDetail";
            }
        }
		
        public override List<SqlParameter> CreateUpdateParameters(IModel obj) 
        { 
            PaymentContractDetail fun_paymentcontractdetail = (PaymentContractDetail)obj;
            
            List<SqlParameter> paras = new List<SqlParameter>();
                
		    SqlParameter detailidpara = new SqlParameter("@DetailId",SqlDbType.Int,4);
            detailidpara.Value = fun_paymentcontractdetail.DetailId;
            paras.Add(detailidpara);

		    SqlParameter paymentidpara = new SqlParameter("@PaymentId",SqlDbType.Int,4);
            paymentidpara.Value = fun_paymentcontractdetail.PaymentId;
            paras.Add(paymentidpara);

		    SqlParameter contractidpara = new SqlParameter("@ContractId",SqlDbType.Int,4);
            contractidpara.Value = fun_paymentcontractdetail.ContractId;
            paras.Add(contractidpara);

		    SqlParameter contractsubidpara = new SqlParameter("@ContractSubId",SqlDbType.Int,4);
            contractsubidpara.Value = fun_paymentcontractdetail.ContractSubId;
            paras.Add(contractsubidpara);

		    SqlParameter payapplyidpara = new SqlParameter("@PayApplyId",SqlDbType.Int,4);
            payapplyidpara.Value = fun_paymentcontractdetail.PayApplyId;
            paras.Add(payapplyidpara);

		    SqlParameter payapplydetailidpara = new SqlParameter("@PayApplyDetailId",SqlDbType.Int,4);
            payapplydetailidpara.Value = fun_paymentcontractdetail.PayApplyDetailId;
            paras.Add(payapplydetailidpara);

		    SqlParameter paybalapara = new SqlParameter("@PayBala",SqlDbType.Decimal,9);
            paybalapara.Value = fun_paymentcontractdetail.PayBala;
            paras.Add(paybalapara);

		    SqlParameter fundsbalapara = new SqlParameter("@FundsBala",SqlDbType.Decimal,9);
            fundsbalapara.Value = fun_paymentcontractdetail.FundsBala;
            paras.Add(fundsbalapara);

		    SqlParameter virtualbalapara = new SqlParameter("@VirtualBala",SqlDbType.Decimal,9);
            virtualbalapara.Value = fun_paymentcontractdetail.VirtualBala;
            paras.Add(virtualbalapara);

             
             return paras;
        }    
        
        #endregion

        #region 新增方法

        public ResultModel GetByPaymentId(UserModel user, int paymentId)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@paymentId", SqlDbType.Int, 4);
            para.Value = paymentId;
            paras.Add(para);

            string cmdText = "select * from dbo.Fun_PaymentContractDetail where PaymentId =@paymentId";

            return Get(user, CommandType.Text, cmdText, paras.ToArray());
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
