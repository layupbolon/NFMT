using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Invoice.Handler
{
    /// <summary>
    /// FinBusInvAllotCreateHandler 的摘要说明
    /// </summary>
    public class FinBusInvAllotCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string allotInfoStr = context.Request.Form["allotInfo"];
            if (string.IsNullOrEmpty(allotInfoStr))
            {
                context.Response.Write("分配信息不能为空");
                context.Response.End();
            }

            int fid = 0;
            if (string.IsNullOrEmpty(context.Request.Form["fid"]) || !int.TryParse(context.Request.Form["fid"], out fid) || fid <= 0)
            {
                context.Response.Write("财务发票序号错误");
                context.Response.End();
            }

            int currencyId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["cur"]) || !int.TryParse(context.Request.Form["cur"], out currencyId) || currencyId <= 0)
            {
                context.Response.Write("币种错误");
                context.Response.End();
            }

            decimal canAllotAmount = 0;
            if (string.IsNullOrEmpty(context.Request.Form["canAllotAmount"]) || !decimal.TryParse(context.Request.Form["canAllotAmount"], out canAllotAmount) || canAllotAmount <= 0)
            {
                context.Response.Write("可分配金额错误");
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<AllotInfo> allotInfo = serializer.Deserialize<List<AllotInfo>>(allotInfoStr);

                List<NFMT.Invoice.Model.FinBusInvAllotDetail> details = new List<NFMT.Invoice.Model.FinBusInvAllotDetail>();
                decimal sumAllotAmount = 0;
                if (allotInfo != null && allotInfo.Any())
                {
                    foreach (AllotInfo info in allotInfo)
                    {
                        sumAllotAmount += info.AllotBala;

                        details.Add(new NFMT.Invoice.Model.FinBusInvAllotDetail()
                        {
                            BusinessInvoiceId = info.BusinessInvoiceId,
                            FinanceInvoiceId = fid,
                            AllotBala = info.AllotBala
                        });
                    }
                }

                if (sumAllotAmount > canAllotAmount)
                {
                    context.Response.Write(string.Format("分配金额不可大于可分配金额 {0} ", canAllotAmount.ToString()));
                    context.Response.End();
                }

                NFMT.Invoice.BLL.FinBusInvAllotBLL bll = new NFMT.Invoice.BLL.FinBusInvAllotBLL();
                result = bll.CreateAllot(user, currencyId, details);
                if (result.ResultStatus == 0)
                {
                    result.Message = "分配成功";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            context.Response.Write(result.Message);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class AllotInfo
        {
            public int BusinessInvoiceId { get; set; }

            public decimal AllotBala { get; set; }
        }
    }
}