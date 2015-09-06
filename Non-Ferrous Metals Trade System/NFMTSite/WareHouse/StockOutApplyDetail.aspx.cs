using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockOutApplyDetail : System.Web.UI.Page
    {
        public NFMT.Contract.Model.ContractSub curSub = null;
        public NFMT.Contract.Model.Contract curContract = null;
        public NFMT.WareHouse.Model.StockOutApply curStockOutApply = null;
        public NFMT.Operate.Model.Apply curApply = null;
        public NFMT.Data.Model.Currency curCurrency = null;
        public NFMT.Data.Model.Asset curAsset = null;
        public NFMT.Data.Model.MeasureUnit curUint = null;
        public string JsonStr = string.Empty;
        public NFMT.Operate.Model.Apply mainApply = new NFMT.Operate.Model.Apply();

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = "StockOutApplyList.aspx";

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 43, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成, NFMT.Common.OperateEnum.执行完成撤销 });

                this.navigation1.Routes.Add("出库申请列表", redirectUrl);
                this.navigation1.Routes.Add("出库申请明细", string.Empty);

                int applyId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["aid"]) || !int.TryParse(Request.QueryString["aid"], out applyId))
                    applyId = 0;

                if (applyId==0 && string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(redirectUrl);

                int stockOutApplyId = 0;
                if (applyId == 0 &&  !int.TryParse(Request.QueryString["id"], out stockOutApplyId))
                    Response.Redirect(redirectUrl);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                //获取出库申请
                NFMT.WareHouse.BLL.StockOutApplyBLL stockOutApplyBLL = new NFMT.WareHouse.BLL.StockOutApplyBLL();
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
                if(applyId>0)
                    result = stockOutApplyBLL.GetByApplyId(user, applyId);
                else
                    result = stockOutApplyBLL.Get(user, stockOutApplyId);               

                if(result.ResultStatus!=0)
                    Response.Redirect(redirectUrl);
                NFMT.WareHouse.Model.StockOutApply apply = result.ReturnValue as NFMT.WareHouse.Model.StockOutApply;
                if(apply==null || apply.ApplyId==0)
                    Response.Redirect(redirectUrl);

                stockOutApplyId = apply.StockOutApplyId;

                //获取主申请表信息
                NFMT.Operate.BLL.ApplyBLL mainApplyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = mainApplyBLL.Get(user, apply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                mainApply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (mainApply == null || mainApply.ApplyId <= 0)
                    Response.Redirect(redirectUrl);

                //获取关联子合约
                NFMT.Contract.BLL.ContractSubBLL subBll = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBll.Get(user, apply.SubContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.ContractId == 0)
                    Response.Redirect(redirectUrl);

                //获取关联合约
                NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
                result = bll.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Contract.Model.Contract con = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (con == null || con.ContractId == 0)
                    Response.Redirect(redirectUrl);

                this.curAsset = NFMT.Data.BasicDataProvider.Assets.FirstOrDefault(temp => temp.AssetId == con.AssetId);
                this.curUint = NFMT.Data.BasicDataProvider.MeasureUnits.FirstOrDefault(temp => temp.MUId == con.UnitId);
                this.curApply = mainApply;
                this.curContract = con;
                this.curSub = sub;
                this.curCurrency = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(temp => temp.CurrencyId == sub.SettleCurrency);
                this.curStockOutApply = apply;


                NFMT.Data.Model.MeasureUnit subMU = NFMT.Data.BasicDataProvider.MeasureUnits.FirstOrDefault(temp => temp.MUId == sub.UnitId);
                this.txbMemo.Value = mainApply.ApplyDesc;

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(mainApply);
                this.hidModel.Value = json;

                NFMT.Common.SelectModel select = stockOutApplyBLL.GetOutApplySelectedSelect(1, 100, "sto.StockId desc", string.Empty, curStockOutApply.StockOutApplyId);
                result = stockOutApplyBLL.Load(user, select);
                int totalRows = result.AffectCount;
                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                this.JsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                this.contractExpander1.CurContract = con;
                this.contractExpander1.CurContractSub = sub;
                this.contractExpander1.RedirectUrl = redirectUrl;

                result = stockOutApplyBLL.GetAuditInfo(user, apply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect("StockOutApplyList.aspx");

                this.txbAuditInfo.InnerHtml = result.ReturnValue.ToString();
            }
        }
    }
}