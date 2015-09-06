using System;
using System.Data;
using System.Web.UI;
using NFMT.Common;
using NFMT.Contract.BLL;
using NFMT.DoPrice.BLL;
using NFMT.Finance.BLL;
using NFMT.Funds.BLL;
using NFMT.Invoice.BLL;
using NFMT.WareHouse.BLL;
using NFMTSite.Utility;
using PledgeApplyBLL = NFMT.Finance.BLL.PledgeApplyBLL;
using RepoApplyBLL = NFMT.Finance.BLL.RepoApplyBLL;

namespace NFMTSite.Report
{
    public partial class ExportExcel : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserModel user = UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                SelectModel select = null;
                ResultModel result = null;
                BaseBLL bll = null;

                int pageIndex = 1, pageSize = 99999999, rt = 0;
                string orderStr = string.Empty;
                if (string.IsNullOrEmpty(Request.QueryString["rt"]) || !int.TryParse(Request.QueryString["rt"], out rt) || rt <= 0)
                {
                    Response.Write("报表导出错误");
                    Response.End();
                }

                ReportType reportType = (ReportType)rt;

                DateTime startDate = DefaultValue.DefaultTime;
                DateTime endDate = DefaultValue.DefaultTime;

                if (string.IsNullOrEmpty(Request.QueryString["sd"]) || !DateTime.TryParse(Request.QueryString["sd"], out startDate))
                    startDate = DefaultValue.DefaultTime;

                if (string.IsNullOrEmpty(Request.QueryString["ed"]) || !DateTime.TryParse(Request.QueryString["ed"], out endDate))
                    endDate = DefaultValue.DefaultTime;
                else
                    endDate = endDate.AddDays(1);

                string contractNo = Request.QueryString["cn"];

