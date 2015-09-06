/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractPayApplyDAL.cs
// 文件功能描述：合约付款申请dbo.Fun_ContractPayApply_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年8月25日
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
    /// 合约付款申请dbo.Fun_ContractPayApply_Ref数据交互类。
    /// </summary>
    public class ContractPayApplyDAL : ApplyOperate, IContractPayApplyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractPayApplyDAL()
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
            ContractPayApply fun_contractpayapply_ref = (ContractPayApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter payapplyidpara = new SqlParameter("@PayApplyId", SqlDbType.Int, 4);
            payapplyidpara.Value = fun_contractpayapply_ref.PayApplyId;
            paras.Add(payapplyidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = fun_contractpayapply_ref.ContractId;
            paras.Add(contractidpara);

            SqlParameter contractsubidpara = new SqlParameter("@ContractSubId", SqlDbType.Int, 4);
            contractsubidpara.Value = fun_contractpayapply_ref.ContractSubId;
            paras.Add(contractsubidpara);

            SqlParameter applybalapara = new SqlParameter("@ApplyBala", SqlDbType.Decimal, 9);
            applybalapara.Value = fun_contractpayapply_ref.ApplyBala;
            paras.Add(applybalapara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractPayApply contractpayapply = new ContractPayApply();

            int indexRefId = dr.GetOrdinal("RefId");
            contractpayapply.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexPayApplyId = dr.GetOrdinal("PayApplyId");
            if (dr["PayApplyId"] != DBNull.Value)
            {
                contractpayapply.PayApplyId = Convert.ToInt32(dr[indexPayApplyId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                contractpayapply.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexContractSubId = dr.GetOrdinal("ContractSubId");
            if (dr["ContractSubId"] != DBNull.Value)
            {
                contractpayapply.ContractSubId = Convert.ToInt32(dr[indexContractSubId]);
            }

            int indexApplyBala = dr.GetOrdinal("ApplyBala");
            if (dr["ApplyBala"] != DBNull.Value)
            {
                contractpayapply.ApplyBala = Convert.ToDecimal(dr[indexApplyBala]);
            }


            return contractpayapply;
        }

        public override string TableName
        {
            get
            {
                return "Fun_ContractPayApply_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractPayApply fun_contractpayapply_ref = (ContractPayApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = fun_contractpayapply_ref.RefId;
            paras.Add(refidpara);

            SqlParameter payapplyidpara = new SqlParameter("@PayApplyId", SqlDbType.Int, 4);
            payapplyidpara.Value = fun_contractpayapply_ref.PayApplyId;
            paras.Add(payapplyidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = fun_contractpayapply_ref.ContractId;
            paras.Add(contractidpara);

            SqlParameter contractsubidpara = new SqlParameter("@ContractSubId", SqlDbType.Int, 4);
            contractsubidpara.Value = fun_contractpayapply_ref.ContractSubId;
            paras.Add(contractsubidpara);

            SqlParameter applybalapara = new SqlParameter("@ApplyBala", SqlDbType.Decimal, 9);
            applybalapara.Value = fun_contractpayapply_ref.ApplyBala;
            paras.Add(applybalapara);


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

        public ResultModel GetByPayApplyId(UserModel user, int payApplyId)
        {
            ResultModel result = new ResultModel();

            if (payApplyId < 1)
            {
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@payApplyId", SqlDbType.Int, 4);
            para.Value = payApplyId;
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                string cmdText = "select * from dbo.Fun_ContractPayApply_Ref where PayApplyId=@payApplyId";

                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, paras.ToArray());

                ContractPayApply contractpayapply = new ContractPayApply();

                if (dr.Read())
                {
                    contractpayapply = this.CreateModel(dr) as ContractPayApply;

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = contractpayapply;
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

        #endregion
    }
}
