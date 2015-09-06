/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoApplyDAL.cs
// 文件功能描述：融资头寸赎回申请单dbo.Fin_RepoApply数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Finance.Model;
using NFMT.DBUtility;
using NFMT.Finance.IDAL;
using NFMT.Common;

namespace NFMT.Finance.DAL
{
    /// <summary>
    /// 融资头寸赎回申请单dbo.Fin_RepoApply数据交互类。
    /// </summary>
    public partial class RepoApplyDAL : ExecOperate, IRepoApplyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepoApplyDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringFinance;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            RepoApply fin_repoapply = (RepoApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RepoApplyId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(fin_repoapply.RepoApplyIdNo))
            {
                SqlParameter repoapplyidnopara = new SqlParameter("@RepoApplyIdNo", SqlDbType.VarChar, 20);
                repoapplyidnopara.Value = fin_repoapply.RepoApplyIdNo;
                paras.Add(repoapplyidnopara);
            }

            SqlParameter pledgeapplyidpara = new SqlParameter("@PledgeApplyId", SqlDbType.Int, 4);
            pledgeapplyidpara.Value = fin_repoapply.PledgeApplyId;
            paras.Add(pledgeapplyidpara);

            SqlParameter sumnetamountpara = new SqlParameter("@SumNetAmount", SqlDbType.Decimal, 9);
            sumnetamountpara.Value = fin_repoapply.SumNetAmount;
            paras.Add(sumnetamountpara);

            SqlParameter sumhandspara = new SqlParameter("@SumHands", SqlDbType.Int, 4);
            sumhandspara.Value = fin_repoapply.SumHands;
            paras.Add(sumhandspara);

