using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// SplitDocStatusHandler 的摘要说明
    /// </summary>
    public class SplitDocStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int splitDocId = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["id"], out splitDocId) || splitDocId <= 0)
            {
                result.Message = "拆单序号错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["oi"], out operateId) || operateId <= 0)
            {
                result.Message = "操作错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;

            NFMT.WareHouse.BLL.SplitDocBLL bll = new NFMT.WareHouse.BLL.SplitDocBLL();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.撤返:
                    result = bll.GoBack(user, splitDocId);
                    break;
                case NFMT.Common.OperateEnum.作废:
                    result = bll.Invalid(user, splitDocId);
                    break;
                case NFMT.Common.OperateEnum.执行完成:
                    result = bll.Complete(user, splitDocId);
                    break;
                case NFMT.Common.OperateEnum.执行完成撤销:
                    result = bll.CompleteCancel(user, splitDocId);
                    break;
                case NFMT.Common.OperateEnum.关闭:
                    result = bll.Close(user, splitDocId);
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