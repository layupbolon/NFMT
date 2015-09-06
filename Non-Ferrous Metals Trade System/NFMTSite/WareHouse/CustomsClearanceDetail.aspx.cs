using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class CustomsClearanceDetail : System.Web.UI.Page
    {
        public NFMT.WareHouse.Model.CustomsClearance customsClearance = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectURL = string.Format("{0}WareHouse/CustomsClearanceList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 96, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成, NFMT.Common.OperateEnum.执行完成撤销 });

                this.navigation1.Routes.Add("报关", redirectURL);
                this.navigation1.Routes.Add("报关明细", string.Empty);

                int customsId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out customsId))
                    Response.Redirect(redirectURL);

                NFMT.WareHouse.BLL.CustomsClearanceBLL customsClearanceBLL = new NFMT.WareHouse.BLL.CustomsClearanceBLL();
                result = customsClearanceBLL.Get(user, customsId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                customsClearance = result.ReturnValue as NFMT.WareHouse.Model.CustomsClearance;
                if (customsClearance == null)
                    Response.Redirect(redirectURL);

                NFMT.WareHouse.BLL.CustomsDetailBLL customsDetailBLL = new NFMT.WareHouse.BLL.CustomsDetailBLL();
                result = customsDetailBLL.GetStockIdForUpGrid(user, customsId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                this.hidsidsUp.Value = result.ReturnValue != null ? result.ReturnValue.ToString() : string.Empty;

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(customsClearance);
                this.hidModel.Value = json;

                //attach
                this.attach1.BusinessIdValue = this.customsClearance.CustomsId;
            }
        }
    }
}