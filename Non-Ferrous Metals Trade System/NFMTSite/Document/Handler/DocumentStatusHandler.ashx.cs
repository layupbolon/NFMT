using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Document.Handler
{
    /// <summary>
    /// DocumentStatusHandler 的摘要说明
    /// </summary>
    public class DocumentStatusHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            context.Response.ContentType = "text/plain";
            int id = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["id"], out id) || id <= 0)
            {
                result.Message = "序号错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["oi"], out operateId) || operateId <= 0)
            {
                result.Message = "操作错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            NFMT.Document.BLL.DocumentBLL bll = new NFMT.Document.BLL.DocumentBLL();
            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.作废:
                    result = bll.Invalid(user, id);
                    break;
                case NFMT.Common.OperateEnum.撤返:
                    result = bll.GoBack(user, id);
                    break;
                case NFMT.Common.OperateEnum.交单:
                    DateTime presentDate = DateTime.MinValue;
                    if (string.IsNullOrEmpty(context.Request.Form["PresentDate"]) || !DateTime.TryParse(context.Request.Form["PresentDate"], out presentDate) || presentDate <= NFMT.Common.DefaultValue.DefaultTime)
                    {
                        result.Message = "交单日期错误";
                        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                        context.Response.End();
                    }

                    result = bll.Present(user, id, presentDate);
                    break;
                case NFMT.Common.OperateEnum.银行承兑:
                    DateTime acceptanceDate = DateTime.MinValue;
                    if (string.IsNullOrEmpty(context.Request.Form["AcceptanceDate"]) || !DateTime.TryParse(context.Request.Form["AcceptanceDate"], out acceptanceDate) || acceptanceDate <= NFMT.Common.DefaultValue.DefaultTime)
                    {
                        result.Message = "承兑日期错误";
                        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                        context.Response.End();
                    }
                    result = bll.Acceptan(user, id,acceptanceDate);
                    break;
                case NFMT.Common.OperateEnum.单据退回:
                    result = bll.BackDocument(user, id);
                    break;
            }

            if (result.ResultStatus == 0)
                result.Message = string.Format("{0}成功", operateEnum.ToString());

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