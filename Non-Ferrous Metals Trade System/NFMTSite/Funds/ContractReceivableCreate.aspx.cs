using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class ContractReceivableCreate : System.Web.UI.Page
    {
        public NFMT.Contract.Model.ContractSub curSub = null;
        public string JsonOutCorp = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 57, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

            string redirectUrl = string.Format("{0}Funds/ContractReceivableReadyContractList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                this.navigation1.Routes.Add("合约收款分配", string.Format("{0}Funds/ContractReceivableList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("可收款分配合约列表", redirectUrl);
                this.navigation1.Routes.Add("合约收款分配新增", string.Empty);

                int subId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out subId) || subId <= 0)
                    Response.Redirect(redirectUrl);


                NFMT.Contract.BLL.ContractSubBLL contractSubBLL = new NFMT.Contract.BLL.ContractSubBLL();
                result = contractSubBLL.Get(user, subId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                    Response.Redirect(redirectUrl);

                this.curSub = sub;

                this.spnSubNo.InnerText = sub.SubNo;
                NFMT.Data.Model.MeasureUnit measureUnit = NFMT.Data.BasicDataProvider.MeasureUnits.SingleOrDefault(a => a.MUId == sub.UnitId);
                if (measureUnit != null)
                    this.spnSubSignAmount.InnerText = sub.SignAmount.ToString() + measureUnit.MUName;
                this.spnPeriodE.InnerText = sub.ContractPeriodE.ToShortDateString();
                
                //this.spanAllotAmount.InnerText = result.ReturnValue == null ? "" : result.ReturnValue.ToString();

                result = contractSubBLL.GetContractOutCorp(user, subId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                if (dt == null || dt.Rows.Count == 0)
                    Response.Redirect(redirectUrl);

                this.JsonOutCorp = Newtonsoft.Json.JsonConvert.SerializeObject(result.ReturnValue);
            
            }
        }
    }
}