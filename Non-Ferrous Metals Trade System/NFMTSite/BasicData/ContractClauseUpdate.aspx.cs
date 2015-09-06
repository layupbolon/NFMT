using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class ContractClauseUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 78, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("合约条款列表", "ContractClauseList.aspx");
                this.navigation1.Routes.Add("合约条款修改", string.Empty);

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

                    this.hidClauseStatus.Value = ((int)clause.ClauseStatus).ToString();
                    this.txbText.Value = clause.ClauseText;
                    this.txbEText.Value = clause.ClauseEnText;
                    this.hidClauseId.Value = clause.ClauseId.ToString();
                }
                else
                    Response.Redirect("ContractClauseList.aspx");
            }
        }
    }
}