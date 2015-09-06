using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Invoice.Model;
using NFMT.DBUtility;
using NFMT.Invoice.IDAL;
using NFMT.Common;

namespace NFMT.Invoice.DAL
{
    public partial class InvoiceApplyDetailDAL : ApplyOperate, IInvoiceApplyDetailDAL
    {
        #region 新增方法

        public ResultModel InvalidAll(UserModel user, int invoiceApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Inv_InvoiceApplyDetail set DetailStatus = {0} where InvoiceApplyId = {1}", (int)Common.StatusEnum.已作废, invoiceApplyId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "作废失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel Load(UserModel user, int invoiceApplyId, Common.StatusEnum status = StatusEnum.已生效)
        {
            string sql = string.Format("select * from dbo.Inv_InvoiceApplyDetail where InvoiceApplyId = {0} and DetailStatus = {1}", invoiceApplyId, (int)status);
            return base.Load<Model.InvoiceApplyDetail>(user, CommandType.Text, sql);
        }

        #endregion
    }
}
