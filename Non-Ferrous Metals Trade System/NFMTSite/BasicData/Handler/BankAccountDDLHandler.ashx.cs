using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using NFMT.Common;
using NFMT.Data;
using NFMT.Data.Model;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BankAccountDDLHandler 的摘要说明
    /// </summary>
    public class BankAccountDDLHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string jsonStr;
            int bankId = 0, corpId = 0;
            if (!string.IsNullOrEmpty(context.Request["b"]))
                int.TryParse(context.Request["b"], out bankId);
            if (!string.IsNullOrEmpty(context.Request["c"]))
                int.TryParse(context.Request["c"], out corpId);
            List<BankAccount> accs = BasicDataProvider.BankAccounts;

            if (bankId > 0)
                accs = BasicDataProvider.BankAccounts.Where(a => a.BankId == bankId && a.BankAccStatus == StatusEnum.已生效).ToList();

            if (corpId > 0)
                accs = BasicDataProvider.BankAccounts.Where(a => a.CompanyId == corpId && a.BankAccStatus == StatusEnum.已生效).ToList();

            jsonStr = JsonConvert.SerializeObject(accs);
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