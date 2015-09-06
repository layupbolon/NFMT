using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class ReceivableCorpUpdate : System.Web.UI.Page
    {
        public NFMT.Funds.Model.ReceivableAllot curRecAllot = null;
        public NFMT.User.Model.Corporation curCorp = null;
        public string curRids = string.Empty;
        public string curDids = string.Empty;
        public string curJsonStr = string.Empty;
        public bool isShare = false;
        public int currencyId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = string.Format("{0}Funds/ReceivableCorpList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            if (!IsPostBack)
            {
                this.navigation1.Routes.Add("公司收款分配", redirectUrl);
                this.navigation1.Routes.Add("公司收款分配修改", string.Empty);

                int receivableAllotId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out receivableAllotId) || receivableAllotId <= 0)
                    Response.Redirect(redirectUrl);

                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                //获取分配
                NFMT.Funds.BLL.ReceivableAllotBLL allotBLL = new NFMT.Funds.BLL.ReceivableAllotBLL();
                result = allotBLL.Get(user, receivableAllotId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Funds.Model.ReceivableAllot recAllot = result.ReturnValue as NFMT.Funds.Model.ReceivableAllot;
                if (recAllot == null || recAllot.ReceivableAllotId <= 0)
                    Response.Redirect(redirectUrl);

                this.curRecAllot = recAllot;
                this.currencyId = recAllot.CurrencyId;

                //获取分配明细列表
                NFMT.Funds.BLL.RecAllotDetailBLL recAllotDetailBLL = new NFMT.Funds.BLL.RecAllotDetailBLL();
                result = recAllotDetailBLL.Load(user, recAllot.ReceivableAllotId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                List<NFMT.Funds.Model.RecAllotDetail> details = result.ReturnValue as List<NFMT.Funds.Model.RecAllotDetail>;
                if (details == null)
                    Response.Redirect(redirectUrl);

                foreach (NFMT.Funds.Model.RecAllotDetail d in details)
                {
                    if (details.IndexOf(d) != 0)
                    {
                        this.curRids += ",";
                        this.curDids += ",";
                    }

                    this.curRids += d.RecId.ToString();
                    this.curDids += d.DetailId.ToString();
                }

                //获取分配公司明细列表
                NFMT.Funds.BLL.CorpReceivableBLL corpReceivalbleBLL = new NFMT.Funds.BLL.CorpReceivableBLL();
                result = corpReceivalbleBLL.Load(user, recAllot.ReceivableAllotId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                List<NFMT.Funds.Model.CorpReceivable> corpDetails = result.ReturnValue as List<NFMT.Funds.Model.CorpReceivable>;
                if (corpDetails == null)
                    Response.Redirect(redirectUrl);

                //公司信息
                if (corpDetails.Count == 0)
                    Response.Redirect(redirectUrl);

                NFMT.Funds.Model.CorpReceivable corpDetail = corpDetails[0];
                NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == corpDetail.CorpId);
                if (corp != null && corp.CorpId > 0)
                {
                    this.spanBlocId.InnerHtml = corp.BlocName;
                    this.spanCorpCode.InnerHtml = corp.CorpCode;
                    this.spanCorpName.InnerHtml = corp.CorpName;
                    this.spanTaxPlayer.InnerHtml = corp.TaxPayerId;
                    this.spanCorpAddress.InnerHtml = corp.CorpAddress;
                    this.spanCorpTel.InnerHtml = corp.CorpTel;
                    this.spanCorpFax.InnerHtml = corp.CorpFax;
                    this.spanCorpZip.InnerHtml = corp.CorpZip;

                    this.curCorp = corp;
                }
                this.isShare = corpDetail.IsShare;

                result = corpReceivalbleBLL.GetRowsDetail(user, receivableAllotId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                this.curJsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt);

                //公司信息
                //NFMT.User.BLL.CorporationBLL corporationBLL = new NFMT.User.BLL.CorporationBLL();
                //result = corporationBLL.Get(user, corpId);
                //if (result.ResultStatus != 0)
                //    Response.Redirect(redirectUrl);

                //NFMT.User.Model.Corporation corp = result.ReturnValue as NFMT.User.Model.Corporation;
                //if (corp != null)
                //{
                //    NFMT.User.Model.Bloc bloc = NFMT.User.UserProvider.Blocs.SingleOrDefault(a => a.BlocId == corp.ParentId);
                //    if (bloc != null)
                //        this.spanBlocId.InnerText = bloc.BlocName;

                //    this.spanCorpCode.InnerText = corp.CorpCode;
                //    this.spanCorpName.InnerText = corp.CorpName;
                //    this.spanTaxPlayer.InnerText = corp.TaxPayerId;
                //    this.spanCorpAddress.InnerText = corp.CorpAddress;
                //    this.spanCorpTel.InnerText = corp.CorpTel;
                //    this.spanCorpFax.InnerText = corp.CorpFax;
                //    this.spanCorpZip.InnerText = corp.CorpZip;

                //    //分配信息
                //    NFMT.Funds.BLL.ReceivableAllotBLL receivableAllotBLL = new NFMT.Funds.BLL.ReceivableAllotBLL();
                //    result = receivableAllotBLL.Get(user, receivableAllotId);
                //    if (result.ResultStatus != 0)
                //        Response.Redirect(redirectUrl);

                //    NFMT.Funds.Model.ReceivableAllot receivableAllot = result.ReturnValue as NFMT.Funds.Model.ReceivableAllot;
                //    if (receivableAllot != null)
                //    {
                //        this.txbMemo.Value = receivableAllot.AllotDesc;
                //    }
                //}
            }
        }
    }
}