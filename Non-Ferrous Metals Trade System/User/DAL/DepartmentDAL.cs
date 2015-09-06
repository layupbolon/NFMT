/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DepartmentDAL.cs
// 文件功能描述：部门dbo.Department数据交互类。
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
    /// 部门dbo.Department数据交互类。
    /// </summary>
    public class DepartmentDAL : DataOperate, IDepartmentDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DepartmentDAL()
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
            Department department = (Department)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DeptId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = department.CorpId;
            paras.Add(corpidpara);

            if (!string.IsNullOrEmpty(department.DeptCode))
            {
                SqlParameter deptcodepara = new SqlParameter("@DeptCode", SqlDbType.VarChar, 80);
                deptcodepara.Value = department.DeptCode;
                paras.Add(deptcodepara);
            }

            SqlParameter deptnamepara = new SqlParameter("@DeptName", SqlDbType.VarChar, 80);
            deptnamepara.Value = department.DeptName;
            paras.Add(deptnamepara);

            if (!string.IsNullOrEmpty(department.DeptFullName))
            {
                SqlParameter deptfullnamepara = new SqlParameter("@DeptFullName", SqlDbType.VarChar, 80);
                deptfullnamepara.Value = department.DeptFullName;
                paras.Add(deptfullnamepara);
            }

            if (!string.IsNullOrEmpty(department.DeptShort))
            {
                SqlParameter deptshortpara = new SqlParameter("@DeptShort", SqlDbType.VarChar, 80);
                deptshortpara.Value = department.DeptShort;
                paras.Add(deptshortpara);
            }

            SqlParameter depttypepara = new SqlParameter("@DeptType", SqlDbType.Int, 4);
            depttypepara.Value = department.DeptType;
            paras.Add(depttypepara);

            SqlParameter parentlevepara = new SqlParameter("@ParentLeve", SqlDbType.Int, 4);
            parentlevepara.Value = department.ParentLeve;
            paras.Add(parentlevepara);

            SqlParameter deptstatuspara = new SqlParameter("@DeptStatus", SqlDbType.Int, 4);
            deptstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(deptstatuspara);

            SqlParameter deptlevelpara = new SqlParameter("@DeptLevel", SqlDbType.Int, 4);
            deptlevelpara.Value = department.DeptLevel;
            paras.Add(deptlevelpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Department department = new Department();

            int indexDeptId = dr.GetOrdinal("DeptId");
            department.DeptId = Convert.ToInt32(dr[indexDeptId]);

            int indexCorpId = dr.GetOrdinal("CorpId");
            department.CorpId = Convert.ToInt32(dr[indexCorpId]);

            int indexDeptCode = dr.GetOrdinal("DeptCode");
            if (dr["DeptCode"] != DBNull.Value)
            {
                department.DeptCode = Convert.ToString(dr[indexDeptCode]);
            }

            int indexDeptName = dr.GetOrdinal("DeptName");
            department.DeptName = Convert.ToString(dr[indexDeptName]);

            int indexDeptFullName = dr.GetOrdinal("DeptFullName");
            if (dr["DeptFullName"] != DBNull.Value)
            {
                department.DeptFullName = Convert.ToString(dr[indexDeptFullName]);
            }

            int indexDeptShort = dr.GetOrdinal("DeptShort");
            if (dr["DeptShort"] != DBNull.Value)
            {
                department.DeptShort = Convert.ToString(dr[indexDeptShort]);
            }

            int indexDeptType = dr.GetOrdinal("DeptType");
            if (dr["DeptType"] != DBNull.Value)
            {
                department.DeptType = Convert.ToInt32(dr[indexDeptType]);
            }

            int indexParentLeve = dr.GetOrdinal("ParentLeve");
            if (dr["ParentLeve"] != DBNull.Value)
            {
                department.ParentLeve = Convert.ToInt32(dr[indexParentLeve]);
            }

            int indexDeptStatus = dr.GetOrdinal("DeptStatus");
            department.DeptStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDeptStatus]);

            int indexDeptLevel = dr.GetOrdinal("DeptLevel");
            if (dr["DeptLevel"] != DBNull.Value)
            {
                department.DeptLevel = Convert.ToInt32(dr[indexDeptLevel]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            department.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            department.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                department.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                department.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return department;
        }

        public override string TableName
        {
            get
            {
                return "Department";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Department department = (Department)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = department.DeptId;
            paras.Add(deptidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = department.CorpId;
            paras.Add(corpidpara);

            if (!string.IsNullOrEmpty(department.DeptCode))
            {
                SqlParameter deptcodepara = new SqlParameter("@DeptCode", SqlDbType.VarChar, 80);
                deptcodepara.Value = department.DeptCode;
                paras.Add(deptcodepara);
            }

            SqlParameter deptnamepara = new SqlParameter("@DeptName", SqlDbType.VarChar, 80);
            deptnamepara.Value = department.DeptName;
            paras.Add(deptnamepara);

            if (!string.IsNullOrEmpty(department.DeptFullName))
            {
                SqlParameter deptfullnamepara = new SqlParameter("@DeptFullName", SqlDbType.VarChar, 80);
                deptfullnamepara.Value = department.DeptFullName;
                paras.Add(deptfullnamepara);
            }

            if (!string.IsNullOrEmpty(department.DeptShort))
            {
                SqlParameter deptshortpara = new SqlParameter("@DeptShort", SqlDbType.VarChar, 80);
                deptshortpara.Value = department.DeptShort;
                paras.Add(deptshortpara);
            }

            SqlParameter depttypepara = new SqlParameter("@DeptType", SqlDbType.Int, 4);
            depttypepara.Value = department.DeptType;
            paras.Add(depttypepara);

            SqlParameter parentlevepara = new SqlParameter("@ParentLeve", SqlDbType.Int, 4);
            parentlevepara.Value = department.ParentLeve;
            paras.Add(parentlevepara);

            SqlParameter deptstatuspara = new SqlParameter("@DeptStatus", SqlDbType.Int, 4);
            deptstatuspara.Value = department.DeptStatus;
            paras.Add(deptstatuspara);

            SqlParameter deptlevelpara = new SqlParameter("@DeptLevel", SqlDbType.Int, 4);
            deptlevelpara.Value = department.DeptLevel;
            paras.Add(deptlevelpara);

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
                string cmdText = "select d.* from dbo.Department d inner join dbo.DeptEmp de on de.EmpId= @empId";

                SqlParameter[] paras = new SqlParameter[1];
                paras[0] = new SqlParameter("@empId", empId);

                DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionStringUser, cmdText, paras, CommandType.Text);

                List<Department> departments = new List<Department>();

                foreach (DataRow dr in dt.Rows)
                {
                    Department department = new Department();
                    department.DeptId = Convert.ToInt32(dr["DeptId"]);

                    if (dr["CorpId"] != DBNull.Value)
                    {
                        department.CorpId = Convert.ToInt32(dr["CorpId"]);
                    }
                    if (dr["DeptCode"] != DBNull.Value)
                    {
                        department.DeptCode = Convert.ToString(dr["DeptCode"]);
                    }
                    if (dr["DeptName"] != DBNull.Value)
                    {
                        department.DeptName = Convert.ToString(dr["DeptName"]);
                    }
                    if (dr["DeptFullName"] != DBNull.Value)
                    {
                        department.DeptFullName = Convert.ToString(dr["DeptFullName"]);
                    }
                    if (dr["DeptShort"] != DBNull.Value)
                    {
                        department.DeptShort = Convert.ToString(dr["DeptShort"]);
                    }
                    if (dr["DeptType"] != DBNull.Value)
                    {
                        department.DeptType = Convert.ToInt32(dr["DeptType"]);
                    }
                    if (dr["ParentLeve"] != DBNull.Value)
                    {
                        department.ParentLeve = Convert.ToInt32(dr["ParentLeve"]);
                    }
                    if (dr["DeptStatus"] != DBNull.Value)
                    {
                        department.DeptStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr["DeptStatus"].ToString());
                    }
                    if (dr["DeptLevel"] != DBNull.Value)
                    {
                        department.DeptLevel = Convert.ToInt32(dr["DeptLevel"]);
                    }
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        department.CreatorId = Convert.ToInt32(dr["CreatorId"]);
                    }
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        department.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    }
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        department.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    }
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        department.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    }
                    departments.Add(department);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = departments;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel GetDeptList(UserModel user, int coprId)
        {
            ResultModel result = new ResultModel();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                sb.Append("select DeptId,DeptName from NFMT_User.dbo.Department");
                sb.AppendFormat(" where DeptStatus = {0} ", (int)Common.StatusEnum.已生效);

                if (coprId > 0)
                    sb.AppendFormat(" and CorpId = {0}", coprId);

                DataTable dt = SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, CommandType.Text);

                if (dt != null && dt.Rows.Count > 0)
                {
                    result.AffectCount = dt.Rows.Count;
                    result.Message = "获取部门列表成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                }
                else
                {
                    result.Message = "获取部门列表失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = string.Format("获取部门列表失败,{0}", e.Message);
                result.ResultStatus = -1;
            }

            return result;
        }

        public override int MenuId
        {
            get
            {
                return 17;
            }
        }

        #endregion
    }
}
