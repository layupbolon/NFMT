using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class CashInAllotCorpUpdate : System.Web.UI.Page
    {
        public NFMT.Funds.Model.CashInAllot curAllot = null;
        public NFMT.User.Model.Corporation curCorp = null;
        public string curSelectedStr = string.Empty;
        public bool isShare = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = "CashInAllotList.aspx";
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 56, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("公司收款分配", redirectUrl);
                this.navigation1.Routes.Add("公司收款分配修改", string.Empty);

                int allotId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out allotId) || allotId <= 0)
                    Response.Redirect(redirectUrl);

                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                //获取收款分配
                NFMT.Funds.BLL.CashInAllotBLL allotBLL = new NFMT.Funds.BLL.CashInAllotBLL();
                result = allotBLL.Get(user, allotId);

                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
               
                NFMT.Funds.Model.CashInAllot cashInAllot = result.ReturnValue as NFMT.Funds.Model.CashInAllot;
                if (cashInAllot == null || cashInAllot.AllotId <= 0)
                    Response.Redirect(redirectUrl);

                this.curAllot = cashInAllot;

                 //获取公司关联列表
                NFMT.Funds.BLL.CashInCorpBLL corpBLL = new NFMT.Funds.BLL.CashInCorpBLL();
                result = corpBLL.Load(user, allotId);
                if(result.ResultStatus!=0)
                    Response.Redirect(redirectUrl);

                List<NFMT.Funds.Model.CashInCorp> cashInCorps = result.ReturnValue as List<NFMT.Funds.Model.CashInCorp>;
                if (cashInCorps == null || cashInCorps.Count == 0)
                    Response.Redirect(redirectUrl);

                this.isShare = cashInCorps[0].IsShare;

                NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == cashInCorps[0].CorpId);
                if (corp == null || corp.CorpId <= 0)
                    Response.Redirect(redirectUrl);

                this.curCorp = corp;


                int pageIndex = 1, pageSize = 100;
                string orderStr = string.Empty, whereStr = string.Empty;

                NFMT.Funds.BLL.CashInAllotDetailBLL bll = new NFMT.Funds.BLL.CashInAllotDetailBLL();
                NFMT.Common.SelectModel select = bll.GetCurDetailsSelect(pageIndex, pageSize, orderStr,cashInAllot.AllotId);
                result = bll.Load(user, select);

                int totalRows = result.AffectCount;
                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

                this.curSelectedStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

            }
        }
    }
}