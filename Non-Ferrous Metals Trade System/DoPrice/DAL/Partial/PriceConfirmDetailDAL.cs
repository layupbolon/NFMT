using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.DoPrice.Model;
using NFMT.DBUtility;
using NFMT.DoPrice.IDAL;
using NFMT.Common;

namespace NFMT.DoPrice.DAL
{
    public partial class PriceConfirmDetailDAL : DetailOperate, IPriceConfirmDetailDAL
    {
        #region 新增方法

        public ResultModel InvalidAll(UserModel user, int priceConfirmId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Pri_PriceConfirmDetail set DetailStatus = {0} where PriceConfirmId = {1}", (int)Common.StatusEnum.已作废, priceConfirmId);
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

        public ResultModel Load(UserModel user, int priceConfirmId, Common.StatusEnum status = Common.StatusEnum.已生效)
        {
            string sql = string.Format("select * from dbo.Pri_PriceConfirmDetail where PriceConfirmId ={0} and DetailStatus = {1}", priceConfirmId, (int)status);
            return base.Load<Model.PriceConfirmDetail>(user, CommandType.Text, sql);
        }

        #endregion
    }
}