                int inCorpId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["ici"]) || !int.TryParse(Request.QueryString["ici"], out inCorpId))
                    inCorpId = 0;

                int outCorpId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["oci"]) || !int.TryParse(Request.QueryString["oci"], out outCorpId))
                    outCorpId = 0;

                int logType = 0;
                if (string.IsNullOrEmpty(Request.QueryString["lt"]) || !int.TryParse(Request.QueryString["lt"], out logType))
                    logType = 0;

                switch (reportType)
                {
                    case ReportType.StockReport:

                        #region StockReport
                        StockBLL stockBLL = new StockBLL();
                        bll = stockBLL;

                        DateTime stockDateBegin = DefaultValue.DefaultTime;
                        DateTime stockDateEnd = DefaultValue.DefaultTime;
                        if (string.IsNullOrEmpty(Request.QueryString["sdb"]) || !DateTime.TryParse(Request.QueryString["sdb"], out stockDateBegin))
                            stockDateBegin = DefaultValue.DefaultTime;

                        if (string.IsNullOrEmpty(Request.QueryString["sde"]) || !DateTime.TryParse(Request.QueryString["sde"], out stockDateEnd))
                            stockDateEnd = DefaultValue.DefaultTime;
                        else
                            stockDateEnd = stockDateEnd.AddDays(1);

                        string stockName = Request.QueryString["sn"];

                        int corpId = 0;
                        if (string.IsNullOrEmpty(Request.QueryString["ci"]) || !int.TryParse(Request.QueryString["ci"], out corpId))
                            corpId = 0;

                        int saleInfo = 0;
                        if (string.IsNullOrEmpty(Request.QueryString["sinfo"]) || !int.TryParse(Request.QueryString["sinfo"], out saleInfo))
                            saleInfo = 0;

                        int stockStatus = 0;
                        if (string.IsNullOrEmpty(Request.QueryString["s"]) || int.TryParse(Request.QueryString["s"], out stockStatus))
                            stockStatus = 0;

                        orderStr = "sto.StockId desc";

                        select = stockBLL.GetStockReportSelect(pageIndex, pageSize, orderStr, stockName, stockDateBegin, stockDateEnd, corpId, stockStatus, saleInfo);
                        #endregion

                        break;
                    case ReportType.StockLogReport:

                        #region StockLogReport
                        StockLogBLL stockLogBLL = new StockLogBLL();
                        bll = stockLogBLL;

                        string refNo = Request.QueryString["rn"];                      

                        int customsType = 0;
                        if (string.IsNullOrEmpty(Request.QueryString["ct"]) || !int.TryParse(Request.QueryString["ct"], out customsType))
                            customsType = 0;

                        int assetId = 0;
                        if (string.IsNullOrEmpty(Request.QueryString["ass"]) || !int.TryParse(Request.QueryString["ass"], out assetId))
                            assetId = 0;

                        orderStr = "sl.StockLogId desc";
                        select = stockLogBLL.GetStockLogReportSelect(pageIndex, pageSize, orderStr, refNo, logType, customsType, assetId, startDate, endDate);
                        #endregion

                        break;
                    case ReportType.PricingLogReport:

                        #region PricingLogReport
                        PricingBLL pricingBLL = new PricingBLL();
                        bll = pricingBLL;

                        DateTime pricingstartDate = DefaultValue.DefaultTime;
                        DateTime pricingendDate = DefaultValue.DefaultTime;

                        if (string.IsNullOrEmpty(Request.QueryString["s"]) || !DateTime.TryParse(Request.QueryString["s"], out pricingstartDate))
                            pricingstartDate = DefaultValue.DefaultTime;

                        if (string.IsNullOrEmpty(Request.QueryString["e"]) || !DateTime.TryParse(Request.QueryString["e"], out pricingendDate))
                            pricingendDate = DefaultValue.DefaultTime;
                        else
                            pricingendDate = pricingendDate.AddDays(1);

                        contractNo = Request.QueryString["c"];

                        int pricingassetId = 0;
                        if (string.IsNullOrEmpty(Request.QueryString["a"]) || !int.TryParse(Request.QueryString["a"], out pricingassetId))
                            pricingassetId = 0;

                        orderStr = "p.PricingId desc";
                        select = pricingBLL.GetDoPriceReportSelect(pageIndex, pageSize, orderStr, contractNo, pricingassetId, pricingstartDate, pricingendDate);
                        #endregion

                        break;
                    case ReportType.CustomsReport:

                        #region CustomsReport
                        CustomsClearanceBLL customsClearanceBLL = new CustomsClearanceBLL();
                        bll = customsClearanceBLL;

                        DateTime customstartDate = DefaultValue.DefaultTime;
                        DateTime customendDate = DefaultValue.DefaultTime;

                        if (string.IsNullOrEmpty(Request.QueryString["s"]) || !DateTime.TryParse(Request.QueryString["s"], out customstartDate))
                            customstartDate = DefaultValue.DefaultTime;

                        if (string.IsNullOrEmpty(Request.QueryString["e"]) || !DateTime.TryParse(Request.QueryString["e"], out customendDate))
                            customendDate = DefaultValue.DefaultTime;
                        else
                            customendDate = customendDate.AddDays(1);

                        string customRefNo = Request.QueryString["r"];

                        int customsCorpId = 0;
                        if (string.IsNullOrEmpty(Request.QueryString["c"]) || !int.TryParse(Request.QueryString["c"], out customsCorpId))
                            customsCorpId = 0;

                        orderStr = "cc.CustomsId desc";
                        select = customsClearanceBLL.GetCustomReportSelect(pageIndex, pageSize, orderStr, customsCorpId, customRefNo, customstartDate, customendDate);
                        #endregion

                        break;
                    case ReportType.ContractProgress:

                        #region ContractProgress
                        int tradeBorder = 0;
                        if (string.IsNullOrEmpty(Request.QueryString["tb"]) || !int.TryParse(Request.QueryString["tb"], out tradeBorder))
                            tradeBorder = 0;

                        int tradeDirection = 0;
                        if (string.IsNullOrEmpty(Request.QueryString["td"]) || !int.TryParse(Request.QueryString["td"], out tradeDirection))
                            tradeDirection = 0;

                        ContractBLL contractBLL = new ContractBLL();
                        bll = contractBLL;
                        orderStr = "con.ContractId desc";
                        select = contractBLL.GetContractProgressSelect(pageIndex, pageSize, orderStr, startDate, endDate, contractNo, inCorpId, outCorpId, tradeBorder, tradeDirection);

                        #endregion

                        break;
                    case ReportType.BusInvReport:

                        #region BusInvReport
                        BusinessInvoiceBLL businessInvoiceBLL = new BusinessInvoiceBLL();
                        bll = businessInvoiceBLL;

                        if (string.IsNullOrEmpty(Request.QueryString["s"]) || !DateTime.TryParse(Request.QueryString["s"], out startDate))
                            startDate = DefaultValue.DefaultTime;

                        if (string.IsNullOrEmpty(Request.QueryString["e"]) || !DateTime.TryParse(Request.QueryString["e"], out endDate))
                            endDate = DefaultValue.DefaultTime;
                        else
                            endDate = endDate.AddDays(1);

                        if (string.IsNullOrEmpty(Request.QueryString["inner"]) || !int.TryParse(Request.QueryString["inner"], out inCorpId))
                            inCorpId = 0;

                        if (string.IsNullOrEmpty(Request.QueryString["outer"]) || !int.TryParse(Request.QueryString["outer"], out outCorpId))
                            outCorpId = 0;

                        int invType = 0;
                        if (string.IsNullOrEmpty(Request.QueryString["invType"]) || !int.TryParse(Request.QueryString["invType"], out invType))
                            invType = 0;

                        int BusInvassetId = 0;
                        if (string.IsNullOrEmpty(Request.QueryString["ass"]) || !int.TryParse(Request.QueryString["ass"], out BusInvassetId))
                            BusInvassetId = 0;

                        orderStr = "inv.InvoiceId desc";
                        select = businessInvoiceBLL.GetBusInvReportSelect(pageIndex, pageSize, orderStr, inCorpId, outCorpId, invType, BusInvassetId, startDate, endDate);

                        #endregion

                        break;

                    case ReportType.CashLogReport:

                        #region CashLogReport
                        FundsLogBLL fundsLogBLL = new FundsLogBLL();
                        bll = fundsLogBLL;
                        orderStr = "fl.FundsLogId desc";
                        select = fundsLogBLL.GetFundsLogReportSelect(pageIndex, pageSize, orderStr,startDate,endDate, inCorpId, outCorpId,logType);
                        #endregion

                        break;
                    case ReportType.FundsCurrent:

                        #region FundsCurrent
                        FundsBLL fundsBLL = new FundsBLL();
                        bll = fundsBLL;

                        if (string.IsNullOrEmpty(Request.QueryString["s"]) || !DateTime.TryParse(Request.QueryString["s"], out startDate))
                            startDate = DefaultValue.DefaultTime;

                        if (string.IsNullOrEmpty(Request.QueryString["e"]) || !DateTime.TryParse(Request.QueryString["e"], out endDate))
                            endDate = DefaultValue.DefaultTime;
                        //else
                        //    endDate = endDate.AddDays(1);

                        if (string.IsNullOrEmpty(Request.QueryString["in"]) || !int.TryParse(Request.QueryString["in"], out inCorpId))
                            inCorpId = 0;

                        if (string.IsNullOrEmpty(Request.QueryString["out"]) || !int.TryParse(Request.QueryString["out"], out outCorpId))
                            outCorpId = 0;

                        orderStr = "corp.CorpName asc";
                        select = fundsBLL.GetFundsCurrentReportSelect(pageIndex, pageSize, orderStr, inCorpId, outCorpId, startDate, endDate);

                        #endregion

                        break;
                    case ReportType.StockReceiptReport:

                        #region StockReceipt
                        StockReceiptBLL stockReceiptBLL = new StockReceiptBLL();
                        bll = stockReceiptBLL;

                        int stockReceiptAssetId = 0;
                        if (string.IsNullOrEmpty(Request.QueryString["ass"]) || !int.TryParse(Request.QueryString["ass"], out stockReceiptAssetId))
                            stockReceiptAssetId = 0;

                        refNo = Request.QueryString["refNo"];

                        orderStr = "sis.RefId desc";
                        select = stockReceiptBLL.GetReceiptReportSelect(pageIndex, pageSize, orderStr, stockReceiptAssetId, refNo);

                        #endregion

                        break;
                    case ReportType.GrossProfitReport:

                        #region GrossProfitReport
                        BusinessInvoiceDetailBLL businessInvoiceDetailBLL = new BusinessInvoiceDetailBLL();
                        bll = businessInvoiceDetailBLL;

                        refNo = string.Empty;
                        refNo = Request.QueryString["refNo"];

                        string cardNo = Request.QueryString["cardNo"];

                        int brandId = 0;
                        if (string.IsNullOrEmpty(Request.QueryString["bid"]) || !int.TryParse(Request.QueryString["bid"], out brandId))
                            brandId = 0;

                        orderStr = "st.StockId desc";
                        select = businessInvoiceDetailBLL.GetReportSelect(pageIndex, pageSize, orderStr, refNo, brandId, cardNo);

                        #endregion

                        break;
                    case ReportType.FinancingPledgeApplyReport:

                        #region FinancingPledgeApplyReport
                        PledgeApplyBLL pledgeApplyBLL = new PledgeApplyBLL();
                        bll = pledgeApplyBLL;

                        string pledgeApplyNo = Request.QueryString["paNo"];
                        refNo = Request.QueryString["refNo"];

                        DateTime beginDate = DefaultValue.DefaultTime;

                        if (!string.IsNullOrEmpty(Request.QueryString["fromDate"]))
                        {
                            if (!DateTime.TryParse(Request.QueryString["fromDate"], out beginDate))
                                beginDate = DefaultValue.DefaultTime;
                        }
                        if (!string.IsNullOrEmpty(Request.QueryString["toDate"]))
                        {
                            if (!DateTime.TryParse(Request.QueryString["toDate"], out endDate))
                                endDate = DefaultValue.DefaultTime;
                            else
                                endDate = endDate.AddDays(1);
                        }
                        int status = -1, bankId = -1;
                        assetId = -1;
                        if (!string.IsNullOrEmpty(Request.QueryString["status"]))
                            int.TryParse(Request.QueryString["status"], out status);

                        if (!string.IsNullOrEmpty(Request.QueryString["assetId"]))
                            int.TryParse(Request.QueryString["assetId"], out assetId);

                        if (!string.IsNullOrEmpty(Request.QueryString["bankId"]))
                            int.TryParse(Request.QueryString["bankId"], out bankId);

                        orderStr = "pa.PledgeApplyId desc";
                        select = pledgeApplyBLL.GetSelectModel(pageIndex, pageSize, orderStr, beginDate, endDate, bankId, assetId, status, pledgeApplyNo, refNo);

                        #endregion

                        break;
                    case ReportType.FinancingRepoApplyReport:

                        #region FinancingRepoApplyReport
                        RepoApplyBLL repoApplyBLL = new RepoApplyBLL();
                        bll = repoApplyBLL;

                        pledgeApplyNo = Request.QueryString["paNo"];
                        string repoApplyIdNo = Request.QueryString["reNo"];
                        refNo = Request.QueryString["refNo"];

                        beginDate = DefaultValue.DefaultTime;

                        if (!string.IsNullOrEmpty(Request.QueryString["fromDate"]))
                        {
                            if (!DateTime.TryParse(Request.QueryString["fromDate"], out beginDate))
                                beginDate = DefaultValue.DefaultTime;
                        }
                        if (!string.IsNullOrEmpty(Request.QueryString["toDate"]))
                        {
                            if (!DateTime.TryParse(Request.QueryString["toDate"], out endDate))
                                endDate = DefaultValue.DefaultTime;
                            else
                                endDate = endDate.AddDays(1);
                        }

                        status = -1;
                        if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                            int.TryParse(Request.QueryString["s"], out status);

                        orderStr = "ra.RepoApplyId desc";
                        select = repoApplyBLL.GetSelectModel(pageIndex, pageSize, orderStr, status, pledgeApplyNo, repoApplyIdNo, refNo, beginDate, endDate);

                        #endregion

                        break;

                    case ReportType.BankPledgeReport:

                        #region BankPledgeReport
                        PledgeApplyStockDetailBLL pledgeApplyStockDetailBLL = new PledgeApplyStockDetailBLL();
                        bll = pledgeApplyStockDetailBLL;

                        refNo = Request.QueryString["refNo"];

                        bankId = 0;
                        if (string.IsNullOrEmpty(Request.QueryString["bankId"]) || !int.TryParse(Request.QueryString["bankId"], out bankId))
                            bankId = 0;

                        beginDate = DefaultValue.DefaultTime;

                        if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                        {
                            if (!DateTime.TryParse(Request.QueryString["s"], out beginDate))
                                beginDate = DefaultValue.DefaultTime;
                        }
                        if (!string.IsNullOrEmpty(Request.QueryString["e"]))
                        {
                            if (!DateTime.TryParse(Request.QueryString["e"], out endDate))
                                endDate = DefaultValue.DefaultTime;
                            else
                                endDate = endDate.AddDays(1);
                        }

                        int repoInfo = 0;
                        if (string.IsNullOrEmpty(Request.QueryString["repoInfo"]) || !int.TryParse(Request.QueryString["repoInfo"], out repoInfo))
                            repoInfo = 0;

                        orderStr = "bank.BankName desc";
                        select = pledgeApplyStockDetailBLL.GetBankPledgeReportSelect(pageIndex, pageSize, orderStr, refNo, bankId, beginDate, endDate, repoInfo);

                        #endregion

                        break;
                    case ReportType.FundsCurrentByStock:

                        #region FundsCurrentByStock
                        fundsBLL = new FundsBLL();
                        bll = fundsBLL;

                        if (string.IsNullOrEmpty(Request.QueryString["s"]) || !DateTime.TryParse(Request.QueryString["s"], out startDate))
                            startDate = DefaultValue.DefaultTime;

                        if (string.IsNullOrEmpty(Request.QueryString["e"]) || !DateTime.TryParse(Request.QueryString["e"], out endDate))
                            endDate = DefaultValue.DefaultTime;
                        //else
                        //    endDate.AddDays(1);

                        if (string.IsNullOrEmpty(Request.QueryString["in"]) || !int.TryParse(Request.QueryString["in"], out inCorpId))
                            inCorpId = 0;

                        if (string.IsNullOrEmpty(Request.QueryString["out"]) || !int.TryParse(Request.QueryString["out"], out outCorpId))
                            outCorpId = 0;

                        orderStr = "corp.CorpName asc";
                        select = fundsBLL.GetFundsCurrentByStockReportSelect(pageIndex, pageSize, orderStr, inCorpId, outCorpId, startDate, endDate);

                        #endregion

                        break;
                    default:
                        Response.Write("报表报导错误");
                        Response.End();
                        break;
                }

                result = bll.Load(user, select);

                if (result.ResultStatus != 0 || result.ReturnValue == null)
                {
                    Response.Write("报表报导错误");
                    Response.End();
                }

                DataTable dt = result.ReturnValue as DataTable;
                if (dt == null)
                {
                    Response.Write("报表报导错误");
                    Response.End();
                }

                #region
                ////生成一个新的文件名用全球唯一标识符 (GUID)来标识
                //string newpath = Server.MapPath(".") + @"\Files\Excel\" + Guid.NewGuid() + ".xlsx";

                ////调用的模板文件
                //System.IO.FileInfo mode = new System.IO.FileInfo(Server.MapPath("~/Report/Model/StockReport.xlsx"));
                //mode.IsReadOnly = false;

                //Excel.Application app = new Excel.Application();
                //if (app == null)
                //{
                //    return;
                //}
                //app.Application.DisplayAlerts = false;
                //app.Visible = false;

                //if (mode.Exists)
                //{
                //    Excel.Workbook tworkbook;
                //    Object missing = System.Reflection.Missing.Value;

                //    app.Workbooks.Add(missing);
                //    //调用模板
                //    tworkbook = app.Workbooks.Open(mode.FullName, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                //    Excel.Worksheet tworksheet = (Excel.Worksheet)tworkbook.Sheets[1];
                //    Excel.Range r = tworksheet.get_Range("A2", missing);
                //    r = r.get_Resize(dt.Rows.Count, 13);
                //    string[,] objData = new string[dt.Rows.Count, 13];

                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        System.Data.DataRow dr = dt.Rows[i];

                //        objData[i, 0] = ((DateTime)dr["StockDate"]).ToShortDateString();
                //        objData[i, 1] = dr["CorpName"].ToString();
                //        objData[i, 2] = dr["RefNo"].ToString();
                //        objData[i, 3] = dr["AssetName"].ToString();
                //        objData[i, 4] = dr["CurGrossAmount"].ToString();
                //        objData[i, 5] = dr["CurNetAmount"].ToString();
                //        objData[i, 6] = dr["MUName"].ToString();
                //        objData[i, 7] = dr["BrandName"].ToString();
                //        objData[i, 8] = dr["DPName"].ToString();
                //        objData[i, 9] = dr["PaperNo"].ToString();
                //        objData[i, 10] = dr["CardNo"].ToString();
                //        objData[i, 11] = dr["CustomsTypeName"].ToString();
                //        objData[i, 12] = dr["StatusName"].ToString();
                //    }
                //    r.Value = objData;

                //    tworksheet.SaveAs(newpath, missing, missing, missing, missing, missing, missing, missing, missing, missing);

                //    tworkbook.Close(false, mode.FullName, missing);
                //    app.Workbooks.Close();
                //    app.Quit();
                //}
                #endregion
                
                string modelPath = Server.MapPath(".") + @"\Model\";
                //string newPath = bll.CreateExcel(dt, modelPath, filePath, reportType);
                DataTable dtSource = bll.SetExcelRangeData(dt);

                ExcelUtility.ExportExcel(modelPath, reportType.ToString("F"), dtSource);
            }
        }
    }
}