using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class CashInAllotStockCreate : System.Web.UI.Page
    {
        public NFMT.Contract.Model.ContractSub curSub = null;
        public NFMT.WareHouse.Model.Stock curStock = null;
        public NFMT.WareHouse.Model.StockLog curStockLog = null;
        public string JsonOutCorp = string.Empty;
        public string curOutCorpIds = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 58, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

            int stockLogId = 0;
            string redirctUrl = "CashInAllotStockList.aspx";

            if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out stockLogId) || stockLogId <= 0)
                Response.Redirect(redirctUrl);

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            
            //获取库存流水
            NFMT.WareHouse.BLL.StockLogBLL stockLogBLL = new NFMT.WareHouse.BLL.StockLogBLL();
            result = stockLogBLL.Get(user, stockLogId);

            if (result.ResultStatus != 0)
                Response.Redirect(redirctUrl);

            NFMT.WareHouse.Model.StockLog stockLog = result.ReturnValue as NFMT.WareHouse.Model.StockLog;
            if (stockLog == null || stockLog.StockLogId <= 0)
                Response.Redirect(redirctUrl);

            this.curStockLog = stockLog;

            //获取子合约
            NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
            result = subBLL.Get(user, stockLog.SubContractId);
            if (result.ResultStatus != 0)
                Response.Redirect(redirctUrl);

            NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
            if(sub == null || sub.SubId<=0)
                Response.Redirect(redirctUrl);

            this.curSub = sub;

            //合约抬头
            NFMT.Contract.BLL.ContractCorporationDetailBLL ccdBll = new NFMT.Contract.BLL.ContractCorporationDetailBLL();

            //内部公司
            result = ccdBll.LoadCorpListByContractId(user, sub.ContractId, true);
            List<NFMT.Contract.Model.ContractCorporationDetail> innerCorps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;

            foreach (NFMT.Contract.Model.ContractCorporationDetail innerCorp in innerCorps)
            {
                this.spnInCorpNames.InnerHtml += string.Format("[{0}]  ", innerCorp.CorpName);
            }

            //外部公司
            result = ccdBll.LoadCorpListByContractId(user, sub.ContractId, false);
            List<NFMT.Contract.Model.ContractCorporationDetail> outCorps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
            foreach (NFMT.Contract.Model.ContractCorporationDetail outCorp in outCorps)
            {
                this.spnOutCorpNames.InnerHtml += string.Format("[{0}]  ", outCorp.CorpName);
            }

            NFMT.Data.Model.MeasureUnit mu = NFMT.Data.BasicDataProvider.MeasureUnits.FirstOrDefault(temp => temp.MUId == sub.UnitId);
            if (mu != null && mu.MUId > 0)
                this.spnSignAmount.InnerHtml = string.Format("{0},{1}",sub.SignAmount,mu.MUName);
            
            //获取库存
            NFMT.WareHouse.BLL.StockBLL stockBLL = new NFMT.WareHouse.BLL.StockBLL();
            result = stockBLL.Get(user, stockLog.StockId);
            if (result.ResultStatus != 0)
                Response.Redirect(redirctUrl);

            NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
            if (stock == null || stock.StockId <= 0)
                Response.Redirect(redirctUrl);

            this.curStock = stock;

            //获取业务单号
            NFMT.WareHouse.BLL.StockNameBLL stockNameBLL = new NFMT.WareHouse.BLL.StockNameBLL();
            result = stockNameBLL.Get(user, stock.StockNameId);
            if (result.ResultStatus != 0)
                Response.Redirect(redirctUrl);

            NFMT.WareHouse.Model.StockName stockName = result.ReturnValue as NFMT.WareHouse.Model.StockName;
            if (stockName == null || stockName.StockNameId <= 0)
                Response.Redirect(redirctUrl);

            this.spanRefNo.InnerHtml = stockName.RefNo;
            this.spanStockDate.InnerHtml = stock.StockDate.ToShortDateString();

            NFMT.User.Model.Corporation ownCorp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp=>temp.CorpId == stock.CorpId);

            if (ownCorp != null && ownCorp.CorpId > 0)
                this.spanCorpId.InnerHtml = ownCorp.CorpName;

            if (mu != null && mu.MUId > 0)
                this.spanGrossAmout.InnerHtml = string.Format("{0},{1}",stock.GrossAmount,mu.MUName);

            NFMT.Data.Model.Asset ass = NFMT.Data.BasicDataProvider.Assets.FirstOrDefault(temp => temp.AssetId == stock.AssetId);
            if (ass != null && ass.AssetId > 0)
                this.spanAssetId.InnerHtml = ass.AssetName;

            NFMT.Data.Model.Brand bra = NFMT.Data.BasicDataProvider.Brands.FirstOrDefault(temp => temp.BrandId == stock.BrandId);
            if (bra != null && bra.BrandId > 0)
                this.spanBrandId.InnerHtml = bra.BrandName;

            this.navigation1.Routes.Add("库存分配列表", redirctUrl);
            this.navigation1.Routes.Add("库存分配新增",string.Empty);

            this.JsonOutCorp = Newtonsoft.Json.JsonConvert.SerializeObject(outCorps);

            for (int i = 0; i < outCorps.Count; i++ )
            {
                NFMT.Contract.Model.ContractCorporationDetail corp = outCorps[i];
                if (corp.CorpId > 0)
                {
                    if (i != 0)
                        this.curOutCorpIds += ",";

                    this.curOutCorpIds += corp.CorpId;
                }
            }

        }
    }
}