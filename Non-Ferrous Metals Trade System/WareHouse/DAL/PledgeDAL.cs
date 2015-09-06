/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeDAL.cs
// 文件功能描述：质押dbo.St_Pledge数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年8月4日
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
    /// 质押dbo.St_Pledge数据交互类。
    /// </summary>
    public class PledgeDAL : ExecOperate, IPledgeDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PledgeDAL()
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
            Pledge st_pledge = (Pledge)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@PledgeId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter pledgeapplyidpara = new SqlParameter("@PledgeApplyId", SqlDbType.Int, 4);
            pledgeapplyidpara.Value = st_pledge.PledgeApplyId;
            paras.Add(pledgeapplyidpara);

            SqlParameter pledgerpara = new SqlParameter("@Pledger", SqlDbType.Int, 4);
            pledgerpara.Value = st_pledge.Pledger;
            paras.Add(pledgerpara);

            SqlParameter pledgetimepara = new SqlParameter("@PledgeTime", SqlDbType.DateTime, 8);
            pledgetimepara.Value = st_pledge.PledgeTime;
            paras.Add(pledgetimepara);

            SqlParameter pledgebankpara = new SqlParameter("@PledgeBank", SqlDbType.Int, 4);
            pledgebankpara.Value = st_pledge.PledgeBank;
            paras.Add(pledgebankpara);

            if (!string.IsNullOrEmpty(st_pledge.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = st_pledge.Memo;
                paras.Add(memopara);
            }

            SqlParameter pledgestatuspara = new SqlParameter("@PledgeStatus", SqlDbType.Int, 4);
            pledgestatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(pledgestatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            Pledge pledge = new Pledge();

            pledge.PledgeId = Convert.ToInt32(dr["PledgeId"]);

            if (dr["PledgeApplyId"] != DBNull.Value)
            {
                pledge.PledgeApplyId = Convert.ToInt32(dr["PledgeApplyId"]);
            }

            if (dr["Pledger"] != DBNull.Value)
            {
                pledge.Pledger = Convert.ToInt32(dr["Pledger"]);
            }

            if (dr["PledgeTime"] != DBNull.Value)
            {
                pledge.PledgeTime = Convert.ToDateTime(dr["PledgeTime"]);
            }

            if (dr["PledgeBank"] != DBNull.Value)
            {
                pledge.PledgeBank = Convert.ToInt32(dr["PledgeBank"]);
            }

            if (dr["Memo"] != DBNull.Value)
            {
                pledge.Memo = Convert.ToString(dr["Memo"]);
            }

            if (dr["PledgeStatus"] != DBNull.Value)
            {
                pledge.PledgeStatus = (Common.StatusEnum)Convert.ToInt32(dr["PledgeStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                pledge.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                pledge.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                pledge.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pledge.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return pledge;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Pledge pledge = new Pledge();

            int indexPledgeId = dr.GetOrdinal("PledgeId");
            pledge.PledgeId = Convert.ToInt32(dr[indexPledgeId]);

            int indexPledgeApplyId = dr.GetOrdinal("PledgeApplyId");
            if (dr["PledgeApplyId"] != DBNull.Value)
            {
                pledge.PledgeApplyId = Convert.ToInt32(dr[indexPledgeApplyId]);
            }

            int indexPledger = dr.GetOrdinal("Pledger");
            if (dr["Pledger"] != DBNull.Value)
            {
                pledge.Pledger = Convert.ToInt32(dr[indexPledger]);
            }

            int indexPledgeTime = dr.GetOrdinal("PledgeTime");
            if (dr["PledgeTime"] != DBNull.Value)
            {
                pledge.PledgeTime = Convert.ToDateTime(dr[indexPledgeTime]);
            }

            int indexPledgeBank = dr.GetOrdinal("PledgeBank");
            if (dr["PledgeBank"] != DBNull.Value)
            {
                pledge.PledgeBank = Convert.ToInt32(dr[indexPledgeBank]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                pledge.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexPledgeStatus = dr.GetOrdinal("PledgeStatus");
            if (dr["PledgeStatus"] != DBNull.Value)
            {
                pledge.PledgeStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexPledgeStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                pledge.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                pledge.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                pledge.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pledge.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return pledge;
        }

        public override string TableName
        {
            get
            {
                return "St_Pledge";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Pledge st_pledge = (Pledge)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter pledgeidpara = new SqlParameter("@PledgeId", SqlDbType.Int, 4);
            pledgeidpara.Value = st_pledge.PledgeId;
            paras.Add(pledgeidpara);

            SqlParameter pledgeapplyidpara = new SqlParameter("@PledgeApplyId", SqlDbType.Int, 4);
            pledgeapplyidpara.Value = st_pledge.PledgeApplyId;
            paras.Add(pledgeapplyidpara);

            SqlParameter pledgerpara = new SqlParameter("@Pledger", SqlDbType.Int, 4);
            pledgerpara.Value = st_pledge.Pledger;
            paras.Add(pledgerpara);

            SqlParameter pledgetimepara = new SqlParameter("@PledgeTime", SqlDbType.DateTime, 8);
            pledgetimepara.Value = st_pledge.PledgeTime;
            paras.Add(pledgetimepara);

            SqlParameter pledgebankpara = new SqlParameter("@PledgeBank", SqlDbType.Int, 4);
            pledgebankpara.Value = st_pledge.PledgeBank;
            paras.Add(pledgebankpara);

            if (!string.IsNullOrEmpty(st_pledge.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = st_pledge.Memo;
                paras.Add(memopara);
            }

            SqlParameter pledgestatuspara = new SqlParameter("@PledgeStatus", SqlDbType.Int, 4);
            pledgestatuspara.Value = st_pledge.PledgeStatus;
            paras.Add(pledgestatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetPledgeIdByApplyId(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();
            string str = string.Empty;

            try
            {
                string sql = string.Format("select PledgeId from dbo.St_Pledge where PledgeApplyId = {0}", pledgeApplyId);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        str += dr["PledgeId"].ToString() + ",";
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

        public override int MenuId
        {
            get
            {
                return 48;
            }
        }

        #endregion
    }
}
