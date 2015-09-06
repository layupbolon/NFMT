using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// PricingPersonDDLHandler 的摘要说明
    /// </summary>
    public class PricingPersonDDLHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.DoPrice.BLL.PricingPersonBLL bll = new NFMT.DoPrice.BLL.PricingPersonBLL();
            var result = bll.Load<NFMT.DoPrice.Model.PricingPerson>(user);

            context.Response.ContentType = "application/json; charset=utf-8";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }          

            List<NFMT.DoPrice.Model.PricingPerson> dt = result.ReturnValue as List<NFMT.DoPrice.Model.PricingPerson>;

            int contractId = 0, isOut = -1;
            if (!string.IsNullOrEmpty(context.Request.QueryString["ContractId"]))
                int.TryParse(context.Request.QueryString["ContractId"], out contractId);
            if (!string.IsNullOrEmpty(context.Request.QueryString["IsOut"]))
                int.TryParse(context.Request.QueryString["IsOut"], out isOut);           
            
            //合约抬头过滤
            if (contractId > 0 && isOut >= 0)
            {
                //获取合约抬头
                bool isFlag = false;
                if (isOut != 0)
                    isFlag = true;

                NFMT.Contract.BLL.ContractCorporationDetailBLL corpBLL = new NFMT.Contract.BLL.ContractCorporationDetailBLL();
                result = corpBLL.LoadCorpListByContractId(user, contractId, isFlag);
                if (result.ResultStatus != 0)
                    context.Response.End();

                List<NFMT.Contract.Model.ContractCorporationDetail> contractCorps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
                var corpIds = contractCorps.Select(c => c.CorpId).ToList();

                var esc = dt.Where(temp => corpIds.Contains(temp.CorpId));
                if (esc != null)
                    dt = esc.ToList();
            }

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            context.Response.Write(jsonStr);
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