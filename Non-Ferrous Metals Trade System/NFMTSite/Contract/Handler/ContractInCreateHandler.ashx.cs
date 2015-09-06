using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Contract.Handler
{
    /// <summary>
    /// ContractInCreateHandler 的摘要说明
    /// </summary>
    public class ContractInCreateHandler : IHttpHandler
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
            //string isSubmitAuditStr = context.Request.Form["IsSubmitAudit"];
            string checkedClauseStr = context.Request.Form["checkedClause"];
            string subStr = context.Request.Form["sub"];
            string stockLogIds = context.Request.Form["stockLogIds"];
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

            //bool isSubmitAudit = false;
            //if (string.IsNullOrEmpty(isSubmitAuditStr) || !bool.TryParse(isSubmitAuditStr, out isSubmitAudit))
            //    isSubmitAudit = false;

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

                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
                    result = bll.ContractInCreate(user, contract, detail, price, outCorps, inCorps, depts, contractClauses, contractSub, contractTypes);

                    NFMT.Contract.Model.Contract resultContract = result.ReturnValue as NFMT.Contract.Model.Contract;
                    if (resultContract == null || resultContract.ContractId <= 0)
                    {
                        result.Message = "合约新增失败";
                        result.ResultStatus = -1;
                        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                        context.Response.End();
                    }
                    int subId = result.AffectCount;

                    List<int> ids = new List<int>();
                    string[] logIds = stockLogIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string id in logIds)
                    {
                        int stockLogId = 0;
                        if (int.TryParse(id, out stockLogId) && stockLogId > 0)
                            ids.Add(stockLogId);
                    }
                    NFMT.WareHouse.BLL.ContractStockIn_BLL sbll = new NFMT.WareHouse.BLL.ContractStockIn_BLL();
                    result = sbll.ContractInCreateStockOperate(user, resultContract, subId, ids);
                    if (result.ResultStatus != 0)
                    {
                        result.Message = "库存关联新增失败";
                        result.ResultStatus = -1;
                        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                        context.Response.End();
                    }
                    else
                    {
                        result.Message = "合约添加成功";
                        result.AffectCount = subId;
                        result.ReturnValue = resultContract;
                    }

                    scope.Complete();
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