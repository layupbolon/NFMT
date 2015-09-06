/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：EmpRoleDAL.cs
// 文件功能描述：员工角色关联表dbo.EmpRole数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
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
    /// 员工角色关联表dbo.EmpRole数据交互类。
    /// </summary>
    public class EmpRoleDAL : ApplyOperate, IEmpRoleDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public EmpRoleDAL()
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
            EmpRole emprole = (EmpRole)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@EmpRoleId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = emprole.EmpId;
            paras.Add(empidpara);

            SqlParameter roleidpara = new SqlParameter("@RoleId", SqlDbType.Int, 4);
            roleidpara.Value = emprole.RoleId;
            paras.Add(roleidpara);

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
            EmpRole emprole = new EmpRole();

            int indexEmpRoleId = dr.GetOrdinal("EmpRoleId");
            emprole.EmpRoleId = Convert.ToInt32(dr[indexEmpRoleId]);

            int indexEmpId = dr.GetOrdinal("EmpId");
            emprole.EmpId = Convert.ToInt32(dr[indexEmpId]);

            int indexRoleId = dr.GetOrdinal("RoleId");
            emprole.RoleId = Convert.ToInt32(dr[indexRoleId]);

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                emprole.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRefStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            emprole.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            emprole.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                emprole.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                emprole.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return emprole;
        }

        public override string TableName
        {
            get
            {
                return "EmpRole";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            EmpRole emprole = (EmpRole)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter emproleidpara = new SqlParameter("@EmpRoleId", SqlDbType.Int, 4);
            emproleidpara.Value = emprole.EmpRoleId;
            paras.Add(emproleidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = emprole.EmpId;
            paras.Add(empidpara);

            SqlParameter roleidpara = new SqlParameter("@RoleId", SqlDbType.Int, 4);
            roleidpara.Value = emprole.RoleId;
            paras.Add(roleidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = emprole.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetEmpIdsByRoleId(UserModel user, int roleId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select EmpId from dbo.EmpRole where RoleId = {0} and RefStatus >= {1} ", roleId, (int)Common.StatusEnum.已生效);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.Message = "获取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                }
                else
                {
                    result.Message = "获取失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = string.Format("获取失败,{0}", e.Message);
                result.ResultStatus = -1;
            }

            return result;
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
                return 83;
            }
        }

        #endregion
    }
}
