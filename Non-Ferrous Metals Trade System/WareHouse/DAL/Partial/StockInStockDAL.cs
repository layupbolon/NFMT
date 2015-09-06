using NFMT.Common;
using NFMT.WareHouse.IDAL;
using NFMT.WareHouse.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.WareHouse.DAL
{
    public partial class StockInStockDAL : ExecOperate, IStockInStockDAL
    {
        #region 新增方法

        public ResultModel UpdateStockStatus(Stock stock, StockStatusEnum stockStatus)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.St_Stock set PreStatus = StockStatus ,StockStatus = {0} where StockId = {1} ", (int)stockStatus, stock.StockId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);

                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "更新状态成功";
                    result.AffectCount = i;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "更新状态失败";
                    result.AffectCount = i;
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = string.Format("更新状态失败,{0}", e.Message);
            }

            return result;
        }

        public ResultModel UpdateStockDP(int stockId, int dpId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.St_Stock set DeliverPlaceId = {0} where StockId = {1}", dpId, stockId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);

                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "更新交货地成功";
                    result.AffectCount = i;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "更新交货地失败";
                    result.AffectCount = i;
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = string.Format("更新状态失败,{0}", e.Message);
            }

            return result;
        }

        public ResultModel UpdateStockStatusToPrevious(UserModel user, Stock stock)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update NFMT.dbo.St_Stock set StockStatus = PreStatus where StockId = {0}", stock.StockId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "更新成功";
                    result.AffectCount = i;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "更新失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        public ResultModel GetStockContractOutCorp(UserModel user, int stockId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select corpdetail.CorpId,c.CorpName ");
                sb.Append("from dbo.Con_ContractSub sub  ");
                sb.Append("left join dbo.Con_ContractCorporationDetail corpdetail on sub.ContractId = corpdetail.ContractId ");
                sb.Append("left join NFMT_User.dbo.Corporation c on corpdetail.CorpId = c.CorpId ");
                sb.AppendFormat("where corpdetail.IsInnerCorp = 0 and sub.SubId = (select top 1 SubContractId from dbo.St_StockLog where StockId = {0})", stockId);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.Message = "获取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                }
                else
                {
                    result.Message = "获取失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        public ResultModel GetCurrencyId(UserModel user, int stockId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select sub.SettleCurrency from St_StockLog sl  ");
                sb.Append("left join dbo.Con_ContractSub sub on sl.SubContractId = sub.SubId  ");
                sb.AppendFormat("where sl.StockId = {0}", stockId);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sb.ToString(), null);
                int i;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && int.TryParse(obj.ToString(), out i))
                {
                    result.Message = "获取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = i;
                }
                else
                {
                    result.Message = "获取失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        public ResultModel GetByStockIn(UserModel user, int stockInId, StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();

            try
            {
                string cmdText = string.Format("select * from dbo.St_StockInStock_Ref where StockInId = {0} and RefStatus >={1}", stockInId, (int)status);

                result = Get(user, CommandType.Text, cmdText, null);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
                return result;
            }

            return result;
        }

        public ResultModel GetByStockLogId(UserModel user, int stockLogId, StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.St_StockInStock_Ref where StockLogId = {0} and RefStatus >={1}", stockLogId, (int)status);

            ResultModel result = Get(user, CommandType.Text, cmdText, null);

            return result;
        }

        #endregion
    }
}
