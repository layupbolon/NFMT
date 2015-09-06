using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class ContractClauseView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 78, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                int clauseId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect("ContractClauseList.aspx");

                if (int.TryParse(Request.QueryString["id"], out clauseId))
                {
                    if (clauseId == 0)
                        Response.Redirect("ContractClauseList.aspx");

                    NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                    NFMT.Data.Model.ContractClause clause = NFMT.Data.BasicDataProvider.ContractClauses.FirstOrDefault(temp => temp.ClauseId == clauseId);

                    if (clause == null || clause.ClauseId <= 0)
                        Response.Redirect("ContractClauseList.aspx");

                    this.spnClauseText.InnerHtml = clause.ClauseText;
                    this.spnClauseEtext.InnerHtml = clause.ClauseEnText;
                    this.spnClauseStatus.InnerHtml = clause.ClauseStatusName;
                    this.hidClauseId.Value = clause.ClauseId.ToString();

                    this.navigation1.Routes.Add("合约条款列表", "ContractClauseList.aspx");
                    this.navigation1.Routes.Add("合约条款明细", string.Empty);
                }
                else
                    Response.Redirect("ContractClauseList.aspx");
            }
        }
    }
}