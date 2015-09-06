using NFMT.Common;
using NFMT.DoPrice.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.DoPrice.DAL
{
    partial class PriceConfirmDAL : ExecOperate, IPriceConfirmDAL
    {
        public ResultModel LoadPriceConfirmListBySubId(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            string cmdTex = string.Format("select * from dbo.Pri_PriceConfirm where SubId={0} and PriceConfirmStatus >={1}", subId, (int)StatusEnum.已生效);

            result = this.Load<Model.PriceConfirm>(user, System.Data.CommandType.Text, cmdTex);

            return result;
        }

        public ResultModel GetPriceConfirmStockListSelect(UserModel user, int subId,System.Text.StringBuilder sb)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Data.DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, System.Data.CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = dt;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }
    }
}
