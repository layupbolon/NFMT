using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// CashInContractDirectCreateHandler 的摘要说明
    /// </summary>
    public class CashInContractDirectCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string detailStr = context.Request.Form["Details"];
            if (string.IsNullOrEmpty(detailStr))
            {
                context.Response.Write("分配信息不能为空");
                context.Response.End();
            }

            string memo = context.Request.Form["Memo"];

            int subId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["SubId"]) || !int.TryParse(context.Request.Form["SubId"], out subId) || subId <= 0)
            {
                context.Response.Write("公司信息错误");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<NFMT.Funds.Model.CashInContractDirect> details = serializer.Deserialize<List<NFMT.Funds.Model.CashInContractDirect>>(detailStr);
                if (details == null)
                {
                    context.Response.Write("分配信息错误");
                    context.Response.End();
                }

                NFMT.Funds.Model.CashInAllot allot = new NFMT.Funds.Model.CashInAllot();
                allot.AllotDesc = memo;
                allot.AllotStatus = NFMT.Common.StatusEnum.已录入;

                NFMT.Funds.BLL.CashInContractBLL bll = new NFMT.Funds.BLL.CashInContractBLL();
                result = bll.CreateDirectContract(user, allot, details, subId);
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            if (result.ResultStatus == 0)
                result.Message = "分配成功";

            context.Response.Write(result.Message);
            context.Response.End();
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