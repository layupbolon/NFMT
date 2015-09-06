/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AuthOperateBLL.cs
// 文件功能描述：操作权限表dbo.AuthOperate业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年11月20日
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
    /// 操作权限表dbo.AuthOperate业务逻辑类。
    /// </summary>
    public class AuthOperateBLL : Common.ExecBLL
    {
        private AuthOperateDAL authoperateDAL = new AuthOperateDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(AuthOperateDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AuthOperateBLL()
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
            get { return this.authoperateDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel Create(UserModel user, int empId, List<Model.AuthOperate> authOperates, List<Model.EmpMenu> empMenus, string exceptItemIds)
        {
            ResultModel result = new ResultModel();
            DAL.MenuDAL menuDAL = new MenuDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //if (authOperates == null || !authOperates.Any())
                    //{
                    //    result.ResultStatus = -1;
                    //    result.Message = "数据错误";
                    //    return result;
                    //}
                    DAL.AuthOperateDAL authOperateDAL = new AuthOperateDAL();
                    result = authOperateDAL.InvalidAll(user, empId, exceptItemIds);
                    if (result.ResultStatus != 0)
                        return result;

                    if (authOperates != null || authOperates.Any())
                    {
                        foreach (Model.AuthOperate authOperate in authOperates)
                        {
                            result = authoperateDAL.Insert(user, authOperate);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    if (empMenus != null && empMenus.Any())
                    {
                        DAL.EmpMenuDAL empMenuDAL = new EmpMenuDAL();
                        //result = empMenuDAL.InvalidAll(user, empId);
                        //if (result.ResultStatus != 0)
                        //    return result;
                        foreach (Model.EmpMenu empMenu in empMenus)
                        {
                            result = empMenuDAL.Insert(user, empMenu);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    scope.Complete();
                }
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

        public ResultModel JudgeOperate(UserModel user, int menuId, List<NFMT.Common.OperateEnum> operateTypes)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (operateTypes == null || !operateTypes.Any())
                {
                    result.Message = "系统内部错误，请设置该页面的权限";
                    result.ResultStatus = -1;
                    return result;
                }

                string oids = string.Empty;
                foreach (NFMT.Common.OperateEnum operate in operateTypes)
                {
                    oids += ((int)operate).ToString() + ",";
                }
                if (!string.IsNullOrEmpty(oids) && oids.IndexOf(',') > -1)
                    oids = oids.Substring(0, oids.Length - 1);

                result = authoperateDAL.JudgeOperate(user, menuId, oids);
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

        public ResultModel InvalidAll(UserModel user, int empId, string exceptItemIds)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = authoperateDAL.InvalidAll(user, empId, exceptItemIds);
            }
            catch (Exception ex)
            {
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

        public ResultModel GetMenuList(UserModel user, int empId, string menuIds)
        {
            ResultModel result = new ResultModel();

            try
            {
                //获取操作权限列表
                DAL.MenuDAL menuDAL = new MenuDAL();
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

                //获取用户操作权限
                result = authoperateDAL.GetOperate(user, empId);
                if (result.ResultStatus != 0)
                    return result;

                DataTable dtEmpAuth = result.ReturnValue as DataTable;

                result = authoperateDAL.GetMenuList(user, empId, menuIds);
                if (result.ResultStatus != 0)
                    return result;

                DataTable dt = result.ReturnValue as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("[");

                    foreach (DataRow dr in dt.AsEnumerable().Where(row => Convert.ToInt32(row["ParentId"]) == 0))
                    {
                        sb.Append("{");
                        //一级菜单
                        sb.AppendFormat("\"label\":\"{0}\",\"id\":\"{1}\",\"checked\":{2}", dr["label"].ToString(), Convert.ToInt32(dr["id"]).ToString(), Convert.ToBoolean(dr["checked"]).ToString().ToLower());
                        if (dt.AsEnumerable().Any(row => Convert.ToInt32(row["ParentId"]) == Convert.ToInt32(dr["id"])))
                        {
                            var items = dt.AsEnumerable().Where(detail => Convert.ToInt32(detail["ParentId"]) == Convert.ToInt32(dr["id"]));
                            sb.Append(",\"items\":[");
                            foreach (DataRow drDetail in items)
                            {
                                sb.Append("{");
                                //二级菜单
                                sb.AppendFormat("\"label\":\"{0}\",\"id\":\"{1}\",\"checked\":{2}", drDetail["label"].ToString(), drDetail["id"].ToString(), drDetail["checked"].ToString().ToLower());
                                sb.Append(",\"items\":[");

                                bool hasAuth = dtEmpAuth.AsEnumerable().Any(temp => Convert.ToInt32(temp["MenuId"]) == Convert.ToInt32(drDetail["id"]));
                                foreach (DataRow drOp in dtOperate.Rows)
                                {
                                    sb.Append("{");
                                    //菜单操作权限
                                    sb.AppendFormat("\"label\":\"{0}\",\"id\":\"{1}\",\"checked\":{2},\"html\":\"{3}\"", drOp["DetailName"].ToString(), "op_" + drDetail["id"].ToString() + "_" + drOp["StyleDetailId"].ToString(), hasAuth ? dtEmpAuth.AsEnumerable().Any(row => Convert.ToInt32(row["OperateType"]) == Convert.ToInt32(drOp["StyleDetailId"]) && Convert.ToInt32(row["MenuId"]) == Convert.ToInt32(drDetail["id"])).ToString().ToLower() : "false", "style='float:left;'");
                                    sb.Append("},");
                                }
                                sb = sb.Remove(sb.Length - 1, 1);
                                sb.Append("]},");
                            }
                            sb = sb.Remove(sb.Length - 1, 1);
                            sb.Append("]");
                        }
                        else
                        {
                            sb.Append(",\"items\":[");
                            bool hasAuth = dtEmpAuth.AsEnumerable().Any(temp => Convert.ToInt32(temp["MenuId"]) == Convert.ToInt32(dr["id"]));
                            foreach (DataRow drOp in dtOperate.Rows)
                            {
                                sb.Append("{");
                                //菜单操作权限
                                sb.AppendFormat("\"label\":\"{0}\",\"id\":\"{1}\",\"checked\":{2},\"html\":\"{3}\"", drOp["DetailName"].ToString(), "op_" + dr["id"].ToString() + "_" + drOp["StyleDetailId"].ToString(), hasAuth ? dtEmpAuth.AsEnumerable().Any(row => Convert.ToInt32(row["OperateType"]) == Convert.ToInt32(drOp["StyleDetailId"]) && Convert.ToInt32(row["MenuId"]) == Convert.ToInt32(dr["id"])).ToString().ToLower() : "false", "style='float:left;'");
                                sb.Append("},");
                            }
                            sb = sb.Remove(sb.Length - 1, 1);
                            sb.Append("]");
                        }
                        sb.Append("},");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                    sb.Append("]");

                    result.ResultStatus = 0;
                    result.Message = "操作成功";
                    result.ReturnValue = sb.ToString();
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "操作失败";
                }
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

        public ResultModel GetUserMenuOperate(UserModel user, int menuId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = authoperateDAL.GetUserMenuOperate(user, menuId);
                if (result.ResultStatus != 0)
                    return result;

                List<AuthOperate> authOperates = result.ReturnValue as List<AuthOperate>;
                if (authOperates == null || !authOperates.Any())
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                    return result;
                }

                List<OperateEnum> operates = authOperates.Select(authOperate => (OperateEnum) authOperate.OperateType).ToList();

                result.ReturnValue = operates;
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
                result.ReturnValue = null;
            }

            return result;
        }

        #endregion
    }
}
