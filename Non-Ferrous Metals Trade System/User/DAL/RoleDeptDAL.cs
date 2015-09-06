/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RoleDeptDAL.cs
// 文件功能描述：角色部门关联表dbo.RoleDept数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月12日
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
    /// 角色部门关联表dbo.RoleDept数据交互类。
    /// </summary>
    public class RoleDeptDAL : DataOperate, IRoleDeptDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RoleDeptDAL()
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
            RoleDept roledept = (RoleDept)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RoleDeptId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter roleidpara = new SqlParameter("@RoleId", SqlDbType.Int, 4);
            roleidpara.Value = roledept.RoleId;
            paras.Add(roleidpara);

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = roledept.DeptId;
            paras.Add(deptidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = roledept.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            RoleDept roledept = new RoleDept();

            roledept.RoleDeptId = Convert.ToInt32(dr["RoleDeptId"]);

            if (dr["RoleId"] != DBNull.Value)
            {
                roledept.RoleId = Convert.ToInt32(dr["RoleId"]);
            }

            if (dr["DeptId"] != DBNull.Value)
            {
                roledept.DeptId = Convert.ToInt32(dr["DeptId"]);
            }

            if (dr["RefStatus"] != DBNull.Value)
            {
                roledept.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr["RefStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                roledept.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                roledept.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                roledept.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                roledept.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return roledept;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            RoleDept roledept = new RoleDept();

            int indexRoleDeptId = dr.GetOrdinal("RoleDeptId");
            roledept.RoleDeptId = Convert.ToInt32(dr[indexRoleDeptId]);

            int indexRoleId = dr.GetOrdinal("RoleId");
            if (dr["RoleId"] != DBNull.Value)
            {
                roledept.RoleId = Convert.ToInt32(dr[indexRoleId]);
            }

            int indexDeptId = dr.GetOrdinal("DeptId");
            if (dr["DeptId"] != DBNull.Value)
            {
                roledept.DeptId = Convert.ToInt32(dr[indexDeptId]);
            }

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                roledept.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRefStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                roledept.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                roledept.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                roledept.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                roledept.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return roledept;
        }

        public override string TableName
        {
            get
            {
                return "RoleDept";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            RoleDept roledept = (RoleDept)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter roledeptidpara = new SqlParameter("@RoleDeptId", SqlDbType.Int, 4);
            roledeptidpara.Value = roledept.RoleDeptId;
            paras.Add(roledeptidpara);

            SqlParameter roleidpara = new SqlParameter("@RoleId", SqlDbType.Int, 4);
            roleidpara.Value = roledept.RoleId;
            paras.Add(roleidpara);

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = roledept.DeptId;
            paras.Add(deptidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = roledept.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
