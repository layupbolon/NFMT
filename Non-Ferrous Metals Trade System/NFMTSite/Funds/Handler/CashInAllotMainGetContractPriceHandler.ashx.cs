using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// CashInAllotMainGetContractPriceHandler 的摘要说明
    /// </summary>
    public class CashInAllotMainGetContractPriceHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int subContractId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["cid"]))
                int.TryParse(context.Request.QueryString["cid"], out subContractId);

            NFMT.Contract.BLL.SubPriceBLL subPriceBLL = new NFMT.Contract.BLL.SubPriceBLL();
            result = subPriceBLL.GetPriceBySubId(user, subContractId);
            if (result.ResultStatus != 0)
                context.Response.End();

            NFMT.Contract.Model.SubPrice subPrice = result.ReturnValue as NFMT.Contract.Model.SubPrice;
            if (subPrice == null)
                context.Response.End();

            NFMT.Contract.BLL.ContractSubBLL contractSubBLL = new NFMT.Contract.BLL.ContractSubBLL();
            result = contractSubBLL.Get(user, subContractId);
            if (result.ResultStatus != 0)
                context.Response.End();

            NFMT.Contract.Model.ContractSub contractSub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
            if (contractSub == null)
                context.Response.End();

            if (contractSub.PriceMode == (int)NFMT.Contract.PriceModeEnum.点价)
                context.Response.Write(subPrice.AlmostPrice);
            else if (contractSub.PriceMode == (int)NFMT.Contract.PriceModeEnum.定价)
                context.Response.Write(subPrice.FixedPrice);
            else
                context.Response.Write(0);
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