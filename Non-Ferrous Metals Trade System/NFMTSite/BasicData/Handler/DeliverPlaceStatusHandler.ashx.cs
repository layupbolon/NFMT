using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// DeliverPlaceStatusHandler 的摘要说明
    /// </summary>
    public class DeliverPlaceStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            context.Response.ContentType = "text/plain";

            int id = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["id"], out id) || id <= 0)
            {
                context.Response.Write("序号错误");
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["oi"], out operateId) || operateId <= 0)
            {
                context.Response.Write("操作错误");
                context.Response.End();
            }

            NFMT.Data.BLL.DeliverPlaceBLL bll = new NFMT.Data.BLL.DeliverPlaceBLL();
            NFMT.Data.Model.DeliverPlace deliverPlace = new NFMT.Data.Model.DeliverPlace()
            {
                DPId = id,
                LastModifyId = user.EmpId
            };

            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.冻结:
                    result = bll.Freeze(user, deliverPlace);
                    break;
                case NFMT.Common.OperateEnum.解除冻结:
                    result = bll.UnFreeze(user, deliverPlace);
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