using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Contract
{
    public partial class ContractUpdate : System.Web.UI.Page
    {
        public NFMT.Contract.BLL.ContractBLL contractBLL;
        public NFMT.Contract.Model.Contract curContract;
        public NFMT.Contract.Model.ContractDetail curContraceDetail;
        public NFMT.Contract.Model.ContractPrice curContractPrice;
        public List<NFMT.Contract.Model.ContractCorporationDetail> curOutCorps;
        public List<NFMT.Contract.Model.ContractCorporationDetail> curInCorps;
        public List<NFMT.Contract.Model.ContractDept> curDepts;

        public string curOutCorpsString = string.Empty;
        public string curInCorpsString = string.Empty;
        public string curDeptsString = string.Empty;
        public string JsonAttachSelect = string.Empty;

        public NFMT.Common.UserModel user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 37, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                int contractId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect("ContractList.aspx");
                if (!int.TryParse(Request.QueryString["id"], out contractId))
                    Response.Redirect("ContractList.aspx");

                this.navigation1.Routes.Add("合约列表", "ContractList.aspx");
                this.navigation1.Routes.Add("合约修改", string.Empty);

                //类型
                this.hidTradeDirection.Value = ((int)NFMT.Data.StyleEnum.TradeDirection).ToString();
                this.hidTradeBorder.Value = ((int)NFMT.Data.StyleEnum.TradeBorder).ToString();
                this.hidContractLimit.Value = ((int)NFMT.Data.StyleEnum.ContractLimit).ToString();
                //this.hidPriceMode.Value = ((int)NFMT.Data.StyleEnum.PriceMode).ToString();
                this.hidMarginMode.Value = ((int)NFMT.Data.StyleEnum.MarginMode).ToString();
                this.hidValueRateType.Value = ((int)NFMT.Data.StyleEnum.ValueRateType).ToString();
                this.hidDiscountBase.Value = ((int)NFMT.Data.StyleEnum.DiscountBase).ToString();
                this.hidWhoDoPrice.Value = ((int)NFMT.Data.StyleEnum.WhoDoPrice).ToString();
                this.hidSummaryPrice.Value = ((int)NFMT.Data.StyleEnum.SummaryPrice).ToString();

                user = Utility.UserUtility.CurrentUser;

                contractBLL = new NFMT.Contract.BLL.ContractBLL();
                NFMT.Contract.BLL.ContractDetailBLL detailBLL = new NFMT.Contract.BLL.ContractDetailBLL();
                NFMT.Contract.BLL.ContractPriceBLL priceBLL = new NFMT.Contract.BLL.ContractPriceBLL();
                NFMT.Contract.BLL.ContractCorporationDetailBLL corpBLL = new NFMT.Contract.BLL.ContractCorporationDetailBLL();
                NFMT.Contract.BLL.ContractDeptBLL deptBLL = new NFMT.Contract.BLL.ContractDeptBLL();

                //获取合约
                NFMT.Common.ResultModel result = contractBLL.Get(user, contractId);
                if(result.ResultStatus!=0)
                    Response.Redirect("ContractList.aspx");
                curContract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if(curContract==null)
                    Response.Redirect("ContractList.aspx");

                //获取明细
                result = detailBLL.GetDetailByContractId(user, contractId);
                if (result.ResultStatus != 0)
                    Response.Redirect("ContractList.aspx");
                curContraceDetail = result.ReturnValue as NFMT.Contract.Model.ContractDetail;
                if(curContraceDetail==null)
                    Response.Redirect("ContractList.aspx");

                //获取价格
                result = priceBLL.GetPriceByContractId(user, contractId);
                if (result.ResultStatus != 0)
                    Response.Redirect("ContractList.aspx");
                curContractPrice = result.ReturnValue as NFMT.Contract.Model.ContractPrice;
                if(curContractPrice==null)
                    Response.Redirect("ContractList.aspx");

                //获取公司列表
                //我方公司
                result = corpBLL.LoadCorpListByContractId(user, contractId, true);
                if (result.ResultStatus != 0)
                    Response.Redirect("ContractList.aspx");
                curInCorps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
                if(curInCorps==null || curInCorps.Count==0)
                    Response.Redirect("ContractList.aspx");

                //对方公司
                result = corpBLL.LoadCorpListByContractId(user, contractId, false);
                if (result.ResultStatus != 0)
                    Response.Redirect("ContractList.aspx");
                curOutCorps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
                if (curOutCorps == null || curOutCorps.Count == 0)
                    Response.Redirect("ContractList.aspx");

                //执行部门
                result = deptBLL.LoadDeptByContractId(user, contractId);
                if (result.ResultStatus != 0)
                    Response.Redirect("ContractList.aspx");

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
                this.attach1.BusinessIdValue = this.curContract.ContractId;
            }
        }
    }
}