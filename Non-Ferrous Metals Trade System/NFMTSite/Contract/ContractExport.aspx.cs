using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Contract
{
    public partial class ContractExport : System.Web.UI.Page
    {
        public NFMT.Contract.Model.Contract contract;
        public NFMT.Data.Model.Asset asset;
        public string tradeDirection = string.Empty;
        public string buyCorpName = string.Empty;
        public string sellCorpName = string.Empty;
        public string contractDetailStr = string.Empty;
        public System.Data.DataTable dt = new System.Data.DataTable();
        public string contractClausesStr = string.Empty;

        private List<NFMT.Contract.Model.ContractCorporationDetail> buyCorporationDetails;
        private List<NFMT.Contract.Model.ContractCorporationDetail> sellCorporationDetails;
        private bool isSelf = true;
        private NFMT.User.Model.Corporation corp;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 37, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.查询 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
                string redirectUrl = string.Format("{0}Contract/ContractList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

                //this.navigation1.Routes.Add("合约列表", redirectUrl);
                //this.navigation1.Routes.Add("合约预览", string.Empty);

                int contractId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out contractId) || contractId <= 0)
                    Utility.JsUtility.WarmAlert(this.Page, "参数错误", redirectUrl);

                //获取合约
                NFMT.Contract.BLL.ContractBLL contractBLL = new NFMT.Contract.BLL.ContractBLL();
                result = contractBLL.Get(user, contractId);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null)
                    Utility.JsUtility.WarmAlert(this.Page, "获取合约失败", redirectUrl);

                //获取品种
                asset = NFMT.Data.BasicDataProvider.Assets.SingleOrDefault(a => a.AssetId == contract.AssetId);
                if (asset == null)
                    Utility.JsUtility.WarmAlert(this.Page, "获取品种失败", redirectUrl);

                NFMT.Contract.BLL.ContractCorporationDetailBLL contractCorporationDetailBLL = new NFMT.Contract.BLL.ContractCorporationDetailBLL();

                //采购销售
                if (contract.TradeDirection == (int)NFMT.Contract.TradeDirectionEnum.采购)
                {
                    tradeDirection = "采购";
                    isSelf = true;
                }
                else if (contract.TradeDirection == (int)NFMT.Contract.TradeDirectionEnum.销售)
                {
                    tradeDirection = "销售";
                    isSelf = false;
                }

                //买方抬头
                result = contractCorporationDetailBLL.LoadByContractId(user, contractId, isSelf);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                buyCorporationDetails = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
                if (buyCorporationDetails == null || !buyCorporationDetails.Any())
                    Utility.JsUtility.WarmAlert(this.Page, "获取买方抬头失败", redirectUrl);

                List<string> corpNames = new List<string>();
                buyCorporationDetails.ForEach(detail =>
                {
                    corp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == detail.CorpId);
                    if (!corpNames.Contains(corp.CorpName))
                        corpNames.Add(corp.CorpName);
                });

                buyCorpName = string.Join(",", corpNames);

                if (string.IsNullOrEmpty(buyCorpName))
                    Utility.JsUtility.WarmAlert(this.Page, "获取买方抬头失败", redirectUrl);

                //卖方抬头
                result = contractCorporationDetailBLL.LoadByContractId(user, contractId, !isSelf);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                sellCorporationDetails = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
                if (sellCorporationDetails == null || !sellCorporationDetails.Any())
                    Utility.JsUtility.WarmAlert(this.Page, "获取卖方抬头失败", redirectUrl);

                corpNames.Clear();
                sellCorporationDetails.ForEach(detail =>
                {
                    corp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == detail.CorpId);
                    if (!corpNames.Contains(corp.CorpName))
                        corpNames.Add(corp.CorpName);
                });

                sellCorpName = string.Join(",", corpNames);

                if (string.IsNullOrEmpty(sellCorpName))
                    Utility.JsUtility.WarmAlert(this.Page, "获取卖方抬头失败", redirectUrl);

                //列表明细
                result = contractBLL.GetContractDetail(user, contractId, (NFMT.Contract.TradeDirectionEnum)contract.TradeDirection);
                //if (result.ResultStatus != 0)
                //    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                dt = result.ReturnValue as System.Data.DataTable;
                //if (dt == null || dt.Rows.Count < 1)
                //    Utility.JsUtility.WarmAlert(this.Page, "获取合约明细失败", redirectUrl);

                contractDetailStr = GetGridDetail(dt, contract);

                //合约条款
                NFMT.Contract.BLL.ContractClauseBLL contractClauseBLL = new NFMT.Contract.BLL.ContractClauseBLL();
                result = contractClauseBLL.LoadClauseByContractId(user, contractId);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                List<NFMT.Data.Model.ContractClause> contractClauses = result.ReturnValue as List<NFMT.Data.Model.ContractClause>;
                if (contractClauses == null || !contractClauses.Any())
                    Utility.JsUtility.WarmAlert(this.Page, "获取合约条款失败", redirectUrl);

                contractClausesStr = GetMasterClause(contractClauses);
            }
        }

        /// <summary>
        /// 获取Grid明细
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string GetGridDetail(System.Data.DataTable dt,NFMT.Contract.Model.Contract contract)
        {
            if (dt == null || dt.Rows.Count < 1)
                return string.Empty;

            System.Text.StringBuilder sbDetail = new System.Text.StringBuilder();

            try
            {
                int i = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    sbDetail.Append("<tr>");
                    sbDetail.Append(Environment.NewLine);
                    sbDetail.AppendFormat("<td>{0}</td>", dr["AssetName"].ToString());
                    sbDetail.Append(Environment.NewLine);
                    sbDetail.AppendFormat("<td>{0}</td>", dr["BrandName"].ToString());
                    sbDetail.Append(Environment.NewLine);
                    sbDetail.AppendFormat("<td>{0}</td>", dr["Format"].ToString());
                    sbDetail.Append(Environment.NewLine);
                    sbDetail.AppendFormat("<td>{0}</td>", dr["OriginPlace"].ToString());
                    sbDetail.Append(Environment.NewLine);
                    sbDetail.AppendFormat("<td>{0}</td>", dr["GrossAmount"].ToString());
                    sbDetail.Append(Environment.NewLine);
                    sbDetail.AppendFormat("<td>{0}</td>", Convert.ToDecimal(dr["Price"]).ToString("#0.00"));
                    sbDetail.Append(Environment.NewLine);
                    sbDetail.AppendFormat("<td>{0}</td>", (Convert.ToDecimal(dr["GrossAmount"]) * Convert.ToDecimal(dr["Price"])).ToString("#0.00"));
                    sbDetail.Append(Environment.NewLine);
                    sbDetail.AppendFormat("<td>{0}</td>", dr["DPAddress"].ToString());
                    sbDetail.Append(Environment.NewLine);

                    if (i == 0)
                    {
                        if (contract.DeliveryStyle == (int)NFMT.Contract.DeliveryStyleEnum.日期交货)
                            sbDetail.AppendFormat("<td rowspan=\"5\">{0}</td>", contract.DeliveryDate.ToString());
                        else
                            sbDetail.Append("<td rowspan=\"5\">款到发货</td>");
                        sbDetail.Append(Environment.NewLine);
                    }

                    sbDetail.Append("</tr>");
                    sbDetail.Append(Environment.NewLine);

                    i++;
                }
            }
            catch
            {
                sbDetail.Clear();
            }

            return sbDetail.ToString();
        }

        /// <summary>
        /// 获取合约条款
        /// </summary>
        /// <param name="contractClauses"></param>
        /// <returns></returns>
        private string GetMasterClause(List<NFMT.Data.Model.ContractClause> contractClauses)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            try
            {
                int i = 2;//从“二”开始
                contractClauses.ForEach(contractClause =>
                {
                    sb.Append("<tr>");
                    sb.Append(Environment.NewLine);
                    sb.AppendFormat("<td colspan=\"10\">{0}、{1}</td>", GetChineseString(i), contractClause.ClauseText);
                    sb.Append(Environment.NewLine);
                    sb.Append("</tr>");
                    sb.Append(Environment.NewLine);

                    i++;
                });
            }
            catch
            {
                sb.Clear();
            }

            return sb.ToString();
        }

        /// <summary>
        /// 将阿拉伯数字转换成大写中文金额
        /// </summary>
        /// <param name="LowerMoney"></param>
        /// <returns></returns>
        protected string MoneyToChinese(string LowerMoney)
        {
            string functionReturnValue = null;
            bool IsNegative = false; // 是否是负数
            if (LowerMoney.Trim().Substring(0, 1) == "-")
            {
                // 是负数则先转为正数
                LowerMoney = LowerMoney.Trim().Remove(0, 1);
                IsNegative = true;
            }
            string strLower = null;
            string strUpart = null;
            string strUpper = null;
            int iTemp = 0;
            // 保留两位小数 123.489→123.49　　123.4→123.4
            LowerMoney = Math.Round(double.Parse(LowerMoney), 2).ToString();
            if (LowerMoney.IndexOf(".") > 0)
            {
                if (LowerMoney.IndexOf(".") == LowerMoney.Length - 2)
                {
                    LowerMoney = LowerMoney + "0";
                }
            }
            else
            {
                LowerMoney = LowerMoney + ".00";
            }
            strLower = LowerMoney;
            iTemp = 1;
            strUpper = "";
            while (iTemp <= strLower.Length)
            {
                switch (strLower.Substring(strLower.Length - iTemp, 1))
                {
                    case ".":
                        strUpart = "圆";
                        break;
                    case "0":
                        strUpart = "零";
                        break;
                    case "1":
                        strUpart = "壹";
                        break;
                    case "2":
                        strUpart = "贰";
                        break;
                    case "3":
                        strUpart = "叁";
                        break;
                    case "4":
                        strUpart = "肆";
                        break;
                    case "5":
                        strUpart = "伍";
                        break;
                    case "6":
                        strUpart = "陆";
                        break;
                    case "7":
                        strUpart = "柒";
                        break;
                    case "8":
                        strUpart = "捌";
                        break;
                    case "9":
                        strUpart = "玖";
                        break;
                }

                switch (iTemp)
                {
                    case 1:
                        strUpart = strUpart + "分";
                        break;
                    case 2:
                        strUpart = strUpart + "角";
                        break;
                    case 3:
                        strUpart = strUpart + "";
                        break;
                    case 4:
                        strUpart = strUpart + "";
                        break;
                    case 5:
                        strUpart = strUpart + "拾";
                        break;
                    case 6:
                        strUpart = strUpart + "佰";
                        break;
                    case 7:
                        strUpart = strUpart + "仟";
                        break;
                    case 8:
                        strUpart = strUpart + "万";
                        break;
                    case 9:
                        strUpart = strUpart + "拾";
                        break;
                    case 10:
                        strUpart = strUpart + "佰";
                        break;
                    case 11:
                        strUpart = strUpart + "仟";
                        break;
                    case 12:
                        strUpart = strUpart + "亿";
                        break;
                    case 13:
                        strUpart = strUpart + "拾";
                        break;
                    case 14:
                        strUpart = strUpart + "佰";
                        break;
                    case 15:
                        strUpart = strUpart + "仟";
                        break;
                    case 16:
                        strUpart = strUpart + "万";
                        break;
                    default:
                        strUpart = strUpart + "";
                        break;
                }

                strUpper = strUpart + strUpper;
                iTemp = iTemp + 1;
            }

            strUpper = strUpper.Replace("零拾", "零");
            strUpper = strUpper.Replace("零佰", "零");
            strUpper = strUpper.Replace("零仟", "零");
            strUpper = strUpper.Replace("零零零", "零");
            strUpper = strUpper.Replace("零零", "零");
            strUpper = strUpper.Replace("零角零分", "整");
            strUpper = strUpper.Replace("零分", "整");
            strUpper = strUpper.Replace("零角", "零");
            strUpper = strUpper.Replace("零亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("零亿零万", "亿");
            strUpper = strUpper.Replace("零万零圆", "万圆");
            strUpper = strUpper.Replace("零亿", "亿");
            strUpper = strUpper.Replace("零万", "万");
            strUpper = strUpper.Replace("零圆", "圆");
            strUpper = strUpper.Replace("零零", "零");

            // 对壹圆以下的金额的处理
            if (strUpper.Substring(0, 1) == "圆")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "零")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "角")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "分")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "整")
            {
                strUpper = "零圆整";
            }
            functionReturnValue = strUpper;

            if (IsNegative == true)
            {
                return "负" + functionReturnValue;
            }
            else
            {
                return functionReturnValue;
            }
        }

        /// <summary>
        /// 数字转中文数字
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private string GetChineseString(long number)
        {
            string cStr = "零一二三四五六七八九 十百千";
            string[] unitStr = new string[] { "", "万", "亿", "万亿", "兆" };
            string result = string.Empty;
            for (int i = 0; i < number.ToString().Length; i++)
            {
                int temp = (int)((long)(number / (long)Math.Pow(10, i)) % 10);
                int unit = (int)i / 4;
                if (i % 4 == 0) result = unitStr[unit] + result;
                result = cStr[temp] + cStr[10 + i % 4].ToString().Trim() + result;
            }
            result = Regex.Replace(result, "(零[十百千])+", "零");
            result = Regex.Replace(result, "零{2,}", "零");
            result = Regex.Replace(result, "零([万亿])", "$1").TrimEnd('零');

            if (number >= 10 && number <= 19)
            {
                if (number == 10) return "十";
                else
                    return result.Substring(1);
            }
            return result;
        }
    }
}