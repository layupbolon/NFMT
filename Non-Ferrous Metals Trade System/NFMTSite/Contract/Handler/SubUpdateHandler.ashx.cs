using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Contract.Handler
{
    /// <summary>
    /// SubUpdateHandler 的摘要说明
    /// </summary>
    public class SubUpdateHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            string subStr = context.Request.Form["Sub"];
            string detailStr = context.Request.Form["SubDetail"];
            string priceStr = context.Request.Form["SubPrice"];
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();


            #region 子合约双方抬头

            //12.05,pekah
            //添加子合约双方抬头
            string outCorpStr = context.Request.Form["OutCorps"];
            string inCorpStr = context.Request.Form["InCorps"];

            if (string.IsNullOrEmpty(outCorpStr))
            {
                result.Message = "子合约对方抬头不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            if (string.IsNullOrEmpty(inCorpStr))
            {
                result.Message = "子合约我方抬头不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            #endregion

            if (string.IsNullOrEmpty(subStr))
            {
                result.Message = "子合约不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (string.IsNullOrEmpty(detailStr))
            {
                result.Message = "子合约明细不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (string.IsNullOrEmpty(priceStr))
            {
                result.Message = "子合约价格不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                NFMT.Contract.Model.ContractSub sub = serializer.Deserialize<NFMT.Contract.Model.ContractSub>(subStr);
                NFMT.Contract.Model.SubDetail detail = serializer.Deserialize<NFMT.Contract.Model.SubDetail>(detailStr);
                NFMT.Contract.Model.SubPrice price = serializer.Deserialize<NFMT.Contract.Model.SubPrice>(priceStr);

                List<NFMT.Contract.Model.SubCorporationDetail> outCorps = serializer.Deserialize<List<NFMT.Contract.Model.SubCorporationDetail>>(outCorpStr);
                List<NFMT.Contract.Model.SubCorporationDetail> inCorps = serializer.Deserialize<List<NFMT.Contract.Model.SubCorporationDetail>>(inCorpStr);

                NFMT.Contract.BLL.ContractSubBLL bll = new NFMT.Contract.BLL.ContractSubBLL();
                result = bll.UpdateSub(user, sub, detail, price, outCorps, inCorps);

                if (result.ResultStatus == 0)
                {
                    result.Message = "子合约更新成功";
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