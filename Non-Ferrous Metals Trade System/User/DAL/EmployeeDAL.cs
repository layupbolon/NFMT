/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：EmployeeDAL.cs
// 文件功能描述：员工表dbo.Employee数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
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
    /// 员工表dbo.Employee数据交互类。
    /// </summary>
    public class EmployeeDAL : DataOperate, IEmployeeDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public EmployeeDAL()
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
            Employee employee = (Employee)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@EmpId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = employee.DeptId;
            paras.Add(deptidpara);

            if (!string.IsNullOrEmpty(employee.EmpCode))
            {
                SqlParameter empcodepara = new SqlParameter("@EmpCode", SqlDbType.VarChar, 20);
                empcodepara.Value = employee.EmpCode;
                paras.Add(empcodepara);
            }

            if (!string.IsNullOrEmpty(employee.Name))
            {
                SqlParameter namepara = new SqlParameter("@Name", SqlDbType.VarChar, 20);
                namepara.Value = employee.Name;
                paras.Add(namepara);
            }

            SqlParameter sexpara = new SqlParameter("@Sex", SqlDbType.Bit, 1);
            sexpara.Value = employee.Sex;
            paras.Add(sexpara);

            SqlParameter birthdaypara = new SqlParameter("@BirthDay", SqlDbType.DateTime, 8);
            birthdaypara.Value = employee.BirthDay;
            paras.Add(birthdaypara);

            if (!string.IsNullOrEmpty(employee.Telephone))
            {
                SqlParameter telephonepara = new SqlParameter("@Telephone", SqlDbType.VarChar, 20);
                telephonepara.Value = employee.Telephone;
                paras.Add(telephonepara);
            }

            if (!string.IsNullOrEmpty(employee.Phone))
            {
                SqlParameter phonepara = new SqlParameter("@Phone", SqlDbType.VarChar, 20);
                phonepara.Value = employee.Phone;
                paras.Add(phonepara);
            }

            SqlParameter workstatuspara = new SqlParameter("@WorkStatus", SqlDbType.Int, 4);
            workstatuspara.Value = employee.WorkStatus;
            paras.Add(workstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Employee employee = new Employee();

            int indexEmpId = dr.GetOrdinal("EmpId");
            employee.EmpId = Convert.ToInt32(dr[indexEmpId]);

            int indexDeptId = dr.GetOrdinal("DeptId");
            employee.DeptId = Convert.ToInt32(dr[indexDeptId]);

            int indexEmpCode = dr.GetOrdinal("EmpCode");
            if (dr["EmpCode"] != DBNull.Value)
            {
                employee.EmpCode = Convert.ToString(dr[indexEmpCode]);
            }

            int indexName = dr.GetOrdinal("Name");
            if (dr["Name"] != DBNull.Value)
            {
                employee.Name = Convert.ToString(dr[indexName]);
            }

            int indexSex = dr.GetOrdinal("Sex");
            if (dr["Sex"] != DBNull.Value)
            {
                employee.Sex = Convert.ToBoolean(dr[indexSex]);
            }

            int indexBirthDay = dr.GetOrdinal("BirthDay");
            if (dr["BirthDay"] != DBNull.Value)
            {
                employee.BirthDay = Convert.ToDateTime(dr[indexBirthDay]);
            }

            int indexTelephone = dr.GetOrdinal("Telephone");
            if (dr["Telephone"] != DBNull.Value)
            {
                employee.Telephone = Convert.ToString(dr[indexTelephone]);
            }

            int indexPhone = dr.GetOrdinal("Phone");
            if (dr["Phone"] != DBNull.Value)
            {
                employee.Phone = Convert.ToString(dr[indexPhone]);
            }

            int indexWorkStatus = dr.GetOrdinal("WorkStatus");
            if (dr["WorkStatus"] != DBNull.Value)
            {
                employee.WorkStatus = Convert.ToInt32(dr[indexWorkStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                employee.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                employee.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                employee.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                employee.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return employee;
        }

        public override string TableName
        {
            get
            {
                return "Employee";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Employee employee = (Employee)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = employee.EmpId;
            paras.Add(empidpara);

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = employee.DeptId;
            paras.Add(deptidpara);

            if (!string.IsNullOrEmpty(employee.EmpCode))
            {
                SqlParameter empcodepara = new SqlParameter("@EmpCode", SqlDbType.VarChar, 20);
                empcodepara.Value = employee.EmpCode;
                paras.Add(empcodepara);
            }

            if (!string.IsNullOrEmpty(employee.Name))
            {
                SqlParameter namepara = new SqlParameter("@Name", SqlDbType.VarChar, 20);
                namepara.Value = employee.Name;
                paras.Add(namepara);
            }

            SqlParameter sexpara = new SqlParameter("@Sex", SqlDbType.Bit, 1);
            sexpara.Value = employee.Sex;
            paras.Add(sexpara);

            SqlParameter birthdaypara = new SqlParameter("@BirthDay", SqlDbType.DateTime, 8);
            birthdaypara.Value = employee.BirthDay;
            paras.Add(birthdaypara);

            if (!string.IsNullOrEmpty(employee.Telephone))
            {
                SqlParameter telephonepara = new SqlParameter("@Telephone", SqlDbType.VarChar, 20);
                telephonepara.Value = employee.Telephone;
                paras.Add(telephonepara);
            }

            if (!string.IsNullOrEmpty(employee.Phone))
            {
                SqlParameter phonepara = new SqlParameter("@Phone", SqlDbType.VarChar, 20);
                phonepara.Value = employee.Phone;
                paras.Add(phonepara);
            }

            SqlParameter workstatuspara = new SqlParameter("@WorkStatus", SqlDbType.Int, 4);
            workstatuspara.Value = employee.WorkStatus;
            paras.Add(workstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetWorkStatusList(UserModel user)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select * from NFMT_Basic.dbo.BDStatusDetail where StatusId = {0}", (int)Common.StatusTypeEnum.在职状态);

                DataTable dt = SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);

                if (dt != null && dt.Rows.Count > 0)
                {
                    result.Message = "获取员工状态成功";
                    result.AffectCount = dt.Rows.Count;
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                }
                else
                {
                    result.Message = "获取员工状态失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = string.Format("获取员工状态失败,{0}", e.Message);
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel IsEmpCodeRepeat(UserModel user, string empCode)
        {
            ResultModel result = new ResultModel();

            if (string.IsNullOrEmpty(empCode))
            {
                result.Message = "员工号为空";
                result.ResultStatus = -1;
                return result;
            }

            try
            {
                string sql = string.Format("select COUNT(1) from dbo.Employee where EmpCode = '{0}' ", empCode);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                int count = 0;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && int.TryParse(obj.ToString(), out count) && count == 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "检查成功,无重复员工号";
                    result.ReturnValue = count;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "员工号重复";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
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
                return 18;
            }
        }

        #endregion
    }
}
