using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// AuthSelfCorpHandler 的摘要说明
    /// </summary>
    public class AuthSelfCorpHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
           
            NFMT.User.BLL.CorporationBLL bll = new NFMT.User.BLL.CorporationBLL();
            result = bll.LoadAuthSelfCorp(user);

            List<NFMT.User.Model.Corporation> corps = new List<NFMT.User.Model.Corporation>();

            if (result.ResultStatus == 0)
            {
                corps = result.ReturnValue as List<NFMT.User.Model.Corporation>;
            }

            //合约过滤
            int contractId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["ContractId"]))
                int.TryParse(context.Request.QueryString["ContractId"], out contractId);

            //合约抬头过滤
            IEnumerable<NFMT.User.Model.Corporation> ecs = new List<NFMT.User.Model.Corporation>();
            if (contractId > 0)
            {
                NFMT.Contract.BLL.ContractCorporationDetailBLL corpBLL = new NFMT.Contract.BLL.ContractCorporationDetailBLL();
                result = corpBLL.LoadCorpListByContractId(user, contractId,true);
                if (result.ResultStatus != 0)
                    context.Response.End();

                List<NFMT.Contract.Model.ContractCorporationDetail> contractCorps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
                var corpIds = contractCorps.Select(c => c.CorpId).ToList();
                ecs = corps.Where(c => corpIds.Contains(c.CorpId));
                corps = ecs.ToList();
            }

            int subId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["SubId"]))
                int.TryParse(context.Request.QueryString["SubId"], out subId);
            if (subId > 0)
            {
                NFMT.Contract.BLL.SubCorporationDetailBLL subCorpBLL = new NFMT.Contract.BLL.SubCorporationDetailBLL();
                result = subCorpBLL.Load(user, subId, true);
                if (result.ResultStatus != 0)
                    context.Response.End();

                List<NFMT.Contract.Model.SubCorporationDetail> subCorps = result.ReturnValue as List<NFMT.Contract.Model.SubCorporationDetail>;
                var corpIds = subCorps.Select(c => c.CorpId).ToList();
                ecs = corps.Where(c => corpIds.Contains(c.CorpId));
                corps = ecs.ToList();
            }

            
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(corps);
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