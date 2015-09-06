using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockOutPrint : System.Web.UI.Page
    {
        public NFMT.WareHouse.Model.StockOut stockOut;
        public NFMT.WareHouse.Model.StockOutApply stockOutApply;
        public NFMT.Common.UserModel user;
        public List<NFMT.WareHouse.Model.StockOutDetail> details;
        public string DPName = string.Empty;
        public string No = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 44, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.查询 });

                user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                string redirectUrl = string.Format("{0}WareHouse/StockOutList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

                int stockOutId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out stockOutId) || stockOutId <= 0)
                    this.Page.WarmAlert("参数错误", redirectUrl);

                //获取出库
                NFMT.WareHouse.BLL.StockOutBLL stockOutBLL = new NFMT.WareHouse.BLL.StockOutBLL();
                result = stockOutBLL.Get(user, stockOutId);
                if (result.ResultStatus != 0)
                    this.Page.WarmAlert(result.Message, redirectUrl);

                stockOut = result.ReturnValue as NFMT.WareHouse.Model.StockOut;
                if (stockOut == null)
                    this.Page.WarmAlert("获取出库失败", redirectUrl);

                //获取出库申请
                NFMT.WareHouse.BLL.StockOutApplyBLL stockOutApplyBLL = new NFMT.WareHouse.BLL.StockOutApplyBLL();
                result = stockOutApplyBLL.Get(user, stockOut.StockOutApplyId);
                if (result.ResultStatus != 0)
                    this.Page.WarmAlert(result.Message, redirectUrl);

                stockOutApply = result.ReturnValue as NFMT.WareHouse.Model.StockOutApply;
                if (stockOutApply == null)
                    this.Page.WarmAlert("获取出库申请失败", redirectUrl);

                //获取出库明细
                NFMT.WareHouse.BLL.StockOutDetailBLL stockOutDetailBLL = new NFMT.WareHouse.BLL.StockOutDetailBLL();
                result = stockOutDetailBLL.Load(user, stockOutId);
                if (result.ResultStatus != 0)
                    this.Page.WarmAlert(result.Message, redirectUrl);

                details = result.ReturnValue as List<NFMT.WareHouse.Model.StockOutDetail>;
                if (details == null || !details.Any())
                    this.Page.WarmAlert("获取出库明细失败", redirectUrl);
            }
        }

        protected string GetDetailHTML(NFMT.Common.UserModel user, List<NFMT.WareHouse.Model.StockOutDetail> details)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            NFMT.WareHouse.Model.Stock stock = new NFMT.WareHouse.Model.Stock();
            NFMT.WareHouse.BLL.StockBLL stockBLL = new NFMT.WareHouse.BLL.StockBLL();
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            foreach (NFMT.WareHouse.Model.StockOutDetail detail in details)
            {
                result = stockBLL.Get(user, detail.StockId);
                if (result.ResultStatus != 0)
                    continue;

                stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                if (stock == null)
                    continue;

                sb.Append("<tr class=\"txt\">");
                sb.AppendFormat("<td>{0}</td>", stock.CardNo);
                sb.AppendFormat("<td>{0}</td>", NFMT.Data.BasicDataProvider.Assets.SingleOrDefault(a => a.AssetId == stock.AssetId).AssetName);
                sb.AppendFormat("<td>{0}</td>", NFMT.Data.BasicDataProvider.Brands.SingleOrDefault(a => a.BrandId == stock.BrandId).BrandName);
                sb.AppendFormat("<td>{0}</td>", detail.GrossAmount);
                sb.Append("<td>&nbsp;</td>");
                sb.Append("<td>&nbsp;</td>");
                sb.Append("<td>&nbsp;</td>");
                sb.Append("</tr>");

                if (string.IsNullOrEmpty(this.DPName))
                    DPName = NFMT.Data.BasicDataProvider.DeliverPlaces.SingleOrDefault(a => a.DPId == stock.DeliverPlaceId).DPName;
            }

            return sb.ToString();
        }
    }
}