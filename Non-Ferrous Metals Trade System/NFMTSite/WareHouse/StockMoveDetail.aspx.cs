using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockMoveDetail : System.Web.UI.Page
    {
        public int stockMoveId = 0;
        public NFMT.WareHouse.Model.StockMove stockMove = new NFMT.WareHouse.Model.StockMove();

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = string.Format("{0}WareHouse/StockMoveList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 46, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成 });

                this.navigation1.Routes.Add("移库", redirectUrl);
                this.navigation1.Routes.Add("移库明细", string.Empty);
                
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out stockMoveId) || stockMoveId <= 0)
                    Response.Redirect(redirectUrl);

                this.hidid.Value = stockMoveId.ToString();

                NFMT.WareHouse.BLL.StockMoveBLL stockMoveBLL = new NFMT.WareHouse.BLL.StockMoveBLL();
                NFMT.Common.ResultModel result = stockMoveBLL.Get(user, stockMoveId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                stockMove = result.ReturnValue as NFMT.WareHouse.Model.StockMove;
                if (stockMove == null)
                    Response.Redirect(redirectUrl);

                NFMT.WareHouse.BLL.StockMoveDetailBLL stockMoveDetailBLL = new NFMT.WareHouse.BLL.StockMoveDetailBLL();
                NFMT.Common.StatusEnum getDetailStatus = NFMT.Common.StatusEnum.已生效;

                if (stockMove.MoveStatus == NFMT.Common.StatusEnum.已完成)
                    getDetailStatus = NFMT.Common.StatusEnum.已完成;
                else if (stockMove.MoveStatus == NFMT.Common.StatusEnum.已关闭)
                    getDetailStatus = NFMT.Common.StatusEnum.已关闭;

                result = stockMoveDetailBLL.Load(user, stockMoveId, getDetailStatus);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                List<NFMT.WareHouse.Model.StockMoveDetail> stockMoveDetails = result.ReturnValue as List<NFMT.WareHouse.Model.StockMoveDetail>;
                if (stockMoveDetails == null || !stockMoveDetails.Any())
                    Response.Redirect(redirectUrl);

                foreach (NFMT.WareHouse.Model.StockMoveDetail detail in stockMoveDetails)
                {
                    NFMT.WareHouse.BLL.StockBLL stockBLL = new NFMT.WareHouse.BLL.StockBLL();
                    result = stockBLL.Get(user, detail.StockId);
                    if (result.ResultStatus != 0)
                        Response.Redirect(redirectUrl);

                    NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                    if (stock == null)
                        Response.Redirect(redirectUrl);

                    NFMT.Data.Model.DeliverPlace deliverPlace = NFMT.Data.BasicDataProvider.DeliverPlaces.SingleOrDefault(a => a.DPId == stock.DeliverPlaceId);
                    if (deliverPlace == null)
                        Response.Redirect(redirectUrl);

                    break;
                }

                this.hidStockMoveApplyId.Value = stockMove.StockMoveApplyId.ToString();
                
                this.txbMoveMemo.InnerText = stockMove.MoveMemo;

                NFMT.Common.AuditModel auditModel = new NFMT.Common.AuditModel()
                {
                    AssName = stockMove.AssName,
                    DalName = stockMove.DalName,
                    DataBaseName = stockMove.DataBaseName,
                    Id = stockMove.Id,
                    Status = stockMove.Status,
                    TableName = stockMove.TableName
                };
                string json = serializer.Serialize(auditModel);
                this.hidModel.Value = json;
            }
        }
    }
}