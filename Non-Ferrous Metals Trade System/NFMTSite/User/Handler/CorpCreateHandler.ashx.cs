using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// CorpCreateHandler 的摘要说明
    /// </summary>
    public class CorpCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";

            int blocId = 0;
            string taxPlayer = context.Request.Form["taxPlayer"];
            string corpCode = context.Request.Form["corpCode"];
            string corpName = context.Request.Form["corpName"];
            string corpEName = context.Request.Form["corpEName"];
            string corpFName = context.Request.Form["corpFName"];
            string corpFEName = context.Request.Form["corpFEName"];
            string corpAddress = context.Request.Form["corpAddress"];
            string corpEAddress = context.Request.Form["corpEAddress"];
            string corpTel = context.Request.Form["corpTel"];
            string corpFax = context.Request.Form["corpFax"];
            string corpZip = context.Request.Form["corpZip"];
            int corpType = 0;

            string resultStr = "添加失败";
            if (string.IsNullOrEmpty(corpCode))
            {
                resultStr = "企业代码不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (string.IsNullOrEmpty(corpName))
            {
                resultStr = "企业名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!string.IsNullOrEmpty(context.Request.Form["blocId"]))
            {
                if (!int.TryParse(context.Request.Form["blocId"], out blocId))
                {
                    resultStr = "所属集团转换错误";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }
            }

            if (string.IsNullOrEmpty(context.Request.Form["taxPlayer"]))
            {
                resultStr = "纳税人识别号不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!string.IsNullOrEmpty(context.Request.Form["corpType"]))
            {
                if (!int.TryParse(context.Request.Form["corpType"], out corpType))
                {
                    resultStr = "企业类型转换错误";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }
            }

            NFMT.Common.ResultModel result = null;
            NFMT.User.Model.Bloc bloc = null;
            NFMT.User.BLL.BlocBLL blocBLL = new NFMT.User.BLL.BlocBLL();

            if (blocId != 0)
            {
                result = blocBLL.Get(user, blocId);
                if (result.ResultStatus != 0)
                {
                    resultStr = "获取集团错误";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }

                bloc = result.ReturnValue as NFMT.User.Model.Bloc;
            }

            NFMT.User.Model.Corporation corp = new NFMT.User.Model.Corporation()
            {
                ParentId = blocId,
                CorpCode = corpCode,
                CorpName = corpName,
                CorpEName = corpEName,
                TaxPayerId = taxPlayer,
                CorpFullName = corpFName,
                CorpFullEName = corpFEName,
                CorpAddress = corpAddress,
                CorpEAddress = corpEAddress,
                CorpTel = corpTel,
                CorpFax = corpFax,
                CorpZip = corpZip,
                CorpType = corpType,
                IsSelf = bloc != null ? bloc.IsSelf : false
            };

            NFMT.User.BLL.CorporationBLL corpBLL = new NFMT.User.BLL.CorporationBLL();
            result = corpBLL.Insert(user, corp);
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