using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class MasterClauseUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int refId = 0;

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect("MasterClauseAllot.aspx");
                if(!int.TryParse(Request.QueryString["id"],out refId))
                    Response.Redirect("MasterClauseAllot.aspx");

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Data.BLL.ClauseContractBLL refBLL = new NFMT.Data.BLL.ClauseContractBLL();

                NFMT.Common.ResultModel result = refBLL.Get(user, refId);
                if(result.ResultStatus!=0)
                    Response.Redirect("MasterClauseAllot.aspx");
                NFMT.Data.Model.ClauseContract masterClause = result.ReturnValue as NFMT.Data.Model.ClauseContract;
                if(masterClause==null || masterClause.RefId<=0)
                    Response.Redirect("MasterClauseAllot.aspx");

                NFMT.Data.BLL.ContractMasterBLL masterBLL = new NFMT.Data.BLL.ContractMasterBLL();
                result = masterBLL.Get(user,masterClause.MasterId);
                if(result.ResultStatus!=0)
                    Response.Redirect("MasterClauseAllot.aspx");
                NFMT.Data.Model.ContractMaster master = result.ReturnValue as NFMT.Data.Model.ContractMaster;
                if(master==null || master.MasterId<=0)
                    Response.Redirect("MasterClauseAllot.aspx");

                NFMT.Data.BLL.ContractClauseBLL clauseBLL = new NFMT.Data.BLL.ContractClauseBLL();
                result = clauseBLL.Get(user, masterClause.ClauseId);
                if(result.ResultStatus!=0)
                    Response.Redirect("MasterClauseAllot.aspx");
                NFMT.Data.Model.ContractClause clause = result.ReturnValue as NFMT.Data.Model.ContractClause;
                if(clause==null || clause.ClauseId<=0)
                    Response.Redirect("MasterClauseAllot.aspx");

                this.spnClauseEtext.InnerHtml = clause.ClauseEnText;
                this.spnClauseText.InnerHtml = clause.ClauseText;
                this.spnMasterEname.InnerHtml = master.MasterEname;
                this.spnMasterName.InnerHtml = master.MasterName;

                this.chkIsChose.Checked = masterClause.IsChose;
                this.txbSort.Value = masterClause.Sort.ToString();

                this.hidRefId.Value = masterClause.RefId.ToString();

                int masterId = 0;
                int.TryParse(Request.QueryString["mid"],out masterId);

                this.navigation1.Routes.Add("合约模板列表", "MasterClauseAllot.aspx");
                this.navigation1.Routes.Add("模板条款分配", string.Format("MasterClauseCreate.aspx?id={0}",masterId));
                this.navigation1.Routes.Add("分配修改",string.Empty);
            }
        }
    }
}