using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockOutApplyUpdate : System.Web.UI.Page
    {
        public NFMT.Contract.Model.ContractSub curSub = null;
        public NFMT.Contract.Model.Contract curContract = null;
        public NFMT.WareHouse.Model.StockOutApply curStockOutApply = null;
        public NFMT.Operate.Model.Apply curApply = null;
        public NFMT.Data.Model.Currency curCurrency = null;
        public NFMT.Data.Model.Asset curAsset = null;
        public NFMT.Data.Model.MeasureUnit curUint = null;
        public string JsonStr = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = "StockOutApplyList.aspx";

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 43, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("出库申请列表", redirectUrl);
                this.navigation1.Routes.Add("出库申请修改", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(redirectUrl);

                int stockOutApplyId = 0;
                if (!int.TryParse(Request.QueryString["id"], out stockOutApplyId))
                    Response.Redirect(redirectUrl);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                //获取出库申请
                NFMT.WareHouse.BLL.StockOutApplyBLL stockOutApplyBLL = new NFMT.WareHouse.BLL.StockOutApplyBLL();
                NFMT.Common.ResultModel result = stockOutApplyBLL.Get(user, stockOutApplyId);
                if(result.ResultStatus!=0)
                    Response.Redirect(redirectUrl);
                NFMT.WareHouse.Model.StockOutApply apply = result.ReturnValue as NFMT.WareHouse.Model.StockOutApply;
                if(apply==null || apply.ApplyId==0)
                    Response.Redirect(redirectUrl);

                this.curStockOutApply = apply;

                //获取关联子合约
                NFMT.Contract.BLL.ContractSubBLL subBll = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBll.Get(user, apply.SubContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.ContractId == 0)
                    Response.Redirect(redirectUrl);

                this.curSub = sub;
                this.curCurrency = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(temp => temp.CurrencyId == sub.SettleCurrency);

                //获取关联合约
                NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
                result = bll.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Contract.Model.Contract con = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (con == null || con.ContractId == 0)
                    Response.Redirect(redirectUrl);

                this.curContract = con;
                this.curAsset = NFMT.Data.BasicDataProvider.Assets.FirstOrDefault(temp => temp.AssetId == con.AssetId);
                this.curUint = NFMT.Data.BasicDataProvider.MeasureUnits.FirstOrDefault(temp => temp.MUId == sub.UnitId);

                //获取主申请信息
                NFMT.Operate.BLL.ApplyBLL mainApplyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = mainApplyBLL.Get(user, apply.ApplyId);
                if (result.ResultStatus != 0) 
                    Response.Redirect(redirectUrl);
                NFMT.Operate.Model.Apply mainApply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if(mainApply==null || mainApply.ApplyId<=0)
                    Response.Redirect(redirectUrl);

                if(mainApply.ApplyStatus >=NFMT.Common.StatusEnum.待审核)
                    Response.Redirect(redirectUrl);

                this.txbMemo.Value = mainApply.ApplyDesc;
                this.curApply = mainApply;

                //获取当前出库申请明细
                //NFMT.WareHouse.BLL.StockOutApplyDetailBLL detailBLL = new NFMT.WareHouse.BLL.StockOutApplyDetailBLL();
                //result = detailBLL.Load(user, apply.StockOutApplyId);
                //if(result.ResultStatus!=0)
                //    Response.Redirect(redirectUrl);

                //List<NFMT.WareHouse.Model.StockOutApplyDetail> details = result.ReturnValue as List<NFMT.WareHouse.Model.StockOutApplyDetail>;
                //if(details==null)
                //    Response.Redirect(redirectUrl);                

                NFMT.Common.SelectModel select = stockOutApplyBLL.GetOutApplySelectedSelect(1, 100, "sto.StockId desc",string.Empty,curStockOutApply.StockOutApplyId);
                result = stockOutApplyBLL.Load(user, select);
                int totalRows = result.AffectCount;
                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                this.JsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                this.contractExpander1.CurContract = con;
                this.contractExpander1.CurContractSub = sub;
                this.contractExpander1.RedirectUrl = redirectUrl;
            }
        }        
    }
}