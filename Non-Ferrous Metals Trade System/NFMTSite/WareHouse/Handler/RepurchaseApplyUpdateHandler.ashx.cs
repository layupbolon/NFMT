using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// RepurChaseApplyUpdateHandler 的摘要说明
    /// </summary>
    public class RepurChaseApplyUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";
            string resultStr = "添加失败";

            string sids = context.Request.Form["sids"];
            if (string.IsNullOrEmpty(sids))
            {
                resultStr = "请选择回购库存";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (sids.Split(',').Length < 1)
            {
                resultStr = "请选择回购库存";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            string memo = context.Request.Form["memo"];

            int deptId = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["deptId"]))
            {
                if (!int.TryParse(context.Request.Form["deptId"], out deptId))
                {
                    resultStr = "部门错误";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }
            }

            int repurApplyId = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["aid"]))
            {
                if (!int.TryParse(context.Request.Form["aid"], out repurApplyId))
                {
                    resultStr = "参数错误";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }
            }

            NFMT.WareHouse.BLL.RepoApplyBLL repoApplyBLL = new NFMT.WareHouse.BLL.RepoApplyBLL();
            NFMT.Common.ResultModel result = repoApplyBLL.Get(user, repurApplyId);
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            NFMT.WareHouse.Model.RepoApply repoApply = result.ReturnValue as NFMT.WareHouse.Model.RepoApply;

            NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
            result = applyBLL.Get(user, repoApply.ApplyId);
            if (result.ResultStatus != 0)
            {
                context.Response.Write(string.Format("申请内容有误,{0}", result.Message));
                context.Response.End();
            }

            NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
            apply.ApplyDesc = memo;
            apply.ApplyDept = deptId;

            result = applyBLL.Update(user, apply);
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            result = repoApplyBLL.RepoApplyUpdateHandle(user, apply, new NFMT.WareHouse.Model.RepoApply() { RepoApplyId = repurApplyId }, sids);
            context.Response.Write(result.Message);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}