/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：EmpMenuDAL.cs
// 文件功能描述：员工菜单关系表dbo.EmpMenu数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月20日
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
    /// 员工菜单关系表dbo.EmpMenu数据交互类。
    /// </summary>
    public class EmpMenuDAL : DataOperate, IEmpMenuDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public EmpMenuDAL()
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
            EmpMenu empmenu = (EmpMenu)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = empmenu.EmpId;
            paras.Add(empidpara);

            SqlParameter menuidpara = new SqlParameter("@MenuId", SqlDbType.Int, 4);
            menuidpara.Value = empmenu.MenuId;
            paras.Add(menuidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = empmenu.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            EmpMenu empmenu = new EmpMenu();

            empmenu.RefId = Convert.ToInt32(dr["RefId"]);

            if (dr["EmpId"] != DBNull.Value)
            {
                empmenu.EmpId = Convert.ToInt32(dr["EmpId"]);
            }

            if (dr["MenuId"] != DBNull.Value)
            {
                empmenu.MenuId = Convert.ToInt32(dr["MenuId"]);
            }

            if (dr["RefStatus"] != DBNull.Value)
            {
                empmenu.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr["RefStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                empmenu.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                empmenu.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                empmenu.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                empmenu.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return empmenu;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            EmpMenu empmenu = new EmpMenu();

            int indexRefId = dr.GetOrdinal("RefId");
            empmenu.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexEmpId = dr.GetOrdinal("EmpId");
            if (dr["EmpId"] != DBNull.Value)
            {
                empmenu.EmpId = Convert.ToInt32(dr[indexEmpId]);
            }

            int indexMenuId = dr.GetOrdinal("MenuId");
            if (dr["MenuId"] != DBNull.Value)
            {
                empmenu.MenuId = Convert.ToInt32(dr[indexMenuId]);
            }

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                empmenu.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRefStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                empmenu.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                empmenu.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                empmenu.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                empmenu.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return empmenu;
        }

        public override string TableName
        {
            get
            {
                return "EmpMenu";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            EmpMenu empmenu = (EmpMenu)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = empmenu.RefId;
            paras.Add(refidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = empmenu.EmpId;
            paras.Add(empidpara);

            SqlParameter menuidpara = new SqlParameter("@MenuId", SqlDbType.Int, 4);
            menuidpara.Value = empmenu.MenuId;
            paras.Add(menuidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = empmenu.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel InvalidAll(UserModel user, int empId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.EmpMenu set RefStatus = {0} where EmpId = {1} ", (int)Common.StatusEnum.已作废, empId);
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

        public override int MenuId
        {
            get
            {
                return 102;
            }
        }

        #endregion
    }
}
