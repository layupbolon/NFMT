using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Document
{
    public partial class OrderReplaceUpdate : System.Web.UI.Page
    {
        public NFMT.Document.Model.DocumentOrder curCommercialOrder = null;
        public NFMT.Document.Model.DocumentOrder curReplaceOrder = null;
        public NFMT.Common.UserModel curUser = null;
        public NFMT.Document.Model.DocumentOrderDetail curOrderDetail = null;
        public int ReplaceOrderType = 0;

        public string JsonReplaceStr = string.Empty;
        public string JsonCommercialStr = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            this.curUser = user;

            if (!IsPostBack)
            {
                string redirectUrl = "OrderReplaceList.aspx";

                this.navigation1.Routes.Add("替临制单列表", redirectUrl);
                this.navigation1.Routes.Add("替临制单指令修改", string.Empty);

                this.ReplaceOrderType = (int)NFMT.Document.OrderTypeEnum.替临制单指令;

                int orderId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out orderId) || orderId <= 0)
                    Response.Redirect(redirectUrl);

                //获取替临指令
                NFMT.Document.BLL.DocumentOrderBLL orderBLL = new NFMT.Document.BLL.DocumentOrderBLL();
                NFMT.Common.ResultModel result = orderBLL.Get(user, orderId);

                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Document.Model.DocumentOrder order = result.ReturnValue as NFMT.Document.Model.DocumentOrder;
                if (order == null || order.OrderId <= 0)
                    Response.Redirect(redirectUrl);

                this.curReplaceOrder = order;

                NFMT.Document.BLL.DocumentOrderDetailBLL detailBLL = new NFMT.Document.BLL.DocumentOrderDetailBLL();
                result = detailBLL.GetByOrderId(user, order.OrderId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Document.Model.DocumentOrderDetail orderDetail = result.ReturnValue as NFMT.Document.Model.DocumentOrderDetail;
                if (orderDetail == null || orderDetail.DetailId <= 0)
                    Response.Redirect(redirectUrl);

                this.curOrderDetail = orderDetail;

                //获取临票指令
                result = orderBLL.Get(user, order.CommercialId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Document.Model.DocumentOrder comOrder = result.ReturnValue as NFMT.Document.Model.DocumentOrder;
                if (comOrder == null || comOrder.OrderId <= 0)
                    Response.Redirect(redirectUrl);

                this.curCommercialOrder = comOrder;

                //替临指令库存
                NFMT.Common.SelectModel select = orderBLL.GetReplaceStocksSelect(1, 100, "dos.DetailId desc", order.CommercialId, false);
                result = orderBLL.Load(user, select);
                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                this.JsonReplaceStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                //临票指令库存
                select = orderBLL.GetReplaceStocksSelect(1, 100, "dos.DetailId desc", order.CommercialId, true);
                result = orderBLL.Load(user, select);
                dt = result.ReturnValue as System.Data.DataTable;
                this.JsonCommercialStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                //attach
                this.attach1.BusinessIdValue = this.curReplaceOrder.OrderId;
            }
        }
    }
}