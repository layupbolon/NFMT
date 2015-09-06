using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Contract
{
    public partial class ContractInUpdate : System.Web.UI.Page
    {
        public NFMT.Common.UserModel user;
        public NFMT.Contract.BLL.ContractBLL contractBLL = new NFMT.Contract.BLL.ContractBLL();
        public NFMT.Data.Model.Asset curAsset = null;
        public NFMT.Contract.Model.Contract curContract = null;
        public NFMT.Contract.Model.ContractSub curSub = null;

        public NFMT.Contract.Model.ContractDetail curContraceDetail;
        public NFMT.Contract.Model.ContractPrice curContractPrice;
        public string curOutCorpsString = string.Empty;
        public string curInCorpsString = string.Empty;
        public string curDeptsString = string.Empty;
        public string curContractTypesString = string.Empty;

        public string SelectedJson = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 37, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

            user = Utility.UserUtility.CurrentUser;
            string redirectUrl = "SubStockInList.aspx";

            this.navigation1.Routes.Add("合约列表", "ContractList.aspx");
            this.navigation1.Routes.Add("库存列表", redirectUrl);
            this.navigation1.Routes.Add("合约修改", string.Empty);

            int subId = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["sid"]))
                int.TryParse(Request.QueryString["sid"], out subId);

            int contractId = 0;
            if (subId <= 0 && (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out contractId) || contractId <= 0))
                Utility.JsUtility.WarmAlert(this, "合约序号错误", redirectUrl);

            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
            NFMT.Contract.BLL.ContractDetailBLL detailBLL = new NFMT.Contract.BLL.ContractDetailBLL();
            NFMT.Contract.BLL.ContractPriceBLL priceBLL = new NFMT.Contract.BLL.ContractPriceBLL();
            NFMT.Contract.BLL.ContractCorporationDetailBLL corpBLL = new NFMT.Contract.BLL.ContractCorporationDetailBLL();
            NFMT.Contract.BLL.ContractDeptBLL deptBLL = new NFMT.Contract.BLL.ContractDeptBLL();

            //主合约进入
            if (contractId > 0)
            {
                result = contractBLL.Get(user, contractId);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this, "合约不存在", redirectUrl);

                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                    Utility.JsUtility.WarmAlert(this, "合约不存在", redirectUrl);

                if (contract.CreateFrom != (int)NFMT.Common.CreateFromEnum.采购合约库存创建)
                    Utility.JsUtility.WarmAlert(this, "合约创建来源不正确", redirectUrl);

                this.curContract = contract;

                result = subBLL.GetSubByContractId(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this, "子合约获取失败", redirectUrl);

                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                    Utility.JsUtility.WarmAlert(this, "子合约获取失败", redirectUrl);

                this.curSub = sub;
            }
            else if (subId > 0)//子合约进入
            {
                result = subBLL.Get(user, subId);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this, "子合约获取失败", redirectUrl);

                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                    Utility.JsUtility.WarmAlert(this, "子合约获取失败", redirectUrl);

                this.curSub = sub;

                result = contractBLL.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this, "合约不存在", redirectUrl);

                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                    Utility.JsUtility.WarmAlert(this, "合约不存在", redirectUrl);

                if (contract.CreateFrom != (int)NFMT.Common.CreateFromEnum.采购合约库存创建)
                    Utility.JsUtility.WarmAlert(this, "合约创建来源不正确", redirectUrl);

                this.curContract = contract;
            }

            //获取合约品种
            NFMT.Data.Model.Asset asset = NFMT.Data.BasicDataProvider.Assets.FirstOrDefault(temp => temp.AssetId == this.curContract.AssetId);
            if (asset == null || asset.AssetId <= 0)
                Utility.JsUtility.WarmAlert(this, "合约品种获取失败", redirectUrl);

            this.curAsset = asset;

            //获取明细
            result = detailBLL.GetDetailByContractId(user, this.curContract.ContractId);
            if (result.ResultStatus != 0)
                Utility.JsUtility.WarmAlert(this, "合约明细获取失败", redirectUrl);
            curContraceDetail = result.ReturnValue as NFMT.Contract.Model.ContractDetail;
            if (curContraceDetail == null)
                Utility.JsUtility.WarmAlert(this, "合约明细获取失败", redirectUrl);

            //获取价格
            result = priceBLL.GetPriceByContractId(user, this.curContract.ContractId);
            if (result.ResultStatus != 0)
                Utility.JsUtility.WarmAlert(this, "合约价格获取失败", redirectUrl);
            curContractPrice = result.ReturnValue as NFMT.Contract.Model.ContractPrice;
            if (curContractPrice == null)
                Utility.JsUtility.WarmAlert(this, "合约价格获取失败", redirectUrl);

            //获取公司列表
            //我方公司
            result = corpBLL.LoadCorpListByContractId(user, this.curContract.ContractId, true);
            if (result.ResultStatus != 0)
                Utility.JsUtility.WarmAlert(this, "合约我方抬头获取失败", redirectUrl);
            List<NFMT.Contract.Model.ContractCorporationDetail> inCorps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
            if (inCorps == null || inCorps.Count == 0)
                Utility.JsUtility.WarmAlert(this, "合约我方抬头获取失败", redirectUrl);

            //对方公司
            result = corpBLL.LoadCorpListByContractId(user, this.curContract.ContractId, false);
            if (result.ResultStatus != 0)
                Utility.JsUtility.WarmAlert(this, "合约对方抬头获取失败", redirectUrl);
            List<NFMT.Contract.Model.ContractCorporationDetail> outCorps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
            if (outCorps == null || outCorps.Count == 0)
                Utility.JsUtility.WarmAlert(this, "合约对方抬头获取失败", redirectUrl);

            //执行部门
            result = deptBLL.LoadDeptByContractId(user, this.curContract.ContractId);
            if (result.ResultStatus != 0)
                Utility.JsUtility.WarmAlert(this, "执行部门获取失败", redirectUrl);

            List<NFMT.Contract.Model.ContractDept> depts = result.ReturnValue as List<NFMT.Contract.Model.ContractDept>;

            foreach (var obj in outCorps)
            {
                if (outCorps.IndexOf(obj) > 0)
                    curOutCorpsString += ",";
                curOutCorpsString += obj.CorpId.ToString();
            }

            foreach (var obj in inCorps)
            {
                if (inCorps.IndexOf(obj) > 0)
                    this.curInCorpsString += ",";
                curInCorpsString += obj.CorpId.ToString();
            }

            foreach (var obj in depts)
            {
                if (depts.IndexOf(obj) > 0)
                    curDeptsString += ",";
                curDeptsString += obj.DeptId.ToString();
            }

            //合约类型
            NFMT.Contract.BLL.ContractTypeDetailBLL contractTypeBLL = new NFMT.Contract.BLL.ContractTypeDetailBLL();
            result = contractTypeBLL.LoadContractTypesById(user, this.curContract.ContractId);
            if (result.ResultStatus != 0)
                this.WarmAlert("合约类型获取失败", redirectUrl);
            List<NFMT.Contract.Model.ContractTypeDetail> contractTypes = result.ReturnValue as List<NFMT.Contract.Model.ContractTypeDetail>;
            if(contractTypes == null)
                this.WarmAlert("合约类型获取失败", redirectUrl);
            foreach (NFMT.Contract.Model.ContractTypeDetail contractType in contractTypes)
            {
                if (contractTypes.IndexOf(contractType) >0)
                    this.curContractTypesString += ",";

                this.curContractTypesString += contractType.ContractType.ToString();
            }

            NFMT.WareHouse.BLL.StockLogBLL stockLogBLL = new NFMT.WareHouse.BLL.StockLogBLL();

            int pageIndex = 1;
            int pageSize = 100;
            string orderStr = string.Empty;

            NFMT.Common.SelectModel select = stockLogBLL.GetStockLogsByContractId(pageIndex, pageSize, orderStr, this.curContract.ContractId);
            result = stockLogBLL.Load(user, select, NFMT.Common.DefaultValue.ClearAuth);
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            this.SelectedJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());           

            this.attach1.BusinessIdValue = this.curContract.ContractId;
        }
    }
}