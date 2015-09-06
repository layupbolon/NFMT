/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SIDetailDAL.cs
// 文件功能描述：价外票明细dbo.Inv_SIDetail数据交互类。
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
    /// 价外票明细dbo.Inv_SIDetail数据交互类。
    /// </summary>
    public class SIDetailDAL : ExecOperate, ISIDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SIDetailDAL()
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
            SIDetail inv_sidetail = (SIDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@SIDetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter siidpara = new SqlParameter("@SIId", SqlDbType.Int, 4);
            siidpara.Value = inv_sidetail.SIId;
            paras.Add(siidpara);

            SqlParameter paydeptpara = new SqlParameter("@PayDept", SqlDbType.Int, 4);
            paydeptpara.Value = inv_sidetail.PayDept;
            paras.Add(paydeptpara);

            SqlParameter feetypepara = new SqlParameter("@FeeType", SqlDbType.Int, 4);
            feetypepara.Value = inv_sidetail.FeeType;
            paras.Add(feetypepara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = inv_sidetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = inv_sidetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = inv_sidetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter contractsubidpara = new SqlParameter("@ContractSubId", SqlDbType.Int, 4);
            contractsubidpara.Value = inv_sidetail.ContractSubId;
            paras.Add(contractsubidpara);

            SqlParameter detailbalapara = new SqlParameter("@DetailBala", SqlDbType.Decimal, 9);
            detailbalapara.Value = inv_sidetail.DetailBala;
            paras.Add(detailbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);

            if (!string.IsNullOrEmpty(inv_sidetail.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = inv_sidetail.Memo;
                paras.Add(memopara);
            }

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            SIDetail sidetail = new SIDetail();

            int indexSIDetailId = dr.GetOrdinal("SIDetailId");
            sidetail.SIDetailId = Convert.ToInt32(dr[indexSIDetailId]);

            int indexSIId = dr.GetOrdinal("SIId");
            if (dr["SIId"] != DBNull.Value)
            {
                sidetail.SIId = Convert.ToInt32(dr[indexSIId]);
            }

            int indexPayDept = dr.GetOrdinal("PayDept");
            if (dr["PayDept"] != DBNull.Value)
            {
                sidetail.PayDept = Convert.ToInt32(dr[indexPayDept]);
            }

            int indexFeeType = dr.GetOrdinal("FeeType");
            if (dr["FeeType"] != DBNull.Value)
            {
                sidetail.FeeType = Convert.ToInt32(dr[indexFeeType]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                sidetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                sidetail.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                sidetail.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexContractSubId = dr.GetOrdinal("ContractSubId");
            if (dr["ContractSubId"] != DBNull.Value)
            {
                sidetail.ContractSubId = Convert.ToInt32(dr[indexContractSubId]);
            }

            int indexDetailBala = dr.GetOrdinal("DetailBala");
            if (dr["DetailBala"] != DBNull.Value)
            {
                sidetail.DetailBala = Convert.ToDecimal(dr[indexDetailBala]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                sidetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                sidetail.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                sidetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                sidetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                sidetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                sidetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return sidetail;
        }

        public override string TableName
        {
            get
            {
                return "Inv_SIDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SIDetail inv_sidetail = (SIDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter sidetailidpara = new SqlParameter("@SIDetailId", SqlDbType.Int, 4);
            sidetailidpara.Value = inv_sidetail.SIDetailId;
            paras.Add(sidetailidpara);

            SqlParameter siidpara = new SqlParameter("@SIId", SqlDbType.Int, 4);
            siidpara.Value = inv_sidetail.SIId;
            paras.Add(siidpara);

            SqlParameter paydeptpara = new SqlParameter("@PayDept", SqlDbType.Int, 4);
            paydeptpara.Value = inv_sidetail.PayDept;
            paras.Add(paydeptpara);

            SqlParameter feetypepara = new SqlParameter("@FeeType", SqlDbType.Int, 4);
            feetypepara.Value = inv_sidetail.FeeType;
            paras.Add(feetypepara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = inv_sidetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = inv_sidetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = inv_sidetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter contractsubidpara = new SqlParameter("@ContractSubId", SqlDbType.Int, 4);
            contractsubidpara.Value = inv_sidetail.ContractSubId;
            paras.Add(contractsubidpara);

            SqlParameter detailbalapara = new SqlParameter("@DetailBala", SqlDbType.Decimal, 9);
            detailbalapara.Value = inv_sidetail.DetailBala;
            paras.Add(detailbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = inv_sidetail.DetailStatus;
            paras.Add(detailstatuspara);

            if (!string.IsNullOrEmpty(inv_sidetail.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = inv_sidetail.Memo;
                paras.Add(memopara);
            }

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetSIDetailForUpdate(UserModel user, int sIId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select detail.StockId,detail.StockLogId,detail.ContractId,detail.ContractSubId,sname.RefNo,c.CorpName,a.AssetName,sub.SubNo,detail.FeeType,bd.DetailName as FeeTypeName,detail.DetailBala,dp.DPName,s.CardNo,bra.BrandName,sub.OutContractNo ");
                sb.Append(" from dbo.Inv_SIDetail detail ");
                sb.Append(" left join dbo.St_Stock s on detail.StockId = s.StockId ");
                sb.Append(" left join dbo.St_StockName sname on s.StockNameId = sname.StockNameId ");
                sb.Append(" left join NFMT_User.dbo.Corporation c on s.CorpId = c.CorpId ");
                sb.Append(" left join NFMT_Basic.dbo.Asset a on s.AssetId = a.AssetId ");
                sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = s.DeliverPlaceId ");
                sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = s.BrandId ");
                sb.Append(" left join dbo.Con_ContractSub sub on detail.ContractSubId = sub.SubId ");
                sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail bd on detail.FeeType = bd.StyleDetailId and bd.BDStyleId = {0} ", (int)NFMT.Data.StyleEnum.发票内容);
                if (sIId > 0)
                    sb.AppendFormat(" where detail.SIId = {0} and detail.DetailStatus >= {1} ", sIId, (int)Common.StatusEnum.已生效);
                else
                    sb.Append(" where 1=2 ");

                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ReturnValue = dt;
                }
                else
                    result.ReturnValue = new DataTable();
                result.ResultStatus = 0;
                result.Message = "获取成功";
                //else
                //{
                //    result.ResultStatus = -1;
                //    result.Message = "获取失败";
                //}
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel InvalidAll(UserModel user, int sIId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Inv_SIDetail set DetailStatus = {0} where SIId = {1}", (int)Common.StatusEnum.已作废, sIId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                    result.AffectCount = i;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "作废失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel Load(UserModel user, int sIId, StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select * from dbo.Inv_SIDetail where SIId={0} and DetailStatus ={1}", sIId, (int)status);
                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, cmdText, null, CommandType.Text);

                List<SIDetail> sIDetails = new List<SIDetail>();

                foreach (DataRow dr in dt.Rows)
                {
                    SIDetail sidetail = new SIDetail();
                    sidetail.SIDetailId = Convert.ToInt32(dr["SIDetailId"]);

                    if (dr["SIId"] != DBNull.Value)
                    {
                        sidetail.SIId = Convert.ToInt32(dr["SIId"]);
                    }
                    if (dr["PayDept"] != DBNull.Value)
                    {
                        sidetail.PayDept = Convert.ToInt32(dr["PayDept"]);
                    }
                    if (dr["FeeType"] != DBNull.Value)
                    {
                        sidetail.FeeType = Convert.ToInt32(dr["FeeType"]);
                    }
                    if (dr["StockId"] != DBNull.Value)
                    {
                        sidetail.StockId = Convert.ToInt32(dr["StockId"]);
                    }
                    if (dr["StockLogId"] != DBNull.Value)
                    {
                        sidetail.StockLogId = Convert.ToInt32(dr["StockLogId"]);
                    }
                    if (dr["ContractId"] != DBNull.Value)
                    {
                        sidetail.ContractId = Convert.ToInt32(dr["ContractId"]);
                    }
                    if (dr["ContractSubId"] != DBNull.Value)
                    {
                        sidetail.ContractSubId = Convert.ToInt32(dr["ContractSubId"]);
                    }
                    if (dr["DetailBala"] != DBNull.Value)
                    {
                        sidetail.DetailBala = Convert.ToDecimal(dr["DetailBala"]);
                    }
                    if (dr["DetailStatus"] != DBNull.Value)
                    {
                        sidetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
                    }
                    if (dr["Memo"] != DBNull.Value)
                    {
                        sidetail.Memo = Convert.ToString(dr["Memo"]);
                    }
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        sidetail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
                    }
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        sidetail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    }
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        sidetail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    }
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        sidetail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    }
                    sIDetails.Add(sidetail);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = sIDetails;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        #endregion
    }
}
