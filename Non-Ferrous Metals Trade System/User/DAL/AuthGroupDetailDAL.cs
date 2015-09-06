/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AuthGroupDetailDAL.cs
// 文件功能描述：员工权限组关联表dbo.AuthGroupDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月18日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.User.Model;
using NFMT.DBUtility;
using NFMT.User.IDAL;
using NFMT.Common;

namespace NFMT.User.DAL
{
    /// <summary>
    /// 员工权限组关联表dbo.AuthGroupDetail数据交互类。
    /// </summary>
    public class AuthGroupDetailDAL : DataOperate, IAuthGroupDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AuthGroupDetailDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringUser;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            AuthGroupDetail authgroupdetail = (AuthGroupDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter authgroupidpara = new SqlParameter("@AuthGroupId", SqlDbType.Int, 4);
            authgroupidpara.Value = authgroupdetail.AuthGroupId;
            paras.Add(authgroupidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = authgroupdetail.EmpId;
            paras.Add(empidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = authgroupdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            AuthGroupDetail authgroupdetail = new AuthGroupDetail();

            authgroupdetail.DetailId = Convert.ToInt32(dr["DetailId"]);

            if (dr["AuthGroupId"] != DBNull.Value)
            {
                authgroupdetail.AuthGroupId = Convert.ToInt32(dr["AuthGroupId"]);
            }

            if (dr["EmpId"] != DBNull.Value)
            {
                authgroupdetail.EmpId = Convert.ToInt32(dr["EmpId"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                authgroupdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                authgroupdetail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                authgroupdetail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                authgroupdetail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                authgroupdetail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return authgroupdetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            AuthGroupDetail authgroupdetail = new AuthGroupDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            authgroupdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexAuthGroupId = dr.GetOrdinal("AuthGroupId");
            if (dr["AuthGroupId"] != DBNull.Value)
            {
                authgroupdetail.AuthGroupId = Convert.ToInt32(dr[indexAuthGroupId]);
            }

            int indexEmpId = dr.GetOrdinal("EmpId");
            if (dr["EmpId"] != DBNull.Value)
            {
                authgroupdetail.EmpId = Convert.ToInt32(dr[indexEmpId]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                authgroupdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                authgroupdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                authgroupdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                authgroupdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                authgroupdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return authgroupdetail;
        }

        public override string TableName
        {
            get
            {
                return "AuthGroupDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            AuthGroupDetail authgroupdetail = (AuthGroupDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = authgroupdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter authgroupidpara = new SqlParameter("@AuthGroupId", SqlDbType.Int, 4);
            authgroupidpara.Value = authgroupdetail.AuthGroupId;
            paras.Add(authgroupidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = authgroupdetail.EmpId;
            paras.Add(empidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = authgroupdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel UpdateStauts(UserModel user, int detailId, Common.StatusEnum status)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.AuthGroupDetail set DetailStatus = {0} where DetailId = {1} ", (int)status, detailId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "操作成功";
                    result.AffectCount = i;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "操作失败";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            return result;
        }

        #endregion
    }
}
