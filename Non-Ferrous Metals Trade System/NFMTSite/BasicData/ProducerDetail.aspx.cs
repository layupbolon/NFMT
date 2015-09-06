using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class ProducerDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 31, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                this.navigation1.Routes.Add("生产商管理", string.Format("{0}BasicData/ProducerList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("生产商明细", string.Empty);

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("ProducerList.aspx");
                        NFMT.Data.BLL.ProducerBLL producerBLL = new NFMT.Data.BLL.ProducerBLL();
                        var result = producerBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("ProducerList.aspx");

                        NFMT.Data.Model.Producer pd = result.ReturnValue as NFMT.Data.Model.Producer;
                        if (pd != null)
                        {
                            this.producerName.InnerText = pd.ProducerName;
                            this.producerFullName.InnerText = pd.ProducerFullName;
                            this.producerShort.InnerText = pd.ProducerShort;
                            this.statusName.InnerText = pd.ProducerStatusName;
                            this.areaName.InnerText = pd.ProducerAreaName;

                            this.hidId.Value = pd.ProducerId.ToString();


                        }
                    }
                }
            }
        }
    }
}