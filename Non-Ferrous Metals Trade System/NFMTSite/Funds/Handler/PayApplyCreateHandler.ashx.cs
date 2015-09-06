using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PayApplyCreateHandler 的摘要说明
    /// </summary>
    public class PayApplyCreateHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string applyStr = context.Request.Form["Apply"];
            if (string.IsNullOrEmpty(applyStr))
            {
                result.Message = "申请不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string payApplyStr = context.Request.Form["PayApply"];
            if (string.IsNullOrEmpty(payApplyStr))
            {
                result.Message = "付款申请不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            int subId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["SubId"]) || !int.TryParse(context.Request.Form["SubId"], out subId))
            {
                result.Message = "子合约序号出错";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string stockAppsStr = context.Request.Form["StockApps"];

            string bankName = context.Request.Form["RecBank"];

            bool isAudit = false;
            if (string.IsNullOrEmpty(context.Request.Form["isAudit"]) || !bool.TryParse(context.Request.Form["isAudit"], out isAudit))
            {
                result.Message = "参数错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                NFMT.Operate.Model.Apply apply = serializer.Deserialize<NFMT.Operate.Model.Apply>(applyStr);
                NFMT.Funds.Model.PayApply payApply = serializer.Deserialize<NFMT.Funds.Model.PayApply>(payApplyStr);
                List<NFMT.Funds.Model.StockPayApply> stockDetails = new List<NFMT.Funds.Model.StockPayApply>();
                if(!string.IsNullOrEmpty(stockAppsStr))
                    stockDetails = serializer.Deserialize<List<NFMT.Funds.Model.StockPayApply>>(stockAppsStr);

                #region 付款申请单：收款人银行和账号可以手输，并且做保留以便下次可以选择  20150702 MKZC

                int bankId = 0, bankAccountId = 0;
                NFMT.Data.BLL.BankAccountBLL bankAccountBLL = new NFMT.Data.BLL.BankAccountBLL();
                result = bankAccountBLL.InsertOrUpdateBankAccountInfo(user, bankName, payApply.RecBankAccount, payApply.RecCorpId);
                if (result.ResultStatus != 0)
                {
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                string bankInfo = result.ReturnValue.ToString();
                if (string.IsNullOrEmpty(bankInfo))
                {
                    result.Message = "银行信息获取出错";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }
                bankId = Convert.ToInt32(bankInfo.Split(',')[0]);
                bankAccountId = Convert.ToInt32(bankInfo.Split(',')[1]);

                payApply.RecBankId = bankId;
                payApply.RecBankAccountId = bankAccountId;

                #endregion

                NFMT.Funds.BLL.PayApplyBLL bll = new NFMT.Funds.BLL.PayApplyBLL();
                result = bll.PayApplyCreate(user, apply, payApply, stockDetails, subId, isAudit);

                if (result.ResultStatus == 0)
                {
                    result.Message = "付款申请添加成功";
                }
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