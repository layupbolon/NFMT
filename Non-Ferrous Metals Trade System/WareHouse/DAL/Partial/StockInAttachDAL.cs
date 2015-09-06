using NFMT.Common;
using NFMT.WareHouse.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.WareHouse.DAL
{
    public partial class StockInAttachDAL : DataOperate, IStockInAttachDAL
    {
        #region 新增方法

        public ResultModel GetAttachIds(UserModel user, int stockInId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select AttachId from dbo.St_StockInAttach sia left join dbo.Attach a on sia.AttachId = a.AttachId where sia.StockInAttachId = {0} and a.AttachStatus = {1} ", stockInId, (int)Common.StatusEnum.已生效);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                string returnStr = string.Empty;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        returnStr += dr["AttachId"].ToString() + ",";
                    }
                    if (!string.IsNullOrEmpty(returnStr) && returnStr.IndexOf(",") > -1)
                        returnStr = returnStr.Substring(0, returnStr.Length - 1);

                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = returnStr;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        #endregion
    }
}
