/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PaymentVirtualDAL.cs
// 文件功能描述：虚拟收付款dbo.Fun_PaymentVirtual数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年12月24日
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
    /// 虚拟收付款dbo.Fun_PaymentVirtual数据交互类。
    /// </summary>
    public class PaymentVirtualDAL : ExecOperate , IPaymentVirtualDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public PaymentVirtualDAL()
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
            PaymentVirtual fun_paymentvirtual = (PaymentVirtual)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
                          returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName ="@VirtualId";
            returnValue.Size = 4;
            paras.Add(returnValue);

			    SqlParameter paymentidpara = new SqlParameter("@PaymentId",SqlDbType.Int,4);
            paymentidpara.Value = fun_paymentvirtual.PaymentId;
            paras.Add(paymentidpara);

			    SqlParameter payapplyidpara = new SqlParameter("@PayApplyId",SqlDbType.Int,4);
            payapplyidpara.Value = fun_paymentvirtual.PayApplyId;
            paras.Add(payapplyidpara);

			    SqlParameter paybalapara = new SqlParameter("@PayBala",SqlDbType.Decimal,9);
            paybalapara.Value = fun_paymentvirtual.PayBala;
            paras.Add(paybalapara);

			    SqlParameter detailstatuspara = new SqlParameter("@DetailStatus",SqlDbType.Int,4);
            detailstatuspara.Value = fun_paymentvirtual.DetailStatus;
            paras.Add(detailstatuspara);

			    SqlParameter isconfirmpara = new SqlParameter("@IsConfirm",SqlDbType.Bit,1);
             isconfirmpara.Value = fun_paymentvirtual.IsConfirm;
             paras.Add(isconfirmpara);

			    if(!string.IsNullOrEmpty(fun_paymentvirtual.Memo))
            {
               SqlParameter memopara = new SqlParameter("@Memo",SqlDbType.VarChar,4000);
               memopara.Value = fun_paymentvirtual.Memo;
               paras.Add(memopara);
            }

			    if(!string.IsNullOrEmpty(fun_paymentvirtual.ConfirmMemo))
            {
               SqlParameter confirmmemopara = new SqlParameter("@ConfirmMemo",SqlDbType.VarChar,4000);
               confirmmemopara.Value = fun_paymentvirtual.ConfirmMemo;
               paras.Add(confirmmemopara);
            }

			    SqlParameter fundslogidpara = new SqlParameter("@FundsLogId",SqlDbType.Int,4);
            fundslogidpara.Value = fun_paymentvirtual.FundsLogId;
            paras.Add(fundslogidpara);


            return paras;
        }
        
		public override IModel CreateModel(SqlDataReader dr)
        {
            PaymentVirtual paymentvirtual = new PaymentVirtual();

                    int indexVirtualId = dr.GetOrdinal("VirtualId");
                    paymentvirtual.VirtualId = Convert.ToInt32(dr[indexVirtualId]);
                    
                    int indexPaymentId = dr.GetOrdinal("PaymentId");
                    paymentvirtual.PaymentId = Convert.ToInt32(dr[indexPaymentId]);
                    
                    int indexPayApplyId = dr.GetOrdinal("PayApplyId");
                    if(dr["PayApplyId"] != DBNull.Value)
                    {
                    paymentvirtual.PayApplyId = Convert.ToInt32(dr[indexPayApplyId]);
                    }
                    
                    int indexPayBala = dr.GetOrdinal("PayBala");
                    if(dr["PayBala"] != DBNull.Value)
                    {
                    paymentvirtual.PayBala = Convert.ToDecimal(dr[indexPayBala]);
                    }
                    
                    int indexDetailStatus = dr.GetOrdinal("DetailStatus");
                    if(dr["DetailStatus"] != DBNull.Value)
                    {
                    paymentvirtual.DetailStatus = (StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
                    }
                    
                    int indexIsConfirm = dr.GetOrdinal("IsConfirm");
                    if(dr["IsConfirm"] != DBNull.Value)
                    {
                    paymentvirtual.IsConfirm = Convert.ToBoolean(dr[indexIsConfirm]);
                    }
                    
                    int indexMemo = dr.GetOrdinal("Memo");
                    if(dr["Memo"] != DBNull.Value)
                    {
                    paymentvirtual.Memo = Convert.ToString(dr[indexMemo]);
                    }
                    
                    int indexConfirmMemo = dr.GetOrdinal("ConfirmMemo");
                    if(dr["ConfirmMemo"] != DBNull.Value)
                    {
                    paymentvirtual.ConfirmMemo = Convert.ToString(dr[indexConfirmMemo]);
                    }
                    
                    int indexFundsLogId = dr.GetOrdinal("FundsLogId");
                    if(dr["FundsLogId"] != DBNull.Value)
                    {
                    paymentvirtual.FundsLogId = Convert.ToInt32(dr[indexFundsLogId]);
                    }
                    

            return paymentvirtual;
        }

        public override string TableName
        {
            get
            {
                return "Fun_PaymentVirtual";
            }
        }
		
        public override List<SqlParameter> CreateUpdateParameters(IModel obj) 
        { 
            PaymentVirtual fun_paymentvirtual = (PaymentVirtual)obj;
            
            List<SqlParameter> paras = new List<SqlParameter>();
                
		    SqlParameter virtualidpara = new SqlParameter("@VirtualId",SqlDbType.Int,4);
            virtualidpara.Value = fun_paymentvirtual.VirtualId;
            paras.Add(virtualidpara);

		    SqlParameter paymentidpara = new SqlParameter("@PaymentId",SqlDbType.Int,4);
            paymentidpara.Value = fun_paymentvirtual.PaymentId;
            paras.Add(paymentidpara);

		    SqlParameter payapplyidpara = new SqlParameter("@PayApplyId",SqlDbType.Int,4);
            payapplyidpara.Value = fun_paymentvirtual.PayApplyId;
            paras.Add(payapplyidpara);

		    SqlParameter paybalapara = new SqlParameter("@PayBala",SqlDbType.Decimal,9);
            paybalapara.Value = fun_paymentvirtual.PayBala;
            paras.Add(paybalapara);

		    SqlParameter detailstatuspara = new SqlParameter("@DetailStatus",SqlDbType.Int,4);
            detailstatuspara.Value = fun_paymentvirtual.DetailStatus;
            paras.Add(detailstatuspara);

		    SqlParameter isconfirmpara = new SqlParameter("@IsConfirm",SqlDbType.Bit,1);
             isconfirmpara.Value = fun_paymentvirtual.IsConfirm;
             paras.Add(isconfirmpara);

		    if(!string.IsNullOrEmpty(fun_paymentvirtual.Memo))
            {
               SqlParameter memopara = new SqlParameter("@Memo",SqlDbType.VarChar,4000);
               memopara.Value = fun_paymentvirtual.Memo;
               paras.Add(memopara);
            }

		    if(!string.IsNullOrEmpty(fun_paymentvirtual.ConfirmMemo))
            {
               SqlParameter confirmmemopara = new SqlParameter("@ConfirmMemo",SqlDbType.VarChar,4000);
               confirmmemopara.Value = fun_paymentvirtual.ConfirmMemo;
               paras.Add(confirmmemopara);
            }

		    SqlParameter fundslogidpara = new SqlParameter("@FundsLogId",SqlDbType.Int,4);
            fundslogidpara.Value = fun_paymentvirtual.FundsLogId;
            paras.Add(fundslogidpara);

             
             return paras;
        }    
        
        #endregion

        #region 新增方法

        public ResultModel GetByPaymentId(UserModel user, int paymentId, Common.StatusEnum status = StatusEnum.已录入)
        {
            ResultModel result = new ResultModel();

            if (paymentId < 1)
            {
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@paymentId", SqlDbType.Int, 4);
            para.Value = paymentId;
            paras.Add(para);

            para = new SqlParameter("@status", SqlDbType.Int, 4);
            para.Value = (int)status;
            paras.Add(para);

            string cmdText = "select * from dbo.Fun_PaymentVirtual where PaymentId =@paymentId and DetailStatus >=@status";

            return Get(user, CommandType.Text, cmdText, paras.ToArray());
        }

        public ResultModel HasEntryVirtaul(UserModel user, int paymentId)
        {
            ResultModel result = new ResultModel();

            if (paymentId < 1)
            {
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@paymentId", SqlDbType.Int, 4);
            para.Value = paymentId;
            paras.Add(para);

            para = new SqlParameter("@status", SqlDbType.Int, 4);
            para.Value = (int)Common.StatusEnum.已录入;
            paras.Add(para);

            try
            {
                object obj = SqlHelper.ExecuteScalar(ConnectString, CommandType.Text, "select count(*) from dbo.Fun_PaymentVirtual where PaymentId =@paymentId and DetailStatus =@status", paras.ToArray());

                int rows = 0;
                if (obj != null && int.TryParse(obj.ToString(), out rows))
                {
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.AffectCount = rows;
                    result.ReturnValue = rows;
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public override ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate)
        {
            if (operate == OperateEnum.修改 && (obj.Status == StatusEnum.已生效 || obj.Status== StatusEnum.已完成))
            {
                ResultModel result = new ResultModel();
                result.ResultStatus = 0;
                return result;
            }

            return base.AllowOperate(user, obj, operate);
        }

        public override IAuthority Authority
        {
            get
            {
                NFMT.Authority.CorpAuth auth = new Authority.CorpAuth();
                auth.AuthColumnNames.Add("pay.PayCorp");
                return auth;
            }
        }

        public override int MenuId
        {
            get
            {
                return 87;
            }
        }

        #endregion
    }
}
