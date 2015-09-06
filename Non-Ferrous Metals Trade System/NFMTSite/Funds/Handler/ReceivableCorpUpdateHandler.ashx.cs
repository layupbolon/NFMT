using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// ReceivableCorpUpdateHandler 的摘要说明
    /// </summary>
    public class ReceivableCorpUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string r = context.Request.Form["Allot"];
            if (string.IsNullOrEmpty(r))
            {
                context.Response.Write("分配信息不能为空");
                context.Response.End();
            }
            string memo = context.Request.Form["memo"];            

            int allotId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["id"]) || !int.TryParse(context.Request.Form["id"], out allotId) || allotId <= 0)
            {
                context.Response.Write("序号信息错误");
                context.Response.End();
            }

            bool isShare = false;
            if (string.IsNullOrEmpty(context.Request.Form["isShare"]) || !bool.TryParse(context.Request.Form["isShare"], out isShare))
            {
                context.Response.Write("是否共享信息错误");
                context.Response.End();
            }


            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<AllotInfo> allotInfos = serializer.Deserialize<List<AllotInfo>>(r);

                if (allotInfos == null)
                {
                    context.Response.Write("分配信息错误");
                    context.Response.End();
                }

                NFMT.Funds.Model.ReceivableAllot allot = new NFMT.Funds.Model.ReceivableAllot();
                allot.AllotDesc = memo;
                allot.AllotTime = DateTime.Now;
                allot.EmpId = user.EmpId;
                allot.ReceivableAllotId = allotId;

                NFMT.Funds.Model.CorpReceivable corpRec = new NFMT.Funds.Model.CorpReceivable();
                corpRec.IsShare = isShare;

                List<NFMT.Funds.Model.RecAllotDetail> details = new List<NFMT.Funds.Model.RecAllotDetail>();
                foreach (AllotInfo info in allotInfos)
                {
                    NFMT.Funds.Model.RecAllotDetail detail = new NFMT.Funds.Model.RecAllotDetail();

                    detail.AllotBala = info.CanAllotBala;
                    detail.DetailStatus = NFMT.Common.StatusEnum.已生效;
                    detail.RecId = info.ReceivableId;

                    details.Add(detail);
                }

                NFMT.Funds.BLL.ReceivableAllotBLL bll = new NFMT.Funds.BLL.ReceivableAllotBLL();
                result = bll.UpdateCorp(user, allot, details, corpRec);
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            if (result.ResultStatus == 0)
                result.Message = "修改成功";

            context.Response.Write(result.Message);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class AllotInfo
        {
            public int ReceivableId { get; set; }
            public decimal CanAllotBala { get; set; }
            public decimal AllotBala { get; set; }
        }
    }
}