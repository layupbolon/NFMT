using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Document
{
    public partial class OrderReplaceDetail : System.Web.UI.Page
    {
        public NFMT.Document.Model.DocumentOrder curOrder = null;
        public NFMT.Document.Model.DocumentOrder curReplaceOrder = null;
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
                this.navigation1.Routes.Add("替临制单指令新增", string.Empty);

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

                //获取对应临票制单指令
                result = orderBLL.Get(user, order.CommercialId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Document.Model.DocumentOrder comOrder = result.ReturnValue as NFMT.Document.Model.DocumentOrder;
                if (comOrder == null || comOrder.OrderId <= 0)
                    Response.Redirect(redirectUrl);

                this.curOrder = comOrder;

                //指令库存
                NFMT.Common.SelectModel select = orderBLL.GetOrderSelectedSelect(1, 100, "dos.DetailId desc", order.OrderId);
                result = orderBLL.Load(user, select);
                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;                
                this.JsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(order);
                this.hidModel.Value = json;

                //attach
                this.attach1.BusinessIdValue = this.curReplaceOrder.OrderId;
            }
        }
    }
}