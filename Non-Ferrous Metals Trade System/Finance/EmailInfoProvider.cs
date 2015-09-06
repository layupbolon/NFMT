using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NFMT.Finance
{
    public class EmailInfoProvider
    {
        private FinType m_finType;
        private int m_id;

        public EmailInfoProvider(FinType finType, int id)
        {
            this.m_finType = finType;
            this.m_id = id;
        }

        public Common.ResultModel GetEmailInfo(Common.UserModel user)
        {
            Common.ResultModel result = new Common.ResultModel();
            StringBuilder sb = new StringBuilder();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    switch (m_finType)
                    {
                        case FinType.质押:
                            sb.Clear();
                            DAL.PledgeApplyDAL pledgeApplyDAL = new DAL.PledgeApplyDAL();
                            result = pledgeApplyDAL.Get(user, m_id);
                            if (result.ResultStatus != 0)
                                return result;

                            Model.PledgeApply pledgeApply = result.ReturnValue as Model.PledgeApply;
                            if (pledgeApply == null)
                            {
                                result.ResultStatus = -1;
                                result.Message = "获取质押申请单失败";
                                return result;
                            }

                            NFMT.Data.Model.Bank bank = NFMT.Data.BasicDataProvider.Banks.SingleOrDefault(a => a.BankId == pledgeApply.FinancingBankId);
                            sb.Append(GetBanInfo(bank));

                            DAL.PledgeApplyCashDetailDAL pledgeApplyCashDetailDAL = new DAL.PledgeApplyCashDetailDAL();
                            result = pledgeApplyCashDetailDAL.LoadByPledgeApplyId(user, m_id);
                            if (result.ResultStatus != 0)
                                return result;

                            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

                            foreach (System.Data.DataRow dr in dt.Rows)
                            {
                                sb.Append("<span>");
                                sb.AppendFormat("We short {0} lots Copper at price  USD{1}/MT, prompt date: {2} under contract {3};", dr["Hands"], dr["Price"], Convert.ToDateTime(dr["ExpiringDate"]).ToString("MMMM dd, yyyy", new CultureInfo("en-us")), dr["StockContractNo"]);
                                sb.Append("</span>");
                                sb.Append("<br>");
                            }

                            break;
                        case FinType.赎回:
                            sb.Clear();
                            DAL.RepoApplyDAL repoApplyDAL = new DAL.RepoApplyDAL();
                            result = repoApplyDAL.GetByPledgeApplyId(user, m_id);
                            if (result.ResultStatus != 0)
                                return result;
                            int bankId = (int)result.ReturnValue;

                            bank = NFMT.Data.BasicDataProvider.Banks.SingleOrDefault(a => a.BankId == bankId);
                            sb.Append(GetBanInfo(bank));

                            DAL.RepoApplyDetailDAL repoApplyDetailDAL = new DAL.RepoApplyDetailDAL();
                            result = repoApplyDetailDAL.LoadByRepoApplyId(user, m_id);
                            if (result.ResultStatus != 0)
                                return result;

                            dt = result.ReturnValue as System.Data.DataTable;

                            foreach (System.Data.DataRow dr in dt.Rows)
                            {
                                sb.Append("<span>");
                                sb.AppendFormat("We long {0} lots Copper at price  USD{1}/MT, prompt date: {2} under contract {3};", dr["Hands"], dr["Price"], Convert.ToDateTime(dr["ExpiringDate"]).ToString("MMMM dd, yyyy", new CultureInfo("en-us")), dr["StockContractNo"]);
                                sb.Append("</span>");
                                sb.Append("<br>");
                            }
                            break;
                        default:
                            break;
                    }

                    if (result.ResultStatus == 0)
                    {
                        scope.Complete();
                        result.ReturnValue = sb.ToString();
                    }
                }
            }
            catch(Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
                result.ReturnValue = sb.Clear();
            }

            return result;
        }

        private StringBuilder GetBanInfo(NFMT.Data.Model.Bank bank)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<span>");

            if (bank == null || bank.BankId <= 0)
                return sb.Clear();

            switch (bank.BankName)
            {
                case "JPM":
                    sb.Append("Please transfer the following position from our account FONE to J.P.Morgan physical department:");
                    break;
                case "SBP":
                    sb.Append("Please transfer the following position from our account TRIWA2 to Standard Bank PLC");
                    break;
                case "SCB":
                    sb.Append("Please transfer the following position from our account TRIWA002 to SCB SIP’s LME account:");
                    break;
                default: break;
            }

            return sb.Length > 0 ? sb.Append("</span>").Append("<br>") : sb.Clear();
        }
    }
}
