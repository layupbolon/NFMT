using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Contract.Handler
{
    /// <summary>
    /// ContractUpdateHandler 的摘要说明
    /// </summary>
    public class ContractUpdateHandler : IHttpHandler
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
           
            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                NFMT.Contract.Model.Contract contract = serializer.Deserialize<NFMT.Contract.Model.Contract>(contractStr);

                NFMT.Contract.Model.ContractDetail detail = serializer.Deserialize<NFMT.Contract.Model.ContractDetail>(detailStr);
                detail.ContractId = contract.ContractId;

                NFMT.Contract.Model.ContractPrice price = serializer.Deserialize<NFMT.Contract.Model.ContractPrice>(priceStr);
                price.ContractId = contract.ContractId;

                List<NFMT.Contract.Model.ContractClause> contractClauses = new List<NFMT.Contract.Model.ContractClause>();
                if (!string.IsNullOrEmpty(checkedClauseStr))
                    contractClauses = serializer.Deserialize<List<NFMT.Contract.Model.ContractClause>>(checkedClauseStr);

                char[] splitStr = new char[1];
                splitStr[0] = ',';

                List<NFMT.Contract.Model.ContractCorporationDetail> outCorps = new List<NFMT.Contract.Model.ContractCorporationDetail>();
                List<NFMT.Contract.Model.ContractCorporationDetail> inCorps = new List<NFMT.Contract.Model.ContractCorporationDetail>();
                List<NFMT.Contract.Model.ContractDept> depts = new List<NFMT.Contract.Model.ContractDept>();

                string[] strs = outCorpStr.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in strs)
                {
                    var obj = serializer.Deserialize<NFMT.Contract.Model.ContractCorporationDetail>(s);
                    obj.ContractId = contract.ContractId;
                    outCorps.Add(obj);
                }

                strs = inCorpStr.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in strs)
                {
                    var obj = serializer.Deserialize<NFMT.Contract.Model.ContractCorporationDetail>(s);
                    obj.ContractId = contract.ContractId;
                    inCorps.Add(obj);
                }

                strs = deptStr.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in strs)
                {
                    var obj = serializer.Deserialize<NFMT.Contract.Model.ContractDept>(s);
                    obj.ContractId = contract.ContractId;
                    depts.Add(obj);
                }

                NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
                result = bll.UpdateContract(user, contract, detail, price, outCorps, inCorps, depts, contractClauses);

                if (result.ResultStatus == 0)
                {
                    result.Message = "合约更新成功";
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