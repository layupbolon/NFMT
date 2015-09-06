using NFMT.Common;
using NFMT.Contract.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Contract.DAL
{
    public partial class ContractSubDAL : ExecOperate, IContractSubDAL
    {
        #region 新增方法

        public ResultModel GetContractOutCorp(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            try
            {
                int readyStatus = (int)StatusEnum.已生效;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select corpdetail.CorpId,corpdetail.CorpName ");
                sb.Append("from dbo.Con_ContractSub sub  ");
                sb.AppendFormat("inner join dbo.Con_ContractCorporationDetail corpdetail on sub.ContractId = corpdetail.ContractId and corpdetail.DetailStatus>={0} ", readyStatus);
                sb.AppendFormat("and corpdetail.IsInnerCorp = 0 where sub.SubId = {0} order by corpdetail.IsDefaultCorp desc", subId);
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

        public ResultModel GetStockInWeightBySubId(UserModel user, int sudId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select ref.ContractSubId,st.GrossAmount ");
                sb.Append(" from dbo.St_StockIn st right join dbo.St_ContractStockIn_Ref ref on ref.StockInId = st.StockInId ");
                sb.AppendFormat(" where st.StockInStatus = {0} and ref.ContractSubId = {1} ", (int)Common.StatusEnum.已完成, sudId);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var items = from row in dt.AsEnumerable()
                                group row by row.Field<int>("ContractSubId") into p
                                select new
                                {
                                    SudId = p.Key,
                                    SumWeight = p.Sum(a => a.Field<decimal>("GrossAmount"))
                                };
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = items.Where(a => a.SudId == sudId).Select(a => a.SumWeight).SingleOrDefault();
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

        public ResultModel Load(UserModel user, int contractId, NFMT.Common.StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select * from dbo.Con_ContractSub where ContractId = {0} and SubStatus >={1}", contractId, (int)status);

                result = Load<Model.ContractSub>(user, CommandType.Text, cmdText);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public override IAuthority Authority
        {
            get
            {
                return new NFMT.Authority.ContractAuth();
            }
        }

        public override int MenuId
        {
            get
            {
                return 79;
            }
        }

        public ResultModel GetSubByContractId(UserModel user, int contractId)
        {
            string cmdText = string.Format("select * from dbo.Con_ContractSub where ContractId = {0}",contractId);
            return Get(user, CommandType.Text, cmdText, null);
        }

        #endregion
    }
}
