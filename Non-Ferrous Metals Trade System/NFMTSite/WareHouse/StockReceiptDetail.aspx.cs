﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockReceiptDetail : System.Web.UI.Page
    {
        public NFMT.Contract.Model.Contract curContract = null;
        public NFMT.Contract.Model.ContractSub curContractSub = null;
        public NFMT.WareHouse.Model.StockReceipt curStockReceipt = null;
        public string curReceiptingJson = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = "StockReceiptList.aspx";

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 88, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成, NFMT.Common.OperateEnum.执行完成撤销 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                //获取仓库回执
                int receiptId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out receiptId))
                    Response.Redirect(redirectUrl);

                NFMT.WareHouse.BLL.StockReceiptBLL receiptBLL = new NFMT.WareHouse.BLL.StockReceiptBLL();
                result = receiptBLL.Get(user, receiptId);

                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.WareHouse.Model.StockReceipt stockReceipt = result.ReturnValue as NFMT.WareHouse.Model.StockReceipt;
                if (stockReceipt == null || stockReceipt.ReceiptId <= 0)
                    Response.Redirect(redirectUrl);

                this.curStockReceipt = stockReceipt;

                //获取合约与子合约               
                NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBLL.Get(user, stockReceipt.ContractSubId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                    Response.Redirect(redirectUrl);

                this.curContractSub = sub;               

                NFMT.Contract.BLL.ContractBLL conBLL = new NFMT.Contract.BLL.ContractBLL();
                result = conBLL.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                //主合约
                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                    Response.Redirect(redirectUrl);

                this.curContract = contract;

                this.contractExpander1.CurContract = contract;
                this.contractExpander1.CurContractSub = sub;
                this.contractExpander1.RedirectUrl = redirectUrl;

                this.navigation1.Routes.Add("回执列表", redirectUrl);
                this.navigation1.Routes.Add("回执明细", string.Empty);

                string sids = "0";
                //获取回执明细
                NFMT.WareHouse.BLL.StockReceiptDetailBLL detailBLL = new NFMT.WareHouse.BLL.StockReceiptDetailBLL();
                result = detailBLL.Load(user, stockReceipt.ReceiptId,NFMT.Common.StatusEnum.已作废);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                List<NFMT.WareHouse.Model.StockReceiptDetail> details = result.ReturnValue as List<NFMT.WareHouse.Model.StockReceiptDetail>;
                if (details == null)
                    Response.Redirect(redirectUrl);

                if (details.Count > 0)
                    sids = string.Empty;
                for (int i = 0; i < details.Count; i++)
                {
                    NFMT.WareHouse.Model.StockReceiptDetail detail = details[i];
                    sids += detail.StockLogId.ToString();
                    if (i < details.Count - 1)
                        sids += ",";
                }

                NFMT.Common.SelectModel select = receiptBLL.GetReceiptingStockListSelect(1, 100, "sto.StockId desc", sub.SubId, receiptId, sids);
                result = receiptBLL.Load(user, select);
                int totalRows = result.AffectCount;
                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                this.curReceiptingJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(stockReceipt);
                this.hidModel.Value = json;
            }
        }
    }
}