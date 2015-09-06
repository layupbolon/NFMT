/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StopLossApplyDAL.cs
// 文件功能描述：止损申请dbo.Pri_StopLossApply数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月23日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.DoPrice.Model;
using NFMT.DBUtility;
using NFMT.DoPrice.IDAL;
using NFMT.Common;

namespace NFMT.DoPrice.DAL
{
    /// <summary>
    /// 止损申请dbo.Pri_StopLossApply数据交互类。
    /// </summary>
    public class StopLossApplyDAL : ApplyOperate, IStopLossApplyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StopLossApplyDAL()
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
            StopLossApply pri_stoplossapply = (StopLossApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StopLossApplyId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = pri_stoplossapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter pricingidpara = new SqlParameter("@PricingId", SqlDbType.Int, 4);
            pricingidpara.Value = pri_stoplossapply.PricingId;
            paras.Add(pricingidpara);

            SqlParameter pricingdirectionpara = new SqlParameter("@PricingDirection", SqlDbType.Int, 4);
            pricingdirectionpara.Value = pri_stoplossapply.PricingDirection;
            paras.Add(pricingdirectionpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = pri_stoplossapply.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = pri_stoplossapply.ContractId;
            paras.Add(contractidpara);

            SqlParameter assertidpara = new SqlParameter("@AssertId", SqlDbType.Int, 4);
            assertidpara.Value = pri_stoplossapply.AssertId;
            paras.Add(assertidpara);

            SqlParameter stoplosspricepara = new SqlParameter("@StopLossPrice", SqlDbType.Decimal, 9);
            stoplosspricepara.Value = pri_stoplossapply.StopLossPrice;
            paras.Add(stoplosspricepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = pri_stoplossapply.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter stoplossweightpara = new SqlParameter("@StopLossWeight", SqlDbType.Decimal, 9);
            stoplossweightpara.Value = pri_stoplossapply.StopLossWeight;
            paras.Add(stoplossweightpara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = pri_stoplossapply.MUId;
            paras.Add(muidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StopLossApply stoplossapply = new StopLossApply();

            int indexStopLossApplyId = dr.GetOrdinal("StopLossApplyId");
            stoplossapply.StopLossApplyId = Convert.ToInt32(dr[indexStopLossApplyId]);

            int indexApplyId = dr.GetOrdinal("ApplyId");
            if (dr["ApplyId"] != DBNull.Value)
            {
                stoplossapply.ApplyId = Convert.ToInt32(dr[indexApplyId]);
            }

            int indexPricingId = dr.GetOrdinal("PricingId");
            if (dr["PricingId"] != DBNull.Value)
            {
                stoplossapply.PricingId = Convert.ToInt32(dr[indexPricingId]);
            }

            int indexPricingDirection = dr.GetOrdinal("PricingDirection");
            if (dr["PricingDirection"] != DBNull.Value)
            {
                stoplossapply.PricingDirection = Convert.ToInt32(dr[indexPricingDirection]);
            }

            int indexSubContractId = dr.GetOrdinal("SubContractId");
            if (dr["SubContractId"] != DBNull.Value)
            {
                stoplossapply.SubContractId = Convert.ToInt32(dr[indexSubContractId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                stoplossapply.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexAssertId = dr.GetOrdinal("AssertId");
            if (dr["AssertId"] != DBNull.Value)
            {
                stoplossapply.AssertId = Convert.ToInt32(dr[indexAssertId]);
            }

            int indexStopLossPrice = dr.GetOrdinal("StopLossPrice");
            if (dr["StopLossPrice"] != DBNull.Value)
            {
                stoplossapply.StopLossPrice = Convert.ToDecimal(dr[indexStopLossPrice]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                stoplossapply.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexStopLossWeight = dr.GetOrdinal("StopLossWeight");
            if (dr["StopLossWeight"] != DBNull.Value)
            {
                stoplossapply.StopLossWeight = Convert.ToDecimal(dr[indexStopLossWeight]);
            }

            int indexMUId = dr.GetOrdinal("MUId");
            if (dr["MUId"] != DBNull.Value)
            {
                stoplossapply.MUId = Convert.ToInt32(dr[indexMUId]);
            }


            return stoplossapply;
        }

        public override string TableName
        {
            get
            {
                return "Pri_StopLossApply";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StopLossApply pri_stoplossapply = (StopLossApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stoplossapplyidpara = new SqlParameter("@StopLossApplyId", SqlDbType.Int, 4);
            stoplossapplyidpara.Value = pri_stoplossapply.StopLossApplyId;
            paras.Add(stoplossapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = pri_stoplossapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter pricingidpara = new SqlParameter("@PricingId", SqlDbType.Int, 4);
            pricingidpara.Value = pri_stoplossapply.PricingId;
            paras.Add(pricingidpara);

            SqlParameter pricingdirectionpara = new SqlParameter("@PricingDirection", SqlDbType.Int, 4);
            pricingdirectionpara.Value = pri_stoplossapply.PricingDirection;
            paras.Add(pricingdirectionpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = pri_stoplossapply.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = pri_stoplossapply.ContractId;
            paras.Add(contractidpara);

            SqlParameter assertidpara = new SqlParameter("@AssertId", SqlDbType.Int, 4);
            assertidpara.Value = pri_stoplossapply.AssertId;
            paras.Add(assertidpara);

            SqlParameter stoplosspricepara = new SqlParameter("@StopLossPrice", SqlDbType.Decimal, 9);
            stoplosspricepara.Value = pri_stoplossapply.StopLossPrice;
            paras.Add(stoplosspricepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = pri_stoplossapply.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter stoplossweightpara = new SqlParameter("@StopLossWeight", SqlDbType.Decimal, 9);
            stoplossweightpara.Value = pri_stoplossapply.StopLossWeight;
            paras.Add(stoplossweightpara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = pri_stoplossapply.MUId;
            paras.Add(muidpara);


            return paras;
        }

        public override IAuthority Authority
        {
            get
            {
                return new NFMT.Authority.ContractAuth();
            }
        }

        #endregion

        #region 新增方法

        public ResultModel HasStopLossApplyDetail(UserModel user, int stopLossApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select count(1) from dbo.Pri_StopLossApplyDetail where StopLossApplyId = {0}", stopLossApplyId);
                object i = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                int count = 0;
                if (i != null && !string.IsNullOrEmpty(i.ToString()) && int.TryParse(i.ToString(), out count) && count > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = true;
                }
                else
                {
                    result.ResultStatus = 0;
                    result.Message = "获取失败";
                    result.ReturnValue = false;
                }

            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel CheckStopLossCanConfirm(UserModel user, int stopLossApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                int entryStatus = (int)Common.StatusEnum.已录入;
                int completeStatus = (int)StatusEnum.已完成;
                int readyStatus = (int)StatusEnum.已生效;
                int closeStatus = (int)StatusEnum.已关闭;

                string cmdText = string.Format("select count(*) from dbo.Pri_StopLoss so where so.StopLossApplyId =@stopLossApplyId and so.StopLossStatus >= @entryStatus and so.StopLossStatus <@completeStatus");
                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter para = new SqlParameter("@stopLossApplyId", stopLossApplyId);
                paras.Add(para);

                para = new SqlParameter("@entryStatus", entryStatus);
                paras.Add(para);

                para = new SqlParameter("@completeStatus", completeStatus);
                paras.Add(para);

                object obj = SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras.ToArray());
                int otherStatusRows = 0;
                if (obj == null || !int.TryParse(obj.ToString(), out otherStatusRows) || otherStatusRows != 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "止损申请拥有待执行的止损，不能进行执行完成确认操作。";
                    return result;
                }

                cmdText = string.Format("select count(*) from dbo.Pri_StopLossApplyDetail soad left join dbo.Pri_StopLossDetail sod on sod.StopLossApplyDetailId = soad.DetailId and sod.DetailStatus = @completeStatus where soad.StopLossApplyId = @stopLossApplyId  and soad.DetailStatus = @readyStatus and sod.DetailId is null");
                paras = new List<SqlParameter>();

                para = new SqlParameter("@stopLossApplyId", stopLossApplyId);
                paras.Add(para);

                para = new SqlParameter("@completeStatus", completeStatus);
                paras.Add(para);

                para = new SqlParameter("@readyStatus", readyStatus);
                paras.Add(para);

                obj = SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras.ToArray());
                int notCompleteRows = 0;
                if (obj == null || !int.TryParse(obj.ToString(), out notCompleteRows) || notCompleteRows != 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "止损申请拥有待执行的止损，不能进行执行完成确认操作。";
                    return result;
                }

                cmdText = string.Format("select count(*) from dbo.Pri_StopLossApplyDetail sod where sod.DetailStatus =@closeStatus and sod.StopLossApplyId=@stopLossApplyId");

                paras = new List<SqlParameter>();

                para = new SqlParameter("@stopLossApplyId", stopLossApplyId);
                paras.Add(para);

                para = new SqlParameter("@closeStatus", closeStatus);
                paras.Add(para);

                obj = SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras.ToArray());
                int closeRows = 0;
                if (obj == null || !int.TryParse(obj.ToString(), out closeRows))
                {
                    result.ResultStatus = -1;
                    result.Message = "止损申请拥有待执行的止损，不能进行执行完成确认操作。";
                    return result;
                }

                result.ResultStatus = 0;
                if (closeRows == 0)
                    result.ReturnValue = StatusEnum.已完成;
                else
                    result.ReturnValue = StatusEnum.部分完成;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel GetModelByApplyId(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                string sql = string.Format("select * from dbo.Pri_StopLossApply where ApplyId = {0}", applyId);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, sql, null);

                Model.StopLossApply model = new StopLossApply();

                if (dr.Read())
                {
                    model = CreateModel(dr) as StopLossApply;

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = model;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
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

        public override int MenuId
        {
            get
            {
                return 91;
            }
        }

        #endregion
    }
}
