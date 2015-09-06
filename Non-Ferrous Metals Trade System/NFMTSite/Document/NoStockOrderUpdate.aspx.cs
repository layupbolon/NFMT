using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Document
{
    public partial class NoStockOrderUpdate : System.Web.UI.Page
    {
        public NFMT.Contract.Model.ContractSub curSub = null;
        public NFMT.Contract.Model.Contract curContract = null;
        public NFMT.Document.Model.DocumentOrder curOrder = null;
        public NFMT.Data.Model.Currency curCurrency = null;
        public NFMT.Document.Model.DocumentOrderDetail curOrderDetail = null;
        public string JsonStr = string.Empty;
        public NFMT.Data.Model.MeasureUnit curUnit = null;
        public NFMT.Data.Model.Asset curAsset = null;
        public NFMT.Common.UserModel curUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                this.curUser = user;

                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                string redirectUrl = "OrderList.aspx";
                this.navigation1.Routes.Add("制单指令列表", redirectUrl);
                this.navigation1.Routes.Add("制单指令修改", string.Empty);

                int orderId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out orderId) || orderId <= 0)
                    Response.Redirect(redirectUrl);

                //获取制单指令
                NFMT.Document.BLL.DocumentOrderBLL orderBLL = new NFMT.Document.BLL.DocumentOrderBLL();
                result = orderBLL.Get(user, orderId);
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

                NFMT.Contract.BLL.ContractSubBLL subBll = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBll.Get(user, order.SubId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.ContractId == 0)
                    Response.Redirect(redirectUrl);
                this.curSub = sub;

                NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
                result = bll.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.Contract con = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (con == null || con.ContractId == 0)
                    Response.Redirect(redirectUrl);
                this.curContract = con;

                NFMT.Data.Model.Asset ass = NFMT.Data.BasicDataProvider.Assets.First(temp => temp.AssetId == con.AssetId);
                this.curAsset = ass;

                NFMT.Data.Model.MeasureUnit muContract = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == con.UnitId);
                this.curUnit = muContract;

                NFMT.Data.Model.Currency cur = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(temp => temp.CurrencyId == sub.SettleCurrency);
                this.curCurrency = cur;

                NFMT.Data.Model.MeasureUnit muSub = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == sub.UnitId);

                this.contractExpander1.CurContract = this.curContract;
                this.contractExpander1.CurContractSub = this.curSub;
                this.contractExpander1.RedirectUrl = redirectUrl;    

                NFMT.Common.SelectModel select = orderBLL.GetNoStockOrderSelect(1, 100, "dos.DetailId desc", order.OrderId);
                result = orderBLL.Load(user, select);
                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                if (dt != null)
                {
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        if (ass != null)
                            dr["AssetName"] = ass.AssetName;
                        if (muSub != null)
                            dr["MUName"] = muSub.MUName;
                    }
                }

                this.JsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                //attach
                this.attach1.BusinessIdValue = this.curOrder.OrderId;
            }
        }
    }
}