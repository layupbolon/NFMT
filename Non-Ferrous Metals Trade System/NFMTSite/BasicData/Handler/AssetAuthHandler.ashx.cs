using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// AssetAuthHandler 的摘要说明
    /// </summary>
    public class AssetAuthHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            List<NFMT.Data.Model.Asset> assets = new List<NFMT.Data.Model.Asset>();

            try
            {
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Data.DAL.AssetDAL dal = new NFMT.Data.DAL.AssetDAL();
                NFMT.Common.ResultModel result = dal.LoadAssetAuth(user);
                if (result.ResultStatus == 0)
                    assets = result.ReturnValue as List<NFMT.Data.Model.Asset>;
            }
            catch
            {
                assets = new List<NFMT.Data.Model.Asset>();
            }

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(assets);
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