/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：MenuBLL.cs
// 文件功能描述：功能菜单表dbo.Menu业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月2日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.User.Model;
using NFMT.User.DAL;
using NFMT.User.IDAL;
using NFMT.Common;
using System.Linq;

namespace NFMT.User.BLL
{
    /// <summary>
    /// 功能菜单表dbo.Menu业务逻辑类。
    /// </summary>
    public class MenuBLL : Common.DataBLL
    {
        private MenuDAL menuDAL = new MenuDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(MenuDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuBLL()
        {
        }

        #endregion

        #region 数据库操作

        protected override log4net.ILog Log
        {
            get { return this.log; }
        }

        public override IOperate Operate
        {
            get { return this.menuDAL; }
        }
        #endregion

        #region 新增方法

        public ResultModel GetMenuWithOperateItem(UserModel user, int empId, string menuIds)
        {
            ResultModel result = new ResultModel();

            try
            {
                //获取操作类型列表
                result = menuDAL.GetOperateMenu(user);
                if (result.ResultStatus != 0)
                    return result;

                DataTable dtOperate = result.ReturnValue as DataTable;
                if (dtOperate == null || dtOperate.Rows.Count < 1)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                    return result;
                }

                //获取用户可操作权限菜单列表
                result = menuDAL.GetMenuOperateList(empId, menuIds);
                if (result.ResultStatus != 0)
                    return result;

                DataTable dtList = result.ReturnValue as DataTable;
                //if (dtList == null || dtList.Rows.Count < 1)
                //{
                //    result.ResultStatus = -1;
                //    result.Message = "获取失败";
                //    return result;
                //}

                //获取所有功能菜单
                result = menuDAL.GetMenu(user, menuIds);
                if (result.ResultStatus != 0)
                    return result;

                List<Model.Menu> menus = result.ReturnValue as List<Model.Menu>;
                if (menus == null || !menus.Any())
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                    return result;
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<ul style=\"margin: 0 0 0 0\">");
                sb.Append(Environment.NewLine);
                sb.Append(GetChildMenu(0, menus, dtOperate, dtList));
                sb.Append(Environment.NewLine);
                sb.Append("</ul>");

                result.ReturnValue = sb.ToString();

            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        private System.Text.StringBuilder GetChildMenu(int parentId, List<NFMT.User.Model.Menu> menus, System.Data.DataTable dtOperate, System.Data.DataTable dtList)
        {
            List<NFMT.User.Model.Menu> childMenus;
            var result = from m in menus
                         where m.ParentId == parentId
                         select m;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (result != null && result.Any())
            {
                childMenus = result.ToList();
                sb.Append(parentId == 0 ? "" : "<ul>");
            }
            else
                return sb;

            foreach (NFMT.User.Model.Menu menu in childMenus)
            {
                int parentMenuId = menu.Id;
                sb.AppendFormat("<li id=\"{0}\" item-expanded=\"false\" item-checked=\"{1}\">", menu.Id, IsIdInTable(dtList, parentMenuId).ToString().ToLower());
                sb.Append(Environment.NewLine);
                sb.AppendFormat("   <img style=\"float: left; margin-right: 5px;\" src=\"{0}images/folder.png\" />", NFMT.Common.DefaultValue.NftmSiteName);
                sb.Append(Environment.NewLine);
                sb.AppendFormat("   <span>{0}</span>", menu.MenuName);
                sb.Append(Environment.NewLine);

                System.Text.StringBuilder sb2 = GetChildMenu(menu.Id, menus, dtOperate, dtList);
                if (sb2.ToString().Length > 1)
                {
                    sb.Append(sb2);
                    sb.Append("</ul>");
                }
                else
                {
                    System.Text.StringBuilder sbOperate = new System.Text.StringBuilder();
                    sbOperate.Append("<ul >");
                    sbOperate.Append(Environment.NewLine);

                    foreach (DataRow dr in dtOperate.Rows)
                    {
                        //if (dr["DetailName"] == null || dr["DetailName"].ToString() == "查询") continue;

                        sbOperate.AppendFormat("<li style=\"float:left;\" id=\"op_{0}_" + dr["StyleDetailId"].ToString() + "\"" + " item-checked=\"{1}\">", parentMenuId, IsOpIdInTable(dtList, parentMenuId, dr["StyleDetailId"].ToString()).ToString().ToLower());
                        sbOperate.Append(Environment.NewLine);
                        //sb.AppendFormat("   <img style=\"float: left; margin-right: 5px;\" src=\"{0}images/folder.png\" />", NFMT.Common.DefaultValue.NftmSiteName);
                        //sb.Append(Environment.NewLine);
                        sbOperate.AppendFormat("   <span>{0}</span>", dr["DetailName"].ToString());
                        sbOperate.Append(Environment.NewLine);
                    }
                    sbOperate.Append("</ul>");
                    sbOperate.Append(Environment.NewLine);

                    sb.Append(sbOperate.ToString());
                }

                sb.Append("</li>");
            }

            return sb;
        }

        public ResultModel GetMenuOperateList(int empId, string menuIds)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = menuDAL.GetMenuOperateList(empId, menuIds);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        private bool IsIdInTable(System.Data.DataTable dt, int menuId)
        {
            try
            {
                if (dt == null || dt.Rows.Count < 1)
                    return false;
                var item = from dr in dt.AsEnumerable()
                           where dr.Field<Int32>("MenuId") == menuId
                           select dr;

                return item.Count() > 0;
            }
            catch
            {
                return false;
            }
        }

        private bool IsOpIdInTable(System.Data.DataTable dt, int menuId, string operateType)
        {
            try
            {
                if (dt == null || dt.Rows.Count < 1)
                    return false;
                var item = from dr in dt.AsEnumerable()
                           where dr.Field<Int32>("MenuId") == menuId && dr.Field<Int32>("OperateType") == Convert.ToInt32(operateType)
                           select dr;

                return item.Count() > 0;
            }
            catch
            {
                return false;
            }
        }

        public ResultModel GetOperateList(UserModel user, int menuId,int empId)
        {
            ResultModel result = new ResultModel();

            DAL.AuthOperateDAL authOperateDAL = new AuthOperateDAL();

            try
            {
                result = menuDAL.GetOperateMenu(user);
                if (result.ResultStatus != 0)
                    return result;
                DataTable dtOperate = result.ReturnValue as DataTable;
                if(dtOperate==null||dtOperate.Rows.Count<1)
                {
                    result.ResultStatus = -1;
                    result.Message ="获取失败";
                    return result;
                }

                result = authOperateDAL.GetOperate(user, empId);
                if (result.ResultStatus != 0)
                    return result;

                DataTable dtEmpAuth = result.ReturnValue as DataTable;
                
                bool hasAuth = false;
                if (dtEmpAuth != null && dtEmpAuth.Rows.Count > 0)
                    hasAuth = true;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("[");
                foreach (DataRow dr in dtOperate.Rows)
                {
                    sb.Append("{");

                    sb.AppendFormat("\"label\":\"{0}\",\"id\":\"{1}\",\"checked\":{2}", dr["DetailName"].ToString(), "op_" + menuId.ToString() + "_" + dr["StyleDetailId"].ToString(), hasAuth ? dtEmpAuth.AsEnumerable().Any(row => Convert.ToInt32(row["OperateType"]) == Convert.ToInt32(dr["StyleDetailId"])).ToString().ToLower() : "false");//, "style=\"float:left;\""   ,\"html\":\"{3}\"
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
                sb.Append("]");

                result.ResultStatus = 0;
                result.Message = "获取成功";
                result.ReturnValue = sb.ToString();
                
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        #endregion
    }
}
