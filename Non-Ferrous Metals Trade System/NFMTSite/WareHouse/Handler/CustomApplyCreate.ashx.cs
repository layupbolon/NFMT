using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// CustomApplyCreate 的摘要说明
    /// </summary>
    public class CustomApplyCreate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string applyStr = context.Request.Form["apply"];
            if (string.IsNullOrEmpty(applyStr))
            {
                context.Response.Write("申请内容不能为空");
                context.Response.End();
            }

            string customApplyStr = context.Request.Form["customApply"];
            if (string.IsNullOrEmpty(customApplyStr))
            {
                context.Response.Write("报关申请内容不能为空");
                context.Response.End();
            }

            string rowsStr = context.Request.Form["rows"];
            if (string.IsNullOrEmpty(rowsStr))
            {
                context.Response.Write("明细内容不能为空");
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Operate.Model.Apply apply = serializer.Deserialize<NFMT.Operate.Model.Apply>(applyStr);
                NFMT.WareHouse.Model.CustomsClearanceApply customsClearanceApply = serializer.Deserialize<NFMT.WareHouse.Model.CustomsClearanceApply>(customApplyStr);
                List<CustomsApplyDetail> details = serializer.Deserialize<List<CustomsApplyDetail>>(rowsStr);
                if (apply == null || customsClearanceApply == null || details == null)
                {
                    context.Response.Write("数据错误");
                    context.Response.End();
                }

                apply.ApplyType = NFMT.Operate.ApplyType.CustomApply;
                apply.EmpId = user.EmpId;
                apply.ApplyTime = DateTime.Now;

                decimal GrossWeight = 0;
                decimal NetWeight = 0;
                List<NFMT.WareHouse.Model.CustomsApplyDetail> customsApplyDetails = new List<NFMT.WareHouse.Model.CustomsApplyDetail>();
                foreach (CustomsApplyDetail detail in details)
                {
                    customsApplyDetails.Add(new NFMT.WareHouse.Model.CustomsApplyDetail()
                    {
                        StockId = detail.StockId,
                        GrossWeight = detail.CurGrossAmount,
                        NetWeight = detail.CurNetAmount,
                        CustomsPrice = customsClearanceApply.CustomsPrice,
                        DetailStatus = NFMT.Common.StatusEnum.已生效
                    });

                    GrossWeight += detail.CurGrossAmount;
                    NetWeight += detail.CurNetAmount;
                }

                customsClearanceApply.GrossWeight = GrossWeight;
                customsClearanceApply.NetWeight = NetWeight;

                NFMT.WareHouse.BLL.CustomsClearanceApplyBLL bll = new NFMT.WareHouse.BLL.CustomsClearanceApplyBLL();
                result = bll.Create(user, apply, customsClearanceApply, customsApplyDetails);
                if (result.ResultStatus == 0)
                {
                    result.Message = "报关申请新增成功";
                }
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

        public class CustomsApplyDetail
        {
            public int StockId { get; set; }
            public decimal CurGrossAmount { get; set; }
            public decimal CurNetAmount { get; set; }
        }
    }
}