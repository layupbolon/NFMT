using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// PledgeApplyUpdateHandler 的摘要说明
    /// </summary>
    public class PledgeApplyUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string applyStr = context.Request.Form["apply"];
            if (string.IsNullOrEmpty(applyStr))
            {
                context.Response.Write("申请信息不能为空");
                context.Response.End();
            }

            string pledgeApplyStr = context.Request.Form["pledgeApply"];
            if (string.IsNullOrEmpty(pledgeApplyStr))
            {
                context.Response.Write("质押申请信息不能为空");
                context.Response.End();
            }

            string detailsStr = context.Request.Form["rows"];
            if (string.IsNullOrEmpty(detailsStr))
            {
                context.Response.Write("明细内容不能为空");
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Operate.Model.Apply apply = serializer.Deserialize<NFMT.Operate.Model.Apply>(applyStr);
                NFMT.WareHouse.Model.PledgeApply pledgeApply = serializer.Deserialize<NFMT.WareHouse.Model.PledgeApply>(pledgeApplyStr);
                List<NFMT.WareHouse.Model.PledgeApplyDetail> details = serializer.Deserialize<List<NFMT.WareHouse.Model.PledgeApplyDetail>>(detailsStr);
                if (apply == null || pledgeApply == null || details == null || !details.Any())
                {
                    context.Response.Write("数据错误");
                    context.Response.End();
                }
                apply.ApplyType = NFMT.Operate.ApplyType.PledgeApply;
                apply.EmpId = user.EmpId;
                apply.ApplyTime = DateTime.Now;

                NFMT.WareHouse.BLL.PledgeApplyBLL pledgeApplyBLL = new NFMT.WareHouse.BLL.PledgeApplyBLL();
                result = pledgeApplyBLL.PledgeApplyUpdateHandle(user, apply, pledgeApply, details);
                if (result.ResultStatus == 0)
                    result.Message = "质押申请修改成功";
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
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