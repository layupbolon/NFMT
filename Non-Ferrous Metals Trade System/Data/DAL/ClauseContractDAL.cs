/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ClauseContractDAL.cs
// 文件功能描述：模板条款关联表dbo.ClauseContract_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Data.Model;
using NFMT.DBUtility;
using NFMT.Data.IDAL;
using NFMT.Common;

namespace NFMT.Data.DAL
{
    /// <summary>
    /// 模板条款关联表dbo.ClauseContract_Ref数据交互类。
    /// </summary>
    public class ClauseContractDAL : DetailOperate, IClauseContractDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ClauseContractDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringBasic;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            ClauseContract clausecontract_ref = (ClauseContract)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter masteridpara = new SqlParameter("@MasterId", SqlDbType.Int, 4);
            masteridpara.Value = clausecontract_ref.MasterId;
            paras.Add(masteridpara);

            SqlParameter clauseidpara = new SqlParameter("@ClauseId", SqlDbType.Int, 4);
            clauseidpara.Value = clausecontract_ref.ClauseId;
            paras.Add(clauseidpara);

            SqlParameter sortpara = new SqlParameter("@Sort", SqlDbType.Int, 4);
            sortpara.Value = clausecontract_ref.Sort;
            paras.Add(sortpara);

            SqlParameter ischosepara = new SqlParameter("@IsChose", SqlDbType.Bit, 1);
            ischosepara.Value = clausecontract_ref.IsChose;
            paras.Add(ischosepara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(refstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ClauseContract clausecontract = new ClauseContract();

            int indexRefId = dr.GetOrdinal("RefId");
            clausecontract.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexMasterId = dr.GetOrdinal("MasterId");
            if (dr["MasterId"] != DBNull.Value)
            {
                clausecontract.MasterId = Convert.ToInt32(dr[indexMasterId]);
            }

            int indexClauseId = dr.GetOrdinal("ClauseId");
            if (dr["ClauseId"] != DBNull.Value)
            {
                clausecontract.ClauseId = Convert.ToInt32(dr[indexClauseId]);
            }

            int indexSort = dr.GetOrdinal("Sort");
            if (dr["Sort"] != DBNull.Value)
            {
                clausecontract.Sort = Convert.ToInt32(dr[indexSort]);
            }

            int indexIsChose = dr.GetOrdinal("IsChose");
            if (dr["IsChose"] != DBNull.Value)
            {
                clausecontract.IsChose = Convert.ToBoolean(dr[indexIsChose]);
            }

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                clausecontract.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRefStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                clausecontract.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                clausecontract.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                clausecontract.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                clausecontract.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return clausecontract;
        }

        public override string TableName
        {
            get
            {
                return "ClauseContract_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ClauseContract clausecontract_ref = (ClauseContract)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = clausecontract_ref.RefId;
            paras.Add(refidpara);

            SqlParameter masteridpara = new SqlParameter("@MasterId", SqlDbType.Int, 4);
            masteridpara.Value = clausecontract_ref.MasterId;
            paras.Add(masteridpara);

            SqlParameter clauseidpara = new SqlParameter("@ClauseId", SqlDbType.Int, 4);
            clauseidpara.Value = clausecontract_ref.ClauseId;
            paras.Add(clauseidpara);

            SqlParameter sortpara = new SqlParameter("@Sort", SqlDbType.Int, 4);
            sortpara.Value = clausecontract_ref.Sort;
            paras.Add(sortpara);

            SqlParameter ischosepara = new SqlParameter("@IsChose", SqlDbType.Bit, 1);
            ischosepara.Value = clausecontract_ref.IsChose;
            paras.Add(ischosepara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = clausecontract_ref.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, Common.StatusEnum status)
        {
            string sql = string.Format("select ref.* from dbo.ClauseContract_Ref ref left join dbo.ContractMaster cm on ref.MasterId = cm.MasterId  left join dbo.ContractClause cc on ref.ClauseId = cc.ClauseId where ref.RefStatus >={0} and cm.MasterStatus >={0} and cc.ClauseStatus >={0}", (int)status);
            return this.Load<Model.ClauseContract>(user, CommandType.Text, sql);
        }

        #endregion
    }
}
