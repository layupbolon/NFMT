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
    public partial class InvoiceApplyDAL : DataOperate, IInvoiceApplyDAL
    {
        #region 新增方法

        public ResultModel GetByApplyId(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                string sql = string.Format(" select * from dbo.Inv_InvoiceApply where ApplyId = {0}", applyId);
                dr = SqlHelper.ExecuteReader(this.ConnectString, CommandType.Text, sql, null);

                Model.InvoiceApply model = null;

                if (dr.Read())
                {
                    model = CreateModel<Model.InvoiceApply>(dr);

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = model;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        #endregion
    }
}
