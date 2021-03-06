﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WorkFlow.Handler
{
    /// <summary>
    /// PaymentApplyConditionHandler 的摘要说明
    /// </summary>
    public class PaymentApplyConditionHandler : IHttpHandler
    {
        NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(context.Request.Form["source"]))
            {
                result.Message = "数据源为空";
                result.ResultStatus = -1;
                context.Response.Write(serializer.Serialize(result));
                context.Response.End();
            }

            try
            {
                string jsonData = context.Request.Form["source"];

                var obj = serializer.Deserialize<NFMT.WorkFlow.Model.DataSource>(jsonData);

                NFMT.Funds.BLL.PayApplyBLL bll = new NFMT.Funds.BLL.PayApplyBLL();
                result = bll.GetCondition(user, obj.RowId);

                //NFMT.Common.Operate operate = NFMT.Common.Operate.CreateOperate(obj.DalName, obj.AssName);
                //result = operate.Get(user, obj.BaseName, obj.TableCode, obj.RowId);

                if (result.ResultStatus == 0)
                {
                    System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dt.Rows[0];
                        foreach (System.Data.DataColumn column in dt.Columns)
                        {
                            string columnName = column.ColumnName;
                            object val = dr[columnName];
                            string valStr = string.Empty;
                            if (val != null)
                                valStr = val.ToString();

                            //condition.ConditionCollection.Add(columnName, valStr);
                            dic.Add(columnName, valStr);
                        }

                        result.ReturnValue = dic;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(serializer.Serialize(result));
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