using NFMT.Common;
using NFMT.DBUtility;
using NFMT.DoPrice.IDAL;
using NFMT.DoPrice.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.DoPrice.DAL
{
    public partial class PricingApplyDAL : ApplyOperate, IPricingApplyDAL
    {
        #region 新增方法

        public ResultModel CheckPledgeCanConfirm(UserModel user, int pricingApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                int entryStatus = (int)Common.StatusEnum.已录入;
                int completeStatus = (int)StatusEnum.已完成;
                int readyStatus = (int)StatusEnum.已生效;
                int closeStatus = (int)StatusEnum.已关闭;

                string cmdText = string.Format("select count(*) from dbo.Pri_Pricing so where so.PricingApplyId =@pricingApplyId and so.PricingStatus >= @entryStatus and so.PricingStatus <@completeStatus");
                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter para = new SqlParameter("@pricingApplyId", pricingApplyId);
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
                    result.Message = "点价申请拥有待执行的点价，不能进行执行完成确认操作。";
                    return result;
                }

                cmdText = string.Format("select count(*) from dbo.Pri_PricingApplyDetail soad left join dbo.Pri_PricingDetail sod on sod.PricingApplyDetailId = soad.DetailId and sod.DetailStatus = @completeStatus where soad.PricingApplyId = @pricingApplyId  and soad.DetailStatus = @readyStatus and sod.DetailId is null");
                paras = new List<SqlParameter>();

                para = new SqlParameter("@pricingApplyId", pricingApplyId);
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
                    result.Message = "点价申请拥有待执行的点价，不能进行执行完成确认操作。";
                    return result;
                }

                cmdText = string.Format("select count(*) from dbo.Pri_PricingApplyDetail sod where sod.DetailStatus =@closeStatus and sod.PricingApplyId=@pricingApplyId");

                paras = new List<SqlParameter>();

                para = new SqlParameter("@pricingApplyId", pricingApplyId);
                paras.Add(para);

                para = new SqlParameter("@closeStatus", closeStatus);
                paras.Add(para);

                obj = SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras.ToArray());
                int closeRows = 0;
                if (obj == null || !int.TryParse(obj.ToString(), out closeRows))
                {
                    result.ResultStatus = -1;
                    result.Message = "点价申请拥有待执行的点价，不能进行执行完成确认操作。";
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

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "更新对象不能为null";
                    return result;
                }

                obj.LastModifyId = user.EmpId;

                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, string.Format("{0}Update", obj.TableName), CreateUpdateParameters(obj).ToArray());

                if (i == 1)
                {
                    result.Message = "更新成功";
                    result.ResultStatus = 0;
                }
                else
                    result.Message = "更新失败";

                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel GetAlreadyPricingWeight(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select ISNULL(SUM(pa.PricingWeight),0) from dbo.Pri_PricingApply pa left join dbo.Apply a on pa.ApplyId = a.ApplyId where pa.SubContractId = {0} and a.ApplyStatus <> {1} ", subId, (int)Common.StatusEnum.已作废);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                decimal alreadyPricingWeight = 0;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && decimal.TryParse(obj.ToString(), out alreadyPricingWeight))
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = alreadyPricingWeight;
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
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel GetModelByApplyId(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                string sql = string.Format("select * from dbo.Pri_PricingApply where ApplyId = {0}", applyId);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, sql, null);

                Model.PricingApply model = new PricingApply();

                if (dr.Read())
                {
                    model = CreateModel(dr) as PricingApply;

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

        public override int MenuId
        {
            get
            {
                return 59;
            }
        }

        public override IAuthority Authority
        {
            get
            {
                return new NFMT.Authority.ContractAuth();
            }
        }

        public ResultModel UpdateDelayInfo(UserModel user, Model.PricingApplyDelay delay)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Pri_PricingApply set DelayAmount = ISNULL(DelayAmount,0) + {0},DelayFee = ISNULL(DelayFee,0) + {1},DelayQPDate = ISNULL('{2}','{3}') where PricingApplyId = {4}", delay.DelayAmount, delay.DelayFee, delay.LastModifyTime, delay.CreateTime, delay.PricingApplyId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "更新成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "更新失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }
            return result;
        }

        public ResultModel UpdateCancelDelayInfo(UserModel user, Model.PricingApplyDelay delay)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Pri_PricingApply set DelayAmount = ISNULL(DelayAmount,0) - {0},DelayFee = ISNULL(DelayFee,0) - {1} where PricingApplyId = {2}", delay.DelayAmount, delay.DelayFee, delay.PricingApplyId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "更新成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "更新失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }
            return result;
        }

        public ResultModel CheckContractSubPricingApplyConfirm(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            string cmdText = "select COUNT(*) from dbo.Pri_PricingApply pa inner join dbo.Apply app on pa.ApplyId = app.ApplyId where pa.SubContractId =@subId and app.ApplyStatus in (@entryStatus,@readyStatus)";

            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@subId", subId);
            paras[1] = new SqlParameter("@entryStatus", (int)NFMT.Common.StatusEnum.已录入);
            paras[1] = new SqlParameter("@readyStatus", (int)NFMT.Common.StatusEnum.已生效);

            object obj = DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras);
            int i = 0;
            if (obj == null || !int.TryParse(obj.ToString(), out i) || i <= 0)
            {
                result.ResultStatus = -1;
                result.Message = "检验点价申请失败";
                return result;
            }
            if (i > 0)
            {
                result.ResultStatus = -1;
                result.Message = "子合约中含有未完成的点价申请，不能进行确认完成操作。";
                return result;
            }

            result.ResultStatus = 0;
            result.Message = "点价申请全部完成";

            return result;
        }

        #endregion
    }
}