            SqlParameter repoapplystatuspara = new SqlParameter("@RepoApplyStatus", SqlDbType.Int, 4);
            repoapplystatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(repoapplystatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            RepoApply repoapply = new RepoApply();

            repoapply.RepoApplyId = Convert.ToInt32(dr["RepoApplyId"]);

            if (dr["RepoApplyIdNo"] != DBNull.Value)
            {
                repoapply.RepoApplyIdNo = Convert.ToString(dr["RepoApplyIdNo"]);
            }

            if (dr["PledgeApplyId"] != DBNull.Value)
            {
                repoapply.PledgeApplyId = Convert.ToInt32(dr["PledgeApplyId"]);
            }

            if (dr["SumNetAmount"] != DBNull.Value)
            {
                repoapply.SumNetAmount = Convert.ToDecimal(dr["SumNetAmount"]);
            }

            if (dr["SumHands"] != DBNull.Value)
            {
                repoapply.SumHands = Convert.ToInt32(dr["SumHands"]);
            }

            if (dr["RepoApplyStatus"] != DBNull.Value)
            {
                repoapply.RepoApplyStatus = (Common.StatusEnum)Convert.ToInt32(dr["RepoApplyStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                repoapply.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                repoapply.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                repoapply.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                repoapply.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return repoapply;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            RepoApply repoapply = new RepoApply();

            int indexRepoApplyId = dr.GetOrdinal("RepoApplyId");
            repoapply.RepoApplyId = Convert.ToInt32(dr[indexRepoApplyId]);

            int indexRepoApplyIdNo = dr.GetOrdinal("RepoApplyIdNo");
            if (dr["RepoApplyIdNo"] != DBNull.Value)
            {
                repoapply.RepoApplyIdNo = Convert.ToString(dr[indexRepoApplyIdNo]);
            }

            int indexPledgeApplyId = dr.GetOrdinal("PledgeApplyId");
            if (dr["PledgeApplyId"] != DBNull.Value)
            {
                repoapply.PledgeApplyId = Convert.ToInt32(dr[indexPledgeApplyId]);
            }

            int indexSumNetAmount = dr.GetOrdinal("SumNetAmount");
            if (dr["SumNetAmount"] != DBNull.Value)
            {
                repoapply.SumNetAmount = Convert.ToDecimal(dr[indexSumNetAmount]);
            }

            int indexSumHands = dr.GetOrdinal("SumHands");
            if (dr["SumHands"] != DBNull.Value)
            {
                repoapply.SumHands = Convert.ToInt32(dr[indexSumHands]);
            }

            int indexRepoApplyStatus = dr.GetOrdinal("RepoApplyStatus");
            if (dr["RepoApplyStatus"] != DBNull.Value)
            {
                repoapply.RepoApplyStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRepoApplyStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                repoapply.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                repoapply.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                repoapply.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                repoapply.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return repoapply;
        }

        public override string TableName
        {
            get
            {
                return "Fin_RepoApply";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            RepoApply fin_repoapply = (RepoApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter repoapplyidpara = new SqlParameter("@RepoApplyId", SqlDbType.Int, 4);
            repoapplyidpara.Value = fin_repoapply.RepoApplyId;
            paras.Add(repoapplyidpara);

            if (!string.IsNullOrEmpty(fin_repoapply.RepoApplyIdNo))
            {
                SqlParameter repoapplyidnopara = new SqlParameter("@RepoApplyIdNo", SqlDbType.VarChar, 20);
                repoapplyidnopara.Value = fin_repoapply.RepoApplyIdNo;
                paras.Add(repoapplyidnopara);
            }

            SqlParameter pledgeapplyidpara = new SqlParameter("@PledgeApplyId", SqlDbType.Int, 4);
            pledgeapplyidpara.Value = fin_repoapply.PledgeApplyId;
            paras.Add(pledgeapplyidpara);

            SqlParameter sumnetamountpara = new SqlParameter("@SumNetAmount", SqlDbType.Decimal, 9);
            sumnetamountpara.Value = fin_repoapply.SumNetAmount;
            paras.Add(sumnetamountpara);

            SqlParameter sumhandspara = new SqlParameter("@SumHands", SqlDbType.Int, 4);
            sumhandspara.Value = fin_repoapply.SumHands;
            paras.Add(sumhandspara);

            SqlParameter repoapplystatuspara = new SqlParameter("@RepoApplyStatus", SqlDbType.Int, 4);
            repoapplystatuspara.Value = fin_repoapply.RepoApplyStatus;
            paras.Add(repoapplystatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 重写方法

        public override ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate)
        {
            ResultModel result = new ResultModel();

            bool allow = false;

            switch (operate)
            {
                case OperateEnum.作废:
                    allow = (obj.Status >= StatusEnum.已录入 && obj.Status < StatusEnum.待审核) || obj.Status == StatusEnum.绑定合约;
                    break;
                case OperateEnum.修改:
                    allow = (obj.Status >= StatusEnum.已录入 && obj.Status <= StatusEnum.待审核);
                    break;
                case OperateEnum.提交审核:
                    allow = (obj.Status >= StatusEnum.已录入 && obj.Status < StatusEnum.待审核);
                    break;
                case OperateEnum.撤返:
                    allow = obj.Status == StatusEnum.待审核;
                    break;
                case OperateEnum.冻结:
                    allow = obj.Status == StatusEnum.已生效;
                    break;
                case OperateEnum.解除冻结:
                    allow = obj.Status == StatusEnum.已冻结;
                    break;
                case OperateEnum.执行完成:
                    allow = obj.Status == StatusEnum.已生效;
                    break;
                case OperateEnum.确认完成:
                    allow = obj.Status == StatusEnum.已生效;
                    break;
                case OperateEnum.执行完成撤销:
                    allow = (obj.Status == StatusEnum.已完成 || obj.Status == StatusEnum.部分完成);
                    break;
                case OperateEnum.确认完成撤销:
                    allow = (obj.Status == StatusEnum.已完成 || obj.Status == StatusEnum.部分完成);
                    break;
                case OperateEnum.关闭:
                    allow = obj.Status == StatusEnum.已生效;
                    break;
                default:
                    allow = true;
                    break;
            }

            if (!allow)
            {
                result.ResultStatus = -1;
                result.Message = string.Format("{0}的数据不能进行{1}操作", obj.Status.ToString("F"), operate.ToString("F"));
                return result;
            }

            if (!this.OperateAuthority(user, operate))
            {
                result.ResultStatus = -1;
                result.Message = string.Format("没有当前数据的{0}权限", operate.ToString("F"));
                return result;
            }

            result.ResultStatus = 0;
            return result;
        }

        #endregion

        #region 新增方法

        public ResultModel GetByPledgeApplyId(UserModel user, int repoId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select FinancingBankId from dbo.Fin_PledgeApply where PledgeApplyId = (select PledgeApplyId from dbo.Fin_RepoApply where RepoApplyId = {0})", repoId);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                int i;
                if (obj == null || string.IsNullOrEmpty(obj.ToString()) || !int.TryParse(obj.ToString(), out i) || i <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
                else
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = i;
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
