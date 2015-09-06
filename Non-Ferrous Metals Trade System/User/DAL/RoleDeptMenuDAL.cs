/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RoleDeptMenuDAL.cs
// 文件功能描述：角色部门菜单关联表dbo.RoleDeptMenu数据交互类。
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
    /// 角色部门菜单关联表dbo.RoleDeptMenu数据交互类。
    /// </summary>
    public class RoleDeptMenuDAL : DataOperate, IRoleDeptMenuDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RoleDeptMenuDAL()
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
            RoleDeptMenu roledeptmenu = (RoleDeptMenu)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RoleDeptMenuID";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter roledeptidpara = new SqlParameter("@RoleDeptId", SqlDbType.Int, 4);
            roledeptidpara.Value = roledeptmenu.RoleDeptId;
            paras.Add(roledeptidpara);

            SqlParameter menuidpara = new SqlParameter("@MenuId", SqlDbType.Int, 4);
            menuidpara.Value = roledeptmenu.MenuId;
            paras.Add(menuidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = roledeptmenu.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            RoleDeptMenu roledeptmenu = new RoleDeptMenu();

            roledeptmenu.RoleDeptMenuID = Convert.ToInt32(dr["RoleDeptMenuID"]);

            if (dr["RoleDeptId"] != DBNull.Value)
            {
                roledeptmenu.RoleDeptId = Convert.ToInt32(dr["RoleDeptId"]);
            }

            if (dr["MenuId"] != DBNull.Value)
            {
                roledeptmenu.MenuId = Convert.ToInt32(dr["MenuId"]);
            }

            if (dr["RefStatus"] != DBNull.Value)
            {
                roledeptmenu.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr["RefStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                roledeptmenu.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                roledeptmenu.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                roledeptmenu.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                roledeptmenu.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return roledeptmenu;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            RoleDeptMenu roledeptmenu = new RoleDeptMenu();

            int indexRoleDeptMenuID = dr.GetOrdinal("RoleDeptMenuID");
            roledeptmenu.RoleDeptMenuID = Convert.ToInt32(dr[indexRoleDeptMenuID]);

            int indexRoleDeptId = dr.GetOrdinal("RoleDeptId");
            if (dr["RoleDeptId"] != DBNull.Value)
            {
                roledeptmenu.RoleDeptId = Convert.ToInt32(dr[indexRoleDeptId]);
            }

            int indexMenuId = dr.GetOrdinal("MenuId");
            if (dr["MenuId"] != DBNull.Value)
            {
                roledeptmenu.MenuId = Convert.ToInt32(dr[indexMenuId]);
            }

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                roledeptmenu.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRefStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                roledeptmenu.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                roledeptmenu.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                roledeptmenu.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                roledeptmenu.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return roledeptmenu;
        }

        public override string TableName
        {
            get
            {
                return "RoleDeptMenu";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            RoleDeptMenu roledeptmenu = (RoleDeptMenu)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter roledeptmenuidpara = new SqlParameter("@RoleDeptMenuID", SqlDbType.Int, 4);
            roledeptmenuidpara.Value = roledeptmenu.RoleDeptMenuID;
            paras.Add(roledeptmenuidpara);

            SqlParameter roledeptidpara = new SqlParameter("@RoleDeptId", SqlDbType.Int, 4);
            roledeptidpara.Value = roledeptmenu.RoleDeptId;
            paras.Add(roledeptidpara);

            SqlParameter menuidpara = new SqlParameter("@MenuId", SqlDbType.Int, 4);
            menuidpara.Value = roledeptmenu.MenuId;
            paras.Add(menuidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = roledeptmenu.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
