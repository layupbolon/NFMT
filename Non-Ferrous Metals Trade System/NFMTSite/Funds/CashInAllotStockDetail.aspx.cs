using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class CashInAllotStockDetail : System.Web.UI.Page
    {
        public NFMT.Funds.Model.CashInAllot cashInAllot = null;
        public NFMT.Contract.Model.ContractSub curSub = null;
        public NFMT.WareHouse.Model.Stock curStock = null;
        public NFMT.WareHouse.Model.StockLog curStockLog = null;
        public string JsonOutCorp = string.Empty;
        public string curOutCorpIds = string.Empty;
        public string JsonContractSelect = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 58, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成, NFMT.Common.OperateEnum.执行完成撤销 });

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            int allotId = 0;
            string redirctUrl = "CashInAllotStockList.aspx";
            
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out allotId) || allotId <= 0)
                Response.Redirect(redirctUrl);

            //获取收款分配主表
            NFMT.Funds.BLL.CashInAllotBLL cashInAllotBLL = new NFMT.Funds.BLL.CashInAllotBLL();
            result = cashInAllotBLL.Get(user, allotId);
            if (result.ResultStatus != 0)
                Response.Redirect(redirctUrl);

            cashInAllot = result.ReturnValue as NFMT.Funds.Model.CashInAllot;
            if (cashInAllot == null)
                Response.Redirect(redirctUrl);

            NFMT.Common.StatusEnum status = NFMT.Common.StatusEnum.已生效;
            if (cashInAllot.AllotStatus == NFMT.Common.StatusEnum.已完成)
                status = NFMT.Common.StatusEnum.已完成;
            else if (cashInAllot.AllotStatus == NFMT.Common.StatusEnum.已作废)
                status = NFMT.Common.StatusEnum.已作废;
            else if (cashInAllot.AllotStatus == NFMT.Common.StatusEnum.已关闭)
                status = NFMT.Common.StatusEnum.已关闭;

            //通过收款分配Id获取库存收款分配明细表
            NFMT.Funds.BLL.CashInStcokBLL cashInStcokBLL = new NFMT.Funds.BLL.CashInStcokBLL();
            result = cashInStcokBLL.LoadByAllot(user, allotId, status);
            if (result.ResultStatus != 0)
                Response.Redirect(redirctUrl);

            List<NFMT.Funds.Model.CashInStcok> cashInStcoks = result.ReturnValue as List<NFMT.Funds.Model.CashInStcok>;
            if (cashInStcoks == null)
                Response.Redirect(redirctUrl);

            NFMT.Funds.Model.CashInStcok cashInStock = cashInStcoks[0];
            if (cashInStock == null)
                Response.Redirect(redirctUrl);

            int stockLogId = cashInStock.StockLogId;

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
            if (sub == null || sub.SubId <= 0)
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
                this.spnSignAmount.InnerHtml = string.Format("{0},{1}", sub.SignAmount, mu.MUName);

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

            NFMT.User.Model.Corporation ownCorp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == stock.CorpId);

            if (ownCorp != null && ownCorp.CorpId > 0)
                this.spanCorpId.InnerHtml = ownCorp.CorpName;

            if (mu != null && mu.MUId > 0)
                this.spanGrossAmout.InnerHtml = string.Format("{0},{1}", stock.GrossAmount, mu.MUName);

            NFMT.Data.Model.Asset ass = NFMT.Data.BasicDataProvider.Assets.FirstOrDefault(temp => temp.AssetId == stock.AssetId);
            if (ass != null && ass.AssetId > 0)
                this.spanAssetId.InnerHtml = ass.AssetName;

            NFMT.Data.Model.Brand bra = NFMT.Data.BasicDataProvider.Brands.FirstOrDefault(temp => temp.BrandId == stock.BrandId);
            if (bra != null && bra.BrandId > 0)
                this.spanBrandId.InnerHtml = bra.BrandName;

            this.navigation1.Routes.Add("库存分配列表", redirctUrl);
            this.navigation1.Routes.Add("库存分配明细", string.Empty);

            this.JsonOutCorp = Newtonsoft.Json.JsonConvert.SerializeObject(outCorps);

            for (int i = 0; i < outCorps.Count; i++)
            {
                NFMT.Contract.Model.ContractCorporationDetail corp = outCorps[i];
                if (corp.CorpId > 0)
                {
                    if (i != 0)
                        this.curOutCorpIds += ",";

                    this.curOutCorpIds += corp.CorpId;
                }
            }

            NFMT.Funds.BLL.CashInStcokBLL bll = new NFMT.Funds.BLL.CashInStcokBLL();
            NFMT.Common.SelectModel select = bll.GetCurDetailsSelect(1, 100, "cisr.RefId desc", stockLogId);
            result = bll.Load(user, select);

            int totalRows = result.AffectCount;
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

            this.JsonContractSelect = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

            string json = serializer.Serialize(cashInAllot);
            this.hidModel.Value = json;
        }
    }
}