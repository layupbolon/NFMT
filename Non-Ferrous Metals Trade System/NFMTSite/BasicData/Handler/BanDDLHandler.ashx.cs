using System.Linq;
using System.Web;
using Newtonsoft.Json;
using NFMT.Common;
using NFMT.Data;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BanDDLHandler 的摘要说明
    /// </summary>
    public class BanDDLHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            //NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            //NFMT.Data.BLL.BankBLL bll = new NFMT.Data.BLL.BankBLL();
            //var result = bll.Load(user);

            //context.Response.ContentType = "application/json; charset=utf-8";
            //if (result.ResultStatus != 0)
            //{
            //    context.Response.Write(result.Message);
            //    context.Response.End();
            //}

            //List<NFMT.Data.Model.Bank> dt = result.ReturnValue as List<NFMT.Data.Model.Bank>;

            string jsonStr =
                JsonConvert.SerializeObject(BasicDataProvider.Banks.Where(a => a.BankStatus == StatusEnum.已生效));
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