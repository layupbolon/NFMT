using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class CashInAllotContractUpdate : System.Web.UI.Page
    {
        public NFMT.Contract.Model.ContractSub curSub = null;
        public string JsonOutCorp = string.Empty;
        public string curOutCorpIds = string.Empty;
        public NFMT.Funds.Model.CashInAllot curCashInAllot = null;
        public string JsonCorpSelect = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 57, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                string redirectUrl = "CashInAllotList.aspx";
                this.navigation1.Routes.Add("收款分配列表", redirectUrl);
                this.navigation1.Routes.Add("合约收款分配修改", string.Empty);

                //int subId = 0;
                //if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out subId) || subId <= 0)
                //    Response.Redirect(redirectUrl);

                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                //获取分配
                int allotId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out allotId) || allotId <= 0)
                    Response.Redirect(redirectUrl);

                NFMT.Funds.BLL.CashInAllotBLL cashInAllotBLL = new NFMT.Funds.BLL.CashInAllotBLL();
                result = cashInAllotBLL.Get(user, allotId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Funds.Model.CashInAllot cashInAllot = result.ReturnValue as NFMT.Funds.Model.CashInAllot;
                if (cashInAllot == null || cashInAllot.AllotId <= 0)
                    Response.Redirect(redirectUrl);

                this.curCashInAllot = cashInAllot;

                //获取合约分配明细
                NFMT.Funds.BLL.CashInContractBLL cashInContractBLL = new NFMT.Funds.BLL.CashInContractBLL();
                result = cashInContractBLL.GetByAllot(user, allotId, NFMT.Common.StatusEnum.已生效);
                if(result.ResultStatus!=0)
                    Response.Redirect(redirectUrl);

                NFMT.Funds.Model.CashInContract cashInContract = result.ReturnValue as NFMT.Funds.Model.CashInContract;
                if (cashInContract == null || cashInContract.RefId == 0)
                    Response.Redirect(redirectUrl);

                //获取子合约
                NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBLL.Get(user, cashInContract.SubContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                    Response.Redirect(redirectUrl);

                this.curSub = sub;

                //获取合约
                NFMT.Contract.BLL.ContractBLL contractBLL = new NFMT.Contract.BLL.ContractBLL();
                result = contractBLL.Get(user, sub.ContractId);

                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                    Response.Redirect(redirectUrl);

                #region 赋值
                //合约信息
                this.spnContractNo.InnerHtml = contract.ContractNo;
                this.spnAsset.InnerHtml = NFMT.Data.BasicDataProvider.Assets.First(temp => temp.AssetId == contract.AssetId).AssetName;
                NFMT.Data.Model.MeasureUnit muContract = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == contract.UnitId);
                this.spnSignAmount.InnerHtml = string.Format("{0}{1}", contract.SignAmount.ToString(), muContract.MUName);

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

                //子合约信息
                this.spnSubNo.InnerHtml = sub.SubNo;

                NFMT.Data.Model.MeasureUnit muSub = NFMT.Data.BasicDataProvider.MeasureUnits.SingleOrDefault(temp => temp.MUId == sub.UnitId);
                if (muSub != null && muSub.MUId > 0)
                    this.spnSubSignAmount.InnerHtml = string.Format("{0}{1}", sub.SignAmount.ToString(), muSub.MUName);
                this.spnPeriodE.InnerHtml = sub.ContractPeriodE.ToShortDateString();

                #endregion

                result = subBLL.GetContractOutCorp(user, sub.SubId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                if (dt == null || dt.Rows.Count == 0)
                    Response.Redirect(redirectUrl);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    System.Data.DataRow dr = dt.Rows[i];

                    if (dr["CorpId"] != DBNull.Value)
                    {
                        if (i != 0)
                            this.curOutCorpIds += ",";

                        this.curOutCorpIds += dr["CorpId"].ToString();
                    }
                }

                this.JsonOutCorp = Newtonsoft.Json.JsonConvert.SerializeObject(result.ReturnValue);

                NFMT.Funds.BLL.CashInContractBLL bll = new NFMT.Funds.BLL.CashInContractBLL();
                NFMT.Common.SelectModel select = bll.GetCurDetailsSelect(1, 100, "cicr.RefId desc", sub.SubId,NFMT.Common.StatusEnum.已生效);
                result = bll.Load(user, select);

                int totalRows = result.AffectCount;                
                dt = result.ReturnValue as System.Data.DataTable;

                this.JsonCorpSelect = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

            }
        }
    }
}