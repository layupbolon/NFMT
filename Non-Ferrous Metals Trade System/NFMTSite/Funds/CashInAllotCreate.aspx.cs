using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class CashInAllotCreate : System.Web.UI.Page
    {
        public NFMT.Contract.Model.ContractSub curSub = null;
        public string JsonOutCorp = string.Empty;
        public string curOutCorpIds = string.Empty;
        public string JsonStock = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 94, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                string redirectUrl = "CashInAllotList.aspx";
                this.navigation1.Routes.Add("收款分配列表", redirectUrl);
                this.navigation1.Routes.Add("合约收款分配新增", string.Empty);

                int subId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out subId) || subId <= 0)
                    Response.Redirect(redirectUrl);

                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                //获取子合约
                NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBLL.Get(user, subId);
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

                result = subBLL.GetContractOutCorp(user, subId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.contractExpander1.CurContract = contract;
                this.contractExpander1.CurContractSub = sub;
                this.contractExpander1.RedirectUrl = redirectUrl;

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

                //拼接合约库存
                NFMT.WareHouse.BLL.StockLogBLL stockLogBLL = new NFMT.WareHouse.BLL.StockLogBLL();
                NFMT.Common.SelectModel select = stockLogBLL.GetLogListBySubSelect(1, 100, "sl.StockLogId desc", sub.SubId);
                result = stockLogBLL.Load(user, select);

                dt = result.ReturnValue as System.Data.DataTable;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("[");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        System.Data.DataRow dr = dt.Rows[i];

                        if (i != 0)
                            sb.Append(",");

                        string refNo = dr["RefNo"].ToString();
                        int stockLogId = 0;
                        decimal stockLogGrossWeight = 0;
                        decimal stockLogNetWeight = 0;
                        int.TryParse(dr["StockLogId"].ToString(), out stockLogId);
                        decimal.TryParse(dr["GrossAmount"].ToString(), out stockLogGrossWeight);
                        decimal.TryParse(dr["NetAmount"].ToString(),out stockLogNetWeight);
                        string muName = dr["MUName"].ToString();
                        string assetName = dr["AssetName"].ToString();
                        string brandName = dr["BrandName"].ToString();

                        sb.Append("{ Html: \"<div style='padding: 1px;'>");
                        sb.AppendFormat("<div>{0}</div>", refNo);
                        sb.AppendFormat("<div>毛重: {0}{1}</div>", stockLogGrossWeight, muName);
                        sb.AppendFormat("<div>净重: {0}{1}</div>", stockLogNetWeight, muName);
                        sb.AppendFormat("<div>品种: {0}</div><div>品牌: {1}</div></div>\"", assetName, brandName);
                        sb.AppendFormat(", Title: \"{0}\"",refNo);
                        sb.Append(",StockLogId:");
                        sb.Append(stockLogId);
                        sb.Append("}");
                    }
                }
                sb.Append("]");

                this.JsonStock = sb.ToString();
            }
        }
    }
}