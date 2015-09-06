using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// CorpDDLHandler 的摘要说明
    /// </summary>
    public class CorpDDLHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            //内外部过滤
            int isSelf = -1;
            if (!string.IsNullOrEmpty(context.Request.QueryString["isSelf"]))
                int.TryParse(context.Request.QueryString["isSelf"], out isSelf);

            //合约过滤
            int contractId = 0, isOut = -1;
            if (!string.IsNullOrEmpty(context.Request.QueryString["ContractId"]))
                int.TryParse(context.Request.QueryString["ContractId"], out contractId);

            if (!string.IsNullOrEmpty(context.Request.QueryString["IsOut"]))
                int.TryParse(context.Request.QueryString["IsOut"], out isOut);

            //集团过滤
            int corpId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["cid"]))
                int.TryParse(context.Request.QueryString["cid"], out corpId);

            List<NFMT.User.Model.Corporation> corps = NFMT.User.UserProvider.Corporations;

            if (corps == null)
            {
                context.Response.Write("获取错误");
                context.Response.End();
            }

            IEnumerable<NFMT.User.Model.Corporation> ecs = null;
            if (isSelf == 1)
            {
                ecs = corps.Where(temp => temp.IsSelf == true && temp.CorpStatus == NFMT.Common.StatusEnum.已生效);

                //NFMT.User.BLL.CorporationBLL corpBLL = new NFMT.User.BLL.CorporationBLL();
                //result = corpBLL.LoadAuthSelfCorpId(user);
                //if (result.ResultStatus == 0) 
                //{
                //    List<int> authCorpIds = result.ReturnValue as List<int>;
                //    ecs = ecs.Where(temp => authCorpIds.Contains(temp.CorpId));
                //}
            }
            else if (isSelf == 0)
                ecs = corps.Where(temp => temp.IsSelf == false && temp.CorpStatus == NFMT.Common.StatusEnum.已生效);
            else
                ecs = corps.Where(temp => temp.CorpStatus == NFMT.Common.StatusEnum.已生效);

            //合约抬头过滤
            if (contractId > 0)
            {
                //获取合约抬头
                bool isFlag = false;
                if (isSelf == 1)
                    isFlag = true;

                NFMT.Contract.BLL.ContractCorporationDetailBLL corpBLL = new NFMT.Contract.BLL.ContractCorporationDetailBLL();
                result = corpBLL.LoadCorpListByContractId(user, contractId, isFlag);
                if (result.ResultStatus != 0)
                    context.Response.End();

                List<NFMT.Contract.Model.ContractCorporationDetail> contractCorps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
                var corpIds = contractCorps.Select(c => c.CorpId).ToList();
                ecs = ecs.Where(c => corpIds.Contains(c.CorpId));
            }

            //子合约抬头过滤
            int subId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["SubId"]))
                int.TryParse(context.Request.QueryString["SubId"], out subId);
            if (subId > 0)
            {
                bool isFlag = false;
                if (isSelf == 1)
                    isFlag = true;

                NFMT.Contract.BLL.SubCorporationDetailBLL subCorpBLL = new NFMT.Contract.BLL.SubCorporationDetailBLL();
                result = subCorpBLL.Load(user, subId, isFlag);
                if (result.ResultStatus != 0)
                    context.Response.End();

                List<NFMT.Contract.Model.SubCorporationDetail> subCorps = result.ReturnValue as List<NFMT.Contract.Model.SubCorporationDetail>;
                var corpIds = subCorps.Select(c => c.CorpId).ToList();
                ecs = ecs.Where(c => corpIds.Contains(c.CorpId));
            }

            //获取选中公司
            if (corpId > 0)
            {
                NFMT.User.Model.Corporation selectedCorp = corps.FirstOrDefault(temp => temp.CorpId == corpId);
                if (selectedCorp != null && selectedCorp.CorpId > 0)
                {
                    if (selectedCorp.ParentId == 0)
                        ecs = ecs.Where(c => c.CorpId == selectedCorp.CorpId);
                    else
                        ecs = ecs.Where(c => c.ParentId == selectedCorp.ParentId);
                }
            }

            if (ecs != null)
                corps = ecs.ToList();

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