/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ClauseContract_DAL.cs
// 文件功能描述：模板条款关联表dbo.ClauseContract_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月8日
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
    public class ClauseContractRefDAL : DataOperate, IClauseContractRefDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ClauseContractRefDAL()
        {
        }

        #endregion

        #region 数据库操作

        /// <summary>
        /// 新增clausecontract_ref信息
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">ClauseContract_对象</param>
        /// <returns></returns>
        public override ResultModel Insert(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();
            try
            {
                ClauseContractRef clausecontract_ref = (ClauseContractRef)obj;

                if (clausecontract_ref == null)
                {
                    result.Message = "新增对象不能为null";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();
                SqlParameter refidpara = new SqlParameter();
                refidpara.Direction = ParameterDirection.Output;
                refidpara.SqlDbType = SqlDbType.Int;
                refidpara.ParameterName = "@RefId";
                refidpara.Size = 4;
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

                SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
                creatoridpara.Value = user.AccountId;
                paras.Add(creatoridpara);


                int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringNFMT, CommandType.StoredProcedure, "ClauseContract_RefInsert", paras.ToArray());

                if (i == 1)
                {
                    result.AffectCount = i;
                    result.ResultStatus = 0;
                    result.Message = "ClauseContractRef添加成功";
                    result.ReturnValue = refidpara.Value;
                }
                else
                    result.Message = "ClauseContractRef添加失败";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 获取指定refId的clausecontract_ref对象
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="refId">主键值</param>
        /// <returns></returns>
        public override ResultModel Get(UserModel user, int refId)
        {
            ResultModel result = new ResultModel();

            if (refId < 1)
            {
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@RefId", SqlDbType.Int, 4);
            para.Value = refId;
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringNFMT, CommandType.StoredProcedure, "ClauseContract_RefGet", paras.ToArray());

                ClauseContractRef clauseContractRef = new ClauseContractRef();

                if (dr.Read())
                {
                    int indexRefId = dr.GetOrdinal("RefId");
                    clauseContractRef.RefId = Convert.ToInt32(dr[indexRefId]);

                    int indexMasterId = dr.GetOrdinal("MasterId");
                    if (dr["MasterId"] != DBNull.Value)
                    {
                        clauseContractRef.MasterId = Convert.ToInt32(dr[indexMasterId]);
                    }

                    int indexClauseId = dr.GetOrdinal("ClauseId");
                    if (dr["ClauseId"] != DBNull.Value)
                    {
                        clauseContractRef.ClauseId = Convert.ToInt32(dr[indexClauseId]);
                    }

                    int indexSort = dr.GetOrdinal("Sort");
                    if (dr["Sort"] != DBNull.Value)
                    {
                        clauseContractRef.Sort = Convert.ToInt32(dr[indexSort]);
                    }

                    int indexIsChose = dr.GetOrdinal("IsChose");
                    if (dr["IsChose"] != DBNull.Value)
                    {
                        clauseContractRef.IsChose = Convert.ToBoolean(dr[indexIsChose]);
                    }

                    int indexCreatorId = dr.GetOrdinal("CreatorId");
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        clauseContractRef.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
                    }

                    int indexCreateTime = dr.GetOrdinal("CreateTime");
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        clauseContractRef.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
                    }

                    int indexLastModifyId = dr.GetOrdinal("LastModifyId");
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        clauseContractRef.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
                    }

                    int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        clauseContractRef.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
                    }

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = clauseContractRef;
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

        /// <summary>
        /// 获取clausecontract_ref集合
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <returns></returns>
        public override ResultModel Load(UserModel user)
        {
            ResultModel result = new ResultModel();
            try
            {
                DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionStringNFMT, "ClauseContract_RefLoad", null, CommandType.StoredProcedure);

                List<ClauseContractRef> clauseContractRefs = new List<ClauseContractRef>();

                foreach (DataRow dr in dt.Rows)
                {
                    ClauseContractRef clauseContractRef = new ClauseContractRef();
                    clauseContractRef.RefId = Convert.ToInt32(dr["RefId"]);

                    if (dr["MasterId"] != DBNull.Value)
                    {
                        clauseContractRef.MasterId = Convert.ToInt32(dr["MasterId"]);
                    }
                    if (dr["ClauseId"] != DBNull.Value)
                    {
                        clauseContractRef.ClauseId = Convert.ToInt32(dr["ClauseId"]);
                    }
                    if (dr["Sort"] != DBNull.Value)
                    {
                        clauseContractRef.Sort = Convert.ToInt32(dr["Sort"]);
                    }
                    if (dr["IsChose"] != DBNull.Value)
                    {
                        clauseContractRef.IsChose = Convert.ToBoolean(dr["IsChose"]);
                    }
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        clauseContractRef.CreatorId = Convert.ToInt32(dr["CreatorId"]);
                    }
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        clauseContractRef.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    }
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        clauseContractRef.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    }
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        clauseContractRef.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    }
                    clauseContractRefs.Add(clauseContractRef);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = clauseContractRefs;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 更新clausecontract_ref
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">ClauseContract_对象</param>
        /// <returns></returns>
        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                ClauseContractRef clauseContractRef = (ClauseContractRef)obj;

                if (clauseContractRef == null)
                {
                    result.Message = "更新对象不能为null";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
                refidpara.Value = clauseContractRef.RefId;
                paras.Add(refidpara);

                SqlParameter masteridpara = new SqlParameter("@MasterId", SqlDbType.Int, 4);
                masteridpara.Value = clauseContractRef.MasterId;
                paras.Add(masteridpara);

                SqlParameter clauseidpara = new SqlParameter("@ClauseId", SqlDbType.Int, 4);
                clauseidpara.Value = clauseContractRef.ClauseId;
                paras.Add(clauseidpara);

                SqlParameter sortpara = new SqlParameter("@Sort", SqlDbType.Int, 4);
                sortpara.Value = clauseContractRef.Sort;
                paras.Add(sortpara);

                SqlParameter ischosepara = new SqlParameter("@IsChose", SqlDbType.Bit, 1);
                ischosepara.Value = clauseContractRef.IsChose;
                paras.Add(ischosepara);

                SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
                lastmodifyidpara.Value = user.AccountId;
                paras.Add(lastmodifyidpara);


                int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringNFMT, CommandType.StoredProcedure, "ClauseContract_RefUpdate", paras.ToArray());

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

    }
}
