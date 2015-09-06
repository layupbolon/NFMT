using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// RepurChaseApplyCreateHandler 的摘要说明
    /// </summary>
    public class RepurChaseApplyCreateHandler : IHttpHandler
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

            NFMT.Operate.Model.Apply apply = new NFMT.Operate.Model.Apply()
            {
                EmpId = user.EmpId,
                ApplyDesc = memo,
                ApplyDept = deptId,
                ApplyTime = DateTime.Now,
                ApplyType = NFMT.Operate.ApplyType.回购申请
            };

            List<NFMT.WareHouse.Model.RepoApplyDetail> details = new List<NFMT.WareHouse.Model.RepoApplyDetail>();

            string[] splitItem = sids.Split(',');
            for (int i = 0; i < splitItem.Length; i++)
            {
                NFMT.WareHouse.Model.RepoApplyDetail detail = new NFMT.WareHouse.Model.RepoApplyDetail()
                {
                    StockId = Convert.ToInt32(splitItem[i])
                };
                details.Add(detail);
            }

            NFMT.WareHouse.BLL.RepoApplyBLL bll = new NFMT.WareHouse.BLL.RepoApplyBLL();
            var result = bll.RepoApplyCreateHandle(user, apply, new NFMT.WareHouse.Model.RepoApply(), details);

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