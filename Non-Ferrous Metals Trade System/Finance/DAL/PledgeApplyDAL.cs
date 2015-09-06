/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeApplyDAL.cs
// 文件功能描述：融资头寸质押申请单dbo.Fin_PledgeApply数据交互类。
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
    /// 融资头寸质押申请单dbo.Fin_PledgeApply数据交互类。
    /// </summary>
    public partial class PledgeApplyDAL : ExecOperate, IPledgeApplyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PledgeApplyDAL()
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
            PledgeApply fin_pledgeapply = (PledgeApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@PledgeApplyId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(fin_pledgeapply.PledgeApplyNo))
            {
                SqlParameter pledgeapplynopara = new SqlParameter("@PledgeApplyNo", SqlDbType.VarChar, 20);
                pledgeapplynopara.Value = fin_pledgeapply.PledgeApplyNo;
                paras.Add(pledgeapplynopara);
            }

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = fin_pledgeapply.DeptId;
            paras.Add(deptidpara);

            SqlParameter applytimepara = new SqlParameter("@ApplyTime", SqlDbType.DateTime, 8);
            applytimepara.Value = fin_pledgeapply.ApplyTime;
            paras.Add(applytimepara);

            SqlParameter financingbankidpara = new SqlParameter("@FinancingBankId", SqlDbType.Int, 4);
            financingbankidpara.Value = fin_pledgeapply.FinancingBankId;
            paras.Add(financingbankidpara);

            SqlParameter financingaccountidpara = new SqlParameter("@FinancingAccountId", SqlDbType.Int, 4);
            financingaccountidpara.Value = fin_pledgeapply.FinancingAccountId;
            paras.Add(financingaccountidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = fin_pledgeapply.AssetId;
            paras.Add(assetidpara);

            SqlParameter switchbackpara = new SqlParameter("@SwitchBack", SqlDbType.Bit, 1);
            switchbackpara.Value = fin_pledgeapply.SwitchBack;
            paras.Add(switchbackpara);

            SqlParameter exchangeidpara = new SqlParameter("@ExchangeId", SqlDbType.Int, 4);
            exchangeidpara.Value = fin_pledgeapply.ExchangeId;
            paras.Add(exchangeidpara);

            SqlParameter sumnetamountpara = new SqlParameter("@SumNetAmount", SqlDbType.Decimal, 9);
            sumnetamountpara.Value = fin_pledgeapply.SumNetAmount;
            paras.Add(sumnetamountpara);

            SqlParameter sumhandspara = new SqlParameter("@SumHands", SqlDbType.Int, 4);
            sumhandspara.Value = fin_pledgeapply.SumHands;
            paras.Add(sumhandspara);

            SqlParameter pledgeapplystatuspara = new SqlParameter("@PledgeApplyStatus", SqlDbType.Int, 4);
            pledgeapplystatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(pledgeapplystatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            PledgeApply pledgeapply = new PledgeApply();

            pledgeapply.PledgeApplyId = Convert.ToInt32(dr["PledgeApplyId"]);

            if (dr["PledgeApplyNo"] != DBNull.Value)
            {
                pledgeapply.PledgeApplyNo = Convert.ToString(dr["PledgeApplyNo"]);
            }

            if (dr["DeptId"] != DBNull.Value)
            {
                pledgeapply.DeptId = Convert.ToInt32(dr["DeptId"]);
            }

            if (dr["ApplyTime"] != DBNull.Value)
            {
                pledgeapply.ApplyTime = Convert.ToDateTime(dr["ApplyTime"]);
            }

            if (dr["FinancingBankId"] != DBNull.Value)
            {
                pledgeapply.FinancingBankId = Convert.ToInt32(dr["FinancingBankId"]);
            }

            if (dr["FinancingAccountId"] != DBNull.Value)
            {
                pledgeapply.FinancingAccountId = Convert.ToInt32(dr["FinancingAccountId"]);
            }

            if (dr["AssetId"] != DBNull.Value)
            {
                pledgeapply.AssetId = Convert.ToInt32(dr["AssetId"]);
            }

            if (dr["SwitchBack"] != DBNull.Value)
            {
                pledgeapply.SwitchBack = Convert.ToBoolean(dr["SwitchBack"]);
            }

            if (dr["ExchangeId"] != DBNull.Value)
            {
                pledgeapply.ExchangeId = Convert.ToInt32(dr["ExchangeId"]);
            }

            if (dr["SumNetAmount"] != DBNull.Value)
            {
                pledgeapply.SumNetAmount = Convert.ToDecimal(dr["SumNetAmount"]);
            }

            if (dr["SumHands"] != DBNull.Value)
            {
                pledgeapply.SumHands = Convert.ToInt32(dr["SumHands"]);
            }

            if (dr["PledgeApplyStatus"] != DBNull.Value)
            {
                pledgeapply.PledgeApplyStatus = (Common.StatusEnum)Convert.ToInt32(dr["PledgeApplyStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                pledgeapply.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                pledgeapply.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                pledgeapply.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pledgeapply.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return pledgeapply;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PledgeApply pledgeapply = new PledgeApply();

            int indexPledgeApplyId = dr.GetOrdinal("PledgeApplyId");
            pledgeapply.PledgeApplyId = Convert.ToInt32(dr[indexPledgeApplyId]);

            int indexPledgeApplyNo = dr.GetOrdinal("PledgeApplyNo");
            if (dr["PledgeApplyNo"] != DBNull.Value)
            {
                pledgeapply.PledgeApplyNo = Convert.ToString(dr[indexPledgeApplyNo]);
            }

            int indexDeptId = dr.GetOrdinal("DeptId");
            if (dr["DeptId"] != DBNull.Value)
            {
                pledgeapply.DeptId = Convert.ToInt32(dr[indexDeptId]);
            }

            int indexApplyTime = dr.GetOrdinal("ApplyTime");
            if (dr["ApplyTime"] != DBNull.Value)
            {
                pledgeapply.ApplyTime = Convert.ToDateTime(dr[indexApplyTime]);
            }

            int indexFinancingBankId = dr.GetOrdinal("FinancingBankId");
            if (dr["FinancingBankId"] != DBNull.Value)
            {
                pledgeapply.FinancingBankId = Convert.ToInt32(dr[indexFinancingBankId]);
            }

            int indexFinancingAccountId = dr.GetOrdinal("FinancingAccountId");
            if (dr["FinancingAccountId"] != DBNull.Value)
            {
                pledgeapply.FinancingAccountId = Convert.ToInt32(dr[indexFinancingAccountId]);
            }

            int indexAssetId = dr.GetOrdinal("AssetId");
            if (dr["AssetId"] != DBNull.Value)
            {
                pledgeapply.AssetId = Convert.ToInt32(dr[indexAssetId]);
            }

            int indexSwitchBack = dr.GetOrdinal("SwitchBack");
            if (dr["SwitchBack"] != DBNull.Value)
            {
                pledgeapply.SwitchBack = Convert.ToBoolean(dr[indexSwitchBack]);
            }

            int indexExchangeId = dr.GetOrdinal("ExchangeId");
            if (dr["ExchangeId"] != DBNull.Value)
            {
                pledgeapply.ExchangeId = Convert.ToInt32(dr[indexExchangeId]);
            }

            int indexSumNetAmount = dr.GetOrdinal("SumNetAmount");
            if (dr["SumNetAmount"] != DBNull.Value)
            {
                pledgeapply.SumNetAmount = Convert.ToDecimal(dr[indexSumNetAmount]);
            }

            int indexSumHands = dr.GetOrdinal("SumHands");
            if (dr["SumHands"] != DBNull.Value)
            {
                pledgeapply.SumHands = Convert.ToInt32(dr[indexSumHands]);
            }

            int indexPledgeApplyStatus = dr.GetOrdinal("PledgeApplyStatus");
            if (dr["PledgeApplyStatus"] != DBNull.Value)
            {
                pledgeapply.PledgeApplyStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexPledgeApplyStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                pledgeapply.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                pledgeapply.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                pledgeapply.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pledgeapply.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return pledgeapply;
        }

        public override string TableName
        {
            get
            {
                return "Fin_PledgeApply";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PledgeApply fin_pledgeapply = (PledgeApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter pledgeapplyidpara = new SqlParameter("@PledgeApplyId", SqlDbType.Int, 4);
            pledgeapplyidpara.Value = fin_pledgeapply.PledgeApplyId;
            paras.Add(pledgeapplyidpara);

            if (!string.IsNullOrEmpty(fin_pledgeapply.PledgeApplyNo))
            {
                SqlParameter pledgeapplynopara = new SqlParameter("@PledgeApplyNo", SqlDbType.VarChar, 20);
                pledgeapplynopara.Value = fin_pledgeapply.PledgeApplyNo;
                paras.Add(pledgeapplynopara);
            }

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = fin_pledgeapply.DeptId;
            paras.Add(deptidpara);

            SqlParameter applytimepara = new SqlParameter("@ApplyTime", SqlDbType.DateTime, 8);
            applytimepara.Value = fin_pledgeapply.ApplyTime;
            paras.Add(applytimepara);

            SqlParameter financingbankidpara = new SqlParameter("@FinancingBankId", SqlDbType.Int, 4);
            financingbankidpara.Value = fin_pledgeapply.FinancingBankId;
            paras.Add(financingbankidpara);

            SqlParameter financingaccountidpara = new SqlParameter("@FinancingAccountId", SqlDbType.Int, 4);
            financingaccountidpara.Value = fin_pledgeapply.FinancingAccountId;
            paras.Add(financingaccountidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = fin_pledgeapply.AssetId;
            paras.Add(assetidpara);

            SqlParameter switchbackpara = new SqlParameter("@SwitchBack", SqlDbType.Bit, 1);
            switchbackpara.Value = fin_pledgeapply.SwitchBack;
            paras.Add(switchbackpara);

            SqlParameter exchangeidpara = new SqlParameter("@ExchangeId", SqlDbType.Int, 4);
            exchangeidpara.Value = fin_pledgeapply.ExchangeId;
            paras.Add(exchangeidpara);

            SqlParameter sumnetamountpara = new SqlParameter("@SumNetAmount", SqlDbType.Decimal, 9);
            sumnetamountpara.Value = fin_pledgeapply.SumNetAmount;
            paras.Add(sumnetamountpara);

            SqlParameter sumhandspara = new SqlParameter("@SumHands", SqlDbType.Int, 4);
            sumhandspara.Value = fin_pledgeapply.SumHands;
            paras.Add(sumhandspara);

            SqlParameter pledgeapplystatuspara = new SqlParameter("@PledgeApplyStatus", SqlDbType.Int, 4);
            pledgeapplystatuspara.Value = fin_pledgeapply.PledgeApplyStatus;
            paras.Add(pledgeapplystatuspara);

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
    }
}
