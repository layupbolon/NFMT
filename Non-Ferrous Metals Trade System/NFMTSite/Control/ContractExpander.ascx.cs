using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Control
{
    public partial class ContractExpander : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SetContractData();
        }

        private void SetContractData()
        {
            NFMT.Contract.Model.ContractSub sub = null;
            NFMT.Contract.Model.Contract contract = null;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = string.IsNullOrEmpty(this.RedirectUrl)?Request.UrlReferrer.AbsolutePath:this.RedirectUrl;

            #region 初始化合约实体
            if (this.CurContractSub != null && this.CurContractSub.SubId > 0)
                sub = this.CurContractSub;
            else
            {
                //获取子合约
                NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBLL.Get(user, this.subContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                    Response.Redirect(redirectUrl);
            }
            if (this.CurContract != null && this.CurContract.ContractId > 0)
                contract = this.CurContract;
            else
            {
                //获取合约
                NFMT.Contract.BLL.ContractBLL contractBLL = new NFMT.Contract.BLL.ContractBLL();
                result = contractBLL.Get(user, this.contractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                    Response.Redirect(redirectUrl);
            }
            #endregion

            #region 赋值

            //我方抬头
            NFMT.Contract.BLL.SubCorporationDetailBLL corpDetailBLL = new NFMT.Contract.BLL.SubCorporationDetailBLL();
            result = corpDetailBLL.Load(user, sub.SubId, true);
            if (result.ResultStatus != 0)
                Response.Redirect(redirectUrl);
            List<NFMT.Contract.Model.SubCorporationDetail> inCorps = result.ReturnValue as List<NFMT.Contract.Model.SubCorporationDetail>;
            if (inCorps != null && inCorps.Count > 0)
            {
                this.spnInCorpNames.InnerHtml = string.Empty;
                foreach (NFMT.Contract.Model.SubCorporationDetail inCorp in inCorps)
                {
                    this.spnInCorpNames.InnerHtml += string.Format("[{0}]", inCorp.CorpName);
                }
            }

            //对方抬头
            result = corpDetailBLL.Load(user, sub.SubId, false);
            if (result.ResultStatus != 0)
                Response.Redirect(redirectUrl);
            List<NFMT.Contract.Model.SubCorporationDetail> outCorps = result.ReturnValue as List<NFMT.Contract.Model.SubCorporationDetail>;
            if (outCorps != null && outCorps.Count > 0)
            {
                this.spnOutCorpNames.InnerHtml = string.Empty;
                foreach (NFMT.Contract.Model.SubCorporationDetail outCorp in outCorps)
                {
                    this.spnOutCorpNames.InnerHtml += string.Format("[{0}]", outCorp.CorpName);
                }
            }

            //子合约编号
            this.spnSubNo.InnerHtml = sub.SubNo;

            //品种
            NFMT.Data.Model.Asset asset = NFMT.Data.BasicDataProvider.Assets.FirstOrDefault(temp => temp.AssetId == contract.AssetId);
            if (asset != null && asset.AssetId > 0)
                this.spnAsset.InnerHtml = asset.AssetName;

            //签订数量
            NFMT.Data.Model.MeasureUnit mu = NFMT.Data.BasicDataProvider.MeasureUnits.FirstOrDefault(temp => temp.MUId == sub.UnitId);
            if (mu != null && mu.MUId > 0)
                this.spnSignAmount.InnerHtml = string.Format("{0}{1}", sub.SignAmount.ToString(), mu.MUName);

            //执行最终日
            this.spnPeriodE.InnerHtml = sub.ContractPeriodE.ToString("yyyy-MM-dd");

            //升贴水
            NFMT.Data.Model.Currency currency = NFMT.Data.BasicDataProvider.Currencies.FirstOrDefault(temp => temp.CurrencyId == sub.SettleCurrency);
            if (currency != null && currency.CurrencyId > 0)
                this.spnPremium.InnerHtml = string.Format("{0}{1}", sub.Premium.ToString("0.00"), currency.CurrencyName);
            #endregion

        }

        private int contractId = 0;
        public int ContractId
        {
            get { return this.contractId; }
            set { this.contractId = value; }
        }

        private NFMT.Contract.Model.Contract contract = new NFMT.Contract.Model.Contract();
        public NFMT.Contract.Model.Contract CurContract { get; set; }

        private int subContractId = 0;
        public int SubContractId
        {
            get { return this.subContractId; }
            set { this.subContractId = value; }
        }

        public NFMT.Contract.Model.ContractSub CurContractSub { get; set; }

        public string RedirectUrl { get; set; }
    }
}