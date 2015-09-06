using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class ProducerUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 31, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("生产商管理", string.Format("{0}BasicData/ProducerList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("生产商修改", string.Empty);

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("ProducerList.aspx");

                        NFMT.Data.BLL.ProducerBLL bBLL = new NFMT.Data.BLL.ProducerBLL();
                        var result = bBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("ProducerList.aspx");

                        NFMT.Data.Model.Producer producer = result.ReturnValue as NFMT.Data.Model.Producer;
                        if (producer != null)
                        {
                            this.txbProducerName.Value = producer.ProducerName;
                            this.txbProducerFullName.Value = producer.ProducerFullName;
                            this.txbProducerShort.Value = producer.ProducerShort;
                            

                            this.hidAreaName.Value = producer.ProducerArea.ToString();
                            this.hidStatusName.Value = ((int)producer.ProducerStatus).ToString();
                        }
                    }
                }
            }
        }
    }
}