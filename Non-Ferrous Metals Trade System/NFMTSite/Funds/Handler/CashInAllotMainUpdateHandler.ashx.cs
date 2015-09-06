using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// CashInAllotMainUpdateHandler 的摘要说明
    /// </summary>
    public class CashInAllotMainUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string cashInAllotStr = context.Request.Form["cashInAllot"];
            if (string.IsNullOrEmpty(cashInAllotStr))
            {
                context.Response.Write("收款分配信息不能为空");
                context.Response.End();
            }

            string cashInCorpStr = context.Request.Form["cashInCorp"];
            if (string.IsNullOrEmpty(cashInCorpStr))
            {
                context.Response.Write("收款分配至公司信息不能为空");
                context.Response.End();
            }

            string cashInContractStr = context.Request.Form["cashInContract"];
            if (string.IsNullOrEmpty(cashInContractStr))
            {
                context.Response.Write("收款分配至合约信息不能为空");
                context.Response.End();
            }

            string cashInStockStr = context.Request.Form["cashInStock"];
            if (string.IsNullOrEmpty(cashInStockStr))
            {
                context.Response.Write("收款分配至库存信息不能为空");
                context.Response.End();
            }
            string cashInInvoiceStr = context.Request.Form["cashInInvoice"];

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                NFMT.Funds.Model.CashInAllot cashInAllot = serializer.Deserialize<NFMT.Funds.Model.CashInAllot>(cashInAllotStr);
                NFMT.Funds.Model.CashInCorp cashInCorp = serializer.Deserialize<NFMT.Funds.Model.CashInCorp>(cashInCorpStr);
                NFMT.Funds.Model.CashInContract cashInContract = serializer.Deserialize<NFMT.Funds.Model.CashInContract>(cashInContractStr);
                List<NFMT.Funds.Model.CashInStcok> cashInStcoks = serializer.Deserialize<List<NFMT.Funds.Model.CashInStcok>>(cashInStockStr);

                List<NFMT.Funds.Model.CashInInvoice> cashInInvoices = new List<NFMT.Funds.Model.CashInInvoice>();
                if (!string.IsNullOrEmpty(cashInInvoiceStr))
                    cashInInvoices = serializer.Deserialize<List<NFMT.Funds.Model.CashInInvoice>>(cashInInvoiceStr);

                cashInAllot.Alloter = user.EmpId;
                cashInAllot.AllotTime = DateTime.Now;

                NFMT.Funds.BLL.CashInAllotBLL bll = new NFMT.Funds.BLL.CashInAllotBLL();
                result = bll.Update(user, cashInAllot, cashInCorp, cashInContract, cashInStcoks, cashInInvoices);

                if (result.ResultStatus == 0)
                    result.Message = "收款分配修改成功";
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
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