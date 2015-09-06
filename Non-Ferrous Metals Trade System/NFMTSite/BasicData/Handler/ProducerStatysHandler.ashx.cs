using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ProducerStatysHandler 的摘要说明
    /// </summary>
    public class ProducerStatysHandler : IHttpHandler
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

            NFMT.Data.BLL.ProducerBLL pd = new NFMT.Data.BLL.ProducerBLL();
            NFMT.Data.Model.Producer producer = new NFMT.Data.Model.Producer()
            {
                ProducerId = id,
                LastModifyId = user.EmpId
            };

            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.冻结:
                    result = pd.Freeze(user, producer);
                    break;
                case NFMT.Common.OperateEnum.解除冻结:
                    result = pd.UnFreeze(user, producer);
                    break;
            }

            context.Response.Write(result.Message);
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