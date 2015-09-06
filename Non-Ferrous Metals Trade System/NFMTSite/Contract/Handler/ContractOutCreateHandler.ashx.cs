using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Contract.Handler
{
    /// <summary>
    /// ContractOutCreateHandler 的摘要说明
    /// </summary>
    public class ContractOutCreateHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            string contractStr = context.Request.Form["Contract"];
            string detailStr = context.Request.Form["ContractDetail"];
            string priceStr = context.Request.Form["ContractPrice"];
            string outCorpStr = context.Request.Form["OutCorps"];
            string inCorpStr = context.Request.Form["InCorps"];
            string deptStr = context.Request.Form["Depts"];
            string checkedClauseStr = context.Request.Form["checkedClause"];
            string subStr = context.Request.Form["sub"];
            string stockStr = context.Request.Form["stocks"];
            string contractTypesStr = context.Request.Form["ContractTypes"];

            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            if (string.IsNullOrEmpty(contractStr))
            {
                result.Message = "合约不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (string.IsNullOrEmpty(detailStr))
            {
                result.Message = "合约明细不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (string.IsNullOrEmpty(priceStr))
            {
                result.Message = "合约价格不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (string.IsNullOrEmpty(outCorpStr))
            {
                result.Message = "外部公司不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (string.IsNullOrEmpty(inCorpStr))
            {
                result.Message = "内部公司不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (string.IsNullOrEmpty(deptStr))
            {
                result.Message = "执行不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (string.IsNullOrEmpty(subStr))
            {
                result.Message = "子合约不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                NFMT.Contract.Model.Contract contract = serializer.Deserialize<NFMT.Contract.Model.Contract>(contractStr);
                NFMT.Contract.Model.ContractDetail detail = serializer.Deserialize<NFMT.Contract.Model.ContractDetail>(detailStr);
                NFMT.Contract.Model.ContractPrice price = serializer.Deserialize<NFMT.Contract.Model.ContractPrice>(priceStr);
                List<NFMT.Contract.Model.ContractClause> contractClauses = new List<NFMT.Contract.Model.ContractClause>();
                if (!string.IsNullOrEmpty(checkedClauseStr))
                    contractClauses = serializer.Deserialize<List<NFMT.Contract.Model.ContractClause>>(checkedClauseStr);

                NFMT.Contract.Model.ContractSub contractSub = serializer.Deserialize<NFMT.Contract.Model.ContractSub>(subStr);

                char[] splitStr = new char[1];
                splitStr[0] = ',';
                List<NFMT.Contract.Model.ContractCorporationDetail> outCorps = new List<NFMT.Contract.Model.ContractCorporationDetail>();
                List<NFMT.Contract.Model.ContractCorporationDetail> inCorps = new List<NFMT.Contract.Model.ContractCorporationDetail>();
                List<NFMT.Contract.Model.ContractDept> depts = new List<NFMT.Contract.Model.ContractDept>();

                string[] strs = outCorpStr.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in strs)
                {
                    var obj = serializer.Deserialize<NFMT.Contract.Model.ContractCorporationDetail>(s);
                    outCorps.Add(obj);
                }

                strs = inCorpStr.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in strs)
                {
                    var obj = serializer.Deserialize<NFMT.Contract.Model.ContractCorporationDetail>(s);
                    inCorps.Add(obj);
                }

                strs = deptStr.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in strs)
                {
                    var obj = serializer.Deserialize<NFMT.Contract.Model.ContractDept>(s);
                    depts.Add(obj);
                }

                List<NFMT.Contract.Model.ContractTypeDetail> contractTypes = new List<NFMT.Contract.Model.ContractTypeDetail>();
                if (!string.IsNullOrEmpty(contractTypesStr))
                {
                    strs = contractTypesStr.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in strs)
                    {
                        var obj = serializer.Deserialize<NFMT.Contract.Model.ContractTypeDetail>(s);
                        obj.ContractId = contract.ContractId;
                        contractTypes.Add(obj);
                    }
                }

                List<NFMT.WareHouse.Model.StockOutApplyDetail> applyDetails = serializer.Deserialize<List<NFMT.WareHouse.Model.StockOutApplyDetail>>(stockStr);

                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
                    NFMT.WareHouse.BLL.StockOutApplyBLL applyBLL = new NFMT.WareHouse.BLL.StockOutApplyBLL();

                    result = bll.ContractOutCreate(user, contract, detail, price, outCorps, inCorps, depts, contractClauses, contractSub, contractTypes);

                    if (result.ResultStatus == 0)
                    {
                        NFMT.Contract.Model.Contract resultContract = result.ReturnValue as NFMT.Contract.Model.Contract;
                        int subId = result.AffectCount;

                        if (resultContract == null || resultContract.ContractId <= 0)
                        {
                            result.Message = "子合约不能为空";
                            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                            context.Response.End();
                        }

                        foreach (NFMT.WareHouse.Model.StockOutApplyDetail applyDetail in applyDetails)
                        {
                            applyDetail.ContractId = resultContract.ContractId;
                            applyDetail.SubContractId = subId;
                        }

                        result = applyBLL.ContractOutCreateStockOperate(user, resultContract, subId, outCorps[0].CorpId, applyDetails);

                        if (result.ResultStatus == 0)
                        {
                            result.AffectCount = subId;
                            result.ReturnValue = resultContract;
                        }
                    }

                    if (result.ResultStatus == 0)
                    {
                        result.Message = "合约添加成功";
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}