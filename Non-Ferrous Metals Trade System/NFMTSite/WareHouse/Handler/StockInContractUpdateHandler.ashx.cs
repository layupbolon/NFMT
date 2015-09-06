using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockInContractUpdateHandler 的摘要说明
    /// </summary>
    public class StockInContractUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int subId = 0;
            int refId = 0;

            if (string.IsNullOrEmpty(context.Request.Form["SubId"]) || !int.TryParse(context.Request.Form["SubId"], out subId))
            {
                result.Message = "未选择分配合约";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            if (string.IsNullOrEmpty(context.Request.Form["RefId"]) || !int.TryParse(context.Request.Form["RefId"], out refId))
            {
                result.Message = "分配序号不存在";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {

                NFMT.WareHouse.BLL.ContractStockIn_BLL bll = new NFMT.WareHouse.BLL.ContractStockIn_BLL();
                result = bll.Update(user, subId, refId);

                if (result.ResultStatus == 0)
                {
                    result.Message = "入库分配修改成功";
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
    }
}