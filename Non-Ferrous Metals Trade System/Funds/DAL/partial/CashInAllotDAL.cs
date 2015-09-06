using NFMT.Common;
using NFMT.DBUtility;
using NFMT.Funds.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Funds.DAL
{
    public partial class CashInAllotDAL : ExecOperate, ICashInAllotDAL
    {
        #region 新增方法

        public ResultModel GetLastBalaByAllotId(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string cmdText = "select ISNULL(cia.AllotBala,0)-ISNULL(ref.SumBala,0) as LastBala from dbo.Fun_CashInAllot cia inner join (select SUM(AllotBala) as SumBala,AllotId from dbo.Fun_CashInStcok_Ref where AllotId = @allotId and DetailStatus = @status group by AllotId) as ref on ref.AllotId = cia.AllotId";

                SqlParameter[] paras = new SqlParameter[2];

                paras[0] = new SqlParameter("@allotId", allotId);
                paras[1] = new SqlParameter("@status", (int)StatusEnum.已生效);

                object obj = SqlHelper.ExecuteScalar(ConnectString, CommandType.Text, cmdText, paras);

                result.ResultStatus = 0;
                result.ReturnValue = obj;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public override int MenuId
        {
            get
            {
                return 122;
            }
        }

        public ResultModel GetContractStock(UserModel user, int subContractId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(" select slog.StockLogId,slog.StockId,slog.StockNameId,slog.RefNo,corp.CorpName,ass.AssetName,brand.BrandName,bd.StatusName,mu.MUName,slog.NetAmount+ISNULL(slog.GapAmount,0) NetGapAmount,0 as AllotAmount ");
                sb.Append(" ,dp.DPName,slog.CardNo ");
                sb.Append(" from NFMT..St_StockLog slog ");
                sb.Append(" left join NFMT..St_Stock st on slog.StockId = st.StockId ");
                sb.Append(" left join NFMT_User..Corporation corp on st.CorpId = corp.CorpId ");
                sb.Append(" left join NFMT_Basic..Asset ass on slog.AssetId = ass.AssetId ");
                sb.Append(" left join NFMT_Basic..Brand brand on slog.BrandId = brand.BrandId ");
                sb.AppendFormat(" left join NFMT_Basic..BDStatusDetail bd on bd.DetailId = st.StockStatus and bd.StatusId = {0} ", (int)NFMT.Common.StatusTypeEnum.库存状态);
                sb.Append(" left join NFMT_Basic..MeasureUnit mu on slog.MUId = mu.MUId ");
                sb.Append(" left join NFMT_Basic..DeliverPlace dp on dp.DPId = slog.DeliverPlaceId ");
                if (subContractId > 0)
                    sb.AppendFormat(" where slog.SubContractId = {0} ", subContractId);
                else
                    sb.Append(" where 1=2 ");

                System.Data.DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, CommandType.Text);
                if (dt == null || dt.Rows.Count < 1)
                {
                    result.ResultStatus = 0;
                    result.Message = "获取失败或无数据";
                }
                else
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = dt;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel GetCanAllotBala(UserModel user, int cashInId, bool isUpdate, decimal alreadyAllotBala = 0)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(" select ci.CashInBala - ISNULL(ref.bala,0) ");
                if (isUpdate)
                    sb.AppendFormat(" + {0} ", alreadyAllotBala);
                sb.AppendFormat(" from dbo.Fun_CashIn ci left join (select ref.CashInId,SUM(ISNULL(ref.AllotBala,0)) bala  from dbo.Fun_CashInCorp_Ref ref where ref.DetailStatus >={0}  group by ref.CashInId) ref on ci.CashInId = ref.CashInId where ci.CashInId = {1}", (int)Common.StatusEnum.已生效,cashInId);

                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sb.ToString(), null);
                decimal canAllotBala = 0;
                if (obj == null || string.IsNullOrEmpty(obj.ToString()) || !decimal.TryParse(obj.ToString(), out canAllotBala))
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
                else
                {
                    result.ReturnValue = canAllotBala;
                    result.Message = "获取成功";
                    result.ResultStatus = 0;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel LoadAllotBySubId(UserModel user, int subId)
        {
            string cmdText = string.Format("select cia.* from dbo.Fun_CashInAllot cia inner join dbo.Fun_CashInContract_Ref cicr on cia.AllotId = cicr.AllotId where cicr.SubContractId ={0} and cia.AllotStatus >={1} and cicr.DetailStatus>={1}",subId,(int)StatusEnum.已生效);
            ResultModel result = this.Load<Model.CashInAllot>(user, System.Data.CommandType.Text, cmdText);

            return result;
        }

        #endregion
    }
}
