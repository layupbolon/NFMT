/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeApplyDAL.cs
// 文件功能描述：质押申请dbo.PledgeApply数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
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
    /// 质押申请dbo.PledgeApply数据交互类。
    /// </summary>
    public class PledgeApplyDAL : ApplyOperate, IPledgeApplyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PledgeApplyDAL()
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
            PledgeApply st_pledgeapply = (PledgeApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@PledgeApplyId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = st_pledgeapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter pledgebankpara = new SqlParameter("@PledgeBank", SqlDbType.Int, 4);
            pledgebankpara.Value = st_pledgeapply.PledgeBank;
            paras.Add(pledgebankpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            PledgeApply pledgeapply = new PledgeApply();

            pledgeapply.PledgeApplyId = Convert.ToInt32(dr["PledgeApplyId"]);

            if (dr["ApplyId"] != DBNull.Value)
            {
                pledgeapply.ApplyId = Convert.ToInt32(dr["ApplyId"]);
            }

            if (dr["PledgeBank"] != DBNull.Value)
            {
                pledgeapply.PledgeBank = Convert.ToInt32(dr["PledgeBank"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                pledgeapply.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                pledgeapply.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                pledgeapply.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pledgeapply.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return pledgeapply;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PledgeApply pledgeapply = new PledgeApply();

            int indexPledgeApplyId = dr.GetOrdinal("PledgeApplyId");
            pledgeapply.PledgeApplyId = Convert.ToInt32(dr[indexPledgeApplyId]);

            int indexApplyId = dr.GetOrdinal("ApplyId");
            if (dr["ApplyId"] != DBNull.Value)
            {
                pledgeapply.ApplyId = Convert.ToInt32(dr[indexApplyId]);
            }

            int indexPledgeBank = dr.GetOrdinal("PledgeBank");
            if (dr["PledgeBank"] != DBNull.Value)
            {
                pledgeapply.PledgeBank = Convert.ToInt32(dr[indexPledgeBank]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                pledgeapply.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                pledgeapply.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                pledgeapply.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pledgeapply.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return pledgeapply;
        }

        public override string TableName
        {
            get
            {
                return "St_PledgeApply";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PledgeApply st_pledgeapply = (PledgeApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter pledgeapplyidpara = new SqlParameter("@PledgeApplyId", SqlDbType.Int, 4);
            pledgeapplyidpara.Value = st_pledgeapply.PledgeApplyId;
            paras.Add(pledgeapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = st_pledgeapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter pledgebankpara = new SqlParameter("@PledgeBank", SqlDbType.Int, 4);
            pledgebankpara.Value = st_pledgeapply.PledgeBank;
            paras.Add(pledgebankpara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetPledgeStockId(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();
            string str = string.Empty;

            try
            {
                string sql = string.Format("select distinct isnull(StockId,0) as StockId from NFMT.dbo.St_PledgeApplyDetail where PledgeApplyId = {0} and DetailStatus = {1} and StockId not in (select distinct ISNULL(StockId,0) from NFMT.dbo.St_PledgeDetial pd right join NFMT.dbo.St_Pledge p on pd.PledgeId = p.PledgeId and p.PledgeApplyId = {2} and pd.DetailStatus not in  ({3},{4}))", pledgeApplyId, (int)Common.StatusEnum.已生效, pledgeApplyId, (int)Common.StatusEnum.已作废,(int)Common.StatusEnum.已关闭);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        str += dr["StockId"].ToString() + ",";
                    }
                    if (!string.IsNullOrEmpty(str))
                        str = str.Substring(0, str.Length - 1);

                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = str;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel GetPledgeApplyStockId(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();
            string str = string.Empty;

            try
            {
                string sql = string.Format("select StockId from dbo.St_PledgeDetial where PledgeId in (select PledgeId from dbo.St_Pledge where PledgeApplyId = {0})", pledgeApplyId);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        str += dr["StockId"].ToString() + ",";
                    }
                    if (!string.IsNullOrEmpty(str))
                        str = str.Substring(0, str.Length - 1);

                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = str;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel CheckPledgeCanConfirm(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                int entryStatus = (int)Common.StatusEnum.已录入;
                int completeStatus = (int)StatusEnum.已完成;
                int readyStatus = (int)StatusEnum.已生效;
                int closeStatus = (int)StatusEnum.已关闭;

                string cmdText = string.Format("select count(*) from dbo.St_Pledge so where so.PledgeApplyId =@pledgeApplyId and so.PledgeStatus >= @entryStatus and so.PledgeStatus <@completeStatus");
                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter para = new SqlParameter("@pledgeApplyId", pledgeApplyId);
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
                    result.Message = "质押申请拥有待执行的质押，不能进行执行完成确认操作。";
                    return result;
                }

                cmdText = string.Format("select count(*) from dbo.St_PledgeApplyDetail soad left join dbo.St_PledgeDetial sod on sod.PledgeApplyDetailId = soad.DetailId and sod.DetailStatus = @completeStatus where soad.PledgeApplyId = @pledgeApplyId  and soad.DetailStatus = @readyStatus and sod.DetailId is null");
                paras = new List<SqlParameter>();

                para = new SqlParameter("@pledgeApplyId", pledgeApplyId);
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
                    result.Message = "质押申请拥有待执行的质押，不能进行执行完成确认操作。";
                    return result;
                }

                cmdText = string.Format("select count(*) from dbo.St_PledgeApplyDetail sod where sod.DetailStatus =@closeStatus and sod.PledgeApplyId=@pledgeApplyId");

                paras = new List<SqlParameter>();

                para = new SqlParameter("@pledgeApplyId", pledgeApplyId);
                paras.Add(para);

                para = new SqlParameter("@closeStatus", closeStatus);
                paras.Add(para);

                obj = SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras.ToArray());
                int closeRows = 0;
                if (obj == null || !int.TryParse(obj.ToString(), out closeRows))
                {
                    result.ResultStatus = -1;
                    result.Message = "质押申请拥有待执行的质押，不能进行执行完成确认操作。";
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

        public ResultModel GetPledgeByApplyId(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                string sql = string.Format("select * from dbo.St_PledgeApply where ApplyId = {0}", applyId);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, sql, null);

                Model.PledgeApply model = null;

                if (dr.Read())
                {
                    model = CreateModel(dr) as Model.PledgeApply;

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

        public ResultModel GetPledgeApplyDetails(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select st.StockId,stn.RefNo,st.PaperNo,a.AssetName,convert(varchar,st.GrossAmount) + mu.MUName as GrossAmount,CONVERT(varchar,st.NetAmount) + mu.MUName as NetAmount,pad.ApplyQty,pad.UintId,pad.PledgePrice,pad.CurrencyId,cur.CurrencyName,dp.DPName,bra.BrandName,st.CardNo ");
                sb.Append(" from dbo.St_PledgeApplyDetail pad ");
                sb.Append(" left join dbo.St_Stock st on pad.StockId = st.StockId ");
                sb.Append(" left join dbo.St_StockName stn on st.StockNameId = stn.StockNameId ");
                sb.Append(" left join NFMT_Basic.dbo.Asset a on a.AssetId = st.AssetId ");
                sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = st.UintId ");
                sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = pad.CurrencyId ");
                sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = st.DeliverPlaceId ");
                sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = st.BrandId ");
                sb.AppendFormat(" where pad.PledgeApplyId = {0} and pad.DetailStatus = {1}", pledgeApplyId, (int)Common.StatusEnum.已生效);
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
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public override int MenuId
        {
            get
            {
                return 47;
            }
        }

        #endregion
    }
}
