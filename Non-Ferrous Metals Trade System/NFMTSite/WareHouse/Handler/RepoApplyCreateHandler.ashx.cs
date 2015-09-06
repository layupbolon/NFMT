using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// RepoApplyCreateHandler 的摘要说明
    /// </summary>
    public class RepoApplyCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string repoStr = context.Request.Form["repo"];
            if (string.IsNullOrEmpty(repoStr))
            {
                result.Message = "回购信息不能为空";
                result.ResultStatus = -1;
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string applyStr = context.Request.Form["apply"];
            if (string.IsNullOrEmpty(applyStr))
            {
                result.Message = "申请信息不能为空";
                result.ResultStatus = -1;
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<NFMT.WareHouse.Model.RepoApplyDetail> repoApplyDetails = serializer.Deserialize<List<NFMT.WareHouse.Model.RepoApplyDetail>>(repoStr);
                NFMT.Operate.Model.Apply apply = serializer.Deserialize<NFMT.Operate.Model.Apply>(applyStr);
                if (apply == null || repoApplyDetails == null || !repoApplyDetails.Any())
                {
                    result.Message = "数据错误";
                    result.ResultStatus = -1;
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                apply.ApplyType = NFMT.Operate.ApplyType.RepoApply;
                apply.EmpId = user.EmpId;
                apply.ApplyTime = DateTime.Now;

                NFMT.WareHouse.BLL.RepoApplyBLL bll = new NFMT.WareHouse.BLL.RepoApplyBLL();
                result = bll.RepoApplyCreateHandle(user, apply, new NFMT.WareHouse.Model.RepoApply(), repoApplyDetails);
                if (result.ResultStatus == 0)
                    result.Message = "新增成功";
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

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