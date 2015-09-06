using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockMoveApplyCreateHandler 的摘要说明
    /// </summary>
    public class StockMoveApplyCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string stockMoveStr = context.Request.Form["stockMove"];
            if (string.IsNullOrEmpty(stockMoveStr))
            {
                result.Message = "移库信息不能为空";
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
                List<NFMT.WareHouse.Model.StockMoveApplyDetail> stockMoveApplyDetails = serializer.Deserialize<List<NFMT.WareHouse.Model.StockMoveApplyDetail>>(stockMoveStr);
                NFMT.Operate.Model.Apply apply = serializer.Deserialize<NFMT.Operate.Model.Apply>(applyStr);
                if (apply == null || stockMoveApplyDetails == null || !stockMoveApplyDetails.Any())
                {
                    result.Message = "数据错误";
                    result.ResultStatus = -1;
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                apply.ApplyType = NFMT.Operate.ApplyType.StockMoveApply;
                apply.EmpId = user.EmpId;
                apply.ApplyTime = DateTime.Now;


                NFMT.WareHouse.BLL.StockMoveApplyBLL bll = new NFMT.WareHouse.BLL.StockMoveApplyBLL();
                result = bll.StockMoveApplyCreateHandle(user, apply, stockMoveApplyDetails);
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