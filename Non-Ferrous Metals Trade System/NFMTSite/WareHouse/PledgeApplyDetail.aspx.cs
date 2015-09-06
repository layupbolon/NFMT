using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class PledgeApplyDetail : System.Web.UI.Page
    {
        public int id = 0;//PledgeApplyId
        public int applyId = 0;
        public NFMT.Operate.Model.Apply apply = new NFMT.Operate.Model.Apply();

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string redirectURL = string.Format("{0}WareHouse/PledgeApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 47, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.确认完成, NFMT.Common.OperateEnum.确认完成撤销 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                this.navigation1.Routes.Add("质押申请", redirectURL);
                this.navigation1.Routes.Add("质押申请明细", string.Empty);

                NFMT.WareHouse.BLL.PledgeApplyBLL pledgeApplyBLL = new NFMT.WareHouse.BLL.PledgeApplyBLL();

                if (!string.IsNullOrEmpty(Request.QueryString["aid"]) && int.TryParse(Request.QueryString["aid"], out applyId) && applyId > 0)
                {
                    result = pledgeApplyBLL.GetPledgeByApplyId(user, applyId);
                    if (result.ResultStatus != 0)
                        Response.Redirect(redirectURL);

                    NFMT.WareHouse.Model.PledgeApply pledgeApply1 = result.ReturnValue as NFMT.WareHouse.Model.PledgeApply;
                    if (pledgeApply1 == null)
                        Response.Redirect(redirectURL);

                    id = pledgeApply1.PledgeApplyId;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                        int.TryParse(Request.QueryString["id"], out id);
                }

                if (id <= 0)
                    Response.Redirect(redirectURL);

                this.hidid.Value = id.ToString();
                
                result = pledgeApplyBLL.Get(user, id);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                NFMT.WareHouse.Model.PledgeApply pledgeApply = result.ReturnValue as NFMT.WareHouse.Model.PledgeApply;
                if (pledgeApply == null)
                    Response.Redirect(redirectURL);

                applyId = pledgeApply.ApplyId;

                result = pledgeApplyBLL.GetPledgeApplyDetails(user, pledgeApply.PledgeApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                this.hidDetails.Value = Newtonsoft.Json.JsonConvert.SerializeObject(result.ReturnValue);

                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, pledgeApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null)
                    Response.Redirect(redirectURL);

                NFMT.User.Model.Department dept = NFMT.User.UserProvider.Departments.SingleOrDefault(a => a.DeptId == apply.ApplyDept);
                if (dept == null)
                    Response.Redirect(redirectURL);

                NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == apply.ApplyCorp);
                if (corp == null)
                    Response.Redirect(redirectURL);

                NFMT.Data.Model.Bank bank = NFMT.Data.BasicDataProvider.Banks.SingleOrDefault(a => a.BankId == pledgeApply.PledgeBank);
                if (bank == null)
                    Response.Redirect(redirectURL);

                this.txbApplyDept.InnerText = dept.DeptName;
                this.txbApplyCorp.InnerText = corp.CorpName;
                this.txbMemo.InnerText = apply.ApplyDesc;
                this.txbPledgeBank.InnerText = bank.BankName;

                string json = serializer.Serialize(apply);
                this.hidmodel.Value = json;
            }
        }
    }
}