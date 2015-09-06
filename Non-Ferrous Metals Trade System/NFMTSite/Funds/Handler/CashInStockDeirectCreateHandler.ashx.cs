using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// CashInStockDeirectCreateHandler 的摘要说明
    /// </summary>
    public class CashInStockDeirectCreateHandler : IHttpHandler
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

            string allotDesc = context.Request.Form["AllotDesc"];

            int stockLogId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["StockLogId"]) || !int.TryParse(context.Request.Form["StockLogId"], out stockLogId) || stockLogId <= 0)
            {
                context.Response.Write("库存信息错误");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<NFMT.Funds.Model.CashInContractDirect> directs = serializer.Deserialize<List<NFMT.Funds.Model.CashInContractDirect>>(detailStr);
                if (directs == null)
                {
                    context.Response.Write("分配信息错误");
                    context.Response.End();
                }

                List<NFMT.Funds.Model.CashInCorp> corps = new List<NFMT.Funds.Model.CashInCorp>();
                foreach (NFMT.Funds.Model.CashInContractDirect direct in directs)
                {
                    NFMT.Funds.Model.CashInCorp corp = new NFMT.Funds.Model.CashInCorp();
                    corp.AllotBala = direct.AllotBala;
                    corp.CorpId = direct.AllotCorpId;
                    corp.IsShare = false;
                    corp.CashInId = direct.CashInId;

                    corps.Add(corp);
                }

                NFMT.Funds.Model.CashInAllot allot = new NFMT.Funds.Model.CashInAllot();
                allot.AllotDesc = allotDesc;
                allot.AllotStatus = NFMT.Common.StatusEnum.已录入;

                NFMT.Funds.BLL.CashInStcokBLL bll = new NFMT.Funds.BLL.CashInStcokBLL();
                result = bll.CreateDirectStock(user, allot, corps, stockLogId);
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