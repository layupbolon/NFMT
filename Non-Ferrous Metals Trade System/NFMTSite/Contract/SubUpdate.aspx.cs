using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Contract
{
    public partial class SubUpdate : System.Web.UI.Page
    {
        public NFMT.Contract.Model.Contract curContract;
        public NFMT.Contract.Model.ContractSub curSub;
        public NFMT.Contract.Model.SubDetail curSubDetail;
        public NFMT.Contract.Model.SubPrice curSubPrice;
        public List<NFMT.Contract.Model.SubCorporationDetail> curOutCorps;
        public List<NFMT.Contract.Model.SubCorporationDetail> curInCorps;
        public List<NFMT.Contract.Model.ContractDept> curDepts;

        public string curOutCorpsString = string.Empty;
        public string curInCorpsString = string.Empty;
        public string curDeptsString = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 79, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            this.hidTradeDirection.Value = ((int)NFMT.Data.StyleEnum.TradeDirection).ToString();
            this.hidTradeBorder.Value = ((int)NFMT.Data.StyleEnum.TradeBorder).ToString();
            this.hidMarginMode.Value = ((int)NFMT.Data.StyleEnum.MarginMode).ToString();
            this.hidValueRateType.Value = ((int)NFMT.Data.StyleEnum.ValueRateType).ToString();
            this.hidDiscountBase.Value = ((int)NFMT.Data.StyleEnum.DiscountBase).ToString();
            this.hidWhoDoPrice.Value = ((int)NFMT.Data.StyleEnum.WhoDoPrice).ToString();
            this.hidSummaryPrice.Value = ((int)NFMT.Data.StyleEnum.SummaryPrice).ToString();

            string redirectUrl = "SubList.aspx";

            int subId = 0;
            if (string.IsNullOrEmpty(Request.QueryString["id"]))
                Response.Redirect(redirectUrl);
            if (!int.TryParse(Request.QueryString["id"], out subId))
                Response.Redirect(redirectUrl);

            this.navigation1.Routes.Add("子合约列表", redirectUrl);
            this.navigation1.Routes.Add("子合约修改", string.Empty);

            this.hidTradeDirection.Value = ((int)NFMT.Data.StyleEnum.TradeDirection).ToString();
            this.hidTradeBorder.Value = ((int)NFMT.Data.StyleEnum.TradeBorder).ToString();
            this.hidMarginMode.Value = ((int)NFMT.Data.StyleEnum.MarginMode).ToString();
            this.hidValueRateType.Value = ((int)NFMT.Data.StyleEnum.ValueRateType).ToString();
            this.hidDiscountBase.Value = ((int)NFMT.Data.StyleEnum.DiscountBase).ToString();
            this.hidWhoDoPrice.Value = ((int)NFMT.Data.StyleEnum.WhoDoPrice).ToString();
            this.hidSummaryPrice.Value = ((int)NFMT.Data.StyleEnum.SummaryPrice).ToString();

            NFMT.Contract.BLL.ContractBLL contractBLL = new NFMT.Contract.BLL.ContractBLL();
            NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
            NFMT.Contract.BLL.SubDetailBLL detailBLL = new NFMT.Contract.BLL.SubDetailBLL();
            NFMT.Contract.BLL.SubPriceBLL priceBLL = new NFMT.Contract.BLL.SubPriceBLL();
            NFMT.Contract.BLL.SubCorporationDetailBLL corpBLL = new NFMT.Contract.BLL.SubCorporationDetailBLL();
            NFMT.Contract.BLL.ContractDeptBLL deptBLL = new NFMT.Contract.BLL.ContractDeptBLL();

            //获取子合约
            NFMT.Common.ResultModel result = subBLL.Get(user, subId);
            if (result.ResultStatus != 0)
                Response.Redirect(redirectUrl);

            this.curSub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
            if (this.curSub == null || this.curSub.SubId <= 0)
                Response.Redirect(redirectUrl);

            int contractId = this.curSub.ContractId;

            //获取合约
            result = contractBLL.Get(user, contractId);
            if (result.ResultStatus != 0)
                Response.Redirect(redirectUrl);
            curContract = result.ReturnValue as NFMT.Contract.Model.Contract;
            if (curContract == null)
                Response.Redirect(redirectUrl);

            //获取明细
            result = detailBLL.GetDetailBySubId(user, subId);
            if (result.ResultStatus != 0)
                Response.Redirect(redirectUrl);
            this.curSubDetail = result.ReturnValue as NFMT.Contract.Model.SubDetail;
            if (curSubDetail == null)
                Response.Redirect(redirectUrl);

            //获取价格
            result = priceBLL.GetPriceBySubId(user, subId);
            if (result.ResultStatus != 0)
                Response.Redirect(redirectUrl);
            curSubPrice = result.ReturnValue as NFMT.Contract.Model.SubPrice;
            if (curSubPrice == null)
                Response.Redirect(redirectUrl);

            //获取公司列表
            //我方公司
            result = corpBLL.Load(user, subId, true);
            if (result.ResultStatus != 0)
                Response.Redirect(redirectUrl);
            curInCorps = result.ReturnValue as List<NFMT.Contract.Model.SubCorporationDetail>;
            if (curInCorps == null || curInCorps.Count == 0)
                Response.Redirect(redirectUrl);

            //对方公司
            result = corpBLL.Load(user, subId, false);
            if (result.ResultStatus != 0)
                Response.Redirect(redirectUrl);
            curOutCorps = result.ReturnValue as List<NFMT.Contract.Model.SubCorporationDetail>;
            if (curOutCorps == null || curOutCorps.Count == 0)
                Response.Redirect(redirectUrl);

            //执行部门
            result = deptBLL.LoadDeptByContractId(user, contractId);
            if (result.ResultStatus != 0)
                Response.Redirect(redirectUrl);

            curDepts = result.ReturnValue as List<NFMT.Contract.Model.ContractDept>;

            foreach (var obj in curOutCorps)
            {
                if (curOutCorps.IndexOf(obj) > 0)
                    curOutCorpsString += ",";
                curOutCorpsString += obj.CorpId.ToString();
            }

            foreach (var obj in this.curInCorps)
            {
                if (curInCorps.IndexOf(obj) > 0)
                    this.curInCorpsString += ",";
                curInCorpsString += obj.CorpId.ToString();
            }

            foreach (var obj in this.curDepts)
            {
                if (curDepts.IndexOf(obj) > 0)
                    curDeptsString += ",";
                curDeptsString += obj.DeptId.ToString();
            }

            //attach
            this.attach1.BusinessIdValue = this.curSub.SubId;
        }
    }
}