using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class BDStyleDtlUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 75, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("BasicData/BDStyleList.aspx");

                        NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                        NFMT.Data.Model.BDStyleDetail detail = NFMT.Data.BasicDataProvider.StyleDetails.FirstOrDefault(temp => temp.StyleDetailId == id);
                        if (detail == null || detail.StyleDetailId == 0 || detail.BDStyleId == 0)
                        {
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('类型明细错误');</script>");
                            Response.Redirect("BasicData/BDStyleList.aspx");
                        }

                        NFMT.Data.Model.BDStyle style = NFMT.Data.BasicDataProvider.BDStyles.FirstOrDefault(temp => temp.BDStyleId == detail.BDStyleId);
                        if (style == null || style.BDStyleId == 0)
                        {
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('类型明细错误');</script>");
                            Response.Redirect("BasicData/BDStyleList.aspx");
                        }
                       
                        this.spnStyleName.InnerHtml = style.BDStyleName;
                        this.spnStyleStatus.InnerHtml = style.BDStyleStatusName;
                        this.hidDetailId.Value = detail.StyleDetailId.ToString();
                        this.txbDetailCode.Value = detail.DetailCode;
                        this.txbDetailName.Value = detail.DetailName;
                        this.hidDetailStatus.Value = ((int)detail.DetailStatus).ToString();

                        this.navigation1.Routes.Add("类型列表", "BDStyleList.aspx");
                        this.navigation1.Routes.Add("类型明细列表", string.Format("BDStyleDtlList.aspx?id={0}", style.BDStyleId));
                        this.navigation1.Routes.Add("类型明细修改", string.Empty);
                    }
                }
            }
        }
    }
}