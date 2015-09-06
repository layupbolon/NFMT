/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RecAllotDetailDAL.cs
// 文件功能描述：收款分配明细dbo.Fun_RecAllotDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
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
    /// 收款分配明细dbo.Fun_RecAllotDetail数据交互类。
    /// </summary>
    public class RecAllotDetailDAL : ExecOperate, IRecAllotDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RecAllotDetailDAL()
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
            RecAllotDetail fun_recallotdetail = (RecAllotDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter recidpara = new SqlParameter("@RecId", SqlDbType.Int, 4);
            recidpara.Value = fun_recallotdetail.RecId;
            paras.Add(recidpara);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_recallotdetail.AllotId;
            paras.Add(allotidpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_recallotdetail.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = fun_recallotdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            RecAllotDetail recallotdetail = new RecAllotDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            recallotdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexRecId = dr.GetOrdinal("RecId");
            if (dr["RecId"] != DBNull.Value)
            {
                recallotdetail.RecId = Convert.ToInt32(dr[indexRecId]);
            }

            int indexAllotId = dr.GetOrdinal("AllotId");
            if (dr["AllotId"] != DBNull.Value)
            {
                recallotdetail.AllotId = Convert.ToInt32(dr[indexAllotId]);
            }

            int indexAllotBala = dr.GetOrdinal("AllotBala");
            if (dr["AllotBala"] != DBNull.Value)
            {
                recallotdetail.AllotBala = Convert.ToDecimal(dr[indexAllotBala]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                recallotdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                recallotdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                recallotdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                recallotdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                recallotdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return recallotdetail;
        }

        public override string TableName
        {
            get
            {
                return "Fun_RecAllotDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            RecAllotDetail fun_recallotdetail = (RecAllotDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = fun_recallotdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter recidpara = new SqlParameter("@RecId", SqlDbType.Int, 4);
            recidpara.Value = fun_recallotdetail.RecId;
            paras.Add(recidpara);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_recallotdetail.AllotId;
            paras.Add(allotidpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_recallotdetail.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = fun_recallotdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user,int allotId,NFMT.Common.StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select * from dbo.Fun_RecAllotDetail where AllotId = {0} and DetailStatus >={1}",allotId,(int)status);

                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, cmdText, null, CommandType.Text);

                List<RecAllotDetail> recAllotDetails = new List<RecAllotDetail>();

                foreach (DataRow dr in dt.Rows)
                {
                    RecAllotDetail recallotdetail = new RecAllotDetail();
                    recallotdetail.DetailId = Convert.ToInt32(dr["DetailId"]);

                    if (dr["RecId"] != DBNull.Value)
                    {
                        recallotdetail.RecId = Convert.ToInt32(dr["RecId"]);
                    }
                    if (dr["AllotId"] != DBNull.Value)
                    {
                        recallotdetail.AllotId = Convert.ToInt32(dr["AllotId"]);
                    }
                    if (dr["AllotBala"] != DBNull.Value)
                    {
                        recallotdetail.AllotBala = Convert.ToDecimal(dr["AllotBala"]);
                    }
                    if (dr["DetailStatus"] != DBNull.Value)
                    {
                        recallotdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
                    }
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        recallotdetail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
                    }
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        recallotdetail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    }
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        recallotdetail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    }
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        recallotdetail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    }
                    recAllotDetails.Add(recallotdetail);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = recAllotDetails;
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
