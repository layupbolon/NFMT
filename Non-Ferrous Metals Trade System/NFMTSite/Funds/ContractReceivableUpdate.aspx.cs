using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class ContractReceivableUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = string.Format("{0}Funds/ContractReceivableList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 57, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("合约收款分配", redirectUrl);
                this.navigation1.Routes.Add("合约收款分配修改", string.Empty);

                int receivableAllotId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out receivableAllotId) || receivableAllotId <= 0)
                    Response.Redirect(redirectUrl);

                this.hidid.Value = receivableAllotId.ToString();


                NFMT.Funds.BLL.ContractReceivableBLL contractReceivableBLL = new NFMT.Funds.BLL.ContractReceivableBLL();
                NFMT.Common.ResultModel result = contractReceivableBLL.GetSubId(user, receivableAllotId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                int subId = (int)result.ReturnValue;
                this.hidsubId.Value = subId.ToString();

                NFMT.Contract.BLL.ContractSubBLL contractSubBLL = new NFMT.Contract.BLL.ContractSubBLL();
                result = contractSubBLL.GetContractOutCorp(user, subId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.hidCorps.Value = Newtonsoft.Json.JsonConvert.SerializeObject(result.ReturnValue);

                result = contractReceivableBLL.GetReceIds(user, receivableAllotId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.hidReceIds.Value = result.ReturnValue.ToString();

                result = contractReceivableBLL.GetCorpRefIds(user, receivableAllotId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.hidCorpRefIds.Value = result.ReturnValue.ToString();

                result = contractReceivableBLL.GetRowsDetail(user, receivableAllotId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

                this.hidRowDetail.Value = Newtonsoft.Json.JsonConvert.SerializeObject(dt);

                result = contractReceivableBLL.GetRowsDetailByCorp(user, receivableAllotId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                System.Data.DataTable dtCorp = result.ReturnValue as System.Data.DataTable;

                this.hidRowDetailCorp.Value = Newtonsoft.Json.JsonConvert.SerializeObject(dtCorp);

                result = contractSubBLL.Get(user, subId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                //子合约信息
                NFMT.Contract.Model.ContractSub contractSub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (contractSub != null)
                {
                    this.hidCurrucyId.Value = contractSub.SettleCurrency.ToString();
                    this.hidcontractId.Value = contractSub.ContractId.ToString();
                    this.spnSubNo.InnerText = contractSub.SubNo;
                    NFMT.Data.Model.MeasureUnit measureUnit = NFMT.Data.BasicDataProvider.MeasureUnits.SingleOrDefault(a => a.MUId == contractSub.UnitId);
                    if (measureUnit != null)
                        this.spnSubSignAmount.InnerText = contractSub.SignAmount.ToString() + measureUnit.MUName;
                    this.spnPeriodE.InnerText = contractSub.ContractPeriodE.ToShortDateString();

                    NFMT.Funds.BLL.ReceivableAllotBLL receivableAllotBLL = new NFMT.Funds.BLL.ReceivableAllotBLL();
                    result = receivableAllotBLL.GetSubContractAllotAmount(user, subId);
                    if (result.ResultStatus != 0)
                        Response.Redirect(redirectUrl);

                    this.spanAllotAmount.InnerText = result.ReturnValue == null ? "" : result.ReturnValue.ToString();

                    //分配信息
                    result = receivableAllotBLL.Get(user, receivableAllotId);
                    if (result.ResultStatus != 0)
                        Response.Redirect(redirectUrl);

                    NFMT.Funds.Model.ReceivableAllot receivableAllot = result.ReturnValue as NFMT.Funds.Model.ReceivableAllot;
                    if (receivableAllot != null)
                    {
                        this.txbMemo.Value = receivableAllot.AllotDesc;
                        this.hidAllotFrom.Value = receivableAllot.AllotFrom.ToString();
                    }
                }
            }
        }
    }
}