using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// ContractReceivableCreateHandler 的摘要说明
    /// </summary>
    public class ContractReceivableCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string r = context.Request.Form["Allot"];
            if (string.IsNullOrEmpty(r))
            {
                context.Response.Write("分配信息不能为空");
                context.Response.End();
            }
            string memo = context.Request.Form["memo"];

            int subId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["subId"]) || !int.TryParse(context.Request.Form["subId"], out subId) || subId <= 0)
            {
                context.Response.Write("子合约信息错误");
                context.Response.End();
            }

            int contractId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["contractId"]) || !int.TryParse(context.Request.Form["contractId"], out contractId) || contractId <= 0)
            {
                context.Response.Write("合约信息错误");
                context.Response.End();
            }

            int curId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["curId"]) || !int.TryParse(context.Request.Form["curId"], out curId) || curId <= 0)
            {
                context.Response.Write("币种信息错误");
                context.Response.End();
            }

            int allotFrom = 0;
            if (string.IsNullOrEmpty(context.Request.Form["allotFrom"]) || !int.TryParse(context.Request.Form["allotFrom"], out allotFrom) || allotFrom <= 0)
            {
                context.Response.Write("分配来源错误");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<AllotInfo> allotInfos = serializer.Deserialize<List<AllotInfo>>(r);
                if (allotInfos == null)
                {
                    context.Response.Write("分配信息错误");
                    context.Response.End();
                }

                List<NFMT.Funds.Model.CorpReceivable> corpReceivables = new List<NFMT.Funds.Model.CorpReceivable>();
                foreach (var item in allotInfos)
                {
                    int rId = 0, corpId = 0;
                    decimal bala = 0;
                    if (!string.IsNullOrEmpty(item.CanAllotBala)
                        && !string.IsNullOrEmpty(item.ReceivableId)
                        && !string.IsNullOrEmpty(item.CorpCode)
                        && int.TryParse(item.ReceivableId, out rId)
                        && decimal.TryParse(item.CanAllotBala, out bala)
                        && int.TryParse(item.CorpCode, out corpId))
                    {
                        NFMT.Funds.Model.CorpReceivable corpReceivable = new NFMT.Funds.Model.CorpReceivable()
                        {
                            //AllotId
                            //BlocId
                            CorpId = corpId,
                            RecId = rId,
                            //AllotBala = bala,
                        };
                        corpReceivables.Add(corpReceivable);
                    }
                }

                NFMT.Funds.BLL.ReceivableAllotBLL bll = new NFMT.Funds.BLL.ReceivableAllotBLL();
                result = bll.ReceivableAllotCreateHandle(user, corpReceivables, contractId, subId, curId, memo, allotFrom);
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

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

        public class AllotInfo
        {
            public string ReceivableId { get; set; }
            public string CanAllotBala { get; set; }
            public string CorpCode { get; set; }
        }
    }
}