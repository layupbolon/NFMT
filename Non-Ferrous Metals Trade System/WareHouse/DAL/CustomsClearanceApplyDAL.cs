/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CustomsClearanceApplyDAL.cs
// 文件功能描述：报关申请dbo.St_CustomsClearanceApply数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月29日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.WareHouse.Model;
using NFMT.DBUtility;
using NFMT.WareHouse.IDAL;
using NFMT.Common;

namespace NFMT.WareHouse.DAL
{
    /// <summary>
    /// 报关申请dbo.St_CustomsClearanceApply数据交互类。
    /// </summary>
    public class CustomsClearanceApplyDAL : ApplyOperate, ICustomsClearanceApplyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomsClearanceApplyDAL()
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
            CustomsClearanceApply st_customsclearanceapply = (CustomsClearanceApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@CustomsApplyId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = st_customsclearanceapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = st_customsclearanceapply.AssetId;
            paras.Add(assetidpara);

            SqlParameter grossweightpara = new SqlParameter("@GrossWeight", SqlDbType.Decimal, 9);
            grossweightpara.Value = st_customsclearanceapply.GrossWeight;
            paras.Add(grossweightpara);

            SqlParameter netweightpara = new SqlParameter("@NetWeight", SqlDbType.Decimal, 9);
            netweightpara.Value = st_customsclearanceapply.NetWeight;
            paras.Add(netweightpara);

            SqlParameter unitidpara = new SqlParameter("@UnitId", SqlDbType.Int, 4);
            unitidpara.Value = st_customsclearanceapply.UnitId;
            paras.Add(unitidpara);

            SqlParameter outcorpidpara = new SqlParameter("@OutCorpId", SqlDbType.Int, 4);
            outcorpidpara.Value = st_customsclearanceapply.OutCorpId;
            paras.Add(outcorpidpara);

            SqlParameter incorpidpara = new SqlParameter("@InCorpId", SqlDbType.Int, 4);
            incorpidpara.Value = st_customsclearanceapply.InCorpId;
            paras.Add(incorpidpara);

            SqlParameter customscorpidpara = new SqlParameter("@CustomsCorpId", SqlDbType.Int, 4);
            customscorpidpara.Value = st_customsclearanceapply.CustomsCorpId;
            paras.Add(customscorpidpara);

            SqlParameter customspricepara = new SqlParameter("@CustomsPrice", SqlDbType.Decimal, 9);
            customspricepara.Value = st_customsclearanceapply.CustomsPrice;
            paras.Add(customspricepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = st_customsclearanceapply.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            CustomsClearanceApply customsclearanceapply = new CustomsClearanceApply();

            customsclearanceapply.CustomsApplyId = Convert.ToInt32(dr["CustomsApplyId"]);

            if (dr["ApplyId"] != DBNull.Value)
            {
                customsclearanceapply.ApplyId = Convert.ToInt32(dr["ApplyId"]);
            }

            if (dr["AssetId"] != DBNull.Value)
            {
                customsclearanceapply.AssetId = Convert.ToInt32(dr["AssetId"]);
            }

            if (dr["GrossWeight"] != DBNull.Value)
            {
                customsclearanceapply.GrossWeight = Convert.ToDecimal(dr["GrossWeight"]);
            }

            if (dr["NetWeight"] != DBNull.Value)
            {
                customsclearanceapply.NetWeight = Convert.ToDecimal(dr["NetWeight"]);
            }

            if (dr["UnitId"] != DBNull.Value)
            {
                customsclearanceapply.UnitId = Convert.ToInt32(dr["UnitId"]);
            }

            if (dr["OutCorpId"] != DBNull.Value)
            {
                customsclearanceapply.OutCorpId = Convert.ToInt32(dr["OutCorpId"]);
            }

            if (dr["InCorpId"] != DBNull.Value)
            {
                customsclearanceapply.InCorpId = Convert.ToInt32(dr["InCorpId"]);
            }

            if (dr["CustomsCorpId"] != DBNull.Value)
            {
                customsclearanceapply.CustomsCorpId = Convert.ToInt32(dr["CustomsCorpId"]);
            }

            if (dr["CustomsPrice"] != DBNull.Value)
            {
                customsclearanceapply.CustomsPrice = Convert.ToDecimal(dr["CustomsPrice"]);
            }

            if (dr["CurrencyId"] != DBNull.Value)
            {
                customsclearanceapply.CurrencyId = Convert.ToInt32(dr["CurrencyId"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                customsclearanceapply.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                customsclearanceapply.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                customsclearanceapply.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                customsclearanceapply.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return customsclearanceapply;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            CustomsClearanceApply customsclearanceapply = new CustomsClearanceApply();

            int indexCustomsApplyId = dr.GetOrdinal("CustomsApplyId");
            customsclearanceapply.CustomsApplyId = Convert.ToInt32(dr[indexCustomsApplyId]);

            int indexApplyId = dr.GetOrdinal("ApplyId");
            if (dr["ApplyId"] != DBNull.Value)
            {
                customsclearanceapply.ApplyId = Convert.ToInt32(dr[indexApplyId]);
            }

            int indexAssetId = dr.GetOrdinal("AssetId");
            if (dr["AssetId"] != DBNull.Value)
            {
                customsclearanceapply.AssetId = Convert.ToInt32(dr[indexAssetId]);
            }

            int indexGrossWeight = dr.GetOrdinal("GrossWeight");
            if (dr["GrossWeight"] != DBNull.Value)
            {
                customsclearanceapply.GrossWeight = Convert.ToDecimal(dr[indexGrossWeight]);
            }

            int indexNetWeight = dr.GetOrdinal("NetWeight");
            if (dr["NetWeight"] != DBNull.Value)
            {
                customsclearanceapply.NetWeight = Convert.ToDecimal(dr[indexNetWeight]);
            }

            int indexUnitId = dr.GetOrdinal("UnitId");
            if (dr["UnitId"] != DBNull.Value)
            {
                customsclearanceapply.UnitId = Convert.ToInt32(dr[indexUnitId]);
            }

            int indexOutCorpId = dr.GetOrdinal("OutCorpId");
            if (dr["OutCorpId"] != DBNull.Value)
            {
                customsclearanceapply.OutCorpId = Convert.ToInt32(dr[indexOutCorpId]);
            }

            int indexInCorpId = dr.GetOrdinal("InCorpId");
            if (dr["InCorpId"] != DBNull.Value)
            {
                customsclearanceapply.InCorpId = Convert.ToInt32(dr[indexInCorpId]);
            }

            int indexCustomsCorpId = dr.GetOrdinal("CustomsCorpId");
            if (dr["CustomsCorpId"] != DBNull.Value)
            {
                customsclearanceapply.CustomsCorpId = Convert.ToInt32(dr[indexCustomsCorpId]);
            }

            int indexCustomsPrice = dr.GetOrdinal("CustomsPrice");
            if (dr["CustomsPrice"] != DBNull.Value)
            {
                customsclearanceapply.CustomsPrice = Convert.ToDecimal(dr[indexCustomsPrice]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                customsclearanceapply.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                customsclearanceapply.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                customsclearanceapply.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                customsclearanceapply.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                customsclearanceapply.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return customsclearanceapply;
        }

        public override string TableName
        {
            get
            {
                return "St_CustomsClearanceApply";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CustomsClearanceApply st_customsclearanceapply = (CustomsClearanceApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter customsapplyidpara = new SqlParameter("@CustomsApplyId", SqlDbType.Int, 4);
            customsapplyidpara.Value = st_customsclearanceapply.CustomsApplyId;
            paras.Add(customsapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = st_customsclearanceapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = st_customsclearanceapply.AssetId;
            paras.Add(assetidpara);

            SqlParameter grossweightpara = new SqlParameter("@GrossWeight", SqlDbType.Decimal, 9);
            grossweightpara.Value = st_customsclearanceapply.GrossWeight;
            paras.Add(grossweightpara);

            SqlParameter netweightpara = new SqlParameter("@NetWeight", SqlDbType.Decimal, 9);
            netweightpara.Value = st_customsclearanceapply.NetWeight;
            paras.Add(netweightpara);

            SqlParameter unitidpara = new SqlParameter("@UnitId", SqlDbType.Int, 4);
            unitidpara.Value = st_customsclearanceapply.UnitId;
            paras.Add(unitidpara);

            SqlParameter outcorpidpara = new SqlParameter("@OutCorpId", SqlDbType.Int, 4);
            outcorpidpara.Value = st_customsclearanceapply.OutCorpId;
            paras.Add(outcorpidpara);

            SqlParameter incorpidpara = new SqlParameter("@InCorpId", SqlDbType.Int, 4);
            incorpidpara.Value = st_customsclearanceapply.InCorpId;
            paras.Add(incorpidpara);

            SqlParameter customscorpidpara = new SqlParameter("@CustomsCorpId", SqlDbType.Int, 4);
            customscorpidpara.Value = st_customsclearanceapply.CustomsCorpId;
            paras.Add(customscorpidpara);

            SqlParameter customspricepara = new SqlParameter("@CustomsPrice", SqlDbType.Decimal, 9);
            customspricepara.Value = st_customsclearanceapply.CustomsPrice;
            paras.Add(customspricepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = st_customsclearanceapply.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel CheckCustomCanConfirm(UserModel user, int customApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                int entryStatus = (int)Common.StatusEnum.已录入;
                int completeStatus = (int)StatusEnum.已完成;
                int readyStatus = (int)StatusEnum.已生效;
                int closeStatus = (int)StatusEnum.已关闭;

                string cmdText = string.Format("select count(*) from dbo.St_CustomsClearance so where so.CustomsApplyId =@customsApplyId and so.CustomsStatus >= @entryStatus and so.CustomsStatus <@completeStatus");
                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter para = new SqlParameter("@customsApplyId", customApplyId);
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
                    result.Message = "报关申请拥有待执行的报关，不能进行执行完成确认操作。";
                    return result;
                }

                cmdText = string.Format("select count(*) from dbo.St_CustomsApplyDetail soad left join dbo.St_CustomsDetail sod on sod.CustomsApplyDetailId = soad.DetailId and sod.DetailStatus = @completeStatus where soad.CustomsApplyId = @customsApplyId  and soad.DetailStatus = @readyStatus and sod.DetailId is null");
                paras = new List<SqlParameter>();

                para = new SqlParameter("@customsApplyId", customApplyId);
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
                    result.Message = "报关申请拥有待执行的报关，不能进行执行完成确认操作。";
                    return result;
                }

                cmdText = string.Format("select count(*) from dbo.St_CustomsApplyDetail sod where sod.DetailStatus =@closeStatus and sod.CustomsApplyId=@customsApplyId");

                paras = new List<SqlParameter>();

                para = new SqlParameter("@customsApplyId", customApplyId);
                paras.Add(para);

                para = new SqlParameter("@closeStatus", closeStatus);
                paras.Add(para);

                obj = SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras.ToArray());
                int closeRows = 0;
                if (obj == null || !int.TryParse(obj.ToString(), out closeRows))
                {
                    result.ResultStatus = -1;
                    result.Message = "报关申请拥有待执行的报关，不能进行执行完成确认操作。";
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
                string sql = string.Format("select * from dbo.St_CustomsClearanceApply where ApplyId = {0}", applyId);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, sql, null);

                Model.CustomsClearanceApply model = new CustomsClearanceApply();

                if (dr.Read())
                {
                    model = CreateModel(dr) as CustomsClearanceApply;

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
                return 95;
            }
        }

        #endregion
    }
}
