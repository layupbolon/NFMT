using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// CorpDetailUpdateAttachIdHandler 的摘要说明
    /// </summary>
    public class CorpDetailUpdateAttachIdHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                int corpDetailId = 0;
                if (string.IsNullOrEmpty(context.Request.QueryString["corpDetailId"]) || !int.TryParse(context.Request.QueryString["corpDetailId"], out corpDetailId) || corpDetailId <= 0)
                {
                    context.Response.Write("公司明细序号错误");
                    context.Response.End();
                }

                int attachId = 0;
                if (string.IsNullOrEmpty(context.Request.QueryString["attachId"]) || !int.TryParse(context.Request.QueryString["attachId"], out attachId) || attachId <= 0)
                {
                    context.Response.Write("附件序号错误");
                    context.Response.End();
                }

                int attachType = 0;
                if (string.IsNullOrEmpty(context.Request.QueryString["attachType"]) || !int.TryParse(context.Request.QueryString["attachType"], out attachType) || attachType <= 0)
                {
                    context.Response.Write("附件类型错误");
                    context.Response.End();
                }

                NFMT.User.BLL.CorporationDetailBLL bll = new NFMT.User.BLL.CorporationDetailBLL();
                //result = bll.UpdateAttachId(user, corpDetailId, attachId, (NFMT.User.BLL.CorporationDetailBLL.CorpDetailAttachEnum)attachType);
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
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