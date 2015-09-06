using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class SICreate : System.Web.UI.Page
    {
        public int deptId = 0;
        public string styleId = ((int)NFMT.Data.StyleEnum.发票内容).ToString();
        public int invoiceType = (int)NFMT.Invoice.InvoiceTypeEnum.价外票;
        public int invoiceDirection = 0;
        public int inId = 0;
        public int outId = 1;

        private string redirectUrl = string.Format("{0}Invoice/SIList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 65, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("价外票", redirectUrl);
                this.navigation1.Routes.Add("价外票新增", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["d"]) || !int.TryParse(Request.QueryString["d"], out invoiceDirection) || invoiceDirection <= 0)
                    this.WarmAlert("参数错误", redirectUrl);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                deptId = user.DeptId;

                if (invoiceDirection == (int)NFMT.Invoice.InvoiceDirectionEnum.开具)
                {
                    inId = 1;
                    outId = 0;
                }
            }
        }
    }
}