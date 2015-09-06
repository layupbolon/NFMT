/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：MenuDAL.cs
// 文件功能描述：功能菜单表dbo.Menu数据交互类。
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
    /// 功能菜单表dbo.Menu数据交互类。
    /// </summary>
    public class MenuDAL : DataOperate, IMenuDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuDAL()
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
            Menu menu = (Menu)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@MenuId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(menu.MenuName))
            {
                SqlParameter menunamepara = new SqlParameter("@MenuName", SqlDbType.VarChar, 80);
                menunamepara.Value = menu.MenuName;
                paras.Add(menunamepara);
            }

            if (!string.IsNullOrEmpty(menu.MenuDesc))
            {
                SqlParameter menudescpara = new SqlParameter("@MenuDesc", SqlDbType.VarChar, 400);
                menudescpara.Value = menu.MenuDesc;
                paras.Add(menudescpara);
            }

            SqlParameter parentidpara = new SqlParameter("@ParentId", SqlDbType.Int, 4);
            parentidpara.Value = menu.ParentId;
            paras.Add(parentidpara);

            SqlParameter firstidpara = new SqlParameter("@FirstId", SqlDbType.Int, 4);
            firstidpara.Value = menu.FirstId;
            paras.Add(firstidpara);

            if (!string.IsNullOrEmpty(menu.Url))
            {
                SqlParameter urlpara = new SqlParameter("@Url", SqlDbType.VarChar, 400);
                urlpara.Value = menu.Url;
                paras.Add(urlpara);
            }

            SqlParameter menustatuspara = new SqlParameter("@MenuStatus", SqlDbType.Int, 4);
            menustatuspara.Value = menu.MenuStatus;
            paras.Add(menustatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Menu menu = new Menu();

            int indexMenuId = dr.GetOrdinal("MenuId");
            menu.MenuId = Convert.ToInt32(dr[indexMenuId]);

            int indexMenuName = dr.GetOrdinal("MenuName");
            if (dr["MenuName"] != DBNull.Value)
            {
                menu.MenuName = Convert.ToString(dr[indexMenuName]);
            }

            int indexMenuDesc = dr.GetOrdinal("MenuDesc");
            if (dr["MenuDesc"] != DBNull.Value)
            {
                menu.MenuDesc = Convert.ToString(dr[indexMenuDesc]);
            }

            int indexParentId = dr.GetOrdinal("ParentId");
            if (dr["ParentId"] != DBNull.Value)
            {
                menu.ParentId = Convert.ToInt32(dr[indexParentId]);
            }

            int indexFirstId = dr.GetOrdinal("FirstId");
            if (dr["FirstId"] != DBNull.Value)
            {
                menu.FirstId = Convert.ToInt32(dr[indexFirstId]);
            }

            int indexUrl = dr.GetOrdinal("Url");
            if (dr["Url"] != DBNull.Value)
            {
                menu.Url = Convert.ToString(dr[indexUrl]);
            }

            int indexMenuStatus = dr.GetOrdinal("MenuStatus");
            if (dr["MenuStatus"] != DBNull.Value)
            {
                menu.MenuStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexMenuStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                menu.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                menu.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                menu.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                menu.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return menu;
        }

        public override string TableName
        {
            get
            {
                return "Menu";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Menu menu = (Menu)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter menuidpara = new SqlParameter("@MenuId", SqlDbType.Int, 4);
            menuidpara.Value = menu.MenuId;
            paras.Add(menuidpara);

            if (!string.IsNullOrEmpty(menu.MenuName))
            {
                SqlParameter menunamepara = new SqlParameter("@MenuName", SqlDbType.VarChar, 80);
                menunamepara.Value = menu.MenuName;
                paras.Add(menunamepara);
            }

            if (!string.IsNullOrEmpty(menu.MenuDesc))
            {
                SqlParameter menudescpara = new SqlParameter("@MenuDesc", SqlDbType.VarChar, 400);
                menudescpara.Value = menu.MenuDesc;
                paras.Add(menudescpara);
            }

            SqlParameter parentidpara = new SqlParameter("@ParentId", SqlDbType.Int, 4);
            parentidpara.Value = menu.ParentId;
            paras.Add(parentidpara);

            SqlParameter firstidpara = new SqlParameter("@FirstId", SqlDbType.Int, 4);
            firstidpara.Value = menu.FirstId;
            paras.Add(firstidpara);

            if (!string.IsNullOrEmpty(menu.Url))
            {
                SqlParameter urlpara = new SqlParameter("@Url", SqlDbType.VarChar, 400);
                urlpara.Value = menu.Url;
                paras.Add(urlpara);
            }

            SqlParameter menustatuspara = new SqlParameter("@MenuStatus", SqlDbType.Int, 4);
            menustatuspara.Value = menu.MenuStatus;
            paras.Add(menustatuspara);

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
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(" select distinct m.* from dbo.AuthOperate ao left join dbo.Menu m on ao.MenuId = m.MenuId where ao.EmpId =@empId and ao.AuthOperateStatus=@status and m.MenuStatus=@status ");
                sb.Append(" union select distinct mParent.* from dbo.EmpMenu em left join dbo.Menu m on em.MenuId = m.MenuId left join dbo.Menu mParent on mParent.MenuId = m.ParentId where em.EmpId =@empId and em.RefStatus=@status and ISNULL(mParent.MenuId,0)<>0 and mParent.MenuStatus=@status");
                SqlParameter[] paras = new SqlParameter[2];
                paras[0] = new SqlParameter("@empId", empId);
                paras[1] = new SqlParameter("@status", (int)Common.StatusEnum.已生效);

                DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionStringUser, sb.ToString(), paras, CommandType.Text);

                List<Menu> menus = new List<Menu>();

                foreach (DataRow dr in dt.Rows)
                {
                    Menu menu = new Menu();
                    menu.MenuId = Convert.ToInt32(dr["MenuId"]);

                    if (dr["MenuName"] != DBNull.Value)
                    {
                        menu.MenuName = Convert.ToString(dr["MenuName"]);
                    }
                    if (dr["MenuDesc"] != DBNull.Value)
                    {
                        menu.MenuDesc = Convert.ToString(dr["MenuDesc"]);
                    }
                    if (dr["ParentId"] != DBNull.Value)
                    {
                        menu.ParentId = Convert.ToInt32(dr["ParentId"]);
                    }
                    if (dr["FirstId"] != DBNull.Value)
                    {
                        menu.FirstId = Convert.ToInt32(dr["FirstId"]);
                    }
                    if (dr["Url"] != DBNull.Value)
                    {
                        menu.Url = Convert.ToString(dr["Url"]);
                    }
                    if (dr["MenuStatus"] != DBNull.Value)
                    {
                        menu.MenuStatus = (Common.StatusEnum)Convert.ToInt32(dr["MenuStatus"]);
                    }
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        menu.CreatorId = Convert.ToInt32(dr["CreatorId"]);
                    }
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        menu.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    }
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        menu.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    }
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        menu.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    }
                    menus.Add(menu);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = menus;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 获取操作菜单，返回datatable
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultModel GetOperateMenu(UserModel user)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select * from NFMT_Basic.dbo.BDStyleDetail where BDStyleId = {0} and DetailStatus = {1}", (int)NFMT.Data.StyleEnum.OperateType, (int)Common.StatusEnum.已生效);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = dt;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取菜单，返回List Model.Menu
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultModel GetMenu(UserModel user, string menuIds)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                string sql = string.Format("select * from dbo.Menu where MenuStatus = {0} and MenuId in ({1}) or ParentId in ({1})", (int)Common.StatusEnum.已生效, menuIds);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, sql, null);
                List<Model.Menu> models = new List<Model.Menu>();

                int i = 0;
                while (dr.Read())
                {
                    models.Add(this.CreateModel<Model.Menu>(dr));
                    i++;
                }

                result.AffectCount = i;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = models;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        public ResultModel GetMenuOperateList(int empId, string menuIds)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select e.Name,m.MenuName,bd.DetailName,em.EmpId,em.MenuId,ISNULL(ao.OperateType,0) as OperateType ");
                sb.Append(" from dbo.EmpMenu em ");
                sb.Append(" left join dbo.Employee e on em.EmpId = e.EmpId ");
                sb.AppendFormat(" left join dbo.Menu m on em.MenuId = m.MenuId and em.MenuId in ({0}) or m.ParentId in ({0})", menuIds);
                sb.Append(" left join dbo.AuthOperate ao on em.EmpId = ao.EmpId and em.MenuId = ao.MenuId ");
                sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail bd on ao.OperateType = bd.StyleDetailId and bd.BDStyleId = {0} ", (int)NFMT.Data.StyleEnum.OperateType);
                sb.AppendFormat(" where em.EmpId = {0} and em.RefStatus = {1} and ao.AuthOperateStatus = {1} and m.MenuStatus = {1}", empId, (int)Common.StatusEnum.已生效);

                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = dt;
                }
                else
                {
                    result.ResultStatus = 0;
                    result.Message = "获取失败";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            return result;
        }

        #endregion
    }
}
