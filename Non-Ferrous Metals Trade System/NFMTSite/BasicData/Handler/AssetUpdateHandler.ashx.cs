using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// AssetUpdateHandler 的摘要说明
    /// </summary>
    public class AssetUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            context.Response.ContentType = "text/plain";

            string assetName = context.Request.Form["assetName"];
            string muid = context.Request.Form["muid"];
            int id = 0;
            int assetStatus = 0;
            string resultStr = "修改失败";

            int amountPerHand = 0;
            if (string.IsNullOrEmpty(context.Request.Form["hands"]) || !int.TryParse(context.Request.Form["hands"], out amountPerHand) || amountPerHand <= 0)
            {
                resultStr = "每手重量错误";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                resultStr = "id未知";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["id"], out id))
            {
                resultStr = "id未知";
                context.Response.Write(resultStr);
                context.Response.End();
            }


            if (string.IsNullOrEmpty(assetName))
            {
                resultStr = "币种名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(muid))
            {
                resultStr = "基本单位名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            NFMT.Data.BLL.AssetBLL cyBLL = new NFMT.Data.BLL.AssetBLL();
            NFMT.Common.ResultModel result = cyBLL.Get(user, id);
            if (result.ResultStatus != 0)
            {
                resultStr = "获取数据错误";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["assetStatus"], out assetStatus) || assetStatus <= 0)
            {
                resultStr = "必须选择数据状态";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            NFMT.Data.Model.Asset cy = result.ReturnValue as NFMT.Data.Model.Asset;
            if (cy != null)
            {
                cy.AssetName = assetName;
                cy.MUId =Convert.ToInt32(muid);
                cy.AssetStatus = (NFMT.Common.StatusEnum)assetStatus;
                cy.AmountPerHand = amountPerHand;
                result = cyBLL.Update(user, cy);
                resultStr = result.Message;
            }

            context.Response.Write(resultStr);
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