using NFMT.Common;
using NFMT.DBUtility;
using NFMT.WareHouse.IDAL;
using NFMT.WareHouse.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.WareHouse.DAL
{
    public partial class StockOutApplyDAL : ApplyOperate, IStockOutApplyDAL
    {
        #region 新增方法

        public ResultModel GetByApplyId(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            if (applyId < 1)
            {
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@applyId", SqlDbType.Int, 4);
            para.Value = applyId;
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                string cmdText = "select * from dbo.St_StockOutApply where ApplyId=@applyId";
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, paras.ToArray());

                StockOutApply stockoutapply = new StockOutApply();

                if (dr.Read())
                {
                    stockoutapply = this.CreateModel<StockOutApply>(dr);

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = stockoutapply;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        public ResultModel CheckStockOutCanConfirm(UserModel user, int stockOutApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                int entryStatus = (int)Common.StatusEnum.已录入;
                int completeStatus = (int)StatusEnum.已完成;
                int readyStatus = (int)StatusEnum.已生效;
                int closeStatus = (int)StatusEnum.已关闭;

                string cmdText = string.Format("select count(*) from dbo.St_StockOut so where so.StockOutApplyId =@stockOutApplyId and so.StockOutStatus >= @entryStatus and so.StockOutStatus <@completeStatus");
                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter para = new SqlParameter("@stockOutApplyId", stockOutApplyId);
                paras.Add(para);

                para = new SqlParameter("@entryStatus", entryStatus);
                paras.Add(para);

                para = new SqlParameter("@completeStatus", completeStatus);
                paras.Add(para);

                object obj = SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras.ToArray());
                int otherStatusRows = 0;
                if (obj == null || !int.TryParse(obj.ToString(), out otherStatusRows) || otherStatusRows != 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "出库申请拥有待执行的出库，不能进行执行完成确认操作。";
                    return result;
                }

                cmdText = string.Format("select count(*) from dbo.St_StockOutApplyDetail soad left join dbo.St_StockOutDetail sod on sod.StockOutApplyDetailId = soad.DetailId and sod.DetailStatus = @completeStatus where soad.StockOutApplyId = @stockOutApplyId  and soad.DetailStatus = @readyStatus and sod.DetailId is null");
                paras = new List<SqlParameter>();

                para = new SqlParameter("@stockOutApplyId", stockOutApplyId);
                paras.Add(para);

                para = new SqlParameter("@completeStatus", completeStatus);
                paras.Add(para);

                para = new SqlParameter("@readyStatus", readyStatus);
                paras.Add(para);

                obj = SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras.ToArray());
                int notCompleteRows = 0;
                if (obj == null || !int.TryParse(obj.ToString(), out notCompleteRows) || notCompleteRows != 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "出库申请拥有待执行的出库，不能进行执行完成确认操作。";
                    return result;
                }

                cmdText = string.Format("select count(*) from dbo.St_StockOutApplyDetail sod where sod.DetailStatus =@closeStatus and sod.StockOutApplyId=@stockOutApplyId");

                paras = new List<SqlParameter>();

                para = new SqlParameter("@stockOutApplyId", stockOutApplyId);
                paras.Add(para);

                para = new SqlParameter("@closeStatus", closeStatus);
                paras.Add(para);

                obj = SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras.ToArray());
                int closeRows = 0;
                if (obj == null || !int.TryParse(obj.ToString(), out closeRows))
                {
                    result.ResultStatus = -1;
                    result.Message = "出库申请拥有待执行的出库，不能进行执行完成确认操作。";
                    return result;
                }

                result.ResultStatus = 0;
                if (closeRows == 0)
                    result.ReturnValue = StatusEnum.已完成;
                else
                    result.ReturnValue = StatusEnum.部分完成;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel GetCondition(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select * ");
                sb.Append(" from dbo.St_StockOutApply soa with (nolock) ");
                sb.Append(" left join dbo.Apply a with (nolock) on soa.ApplyId = a.ApplyId ");
                sb.AppendFormat(" where soa.ApplyId = {0}", applyId);

                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, sb.ToString(), null, CommandType.Text);

                if (dt != null && dt.Rows.Count > 0)
                {
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.ResultStatus = -1;
                }
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
                return 43;
            }
        }

        public ResultModel LoadBySubId(UserModel user, int subId)
        {
            string cmdText = string.Format("select * from dbo.St_StockOutApply where SubContractId = {0}",subId);

            return Load<Model.StockOutApply>(user, CommandType.Text, cmdText);
        }

        public ResultModel CheckContractSubStockOutApplyConfirm(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            string cmdText = "select COUNT(*) from dbo.St_StockOutApply soa inner join dbo.Apply app on app.ApplyId = soa.ApplyId where soa.SubContractId=@subId and app.ApplyStatus in  (@entryStatus,@readyStatus)";

            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@subId", subId);
            paras[1] = new SqlParameter("@entryStatus", (int)NFMT.Common.StatusEnum.已录入);
            paras[1] = new SqlParameter("@readyStatus", (int)NFMT.Common.StatusEnum.已生效);

            object obj = DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras);
            int i = 0;
            if (obj == null || !int.TryParse(obj.ToString(), out i) || i <= 0)
            {
                result.ResultStatus = -1;
                result.Message = "检验出库申请失败";
                return result;
            }
            if (i > 0)
            {
                result.ResultStatus = -1;
                result.Message = "子合约中含有未完成的出库申请，不能进行确认完成操作。";
                return result;
            }

            result.ResultStatus = 0;
            result.Message = "出库申请全部完成";

            return result;
        }

        #endregion
    }
}
