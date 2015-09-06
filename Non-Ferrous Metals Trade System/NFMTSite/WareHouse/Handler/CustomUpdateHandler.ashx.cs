using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// CustomUpdateHandler 的摘要说明
    /// </summary>
    public class CustomUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string customStr = context.Request.Form["custom"];
            if (string.IsNullOrEmpty(customStr))
            {
                result.Message = "报关信息不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string rowsStr = context.Request.Form["rows"];
            if (string.IsNullOrEmpty(rowsStr))
            {
                result.Message = "明细内容不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.WareHouse.Model.CustomsClearance customsClearance = serializer.Deserialize<NFMT.WareHouse.Model.CustomsClearance>(customStr);
                List<CustomDetailInfo> customsDetailInfos = serializer.Deserialize<List<CustomDetailInfo>>(rowsStr);
                if (customsClearance == null || customsDetailInfos == null)
                {
                    result.Message = "数据错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                decimal GrossWeight = 0;
                decimal NetWeight = 0;
                List<NFMT.WareHouse.Model.CustomsDetail> customsDetails = new List<NFMT.WareHouse.Model.CustomsDetail>();
                foreach (CustomDetailInfo detail in customsDetailInfos)
                {
                    customsDetails.Add(new NFMT.WareHouse.Model.CustomsDetail()
                    {
                        CustomsApplyId = customsClearance.CustomsApplyId,
                        CustomsApplyDetailId = detail.DetailId,
                        StockId = detail.StockId,
                        GrossWeight = detail.CurGrossAmount,
                        NetWeight = detail.CurNetAmount,
                        CustomsPrice = customsClearance.CustomsPrice,
                        DeliverPlaceId = detail.DeliverPlaceId,
                        CardNo = detail.CardNo,
                        DetailStatus = NFMT.Common.StatusEnum.已生效
                    });

                    GrossWeight += detail.CurGrossAmount;
                    NetWeight += detail.CurNetAmount;
                }

                customsClearance.GrossWeight = GrossWeight;
                customsClearance.NetWeight = NetWeight;
                customsClearance.AddedValueRate = customsClearance.AddedValueRate / 100;
                customsClearance.TariffRate = customsClearance.TariffRate / 100;

                NFMT.WareHouse.BLL.CustomsClearanceBLL bll = new NFMT.WareHouse.BLL.CustomsClearanceBLL();
                result = bll.Update(user, customsClearance, customsDetails);
                if (result.ResultStatus == 0)
                {
                    result.Message = "报关修改成功";
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

        public class CustomDetailInfo
        {
            public int DetailId { get; set; }
            public int StockId { get; set; }
            public decimal CurGrossAmount { get; set; }
            public decimal CurNetAmount { get; set; }
            public int DeliverPlaceId { get; set; }
            public string CardNo { get; set; }
        }
    }
}