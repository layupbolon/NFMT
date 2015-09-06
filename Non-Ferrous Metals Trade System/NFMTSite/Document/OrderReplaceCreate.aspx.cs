using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Document
{
    public partial class OrderReplaceCreate : System.Web.UI.Page
    {
        public NFMT.Document.Model.DocumentOrder curOrder = null;
        public NFMT.Common.UserModel curUser = null;
        public NFMT.Document.Model.DocumentOrderDetail curOrderDetail = null;
        public string JsonStr = string.Empty;
        public int ReplaceOrderType = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            this.curUser = user;

            if (!IsPostBack)
            {
                string redirectUrl = "OrderReplaceList.aspx";

                this.navigation1.Routes.Add("替临制单列表", redirectUrl);
                this.navigation1.Routes.Add("已承兑临票制单指令列表", "OrderCommercialList.aspx");
                this.navigation1.Routes.Add("替临制单指令新增", string.Empty);

                this.ReplaceOrderType = (int)NFMT.Document.OrderTypeEnum.替临制单指令;

                int orderId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out orderId) || orderId <= 0)
                    Response.Redirect(redirectUrl);

                //获取临票指令
                NFMT.Document.BLL.DocumentOrderBLL orderBLL = new NFMT.Document.BLL.DocumentOrderBLL();
                NFMT.Common.ResultModel result = orderBLL.Get(user, orderId);

                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Document.Model.DocumentOrder order = result.ReturnValue as NFMT.Document.Model.DocumentOrder;
                if (order == null || order.OrderId <= 0)
                    Response.Redirect(redirectUrl);

                this.curOrder = order;

                NFMT.Document.BLL.DocumentOrderDetailBLL detailBLL = new NFMT.Document.BLL.DocumentOrderDetailBLL();
                result = detailBLL.GetByOrderId(user, order.OrderId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Document.Model.DocumentOrderDetail orderDetail = result.ReturnValue as NFMT.Document.Model.DocumentOrderDetail;
                if (orderDetail == null || orderDetail.DetailId <= 0)
                    Response.Redirect(redirectUrl);

                this.curOrderDetail = orderDetail;

                //指令库存
                NFMT.Common.SelectModel select = orderBLL.GetComOrderStocksSelect(1, 100, "dos.DetailId desc", order.OrderId, true);
                result = orderBLL.Load(user, select);
                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

                if (dt != null)
                {
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        if (dr["InvoiceNo"] != null && dr["InvoiceNo"] != DBNull.Value)
                        {
                            string invoiceNo = dr["InvoiceNo"].ToString();
                            invoiceNo = string.Format("{0}{1}",invoiceNo.Substring(0, invoiceNo.Length - 1),"P");

                        }
                    }
                }

                this.JsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());
            }
        }
    }
}