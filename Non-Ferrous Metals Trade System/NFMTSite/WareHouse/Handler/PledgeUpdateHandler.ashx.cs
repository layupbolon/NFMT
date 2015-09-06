using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// PledgeUpdateHandler 的摘要说明
    /// </summary>
    public class PledgeUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string pledgeStr = context.Request.Form["pledge"];
            if (string.IsNullOrEmpty(pledgeStr))
            {
                context.Response.Write("质押信息不能为空");
                context.Response.End();
            }

            string rowsStr = context.Request.Form["rows"];
            if (string.IsNullOrEmpty(rowsStr))
            {
                context.Response.Write("质押明细信息不能为空");
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.WareHouse.Model.Pledge pledge = serializer.Deserialize<NFMT.WareHouse.Model.Pledge>(pledgeStr);
                List<NFMT.WareHouse.Model.PledgeApplyDetail> details = serializer.Deserialize<List<NFMT.WareHouse.Model.PledgeApplyDetail>>(rowsStr);
                if (pledge == null || details == null || !details.Any())
                {
                    context.Response.Write("数据错误");
                    context.Response.End();
                }

                List<NFMT.WareHouse.Model.PledgeDetial> pledgeDetials = new List<NFMT.WareHouse.Model.PledgeDetial>();
                foreach (NFMT.WareHouse.Model.PledgeApplyDetail detail in details)
                {
                    pledgeDetials.Add(new NFMT.WareHouse.Model.PledgeDetial()
                    {
                        PledgeApplyDetailId = detail.DetailId,
                        StockId = detail.StockId,
                        GrossAmount = detail.ApplyQty,
                        Unit = detail.UintId,
                        PledgePrice = detail.PledgePrice,
                        CurrencyId = detail.CurrencyId
                    });
                }

                pledge.Pledger = user.EmpId;
                pledge.PledgeTime = DateTime.Now;

                NFMT.WareHouse.BLL.PledgeBLL bll = new NFMT.WareHouse.BLL.PledgeBLL();
                result = bll.PledgeUpdateHandle(user, pledge, pledgeDetials);

                if (result.ResultStatus == 0)
                    result.Message = "质押修改成功";
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