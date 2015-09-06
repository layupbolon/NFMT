/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractClauseDAL.cs
// 文件功能描述：合约条款关联dbo.Con_ContractClause_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年1月13日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Contract.Model;
using NFMT.DBUtility;
using NFMT.Contract.IDAL;
using NFMT.Common;

namespace NFMT.Contract.DAL
{
    /// <summary>
    /// 合约条款关联dbo.Con_ContractClause_Ref数据交互类。
    /// </summary>
    public class ContractClauseDAL : DataOperate, IContractClauseDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractClauseDAL()
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
            ContractClause con_contractclause_ref = (ContractClause)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contractclause_ref.ContractId;
            paras.Add(contractidpara);

            SqlParameter masteridpara = new SqlParameter("@MasterId", SqlDbType.Int, 4);
            masteridpara.Value = con_contractclause_ref.MasterId;
            paras.Add(masteridpara);

            SqlParameter clauseidpara = new SqlParameter("@ClauseId", SqlDbType.Int, 4);
            clauseidpara.Value = con_contractclause_ref.ClauseId;
            paras.Add(clauseidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(refstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            ContractClause contractclause = new ContractClause();

            contractclause.RefId = Convert.ToInt32(dr["RefId"]);

            if (dr["ContractId"] != DBNull.Value)
            {
                contractclause.ContractId = Convert.ToInt32(dr["ContractId"]);
            }

            if (dr["MasterId"] != DBNull.Value)
            {
                contractclause.MasterId = Convert.ToInt32(dr["MasterId"]);
            }

            if (dr["ClauseId"] != DBNull.Value)
            {
                contractclause.ClauseId = Convert.ToInt32(dr["ClauseId"]);
            }

            if (dr["RefStatus"] != DBNull.Value)
            {
                contractclause.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr["RefStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                contractclause.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                contractclause.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                contractclause.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                contractclause.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return contractclause;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractClause contractclause = new ContractClause();

            int indexRefId = dr.GetOrdinal("RefId");
            contractclause.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                contractclause.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexMasterId = dr.GetOrdinal("MasterId");
            if (dr["MasterId"] != DBNull.Value)
            {
                contractclause.MasterId = Convert.ToInt32(dr[indexMasterId]);
            }

            int indexClauseId = dr.GetOrdinal("ClauseId");
            if (dr["ClauseId"] != DBNull.Value)
            {
                contractclause.ClauseId = Convert.ToInt32(dr[indexClauseId]);
            }

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                contractclause.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRefStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                contractclause.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                contractclause.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                contractclause.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                contractclause.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return contractclause;
        }

        public override string TableName
        {
            get
            {
                return "Con_ContractClause_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractClause con_contractclause_ref = (ContractClause)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = con_contractclause_ref.RefId;
            paras.Add(refidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contractclause_ref.ContractId;
            paras.Add(contractidpara);

            SqlParameter masteridpara = new SqlParameter("@MasterId", SqlDbType.Int, 4);
            masteridpara.Value = con_contractclause_ref.MasterId;
            paras.Add(masteridpara);

            SqlParameter clauseidpara = new SqlParameter("@ClauseId", SqlDbType.Int, 4);
            clauseidpara.Value = con_contractclause_ref.ClauseId;
            paras.Add(clauseidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = con_contractclause_ref.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel LoadByContractId(UserModel user, int contractId)
        {
            string sql = string.Format("select * from dbo.Con_ContractClause_Ref where ContractId = {0} and RefStatus = {1}", contractId, (int)Common.StatusEnum.已生效);
            return this.Load<Model.ContractClause>(user, CommandType.Text, sql);
        }

        public ResultModel InvalidAll(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Con_ContractClause_Ref set RefStatus = {0} where ContractId = {1} ", (int)Common.StatusEnum.已作废, contractId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                }
                else
                {
                    result.ResultStatus = 0;
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

        public ResultModel LoadClauseByContractId(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                string sql = string.Format("select cc.* from dbo.Con_ContractClause_Ref ref left join NFMT_Basic..ContractClause cc on ref.ClauseId = cc.ClauseId and cc.ClauseStatus = {0} where ref.ContractId = {1} and ref.RefStatus = {0}", (int)Common.StatusEnum.已生效, contractId);
                dr = SqlHelper.ExecuteReader(this.ConnectString, CommandType.Text, sql, null);
                List<NFMT.Data.Model.ContractClause> models = new List<NFMT.Data.Model.ContractClause>();

                NFMT.Data.DAL.ContractClauseDAL contractClauseDAL = new Data.DAL.ContractClauseDAL();

                int i = 0;
                while (dr.Read())
                {
                    NFMT.Data.Model.ContractClause t = contractClauseDAL.CreateModel<NFMT.Data.Model.ContractClause>(dr);
                    models.Add(t);
                    i++;
                }

                result.AffectCount = i;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = models;
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

        #endregion
    }
}
