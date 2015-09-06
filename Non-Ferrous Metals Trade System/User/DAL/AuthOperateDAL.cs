/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AuthOperateDAL.cs
// 文件功能描述：操作权限表dbo.AuthOperate数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
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
    /// 操作权限表dbo.AuthOperate数据交互类。
    /// </summary>
    public class AuthOperateDAL : DataOperate, IAuthOperateDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AuthOperateDAL()
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
            AuthOperate authoperate = (AuthOperate)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@AuthOperateId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(authoperate.OperateCode))
            {
                SqlParameter operatecodepara = new SqlParameter("@OperateCode", SqlDbType.VarChar, 50);
                operatecodepara.Value = authoperate.OperateCode;
                paras.Add(operatecodepara);
            }

            if (!string.IsNullOrEmpty(authoperate.OperateName))
            {
                SqlParameter operatenamepara = new SqlParameter("@OperateName", SqlDbType.VarChar, 50);
                operatenamepara.Value = authoperate.OperateName;
                paras.Add(operatenamepara);
            }

            SqlParameter operatetypepara = new SqlParameter("@OperateType", SqlDbType.Int, 4);
            operatetypepara.Value = authoperate.OperateType;
            paras.Add(operatetypepara);

            SqlParameter menuidpara = new SqlParameter("@MenuId", SqlDbType.Int, 4);
            menuidpara.Value = authoperate.MenuId;
            paras.Add(menuidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = authoperate.EmpId;
            paras.Add(empidpara);

            SqlParameter authoperatestatuspara = new SqlParameter("@AuthOperateStatus", SqlDbType.Int, 4);
            authoperatestatuspara.Value = authoperate.AuthOperateStatus;
            paras.Add(authoperatestatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            AuthOperate authoperate = new AuthOperate();

            authoperate.AuthOperateId = Convert.ToInt32(dr["AuthOperateId"]);

            if (dr["OperateCode"] != DBNull.Value)
            {
                authoperate.OperateCode = Convert.ToString(dr["OperateCode"]);
            }

            if (dr["OperateName"] != DBNull.Value)
            {
                authoperate.OperateName = Convert.ToString(dr["OperateName"]);
            }

            if (dr["OperateType"] != DBNull.Value)
            {
                authoperate.OperateType = Convert.ToInt32(dr["OperateType"]);
            }

            if (dr["MenuId"] != DBNull.Value)
            {
                authoperate.MenuId = Convert.ToInt32(dr["MenuId"]);
            }

            if (dr["EmpId"] != DBNull.Value)
            {
                authoperate.EmpId = Convert.ToInt32(dr["EmpId"]);
            }

            if (dr["AuthOperateStatus"] != DBNull.Value)
            {
                authoperate.AuthOperateStatus = (Common.StatusEnum)Convert.ToInt32(dr["AuthOperateStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                authoperate.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                authoperate.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                authoperate.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                authoperate.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return authoperate;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            AuthOperate authoperate = new AuthOperate();

            int indexAuthOperateId = dr.GetOrdinal("AuthOperateId");
            authoperate.AuthOperateId = Convert.ToInt32(dr[indexAuthOperateId]);

            int indexOperateCode = dr.GetOrdinal("OperateCode");
            if (dr["OperateCode"] != DBNull.Value)
            {
                authoperate.OperateCode = Convert.ToString(dr[indexOperateCode]);
            }

            int indexOperateName = dr.GetOrdinal("OperateName");
            if (dr["OperateName"] != DBNull.Value)
            {
                authoperate.OperateName = Convert.ToString(dr[indexOperateName]);
            }

            int indexOperateType = dr.GetOrdinal("OperateType");
            if (dr["OperateType"] != DBNull.Value)
            {
                authoperate.OperateType = Convert.ToInt32(dr[indexOperateType]);
            }

            int indexMenuId = dr.GetOrdinal("MenuId");
            if (dr["MenuId"] != DBNull.Value)
            {
                authoperate.MenuId = Convert.ToInt32(dr[indexMenuId]);
            }

            int indexEmpId = dr.GetOrdinal("EmpId");
            if (dr["EmpId"] != DBNull.Value)
            {
                authoperate.EmpId = Convert.ToInt32(dr[indexEmpId]);
            }

            int indexAuthOperateStatus = dr.GetOrdinal("AuthOperateStatus");
            if (dr["AuthOperateStatus"] != DBNull.Value)
            {
                authoperate.AuthOperateStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexAuthOperateStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                authoperate.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                authoperate.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                authoperate.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                authoperate.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return authoperate;
        }

        public override string TableName
        {
            get
            {
                return "AuthOperate";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            AuthOperate authoperate = (AuthOperate)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter authoperateidpara = new SqlParameter("@AuthOperateId", SqlDbType.Int, 4);
            authoperateidpara.Value = authoperate.AuthOperateId;
            paras.Add(authoperateidpara);

            if (!string.IsNullOrEmpty(authoperate.OperateCode))
            {
                SqlParameter operatecodepara = new SqlParameter("@OperateCode", SqlDbType.VarChar, 50);
                operatecodepara.Value = authoperate.OperateCode;
                paras.Add(operatecodepara);
            }

            if (!string.IsNullOrEmpty(authoperate.OperateName))
            {
                SqlParameter operatenamepara = new SqlParameter("@OperateName", SqlDbType.VarChar, 50);
                operatenamepara.Value = authoperate.OperateName;
                paras.Add(operatenamepara);
            }

            SqlParameter operatetypepara = new SqlParameter("@OperateType", SqlDbType.Int, 4);
            operatetypepara.Value = authoperate.OperateType;
            paras.Add(operatetypepara);

            SqlParameter menuidpara = new SqlParameter("@MenuId", SqlDbType.Int, 4);
            menuidpara.Value = authoperate.MenuId;
            paras.Add(menuidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = authoperate.EmpId;
            paras.Add(empidpara);

            SqlParameter authoperatestatuspara = new SqlParameter("@AuthOperateStatus", SqlDbType.Int, 4);
            authoperatestatuspara.Value = authoperate.AuthOperateStatus;
            paras.Add(authoperatestatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel InvalidAll(UserModel user, int empId, string exceptItemIds)
        {
            ResultModel result = new ResultModel();

            try
            {
                string exceptMenuId = string.Format(" and MenuId not in ({0})", string.Format("select MenuId from dbo.Menu where MenuId in ({0}) and MenuStatus <> {1} union select MenuId from dbo.Menu where ParentId in ({0}) and MenuStatus <> {1}", exceptItemIds, (int)Common.StatusEnum.已作废));


                string sql = string.Format("update dbo.AuthOperate set AuthOperateStatus = {0} where EmpId = {1} ", (int)Common.StatusEnum.已作废, empId);
                if (!string.IsNullOrEmpty(exceptItemIds))
                    sql += exceptMenuId;

                sql += string.Format(" update dbo.EmpMenu set RefStatus = {0} where EmpId = {1}", (int)Common.StatusEnum.已作废, empId);
                if (!string.IsNullOrEmpty(exceptItemIds))
                    sql += exceptMenuId;

                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                }
                else
                {
                    result.ResultStatus = 0;
                    result.Message = "作废失败或无数据";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel JudgeOperate(UserModel user, int menuId, string operateTypes)
        {
            ResultModel result = new ResultModel();

            try
            {
                int status = (int)Common.StatusEnum.已生效;

                string sql = string.Format("select 1 from dbo.AuthOperate where MenuId = {0} and EmpId = {1} and AuthOperateStatus = {2} and OperateType in ({3}) ", menuId, user.EmpId, status, operateTypes);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                int i;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && int.TryParse(obj.ToString(), out i))
                {
                    result.ResultStatus = 0;
                    result.Message = "用户在该页面拥有权限";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "用户在该页面不拥有权限";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel GetMenuList(UserModel user, int empId, string menuIds)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select m.MenuId as id,m.MenuName as label,case when ISNULL(em.RefId,0) = 0 then 'false' else 'true' end as checked,m.ParentId from dbo.Menu m left join dbo.EmpMenu em on m.MenuId = em.MenuId and em.RefStatus = {0} and em.EmpId = {1} where m.ParentId = 0 and m.MenuId in ({3}) and m.MenuStatus = {0} union select m.MenuId as id,m.MenuName as label,case when a.[count] = (select count(1) from NFMT_Basic..BDStyleDetail where BDStyleId = {2} and DetailStatus = {0}) then 'true'  else 'false' end as checked,m.ParentId from dbo.Menu m left join (select COUNT(1) [count],MenuId from dbo.AuthOperate ao where ao.EmpId = {1} and ao.AuthOperateStatus = {0} group by MenuId) a on m.MenuId = a.MenuId where m.ParentId <> 0 and m.ParentId in ({3}) and m.MenuStatus = {0} ", (int)Common.StatusEnum.已生效, empId, (int)NFMT.Data.StyleEnum.OperateType, menuIds);

                //when a.[count] >0 and a.[count] < (select count(1) from NFMT_Basic..BDStyleDetail where BDStyleId = {2} and DetailStatus = {0}) then 'indeterminate'
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = dt;
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

        public ResultModel GetOperate(UserModel user, int empId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select * from dbo.AuthOperate where AuthOperateStatus = {0} and EmpId = {1}", (int)Common.StatusEnum.已生效, empId);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                }
                else
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = new DataTable();
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        public ResultModel GetUserMenuOperate(UserModel user, int menuId)
        {
            string sql =
                string.Format(
                    "select * from dbo.AuthOperate where EmpId = {0} and MenuId = {1} and AuthOperateStatus = {2}",
                    user.EmpId, menuId, (int) StatusEnum.已生效);
            return Load<AuthOperate>(user, CommandType.Text, sql);
        }

        #endregion
    }
}
