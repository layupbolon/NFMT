using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFMT.Funds.Model;
using NFMT.DBUtility;
using NFMT.Funds.IDAL;
using NFMT.Common;
using System.Data;

namespace NFMT.Funds.DAL
{
    public partial class CashInStcokDAL : DetailOperate, ICashInStcokDAL
    {
        #region 新增方法

        public ResultModel Load(UserModel user, int stockLogId, StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.Fun_CashInStcok_Ref where StockLogId ={0} and DetailStatus = {1}", stockLogId, (int)status);
            return Load<Model.CashInStcok>(user, CommandType.Text, cmdText);
        }

        public ResultModel LoadByContractRefId(UserModel user, int contractRefId, StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.Fun_CashInStcok_Ref where ContractRefId ={0} and DetailStatus = {1}", contractRefId, (int)status);
            return Load<Model.CashInStcok>(user, CommandType.Text, cmdText);
        }

        public ResultModel LoadByAllot(UserModel user, int allotId, StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.Fun_CashInStcok_Ref where AllotId ={0} and DetailStatus = {1}", allotId, (int)status);
            return Load<Model.CashInStcok>(user, CommandType.Text, cmdText);
        }

        public override int MenuId
        {
            get
            {
                return 58;
            }
        }

        public ResultModel GetStockInfoByAlotId(UserModel user, int allotId, NFMT.Common.StatusEnum status)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select slog.StockLogId,slog.StockId,slog.StockNameId,slog.RefNo,corp.CorpName,ass.AssetName,brand.BrandName,bd.StatusName,mu.MUName,slog.NetAmount+ISNULL(slog.GapAmount,0) NetGapAmount,ref.AllotBala,dp.DPName,slog.CardNo,ref.AllotNetAmount ");
                sb.Append(" from NFMT..Fun_CashInStcok_Ref ref ");
                sb.Append(" left join NFMT..St_StockLog slog on ref.StockLogId = slog.StockLogId ");
                sb.Append(" left join NFMT..St_Stock st on slog.StockId = st.StockId ");
                sb.Append(" left join NFMT_User..Corporation corp on st.CorpId = corp.CorpId ");
                sb.Append(" left join NFMT_Basic..Asset ass on slog.AssetId = ass.AssetId ");
                sb.Append(" left join NFMT_Basic..Brand brand on slog.BrandId = brand.BrandId ");
                sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = st.DeliverPlaceId ");
                sb.AppendFormat(" left join NFMT_Basic..BDStatusDetail bd on bd.DetailId = st.StockStatus and bd.StatusId = {0} ", (int)NFMT.Common.StatusTypeEnum.库存状态);
                sb.Append(" left join NFMT_Basic..MeasureUnit mu on slog.MUId = mu.MUId ");
                sb.AppendFormat(" where ref.AllotId = {0} and ref.DetailStatus = {1} ", allotId, (int)status);

                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                    result.Message = "获取成功";
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

        public ResultModel InvalidAll(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Fun_CashInStcok_Ref set DetailStatus = {0} where AllotId = {1}", (int)Common.StatusEnum.已作废, allotId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                }
                else
                {
                    result.Message = "作废失败";
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

        #endregion
    }
}
