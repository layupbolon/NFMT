/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RoleDAL.cs
// 文件功能描述：dbo.Role数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
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
    /// dbo.Role数据交互类。
    /// </summary>
    public class RoleDAL : DataOperate, IRoleDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RoleDAL()
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
            Role role = (Role)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RoleId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter rolenamepara = new SqlParameter("@RoleName", SqlDbType.VarChar, 80);
            rolenamepara.Value = role.RoleName;
            paras.Add(rolenamepara);

            SqlParameter rolestatuspara = new SqlParameter("@RoleStatus", SqlDbType.Int, 4);
            rolestatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(rolestatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Role role = new Role();

            int indexRoleId = dr.GetOrdinal("RoleId");
            role.RoleId = Convert.ToInt32(dr[indexRoleId]);

            int indexRoleName = dr.GetOrdinal("RoleName");
            role.RoleName = Convert.ToString(dr[indexRoleName]);

            int indexRoleStatus = dr.GetOrdinal("RoleStatus");
            if (dr["RoleStatus"] != DBNull.Value)
            {
                role.RoleStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRoleStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            role.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            role.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                role.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                role.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return role;
        }

        public override string TableName
        {
            get
            {
                return "Role";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Role role = (Role)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter roleidpara = new SqlParameter("@RoleId", SqlDbType.Int, 4);
            roleidpara.Value = role.RoleId;
            paras.Add(roleidpara);

            SqlParameter rolenamepara = new SqlParameter("@RoleName", SqlDbType.VarChar, 80);
            rolenamepara.Value = role.RoleName;
            paras.Add(rolenamepara);

            SqlParameter rolestatuspara = new SqlParameter("@RoleStatus", SqlDbType.Int, 4);
            rolestatuspara.Value = role.RoleStatus;
            paras.Add(rolestatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int empId)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = "select r.* from dbo.Role r inner join dbo.EmpRole er on er.RoleId = r.RoleId where er.EmpId=@empId";
                SqlParameter[] paras = new SqlParameter[1];
                paras[0] = new SqlParameter("@empId", empId);

                DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionStringUser, cmdText, paras, CommandType.Text);

                List<Role> roles = new List<Role>();

                foreach (DataRow dr in dt.Rows)
                {
                    Role role = new Role();
                    role.RoleId = Convert.ToInt32(dr["RoleId"]);

                    role.RoleName = Convert.ToString(dr["RoleName"]);

                    if (dr["RoleStatus"] != DBNull.Value)
                    {
                        role.RoleStatus = (Common.StatusEnum)Convert.ToInt32(dr["RoleStatus"]);
                    }
                    role.CreatorId = Convert.ToInt32(dr["CreatorId"]);

                    role.CreateTime = Convert.ToDateTime(dr["CreateTime"]);

                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        role.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    }
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        role.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    }
                    roles.Add(role);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = roles;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        #endregion

        #region 重载方法

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "更新对象不能为null";
                    return result;
                }

                obj.LastModifyId = user.EmpId;

                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, string.Format("{0}Update", obj.TableName), CreateUpdateParameters(obj).ToArray());

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

        public override int MenuId
        {
            get
            {
                return 19;
            }
        }

        #endregion
    }
}
