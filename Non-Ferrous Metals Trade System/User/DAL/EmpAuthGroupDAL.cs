/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：EmpAuthGroupDAL.cs
// 文件功能描述：dbo.EmpAuthGroup数据交互类。
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
    /// dbo.EmpAuthGroup数据交互类。
    /// </summary>
    public class EmpAuthGroupDAL : DataOperate, IEmpAuthGroupDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public EmpAuthGroupDAL()
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
            EmpAuthGroup empauthgroup = (EmpAuthGroup)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@EmpAuthGroupId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter authgroupidpara = new SqlParameter("@AuthGroupId", SqlDbType.Int, 4);
            authgroupidpara.Value = empauthgroup.AuthGroupId;
            paras.Add(authgroupidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = empauthgroup.EmpId;
            paras.Add(empidpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            EmpAuthGroup empauthgroup = new EmpAuthGroup();

            int indexEmpAuthGroupId = dr.GetOrdinal("EmpAuthGroupId");
            empauthgroup.EmpAuthGroupId = Convert.ToInt32(dr[indexEmpAuthGroupId]);

            int indexAuthGroupId = dr.GetOrdinal("AuthGroupId");
            empauthgroup.AuthGroupId = Convert.ToInt32(dr[indexAuthGroupId]);

            int indexEmpId = dr.GetOrdinal("EmpId");
            empauthgroup.EmpId = Convert.ToInt32(dr[indexEmpId]);

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                empauthgroup.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                empauthgroup.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                empauthgroup.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                empauthgroup.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return empauthgroup;
        }

        public override string TableName
        {
            get
            {
                return "EmpAuthGroup";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            EmpAuthGroup empauthgroup = (EmpAuthGroup)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter empauthgroupidpara = new SqlParameter("@EmpAuthGroupId", SqlDbType.Int, 4);
            empauthgroupidpara.Value = empauthgroup.EmpAuthGroupId;
            paras.Add(empauthgroupidpara);

            SqlParameter authgroupidpara = new SqlParameter("@AuthGroupId", SqlDbType.Int, 4);
            authgroupidpara.Value = empauthgroup.AuthGroupId;
            paras.Add(authgroupidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = empauthgroup.EmpId;
            paras.Add(empidpara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public override int MenuId
        {
            get
            {
                return 100;
            }
        }

        #endregion
    }
}
