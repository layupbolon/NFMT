/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RoleMenuDAL.cs
// 文件功能描述：角色菜单关联表dbo.RoleMenu数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
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
    /// 角色菜单关联表dbo.RoleMenu数据交互类。
    /// </summary>
    public class RoleMenuDAL : DataOperate, IRoleMenuDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RoleMenuDAL()
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
            RoleMenu rolemenu = (RoleMenu)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RoleMenuID";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter roleidpara = new SqlParameter("@RoleId", SqlDbType.Int, 4);
            roleidpara.Value = rolemenu.RoleId;
            paras.Add(roleidpara);

            SqlParameter menuidpara = new SqlParameter("@MenuId", SqlDbType.Int, 4);
            menuidpara.Value = rolemenu.MenuId;
            paras.Add(menuidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = rolemenu.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            RoleMenu rolemenu = new RoleMenu();

            int indexRoleMenuID = dr.GetOrdinal("RoleMenuID");
            rolemenu.RoleMenuID = Convert.ToInt32(dr[indexRoleMenuID]);

            int indexRoleId = dr.GetOrdinal("RoleId");
            rolemenu.RoleId = Convert.ToInt32(dr[indexRoleId]);

            int indexMenuId = dr.GetOrdinal("MenuId");
            rolemenu.MenuId = Convert.ToInt32(dr[indexMenuId]);

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                rolemenu.RefStatus = Convert.ToInt32(dr[indexRefStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                rolemenu.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                rolemenu.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                rolemenu.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                rolemenu.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return rolemenu;
        }

        public override string TableName
        {
            get
            {
                return "RoleMenu";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            RoleMenu rolemenu = (RoleMenu)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter rolemenuidpara = new SqlParameter("@RoleMenuID", SqlDbType.Int, 4);
            rolemenuidpara.Value = rolemenu.RoleMenuID;
            paras.Add(rolemenuidpara);

            SqlParameter roleidpara = new SqlParameter("@RoleId", SqlDbType.Int, 4);
            roleidpara.Value = rolemenu.RoleId;
            paras.Add(roleidpara);

            SqlParameter menuidpara = new SqlParameter("@MenuId", SqlDbType.Int, 4);
            menuidpara.Value = rolemenu.MenuId;
            paras.Add(menuidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = rolemenu.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
