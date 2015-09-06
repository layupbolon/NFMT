using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// ReceivableUpdateHandler 的摘要说明
    /// </summary>
    public class ReceivableUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string r = context.Request.Form["Receivable"];
            if (string.IsNullOrEmpty(r))
            {
                context.Response.Write("收款信息不能为空");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Funds.Model.Receivable receivable = serializer.Deserialize<NFMT.Funds.Model.Receivable>(r);
                if (receivable == null)
                {
                    context.Response.Write("收款信息错误");
                    context.Response.End();
                }

                NFMT.User.Model.Corporation innerCorp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == receivable.ReceivableCorpId);
                if (innerCorp != null)
                    receivable.ReceivableGroupId = innerCorp.ParentId;

                receivable.ReceiveEmpId = user.EmpId;

                NFMT.User.Model.Corporation outCorp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == receivable.PayCorpId);
                if (outCorp != null)
                    receivable.PayGroupId = outCorp.ParentId;

                if (string.IsNullOrEmpty(receivable.PayCorpName))
                {
                    receivable.PayCorpName = outCorp.CorpName;
                }

                if (string.IsNullOrEmpty(receivable.PayBank))
                {
                    NFMT.Data.Model.Bank bank = NFMT.Data.BasicDataProvider.Banks.SingleOrDefault(a => a.BankId == receivable.PayBankId);
                    receivable.PayBank = bank.BankName;
                }

                if (string.IsNullOrEmpty(receivable.PayAccount))
                {
                    NFMT.Data.Model.BankAccount bankAccount = NFMT.Data.BasicDataProvider.BankAccounts.SingleOrDefault(a => a.BankAccId == receivable.PayAccountId);
                    receivable.PayAccount = bankAccount.AccountNo;
                }

                receivable.ReceiveEmpId = user.EmpId;
                NFMT.Funds.BLL.ReceivableBLL bll = new NFMT.Funds.BLL.ReceivableBLL();
                result = bll.Update(user, receivable);
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
    }
}