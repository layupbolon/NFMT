using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// AssetGreateHandler 的摘要说明
    /// </summary>
    public class AssetGreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string assetName = context.Request.Form["assetName"];
            string muId = context.Request.Form["muId"];
            string hand = context.Request.Form["hands"];

            string resultStr = "添加失败";

            if (string.IsNullOrEmpty(assetName))
            {
                resultStr = "品种名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(muId))
            {
                resultStr = "品种主要计量单位不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(hand))
            {
                resultStr = "每手重量不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            //录入品种种（add）
            NFMT.Data.BLL.AssetBLL bll = new NFMT.Data.BLL.AssetBLL();
            NFMT.Data.Model.Asset asset = new NFMT.Data.Model.Asset();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            asset.CreatorId = user.EmpId;
            asset.AssetName = assetName;
            asset.MUId = Convert.ToInt32(muId);
            asset.AssetStatus = NFMT.Common.StatusEnum.已录入;
            asset.AmountPerHand = Convert.ToInt32(hand);

            NFMT.Common.ResultModel result = bll.Insert(user, asset);
            resultStr = result.Message;
            context.Response.Write(resultStr);
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