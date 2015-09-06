using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Contract.Handler
{
    /// <summary>
    /// ContractJsonHandler 的摘要说明
    /// </summary>
    public class ContractJsonHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int id = 0;
            if (string.IsNullOrEmpty(context.Request.Params["id"]) || !int.TryParse(context.Request.Params["id"], out id) || id <= 0)
            {
                result.Message = "合约不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
            result = bll.Get(user, id);
            if (result.ResultStatus != 0)
            {
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            NFMT.Contract.Model.Contract model = result.ReturnValue as NFMT.Contract.Model.Contract;
            if (model== null || model.Id<=0)
            {
                result.ResultStatus = -1;
                result.Message = "获取实体失败";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            result.ReturnValue = serializer.Serialize(model);

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