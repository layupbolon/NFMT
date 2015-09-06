/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DeptEmpDAL.cs
// 文件功能描述：员工部门表dbo.DeptEmp数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月11日
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
    /// 员工部门表dbo.DeptEmp数据交互类。
    /// </summary>
    public class DeptEmpDAL : ApplyOperate, IDeptEmpDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeptEmpDAL()
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
            DeptEmp deptemp = (DeptEmp)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DeptEmpId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = deptemp.DeptId;
            paras.Add(deptidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = deptemp.EmpId;
            paras.Add(empidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = deptemp.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            DeptEmp deptemp = new DeptEmp();

            int indexDeptEmpId = dr.GetOrdinal("DeptEmpId");
            deptemp.DeptEmpId = Convert.ToInt32(dr[indexDeptEmpId]);

            int indexDeptId = dr.GetOrdinal("DeptId");
            deptemp.DeptId = Convert.ToInt32(dr[indexDeptId]);

            int indexEmpId = dr.GetOrdinal("EmpId");
            deptemp.EmpId = Convert.ToInt32(dr[indexEmpId]);

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                deptemp.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRefStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                deptemp.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                deptemp.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                deptemp.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                deptemp.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return deptemp;
        }

        public override string TableName
        {
            get
            {
                return "DeptEmp";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            DeptEmp deptemp = (DeptEmp)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter deptempidpara = new SqlParameter("@DeptEmpId", SqlDbType.Int, 4);
            deptempidpara.Value = deptemp.DeptEmpId;
            paras.Add(deptempidpara);

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = deptemp.DeptId;
            paras.Add(deptidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = deptemp.EmpId;
            paras.Add(empidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = deptemp.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 重载方法

        public override ResultModel Invalid(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "作废对象不能为null";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter idPara = new SqlParameter("@id", SqlDbType.Int, 4);
                idPara.Value = obj.Id;
                paras.Add(idPara);

                SqlParameter statusPara = new SqlParameter("@status", SqlDbType.Int, 4);
                statusPara.Value = (int)StatusEnum.已作废;
                paras.Add(statusPara);

                SqlParameter lastModifyIdPara = new SqlParameter("@lastModifyId", SqlDbType.Int, 4);
                lastModifyIdPara.Value = user.AccountId;
                paras.Add(lastModifyIdPara);

                int i = SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.StoredProcedure, string.Format("{0}UpdateStatus", obj.TableName), paras.ToArray());

                if (i == 1)
                {
                    result.Message = "作废成功";
                    result.ResultStatus = 0;
                }
                else
                    result.Message = "作废失败";

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
                return 80;
            }
        }

        #endregion

        public ResultModel Complete(UserModel user, IModel obj)
        {
            throw new NotImplementedException();
        }

    }
}
