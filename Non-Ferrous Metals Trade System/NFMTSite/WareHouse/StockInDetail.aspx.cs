using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockInDetail : System.Web.UI.Page
    {
        public NFMT.WareHouse.Model.StockIn curStockIn = null;
        public NFMT.WareHouse.Model.ContractStockIn curContractStockIn = null;
        public int curStockType = 0;
        public int curSubId = 0;
        public int curCustomType = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = string.Format("{0}WareHouse/StockInList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 41, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.确认完成 });

                this.navigation1.Routes.Add("入库登记", redirectUrl);
                this.navigation1.Routes.Add("入库登记明细", string.Empty);

                this.curStockType = (int)NFMT.Data.StyleEnum.库存类型;
                this.curCustomType = (int)NFMT.Data.StyleEnum.报关状态;

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(redirectUrl);

                int stockInId = 0;
                if (!int.TryParse(Request.QueryString["id"], out stockInId) || stockInId <= 0)
                    Response.Redirect(redirectUrl);

                NFMT.WareHouse.BLL.StockInBLL stockInBLL = new NFMT.WareHouse.BLL.StockInBLL();
                NFMT.Common.ResultModel result = stockInBLL.Get(user, stockInId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.WareHouse.Model.StockIn stockIn = result.ReturnValue as NFMT.WareHouse.Model.StockIn;
                this.curStockIn = stockIn;

                if (stockIn != null)
                {
                    NFMT.Common.AuditModel model = new NFMT.Common.AuditModel()
                    {
                        AssName = stockIn.AssName,
                        DalName = stockIn.DalName,
                        DataBaseName = stockIn.DataBaseName,
                        Id = stockIn.Id,
                        Status = stockIn.Status,
                        TableName = stockIn.TableName
                    };
                    string json = serializer.Serialize(model);
                    this.hidmodel.Value = json;
                }
            }
        }
    }
}