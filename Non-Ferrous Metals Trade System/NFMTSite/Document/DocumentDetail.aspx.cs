﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Document
{
    public partial class DocumentDetail : System.Web.UI.Page
    {
        public NFMT.Document.Model.Document curDocument = null;
        public NFMT.Document.Model.DocumentOrder curOrder = null;
        public NFMT.Common.UserModel curUser = null;
        public NFMT.Document.Model.DocumentOrderDetail curOrderDetail = null;      
        public string DocumentJsonStr = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            this.curUser = user;

            if (!IsPostBack)
            {
                string redirectUrl = "DocumentList.aspx";

                this.navigation1.Routes.Add("制单列表", redirectUrl);
                this.navigation1.Routes.Add("制单明细", string.Empty);

                int docId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out docId) || docId <= 0)
                    Response.Redirect(redirectUrl);

                //获取制单
                NFMT.Document.BLL.DocumentBLL docBLL = new NFMT.Document.BLL.DocumentBLL();
                NFMT.Common.ResultModel result = docBLL.Get(user, docId);

                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Document.Model.Document documnet = result.ReturnValue as NFMT.Document.Model.Document;
                if (documnet == null || documnet.DocumentId <= 0)
                    Response.Redirect(redirectUrl);

                this.curDocument = documnet;

                //获取指令
                NFMT.Document.BLL.DocumentOrderBLL orderBLL = new NFMT.Document.BLL.DocumentOrderBLL();
                result = orderBLL.Get(user, documnet.OrderId);

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
                //NFMT.Common.SelectModel select = orderBLL.GetOrderSelectedSelect(1, 100, "dos.DetailId desc", order.OrderId, true);
                //result = orderBLL.Load(user, select);
                //int totalRows = result.AffectCount;
                //System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                //this.OrderJsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                NFMT.Common.SelectModel select = docBLL.GetDocumnetStocksSelect(1, 100, "ds.DetailId desc", documnet.DocumentId);
                result = orderBLL.Load(user, select);
                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                this.DocumentJsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(documnet);
                this.hidModel.Value = json;

                //attach
                this.attach1.BusinessIdValue = this.curOrder.OrderId;
            }
        }
    }
}