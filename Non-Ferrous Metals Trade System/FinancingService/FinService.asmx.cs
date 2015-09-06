using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.Script.Serialization;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NFMT.Common;
using NFMT.DBUtility;
using NFMT.Finance.BLL;
using NFMT.Finance.DAL;
using NFMT.Finance.Model;
using NFMT.Finance.TaskProvider;
using NFMT.WorkFlow;
using NFMT.WorkFlow.DAL;

namespace FinancingService
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FinService : WebService
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(FinService));

        /// <summary>
        /// 获取质押申请单列表
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="pagesize"></param>
        /// <param name="orderStr"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="bankId"></param>
        /// <param name="assetId"></param>
        /// <param name="status"></param>
        /// <param name="pledgeApplyNo"></param>
        /// <param name="refNo"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetFinancingPledgeApplyList(int pagenum, int pagesize, string orderStr, DateTime fromDate, DateTime toDate, int bankId, int assetId, int status, string pledgeApplyNo, string refNo)
        {
            try
            {
                UserModel user = new UserModel();

                PledgeApplyBLL pledgeApplyBLL = new PledgeApplyBLL();
                SelectModel select = pledgeApplyBLL.GetSelectModel(pagenum, pagesize, orderStr, fromDate, toDate, bankId, assetId, status, pledgeApplyNo, refNo);
                ResultModel result = pledgeApplyBLL.Load(user, select);

                DataTable dt = new DataTable();

                if (result.ResultStatus == 0)
                    dt = result.ReturnValue as DataTable;

                Dictionary<string, object> dic = new Dictionary<string, object>();

                dic.Add("count", result.AffectCount);
                dic.Add("data", dt);

                return JsonConvert.SerializeObject(dic, new DataTableConverter());
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "GetFinancingPledgeApplyList", e.Message);
                return e.Message;
            }
        }

        /// <summary>
        /// 获取质押申请单实货明细列表
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="pagesize"></param>
        /// <param name="orderStr"></param>
        /// <param name="pledgeApplyId"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetFinancingPledgeApplyStockDetailList(int pagenum, int pagesize, string orderStr, int pledgeApplyId)
        {
            try
            {
                UserModel user = new UserModel();

                PledgeApplyStockDetailBLL bll = new PledgeApplyStockDetailBLL();
                SelectModel select = bll.GetSelectModel(pagenum, pagesize, orderStr, pledgeApplyId);
                ResultModel result = bll.Load(user, select);

                if (result.ResultStatus != 0)
                    return string.Empty;

                DataTable dt = result.ReturnValue as DataTable;
                Dictionary<string, object> dic = new Dictionary<string, object>();

                dic.Add("count", result.AffectCount);
                dic.Add("data", dt);

                return JsonConvert.SerializeObject(dic, new DataTableConverter());
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "GetFinancingPledgeApplyStockDetailList", e.Message);
                return e.Message;
            }
        }

        /// <summary>
        /// 获取质押申请单实货明细列表
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="pagesize"></param>
        /// <param name="orderStr"></param>
        /// <param name="pledgeApplyId"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetFinancingPledgeApplyStockDetailForHandList(int pagenum, int pagesize, string orderStr, int pledgeApplyId)
        {
            try
            {
                UserModel user = new UserModel();

                PledgeApplyStockDetailBLL bll = new PledgeApplyStockDetailBLL();
                SelectModel select = bll.GetSelectModelForHands(pagenum, pagesize, orderStr, pledgeApplyId);
                ResultModel result = bll.Load(user, select);

                if (result.ResultStatus != 0)
                    return string.Empty;

                DataTable dt = result.ReturnValue as DataTable;
                Dictionary<string, object> dic = new Dictionary<string, object>();

                dic.Add("count", result.AffectCount);
                dic.Add("data", dt);

                return JsonConvert.SerializeObject(dic, new DataTableConverter());
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "GetFinancingPledgeApplyStockDetailForHandList", e.Message);
                return e.Message;
            }
        }

        /// <summary>
        /// 获取质押申请单期货头寸明细列表
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="pagesize"></param>
        /// <param name="orderStr"></param>
        /// <param name="pledgeApplyId"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetFinancingPledgeApplyCashDetailList(int pagenum, int pagesize, string orderStr, int pledgeApplyId)
        {
            try
            {
                UserModel user = new UserModel();

                PledgeApplyCashDetailBLL bll = new PledgeApplyCashDetailBLL();
                SelectModel select = bll.GetSelectModel(pagenum, pagesize, orderStr, pledgeApplyId);
                ResultModel result = bll.Load(user, select);

                if (result.ResultStatus != 0)
                    return string.Empty;

                DataTable dt = result.ReturnValue as DataTable;
                Dictionary<string, object> dic = new Dictionary<string, object>();

                dic.Add("count", result.AffectCount);
                dic.Add("data", dt);

                return JsonConvert.SerializeObject(dic, new DataTableConverter());
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "GetFinancingPledgeApplyCashDetailList", e.Message);
                return e.Message;
            }
        }

        /// <summary>
        /// 根据实货明细表获取期货头寸信息
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="pagesize"></param>
        /// <param name="orderStr"></param>
        /// <param name="pledgeApplyId"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetFinancingPledgeApplyCashInfoList(int pagenum, int pagesize, string orderStr, int pledgeApplyId)
        {
            try
            {
                UserModel user = new UserModel();

                PledgeApplyCashDetailBLL bll = new PledgeApplyCashDetailBLL();
                SelectModel select = bll.GetCashSelectModel(pagenum, pagesize, orderStr, pledgeApplyId);
                ResultModel result = bll.Load(user, select);

                if (result.ResultStatus != 0)
                    return string.Empty;

                DataTable dt = result.ReturnValue as DataTable;
                Dictionary<string, object> dic = new Dictionary<string, object>();

                dic.Add("count", result.AffectCount);
                dic.Add("data", dt);

                return JsonConvert.SerializeObject(dic, new DataTableConverter());
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "GetFinancingPledgeApplyCashInfoList", e.Message);
                return e.Message;
            }
        }

        /// <summary>
        /// 从V1业务系统中获取库存信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetStockInfoFromBusinessSystem()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                //sb.Append(" select st.StockSerial as StockId,sn.StockName as RefNo,case when (st.NetAmount-ISNULL(psd.NetAmount,0)+ISNULL(rad.NetAmount,0)) > st.NetAmount then st.NetAmount else st.NetAmount-ISNULL(psd.NetAmount,0)+ISNULL(rad.NetAmount,0) end as NetAmount ");
                //sb.Append(" from dbo.TStock2 st ");
                //sb.Append(" left join dbo.TStockName sn on st.StockNameSerial = sn.StockNameSerial ");
                //sb.AppendFormat(" left join (select detail.StockId,SUM(detail.NetAmount) NetAmount from Financing.dbo.Fin_PledgeApplyStockDetail detail left join Financing.dbo.Fin_PledgeApply pa on detail.PledgeApplyId = pa.PledgeApplyId where detail.DetailStatus in ({0},{1}) and pa.PledgeApplyStatus >={2} group by detail.StockId ) psd on st.StockSerial = psd.StockId ", (int)StatusEnum.已生效, (int)StatusEnum.已完成, (int)StatusEnum.已录入);
                //sb.AppendFormat(" left join (select detail.StockId,SUM(detail.NetAmount) NetAmount from Financing.dbo.Fin_RepoApplyDetail detail left join Financing.dbo.Fin_RepoApply ra on ra.RepoApplyId = detail.RepoApplyId where ra.RepoApplyStatus >={0} and detail.DetailStatus in ({0},{1}) group by detail.StockId) rad on st.StockSerial = rad.StockId ", (int)StatusEnum.已生效, (int)StatusEnum.已完成);
                //sb.Append(" where (st.NetAmount-ISNULL(psd.NetAmount,0)+ISNULL(rad.NetAmount,0)) >0  ");
                //sb.Append(" order by sn.StockName");

                sb.Append(" select * from ( ");
                sb.Append(" select ROW_NUMBER() OVER(PARTITION BY sn.StockName ORDER BY sn.StockName) as rowId, st.StockSerial,sn.StockName as RefNo,st.NetAmount as NetAmount ");
                sb.Append(" from dbo.TStock2 st left join dbo.TStockName sn on st.StockNameSerial = sn.StockNameSerial) a ");
                sb.Append(" where a.rowId = 1 ");

                DataTable dt = SqlHelper.ExecuteDataTable(DefaultValue.V1ConnectString, sb.ToString(), null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(dt, new DataTableConverter());
                }
                else
                    return string.Empty;
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "GetStockInfoFromBusinessSystem", e.Message);
                return e.Message;
            }
        }

        [WebMethod]
        public string GetFinStockInfo(string refNo)
        {
            ResultModel result = new ResultModel();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(" if exists(select 1 from Financing.dbo.Fin_StockNo where RefNo = '{0}') ", refNo);
                sb.Append(" begin ");
                sb.AppendFormat(" 	select NetAmount from Financing.dbo.Fin_StockNo where RefNo = '{0}' ", refNo);
                sb.Append(" end else begin ");
                sb.Append(" 	select 0 ");
                sb.Append(" end ");

                object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringFinance, CommandType.Text, sb.ToString(), null);
                decimal netAmount;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && decimal.TryParse(obj.ToString(), out netAmount) && netAmount > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "该业务单号在系统中已存在";
                    result.ReturnValue = netAmount;                    
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "该业务单号不存在此系统中";
                    result.ReturnValue = 0;
                }
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "GetFinStockInfo", e.Message);
                result.ResultStatus = 0;
                result.Message = e.Message;
                result.ReturnValue = 0;
            }
            return JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 质押申请单新增
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pledgeApply"></param>
        /// <param name="details"></param>
        /// <param name="isSubmitAudit"></param>
        /// <returns></returns>
        [WebMethod]
        public string FinancingPledgeApplyCreate(UserModel user, PledgeApply pledgeApply, List<PledgeApplyStockDetail> details, bool isSubmitAudit)
        {
            ResultModel result = new ResultModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    decimal SumNetAmount = 0;
                    int SumHands = 0;
                    if (details != null && details.Any())
                    {
                        foreach (PledgeApplyStockDetail detail in details)
                        {
                            SumNetAmount += detail.NetAmount;
                            SumHands += detail.Hands;
                        }
                    }

                    pledgeApply.SumNetAmount = SumNetAmount;
                    pledgeApply.SumHands = SumHands;

                    PledgeApplyDAL pledgeApplyDAL = new PledgeApplyDAL();
                    result = pledgeApplyDAL.Insert(user, pledgeApply);
                    if (result.ResultStatus != 0)
                        return serializer.Serialize(result);

                    int pledgeApplyId = (int)result.ReturnValue;

                    PledgeApplyStockDetailDAL pledgeApplyStockDetailDAL = new PledgeApplyStockDetailDAL();
                    StockNoDAL stockNoDAL = new StockNoDAL();
                    int stockId;
                    foreach (PledgeApplyStockDetail detail in details)
                    {
                        result = stockNoDAL.Insert(user, new StockNo()
                        {
                            RefNo = detail.RefNo.Trim(),
                            NetAmount = detail.NetAmount
                        });
                        stockId = (int)result.ReturnValue;

                        detail.RefNo = detail.RefNo.Trim();
                        detail.StockId = stockId;
                        detail.PledgeApplyId = pledgeApplyId;
                        result = pledgeApplyStockDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return serializer.Serialize(result);
                    }

                    if (result.ResultStatus == 0)
                    {
                        if (isSubmitAudit)
                        {
                            pledgeApply.PledgeApplyId = pledgeApplyId;

                            AutoSubmit submit = new AutoSubmit();
                            result = submit.Submit(user, pledgeApply, new PledgeApplyTaskProvider(), MasterEnum.质押申请单审核);
                        }

                        result.Message = "质押申请单新增成功";
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinancingPledgeApplyCreate", ex.Message);
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }

            return serializer.Serialize(result);
        }

        /// <summary>
        /// 质押申请单修改
        /// </summary>
        /// <param name="userStr"></param>
        /// <param name="pledgeApplyStr"></param>
        /// <param name="detailsStr"></param>
        /// <returns></returns>
        [WebMethod]
        public string FinancingPledgeApplyUpdate(string userStr, string pledgeApplyStr, string detailsStr)
        {
            ResultModel result = new ResultModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    UserModel user = serializer.Deserialize<UserModel>(userStr);
                    PledgeApply pledgeApply = serializer.Deserialize<PledgeApply>(pledgeApplyStr);
                    List<PledgeApplyStockDetail> details = serializer.Deserialize<List<PledgeApplyStockDetail>>(detailsStr);

                    PledgeApplyDAL pledgeApplyDAL = new PledgeApplyDAL();
                    result = pledgeApplyDAL.Get(user, pledgeApply.PledgeApplyId);
                    if (result.ResultStatus != 0)
                        return serializer.Serialize(result);

                    PledgeApply pledgeApplyGet = result.ReturnValue as PledgeApply;

                    pledgeApplyGet.DeptId = pledgeApply.DeptId;
                    pledgeApplyGet.ApplyTime = pledgeApply.ApplyTime;
                    pledgeApplyGet.FinancingBankId = pledgeApply.FinancingBankId;
                    pledgeApplyGet.FinancingAccountId = pledgeApply.FinancingAccountId;
                    pledgeApplyGet.AssetId = pledgeApply.AssetId;
                    pledgeApplyGet.SwitchBack = pledgeApply.SwitchBack;
                    pledgeApplyGet.ExchangeId = pledgeApply.ExchangeId;

                    decimal SumNetAmount = 0;
                    int SumHands = 0;
                    if (details != null && details.Any())
                    {
                        foreach (PledgeApplyStockDetail detail in details)
                        {
                            SumNetAmount += detail.NetAmount;
                            SumHands += detail.Hands;
                        }
                    }

                    pledgeApplyGet.SumNetAmount = SumNetAmount;
                    pledgeApplyGet.SumHands = SumHands;


                    result = pledgeApplyDAL.Update(user, pledgeApplyGet);
                    if (result.ResultStatus != 0)
                        return serializer.Serialize(result);

                    PledgeApplyStockDetailDAL pledgeApplyStockDetailDAL = new PledgeApplyStockDetailDAL();
                    result = pledgeApplyStockDetailDAL.UpdateDetailStatus(user, pledgeApply.PledgeApplyId, StatusEnum.无效数据);
                    if (result.ResultStatus != 0)
                        return serializer.Serialize(result);

                    StockNoDAL stockNoDAL = new StockNoDAL();
                    int stockId;
                    foreach (PledgeApplyStockDetail detail in details)
                    {
                        result = stockNoDAL.Insert(user, new StockNo()
                        {
                            RefNo = detail.RefNo.Trim(),
                            NetAmount = detail.NetAmount
                        });
                        if (result.ResultStatus != 0)
                            return serializer.Serialize(result);

                        stockId = (int)result.ReturnValue;

                        detail.RefNo = detail.RefNo.Trim();
                        detail.StockId = stockId;
                        detail.PledgeApplyId = pledgeApply.PledgeApplyId;
                        result = pledgeApplyStockDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return serializer.Serialize(result);
                    }

                    if (result.ResultStatus == 0)
                        result.Message = "质押申请单修改成功";

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinancingPledgeApplyUpdate", ex.Message);
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }

            return serializer.Serialize(result);
        }

        /// <summary>
        /// 质押申请单作废
        /// </summary>
        /// <param name="userStr"></param>
        /// <param name="pledgeApplyId"></param>
        /// <returns></returns>
        [WebMethod]
        public string FinancingPledgeApplyInvalid(string userStr, int pledgeApplyId)
        {
            ResultModel result;
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                UserModel user = serializer.Deserialize<UserModel>(userStr);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    PledgeApplyDAL pledgeApplyDAL = new PledgeApplyDAL();
                    result = pledgeApplyDAL.Get(user, pledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    PledgeApply pledgeApply = result.ReturnValue as PledgeApply;

                    result = pledgeApplyDAL.Invalid(user, pledgeApply);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    PledgeApplyCashDetailDAL pledgeApplyCashDetailDAL = new PledgeApplyCashDetailDAL();
                    result = pledgeApplyCashDetailDAL.InvalidAll(user, pledgeApplyId);
                    //if (result.ResultStatus != 0)
                    //    return result.Message;

                    PledgeApplyStockDetailDAL pledgeApplyStockDetailDAL = new PledgeApplyStockDetailDAL();
                    result = pledgeApplyStockDetailDAL.InvalidAll(user, pledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    if (result.ResultStatus == 0)
                    {
                        scope.Complete();
                        return "作废成功";
                    }
                    else
                        return "作废失败";
                }
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinancingPledgeApplyInvalid", e.Message);
                return e.Message;
            }
        }

        /// <summary>
        /// 质押申请单撤返
        /// </summary>
        /// <param name="userStr"></param>
        /// <param name="pledgeApplyId"></param>
        /// <returns></returns>
        [WebMethod]
        public string FinancingPledgeApplyGoBack(string userStr, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                UserModel user = serializer.Deserialize<UserModel>(userStr);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    PledgeApplyDAL pledgeApplyDAL = new PledgeApplyDAL();
                    result = pledgeApplyDAL.Get(user, pledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    PledgeApply pledgeApply = result.ReturnValue as PledgeApply;

                    result = pledgeApplyDAL.Goback(user, pledgeApply);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    //工作流任务关闭
                    DataSourceDAL sourceDAL = new DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, pledgeApply);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    if (result.ResultStatus == 0)
                    {
                        scope.Complete();
                        return "撤返成功";
                    }
                    else
                        return "撤返失败";
                }
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinancingPledgeApplyGoBack", e.Message);
                return e.Message;
            }
        }

        /// <summary>
        /// 质押申请单执行完成
        /// </summary>
        /// <param name="userStr"></param>
        /// <param name="pledgeApplyId"></param>
        /// <returns></returns>
        [WebMethod]
        public string FinancingPledgeApplyIdComplete(string userStr, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                UserModel user = serializer.Deserialize<UserModel>(userStr);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //完成质押申请单
                    PledgeApplyDAL pledgeApplyDAL = new PledgeApplyDAL();
                    result = pledgeApplyDAL.Get(user, pledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    PledgeApply pledgeApply = result.ReturnValue as PledgeApply;

                    result = pledgeApplyDAL.Complete(user, pledgeApply);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    //完成质押申请单实货明细
                    PledgeApplyStockDetailDAL pledgeApplyStockDetailDAL = new PledgeApplyStockDetailDAL();
                    result = pledgeApplyStockDetailDAL.LoadByPledgeApplyId(user, pledgeApplyId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    List<PledgeApplyStockDetail> stockDetails = result.ReturnValue as List<PledgeApplyStockDetail>;
                    if (stockDetails == null || !stockDetails.Any())
                        return "获取质押申请单实货明细失败";

                    foreach (PledgeApplyStockDetail detail in stockDetails)
                    {
                        result = pledgeApplyStockDetailDAL.Complete(user, detail);
                        if (result.ResultStatus != 0)
                            return result.Message;
                    }

                    //完成质押申请单头寸明细
                    PledgeApplyCashDetailDAL pledgeApplyCashDetailDAL = new PledgeApplyCashDetailDAL();
                    result = pledgeApplyCashDetailDAL.LoadByPledgeApplyId(user, pledgeApplyId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    List<PledgeApplyCashDetail> cashDetails = result.ReturnValue as List<PledgeApplyCashDetail>;
                    if (cashDetails == null || !cashDetails.Any())
                        return "获取质押申请单头寸明细失败";

                    foreach (PledgeApplyCashDetail detail in cashDetails)
                    {
                        result = pledgeApplyCashDetailDAL.Complete(user, detail);
                        if (result.ResultStatus != 0)
                            return result.Message;
                    }

                    if (result.ResultStatus == 0)
                    {
                        scope.Complete();
                        return "执行完成成功";
                    }
                    else
                        return "执行完成失败";
                }
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinancingPledgeApplyIdComplete", e.Message);
                return e.Message;
            }
        }

        /// <summary>
        /// 质押申请单执行完成撤销
        /// </summary>
        /// <param name="userStr"></param>
        /// <param name="pledgeApplyId"></param>
        /// <returns></returns>
        [WebMethod]
        public string FinancingPledgeApplyIdCompleteCancel(string userStr, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                UserModel user = serializer.Deserialize<UserModel>(userStr);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    PledgeApplyDAL pledgeApplyDAL = new PledgeApplyDAL();
                    result = pledgeApplyDAL.Get(user, pledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    PledgeApply pledgeApply = result.ReturnValue as PledgeApply;

                    result = pledgeApplyDAL.CompleteCancel(user, pledgeApply);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    //完成撤销质押申请单实货明细
                    PledgeApplyStockDetailDAL pledgeApplyStockDetailDAL = new PledgeApplyStockDetailDAL();
                    result = pledgeApplyStockDetailDAL.LoadByPledgeApplyId(user, pledgeApplyId, StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    List<PledgeApplyStockDetail> stockDetails = result.ReturnValue as List<PledgeApplyStockDetail>;
                    if (stockDetails == null || !stockDetails.Any())
                        return "获取质押申请单实货明细失败";

                    foreach (PledgeApplyStockDetail detail in stockDetails)
                    {
                        result = pledgeApplyStockDetailDAL.CompleteCancel(user, detail);
                        if (result.ResultStatus != 0)
                            return result.Message;
                    }

                    //完成撤销质押申请单头寸明细
                    PledgeApplyCashDetailDAL pledgeApplyCashDetailDAL = new PledgeApplyCashDetailDAL();
                    result = pledgeApplyCashDetailDAL.LoadByPledgeApplyId(user, pledgeApplyId, StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    List<PledgeApplyCashDetail> cashDetails = result.ReturnValue as List<PledgeApplyCashDetail>;
                    if (cashDetails == null || !cashDetails.Any())
                        return "获取质押申请单头寸明细失败";

                    foreach (PledgeApplyCashDetail detail in cashDetails)
                    {
                        result = pledgeApplyCashDetailDAL.CompleteCancel(user, detail);
                        if (result.ResultStatus != 0)
                            return result.Message;
                    }

                    if (result.ResultStatus == 0)
                    {
                        scope.Complete();
                        return "执行完成撤销 成功";
                    }
                    else
                        return "执行完成撤销 失败";
                }
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinancingPledgeApplyIdCompleteCancel", e.Message);
                return e.Message;
            }
        }

        [WebMethod]
        public string FinancingPledgeApplyClose(string userStr, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                UserModel user = serializer.Deserialize<UserModel>(userStr);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    PledgeApplyDAL pledgeApplyDAL = new PledgeApplyDAL();
                    result = pledgeApplyDAL.Get(user, pledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    PledgeApply pledgeApply = result.ReturnValue as PledgeApply;

                    result = pledgeApplyDAL.Close(user, pledgeApply);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    //关闭质押申请单实货明细
                    PledgeApplyStockDetailDAL pledgeApplyStockDetailDAL = new PledgeApplyStockDetailDAL();
                    result = pledgeApplyStockDetailDAL.UpdateDetailStatus(user, pledgeApplyId, StatusEnum.已关闭);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    if (result.ResultStatus == 0)
                    {
                        scope.Complete();
                        return "执行关闭操作成功";
                    }
                    else
                        return "执行关闭操作失败";
                }
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinancingPledgeApplyClose", e.Message);
                return e.Message;
            }
        }

        /// <summary>
        /// 质押申请单更新手数
        /// </summary>
        /// <param name="userStr"></param>
        /// <param name="detailsStr"></param>
        /// <param name="pledgeApplyId"></param>
        /// <returns></returns>
        [WebMethod]
        public string FinancingPledgeApplyUpdateHands(string userStr, string detailsStr, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    UserModel user = serializer.Deserialize<UserModel>(userStr);
                    List<PledgeApplyStockDetail> details = serializer.Deserialize<List<PledgeApplyStockDetail>>(detailsStr);

                    int sumHands = 0;

                    PledgeApplyStockDetailDAL pledgeApplyStockDetailDAL = new PledgeApplyStockDetailDAL();
                    PledgeApplyStockDetail pledgeApplyStockDetail = null;
                    foreach (PledgeApplyStockDetail detail in details)
                    {
                        sumHands += detail.Hands;

                        result = pledgeApplyStockDetailDAL.Get(user, detail.StockDetailId);
                        if (result.ResultStatus != 0)
                            return result.Message;

                        pledgeApplyStockDetail = result.ReturnValue as PledgeApplyStockDetail;

                        pledgeApplyStockDetail.PledgeApplyId = detail.PledgeApplyId;
                        pledgeApplyStockDetail.ContractNo = detail.ContractNo;
                        pledgeApplyStockDetail.NetAmount = detail.NetAmount;
                        pledgeApplyStockDetail.StockId = detail.StockId;
                        pledgeApplyStockDetail.RefNo = detail.RefNo;
                        pledgeApplyStockDetail.Deadline = detail.Deadline;
                        pledgeApplyStockDetail.Hands = detail.Hands;
                        pledgeApplyStockDetail.Memo = detail.Memo;

                        result = pledgeApplyStockDetailDAL.Update(user, pledgeApplyStockDetail);
                        if (result.ResultStatus != 0)
                            return result.Message;
                    }

                    PledgeApplyDAL pledgeApplyDAL = new PledgeApplyDAL();
                    result = pledgeApplyDAL.Get(user, pledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    PledgeApply pledgeApply = result.ReturnValue as PledgeApply;

                    pledgeApply.SumHands = sumHands;
                    result = pledgeApplyDAL.Update(user, pledgeApply);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    if (result.ResultStatus == 0)
                        result.Message = "质押申请单手数更新成功";

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinancingPledgeApplyUpdateHands", ex.Message);
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }

            return serializer.Serialize(result);
        }

        /// <summary>
        /// 质押申请单添加期货头寸
        /// </summary>
        /// <param name="userStr"></param>
        /// <param name="detailsStr"></param>
        /// <param name="pledgeApplyId"></param>
        /// <returns></returns>
        [WebMethod]
        public string FinancingPledgeApplyUpdateCash(string userStr, string detailsStr, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    UserModel user = serializer.Deserialize<UserModel>(userStr);
                    List<PledgeApplyCashDetail> details = serializer.Deserialize<List<PledgeApplyCashDetail>>(detailsStr);

                    PledgeApplyCashDetailDAL pledgeApplyCashDetailDAL = new PledgeApplyCashDetailDAL();
                    result = pledgeApplyCashDetailDAL.UpdateStatus(user, pledgeApplyId, StatusEnum.无效数据);

                    foreach (PledgeApplyCashDetail detail in details)
                    {
                        detail.PledgeApplyId = pledgeApplyId;
                        result = pledgeApplyCashDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result.Message;
                    }

                    if (result.ResultStatus == 0)
                        result.Message = "期货头寸添加成功";

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinancingPledgeApplyUpdateCash", ex.Message);
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }

            return serializer.Serialize(result);
        }

        /// <summary>
        /// 获取赎回申请单列表
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="pagesize"></param>
        /// <param name="orderStr"></param>
        /// <param name="status"></param>
        /// <param name="pledgeApplyNo"></param>
        /// <param name="repoApplyNo"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetFinancingRepoApplyList(int pagenum, int pagesize, string orderStr, int status, string pledgeApplyNo, string repoApplyNo, string refNo, DateTime beginDate, DateTime endDate)
        {
            try
            {
                UserModel user = new UserModel();

                RepoApplyBLL bll = new RepoApplyBLL();
                SelectModel select = bll.GetSelectModel(pagenum, pagesize, orderStr, status, pledgeApplyNo, repoApplyNo, refNo, beginDate, endDate);
                ResultModel result = bll.Load(user, select);

                DataTable dt = new DataTable();

                if (result.ResultStatus == 0)
                    dt = result.ReturnValue as DataTable;

                Dictionary<string, object> dic = new Dictionary<string, object>();

                dic.Add("count", result.AffectCount);
                dic.Add("data", dt);

                return JsonConvert.SerializeObject(dic, new DataTableConverter());
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "GetFinancingRepoApplyList", e.Message);
                return e.Message;
            }
        }

        /// <summary>
        /// 获取质押申请单实货明细列表
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="pagesize"></param>
        /// <param name="orderStr"></param>
        /// <param name="pledgeApplyId"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetFinPledgeApplyStockDetailForRepoList(int pagenum, int pagesize, string orderStr, int pledgeApplyId)
        {
            try
            {
                UserModel user = new UserModel();

                PledgeApplyStockDetailBLL bll = new PledgeApplyStockDetailBLL();
                SelectModel select = bll.GetSelectModelForRepo(pagenum, pagesize, orderStr, pledgeApplyId);
                ResultModel result = bll.Load(user, select);

                if (result.ResultStatus != 0)
                    return string.Empty;

                DataTable dt = result.ReturnValue as DataTable;
                Dictionary<string, object> dic = new Dictionary<string, object>();

                dic.Add("count", result.AffectCount);
                dic.Add("data", dt);

                return JsonConvert.SerializeObject(dic, new DataTableConverter());
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "GetFinPledgeApplyStockDetailForRepoList", e.Message);
                return e.Message;
            }
        }

        [WebMethod]
        public string FinRepoApplyCreate(string userStr, string repoApplyStr, string repoApplyDetailsStr, bool isSubmitAudit)
        {
            ResultModel result = new ResultModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    UserModel user = serializer.Deserialize<UserModel>(userStr);
                    RepoApply repoApply = serializer.Deserialize<RepoApply>(repoApplyStr);
                    List<RepoApplyDetail> details = serializer.Deserialize<List<RepoApplyDetail>>(repoApplyDetailsStr);

                    RepoApplyDAL repoApplyDAL = new RepoApplyDAL();
                    result = repoApplyDAL.Insert(user, repoApply);
                    if (result.ResultStatus != 0)
                        return serializer.Serialize(result);

                    int repoApplyId = (int)result.ReturnValue;

                    RepoApplyDetailDAL repoApplyDetailDAL = new RepoApplyDetailDAL();
                    foreach (RepoApplyDetail detail in details)
                    {
                        detail.RepoApplyId = repoApplyId;
                        detail.PledgeApplyId = repoApply.PledgeApplyId;

                        result = repoApplyDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return serializer.Serialize(result);
                    }

                    if (result.ResultStatus == 0)
                    {
                        if (isSubmitAudit)
                        {
                            repoApply.RepoApplyId = repoApplyId;

                            AutoSubmit submit = new AutoSubmit();
                            result = submit.Submit(user, repoApply, new RepoApplyTaskProvider(), MasterEnum.赎回申请单审核);
                        }
                        result.Message = "赎回申请单新增成功";
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinRepoApplyCreate", ex.Message);
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }

            return serializer.Serialize(result);
        }

        [WebMethod]
        public string GetFinRepoApplyStockDetailForUpdateUp(int pagenum, int pagesize, string orderStr, int pledgeApplyId, int repoApplyId)
        {
            try
            {
                UserModel user = new UserModel();

                PledgeApplyStockDetailBLL bll = new PledgeApplyStockDetailBLL();
                SelectModel select = bll.GetSelectModelForRepoUpdate(pagenum, pagesize, orderStr, pledgeApplyId, repoApplyId);
                ResultModel result = bll.Load(user, select);

                if (result.ResultStatus != 0)
                    return string.Empty;

                DataTable dt = result.ReturnValue as DataTable;
                Dictionary<string, object> dic = new Dictionary<string, object>();

                dic.Add("count", result.AffectCount);
                dic.Add("data", dt);

                return JsonConvert.SerializeObject(dic, new DataTableConverter());
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "GetFinRepoApplyStockDetailForUpdateUp", e.Message);
                return e.Message;
            }
        }

        [WebMethod]
        public string GetFinRepoApplyStockDetailForUpdateDown(int pagenum, int pagesize, string orderStr, int pledgeApplyId, int repoApplyId)
        {
            try
            {
                UserModel user = new UserModel();

                RepoApplyDetailBLL bll = new RepoApplyDetailBLL();
                SelectModel select = bll.GetSelectModelForRepoUpdate(pagenum, pagesize, orderStr, pledgeApplyId, repoApplyId);
                ResultModel result = bll.Load(user, select);

                if (result.ResultStatus != 0)
                    return string.Empty;

                DataTable dt = result.ReturnValue as DataTable;
                Dictionary<string, object> dic = new Dictionary<string, object>();

                dic.Add("count", result.AffectCount);
                dic.Add("data", dt);

                return JsonConvert.SerializeObject(dic, new DataTableConverter());
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "GetFinRepoApplyStockDetailForUpdateDown", e.Message);
                return e.Message;
            }
        }

        [WebMethod]
        public string FinRepoApplyUpdate(string userStr, string repoApplyStr, string repoApplyDetailsStr)
        {
            ResultModel result = new ResultModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    UserModel user = serializer.Deserialize<UserModel>(userStr);
                    RepoApply repoApply = serializer.Deserialize<RepoApply>(repoApplyStr);
                    List<RepoApplyDetail> details = serializer.Deserialize<List<RepoApplyDetail>>(repoApplyDetailsStr);

                    RepoApplyDAL repoApplyDAL = new RepoApplyDAL();
                    result = repoApplyDAL.Get(user, repoApply.RepoApplyId);
                    if (result.ResultStatus != 0)
                        return serializer.Serialize(result);

                    RepoApply repoApplyGet = result.ReturnValue as RepoApply;
                    repoApplyGet.SumNetAmount = repoApply.SumNetAmount;
                    repoApplyGet.SumHands = repoApply.SumHands;

                    result = repoApplyDAL.Update(user, repoApplyGet);
                    if (result.ResultStatus != 0)
                        return serializer.Serialize(result);

                    RepoApplyDetailDAL repoApplyDetailDAL = new RepoApplyDetailDAL();
                    result = repoApplyDetailDAL.UpdateStatus(user, repoApply.RepoApplyId, NFMT.Common.StatusEnum.无效数据);
                    if (result.ResultStatus != 0)
                        return serializer.Serialize(result);

                    foreach (RepoApplyDetail detail in details)
                    {
                        detail.RepoApplyId = repoApply.RepoApplyId;
                        detail.PledgeApplyId = repoApply.PledgeApplyId;

                        result = repoApplyDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return serializer.Serialize(result);
                    }

                    if (result.ResultStatus == 0)
                        result.Message = "赎回申请单修改成功";

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinRepoApplyUpdate", ex.Message);
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }

            return serializer.Serialize(result);
        }

        /// <summary>
        /// 质押申请单作废
        /// </summary>
        /// <param name="userStr"></param>
        /// <param name="repoApplyId"></param>
        /// <returns></returns>
        [WebMethod]
        public string FinancingRepoApplyInvalid(string userStr, int repoApplyId)
        {
            ResultModel result = new ResultModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                UserModel user = serializer.Deserialize<UserModel>(userStr);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    RepoApplyDAL repoApplyDAL = new RepoApplyDAL();
                    result = repoApplyDAL.Get(user, repoApplyId);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    RepoApply repoApply = result.ReturnValue as RepoApply;

                    result = repoApplyDAL.Invalid(user, repoApply);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    RepoApplyDetailDAL repoApplyDetailDAL = new RepoApplyDetailDAL();
                    result = repoApplyDetailDAL.InvalidAll(user, repoApplyId);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    if (result.ResultStatus == 0)
                    {
                        scope.Complete();
                        return "作废成功";
                    }
                    else
                        return "作废失败";
                }
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinancingRepoApplyInvalid", e.Message);
                return e.Message;
            }
        }

        /// <summary>
        /// 质押申请单撤返
        /// </summary>
        /// <param name="userStr"></param>
        /// <param name="repoApplyId"></param>
        /// <returns></returns>
        [WebMethod]
        public string FinancingRepoApplyGoBack(string userStr, int repoApplyId)
        {
            ResultModel result = new ResultModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                UserModel user = serializer.Deserialize<UserModel>(userStr);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    RepoApplyDAL repoApplyDAL = new RepoApplyDAL();
                    result = repoApplyDAL.Get(user, repoApplyId);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    RepoApply repoApply = result.ReturnValue as RepoApply;

                    result = repoApplyDAL.Goback(user, repoApply);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    //工作流任务关闭
                    DataSourceDAL sourceDAL = new DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, repoApply);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    if (result.ResultStatus == 0)
                    {
                        scope.Complete();
                        return "撤返成功";
                    }
                    else
                        return "撤返失败";
                }
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinancingRepoApplyGoBack", e.Message);
                return e.Message;
            }
        }

        /// <summary>
        /// 质押申请单执行完成
        /// </summary>
        /// <param name="userStr"></param>
        /// <param name="repoApplyId"></param>
        /// <returns></returns>
        [WebMethod]
        public string FinancingRepoApplyIdComplete(string userStr, int repoApplyId)
        {
            ResultModel result = new ResultModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                UserModel user = serializer.Deserialize<UserModel>(userStr);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //完成赎回申请单
                    RepoApplyDAL repoApplyDAL = new RepoApplyDAL();
                    result = repoApplyDAL.Get(user, repoApplyId);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    RepoApply repoApply = result.ReturnValue as RepoApply;

                    result = repoApplyDAL.Complete(user, repoApply);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    //完成赎回申请单明细
                    RepoApplyDetailDAL repoApplyDetailDAL = new RepoApplyDetailDAL();
                    result = repoApplyDetailDAL.LoadByRepoApplyId(user, repoApplyId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    List<RepoApplyDetail> details = result.ReturnValue as List<RepoApplyDetail>;
                    if (details == null || !details.Any())
                        return "获取赎回申请单明细失败";

                    foreach (RepoApplyDetail detail in details)
                    {
                        result = repoApplyDetailDAL.Complete(user, detail);
                        if (result.ResultStatus != 0)
                            return result.Message;
                    }

                    if (result.ResultStatus == 0)
                    {
                        scope.Complete();
                        return "执行完成成功";
                    }
                    else
                        return "执行完成失败";
                }
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinancingRepoApplyIdComplete", e.Message);
                return e.Message;
            }
        }

        /// <summary>
        /// 质押申请单执行完成撤销
        /// </summary>
        /// <param name="userStr"></param>
        /// <param name="repoApplyId"></param>
        /// <returns></returns>
        [WebMethod]
        public string FinancingRepoApplyIdCompleteCancel(string userStr, int repoApplyId)
        {
            ResultModel result = new ResultModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                UserModel user = serializer.Deserialize<UserModel>(userStr);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    RepoApplyDAL repoApplyDAL = new RepoApplyDAL();
                    result = repoApplyDAL.Get(user, repoApplyId);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    RepoApply repoApply = result.ReturnValue as RepoApply;

                    result = repoApplyDAL.CompleteCancel(user, repoApply);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    //完成撤销赎回申请单明细
                    RepoApplyDetailDAL repoApplyDetailDAL = new RepoApplyDetailDAL();
                    result = repoApplyDetailDAL.LoadByRepoApplyId(user, repoApplyId, StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    List<RepoApplyDetail> details = result.ReturnValue as List<RepoApplyDetail>;
                    if (details == null || !details.Any())
                        return "获取质押申请单实货明细失败";

                    foreach (RepoApplyDetail detail in details)
                    {
                        result = repoApplyDetailDAL.CompleteCancel(user, detail);
                        if (result.ResultStatus != 0)
                            return result.Message;
                    }

                    if (result.ResultStatus == 0)
                    {
                        scope.Complete();
                        return "执行完成撤销 成功";
                    }
                    else
                        return "执行完成撤销 失败";
                }
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinancingRepoApplyIdCompleteCancel", e.Message);
                return e.Message;
            }
        }
                
        [WebMethod]
        public string GetFinRepoApplyStockDetailForHands(int pagenum, int pagesize, string orderStr, int pledgeApplyId, int repoApplyId)
        {
            try
            {
                UserModel user = new UserModel();

                RepoApplyDetailBLL bll = new RepoApplyDetailBLL();
                SelectModel select = bll.GetSelectModelForHand(pagenum, pagesize, orderStr, pledgeApplyId, repoApplyId);
                ResultModel result = bll.Load(user, select);

                if (result.ResultStatus != 0)
                    return string.Empty;

                DataTable dt = result.ReturnValue as DataTable;
                Dictionary<string, object> dic = new Dictionary<string, object>();

                dic.Add("count", result.AffectCount);
                dic.Add("data", dt);

                return JsonConvert.SerializeObject(dic, new DataTableConverter());
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "GetFinRepoApplyStockDetailForHands", e.Message);
                return e.Message;
            }
        }

        [WebMethod]
        public string FinancingRepoApplyUpdateHands(string userStr, string detailsStr, int repoApplyId)
        {
            ResultModel result = new ResultModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    UserModel user = serializer.Deserialize<UserModel>(userStr);
                    List<RepoApplyDetail> details = serializer.Deserialize<List<RepoApplyDetail>>(detailsStr);

                    int sumHands = 0;

                    RepoApplyDetailDAL repoApplyDetailDAL = new RepoApplyDetailDAL();
                    RepoApplyDetail repoApplyDetail = null;
                    foreach (RepoApplyDetail detail in details)
                    {
                        sumHands += detail.Hands;

                        result = repoApplyDetailDAL.Get(user, detail.DetailId);
                        if (result.ResultStatus != 0)
                            return result.Message;

                        repoApplyDetail = result.ReturnValue as RepoApplyDetail;

                        repoApplyDetail.Hands = detail.Hands;

                        result = repoApplyDetailDAL.Update(user, repoApplyDetail);
                        if (result.ResultStatus != 0)
                            return result.Message;
                    }

                    RepoApplyDAL repoApplyDAL = new RepoApplyDAL();
                    result = repoApplyDAL.Get(user, repoApplyId);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    RepoApply repoApply = result.ReturnValue as RepoApply;

                    repoApply.SumHands = sumHands;
                    result = repoApplyDAL.Update(user, repoApply);
                    if (result.ResultStatus != 0)
                        return result.Message;

                    if (result.ResultStatus == 0)
                        result.Message = "赎回申请单手数更新成功";

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinancingRepoApplyUpdateHands", ex.Message);
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }

            return serializer.Serialize(result);
        }

        [WebMethod]
        public string FinancingRepoApplyUpdateCash(string userStr, string detailsStr, int repoApplyId)
        {
            ResultModel result = new ResultModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    UserModel user = serializer.Deserialize<UserModel>(userStr);
                    List<RepoApplyDetail> details = serializer.Deserialize<List<RepoApplyDetail>>(detailsStr);

                    RepoApplyDetailDAL repoApplyDetailDAL = new RepoApplyDetailDAL();
                    RepoApplyDetail repoApplyDetail = null;
                    foreach (RepoApplyDetail detail in details)
                    {
                        result = repoApplyDetailDAL.Get(user, detail.DetailId);
                        if (result.ResultStatus != 0)
                            return result.Message;

                        repoApplyDetail = result.ReturnValue as RepoApplyDetail;

                        repoApplyDetail.Price = detail.Price;
                        repoApplyDetail.ExpiringDate = detail.ExpiringDate;

                        result = repoApplyDetailDAL.Update(user, repoApplyDetail);
                        if (result.ResultStatus != 0)
                            return result.Message;
                    }

                    if (result.ResultStatus == 0)
                        result.Message = "期货头寸添加成功";

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "FinancingRepoApplyUpdateCash", ex.Message);
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }

            return serializer.Serialize(result);
        }

        [WebMethod]
        public string GetFinancingPledgeApplyRepoInfoList(int pagenum, int pagesize, string orderStr, int pledgeApplyId)
        {
            try
            {
                UserModel user = new UserModel();

                RepoApplyBLL bll = new RepoApplyBLL();
                SelectModel select = bll.GetSelectModelRepoInfo(pagenum, pagesize, orderStr, pledgeApplyId);
                ResultModel result = bll.Load(user, select);

                DataTable dt = new DataTable();

                if (result.ResultStatus == 0)
                    dt = result.ReturnValue as DataTable;

                Dictionary<string, object> dic = new Dictionary<string, object>();

                dic.Add("count", result.AffectCount);
                dic.Add("data", dt);

                return JsonConvert.SerializeObject(dic, new DataTableConverter());
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "GetFinancingPledgeApplyRepoInfoList", e.Message);
                return e.Message;
            }
        }

        [WebMethod]
        public string GetFinancingPledgeApplyListForRepoCreate(int pagenum, int pagesize, string orderStr, DateTime fromDate, DateTime toDate, int bankId, int assetId, int status, string pledgeApplyNo, string refNo)
        {
            try
            {
                UserModel user = new UserModel();

                PledgeApplyBLL pledgeApplyBLL = new PledgeApplyBLL();
                SelectModel select = pledgeApplyBLL.GetSelectModelForRepoCreate(pagenum, pagesize, orderStr, fromDate, toDate, bankId, assetId, status, pledgeApplyNo, refNo);
                ResultModel result = pledgeApplyBLL.Load(user, select);

                DataTable dt = new DataTable();

                if (result.ResultStatus == 0)
                    dt = result.ReturnValue as DataTable;

                Dictionary<string, object> dic = new Dictionary<string, object>();

                dic.Add("count", result.AffectCount);
                dic.Add("data", dt);

                return JsonConvert.SerializeObject(dic, new DataTableConverter());
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("FinService出错，出错方法：{0}，{1}", "GetFinancingPledgeApplyListForRepoCreate", e.Message);
                return e.Message;
            }
        }
    }
}
