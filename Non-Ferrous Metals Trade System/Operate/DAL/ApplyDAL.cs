/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ApplyDAL.cs
// 文件功能描述：申请dbo.Apply数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月14日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Operate.Model;
using NFMT.DBUtility;
using NFMT.Operate.IDAL;
using NFMT.Common;

namespace NFMT.Operate.DAL
{
    /// <summary>
    /// 申请dbo.Apply数据交互类。
    /// </summary>
    public class ApplyDAL : ApplyOperate, IApplyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ApplyDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringNFMT;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            Apply apply = (Apply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ApplyId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(apply.ApplyNo))
            {
                SqlParameter applynopara = new SqlParameter("@ApplyNo", SqlDbType.VarChar, 20);
                applynopara.Value = apply.ApplyNo;
                paras.Add(applynopara);
            }

            SqlParameter applytypepara = new SqlParameter("@ApplyType", SqlDbType.Int, 4);
            applytypepara.Value = apply.ApplyType;
            paras.Add(applytypepara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = apply.EmpId;
            paras.Add(empidpara);

            SqlParameter applytimepara = new SqlParameter("@ApplyTime", SqlDbType.DateTime, 8);
            applytimepara.Value = apply.ApplyTime;
            paras.Add(applytimepara);

            SqlParameter applystatuspara = new SqlParameter("@ApplyStatus", SqlDbType.Int, 4);
            applystatuspara.Value = (int)apply.ApplyStatus;
            paras.Add(applystatuspara);

            SqlParameter applydeptpara = new SqlParameter("@ApplyDept", SqlDbType.Int, 4);
            applydeptpara.Value = apply.ApplyDept;
            paras.Add(applydeptpara);

            SqlParameter applycorppara = new SqlParameter("@ApplyCorp", SqlDbType.Int, 4);
            applycorppara.Value = apply.ApplyCorp;
            paras.Add(applycorppara);

            if (!string.IsNullOrEmpty(apply.ApplyDesc))
            {
                SqlParameter applydescpara = new SqlParameter("@ApplyDesc", SqlDbType.VarChar, 4000);
                applydescpara.Value = apply.ApplyDesc;
                paras.Add(applydescpara);
            }

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            Apply apply = new Apply();

            apply.ApplyId = Convert.ToInt32(dr["ApplyId"]);

            if (dr["ApplyNo"] != DBNull.Value)
            {
                apply.ApplyNo = Convert.ToString(dr["ApplyNo"]);
            }

            if (dr["ApplyType"] != DBNull.Value)
            {
                apply.ApplyType = (ApplyType)Convert.ToInt32(dr["ApplyType"]);
            }

            if (dr["EmpId"] != DBNull.Value)
            {
                apply.EmpId = Convert.ToInt32(dr["EmpId"]);
            }

            if (dr["ApplyTime"] != DBNull.Value)
            {
                apply.ApplyTime = Convert.ToDateTime(dr["ApplyTime"]);
            }

            if (dr["ApplyStatus"] != DBNull.Value)
            {
                apply.ApplyStatus = (Common.StatusEnum)Convert.ToInt32(dr["ApplyStatus"]);
            }

            if (dr["ApplyDept"] != DBNull.Value)
            {
                apply.ApplyDept = Convert.ToInt32(dr["ApplyDept"]);
            }

            if (dr["ApplyCorp"] != DBNull.Value)
            {
                apply.ApplyCorp = Convert.ToInt32(dr["ApplyCorp"]);
            }

            if (dr["ApplyDesc"] != DBNull.Value)
            {
                apply.ApplyDesc = Convert.ToString(dr["ApplyDesc"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                apply.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                apply.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                apply.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                apply.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return apply;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Apply apply = new Apply();

            int indexApplyId = dr.GetOrdinal("ApplyId");
            apply.ApplyId = Convert.ToInt32(dr[indexApplyId]);

            int indexApplyNo = dr.GetOrdinal("ApplyNo");
            if (dr["ApplyNo"] != DBNull.Value)
            {
                apply.ApplyNo = Convert.ToString(dr[indexApplyNo]);
            }

            int indexApplyType = dr.GetOrdinal("ApplyType");
            if (dr["ApplyType"] != DBNull.Value)
            {
                apply.ApplyType = (ApplyType)Convert.ToInt32(dr[indexApplyType]);
            }

            int indexEmpId = dr.GetOrdinal("EmpId");
            if (dr["EmpId"] != DBNull.Value)
            {
                apply.EmpId = Convert.ToInt32(dr[indexEmpId]);
            }

            int indexApplyTime = dr.GetOrdinal("ApplyTime");
            if (dr["ApplyTime"] != DBNull.Value)
            {
                apply.ApplyTime = Convert.ToDateTime(dr[indexApplyTime]);
            }

            int indexApplyStatus = dr.GetOrdinal("ApplyStatus");
            if (dr["ApplyStatus"] != DBNull.Value)
            {
                apply.ApplyStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexApplyStatus]);
            }

            int indexApplyDept = dr.GetOrdinal("ApplyDept");
            if (dr["ApplyDept"] != DBNull.Value)
            {
                apply.ApplyDept = Convert.ToInt32(dr[indexApplyDept]);
            }

            int indexApplyCorp = dr.GetOrdinal("ApplyCorp");
            if (dr["ApplyCorp"] != DBNull.Value)
            {
                apply.ApplyCorp = Convert.ToInt32(dr[indexApplyCorp]);
            }

            int indexApplyDesc = dr.GetOrdinal("ApplyDesc");
            if (dr["ApplyDesc"] != DBNull.Value)
            {
                apply.ApplyDesc = Convert.ToString(dr[indexApplyDesc]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                apply.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                apply.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                apply.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                apply.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return apply;
        }

        public override string TableName
        {
            get
            {
                return "Apply";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Apply apply = (Apply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = apply.ApplyId;
            paras.Add(applyidpara);

            if (!string.IsNullOrEmpty(apply.ApplyNo))
            {
                SqlParameter applynopara = new SqlParameter("@ApplyNo", SqlDbType.VarChar, 20);
                applynopara.Value = apply.ApplyNo;
                paras.Add(applynopara);
            }

            SqlParameter applytypepara = new SqlParameter("@ApplyType", SqlDbType.Int, 4);
            applytypepara.Value = apply.ApplyType;
            paras.Add(applytypepara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = apply.EmpId;
            paras.Add(empidpara);

            SqlParameter applytimepara = new SqlParameter("@ApplyTime", SqlDbType.DateTime, 8);
            applytimepara.Value = apply.ApplyTime;
            paras.Add(applytimepara);

            SqlParameter applystatuspara = new SqlParameter("@ApplyStatus", SqlDbType.Int, 4);
            applystatuspara.Value = apply.ApplyStatus;
            paras.Add(applystatuspara);

            SqlParameter applydeptpara = new SqlParameter("@ApplyDept", SqlDbType.Int, 4);
            applydeptpara.Value = apply.ApplyDept;
            paras.Add(applydeptpara);

            SqlParameter applycorppara = new SqlParameter("@ApplyCorp", SqlDbType.Int, 4);
            applycorppara.Value = apply.ApplyCorp;
            paras.Add(applycorppara);

            if (!string.IsNullOrEmpty(apply.ApplyDesc))
            {
                SqlParameter applydescpara = new SqlParameter("@ApplyDesc", SqlDbType.VarChar, 4000);
                applydescpara.Value = apply.ApplyDesc;
                paras.Add(applydescpara);
            }

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetCustomerBala(UserModel user, int customCorpId, int subContractId, int applyId,decimal payApplyAmount,decimal stockOutNetWeight)
        {
            ResultModel result = new ResultModel();

            if (customCorpId <= 0 || subContractId <= 0 || applyId <= 0)
            {
                result.ResultStatus = -1;
                result.Message = "数据错误";
                return result;
            }

            try
            {
                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter paraCustomCorpId = new SqlParameter("@CustomCorpId", SqlDbType.Int, 4);
                paraCustomCorpId.Value = customCorpId;
                paras.Add(paraCustomCorpId);

                SqlParameter paraSubContractId = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
                paraSubContractId.Value = subContractId;
                paras.Add(paraSubContractId);

                SqlParameter paraApplyId = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
                paraApplyId.Value = applyId;
                paras.Add(paraApplyId);

                SqlParameter paraPayApplyAmount = new SqlParameter("@payApplyAmount", SqlDbType.Decimal, 18);
                paraPayApplyAmount.Value = payApplyAmount;
                paras.Add(paraPayApplyAmount);

                SqlParameter paraStockOutNetWeight = new SqlParameter("@stockOutNetWeight", SqlDbType.Decimal, 18);
                paraStockOutNetWeight.Value = stockOutNetWeight;
                paras.Add(paraStockOutNetWeight);

                SqlParameter paraAmout = new SqlParameter();
                paraAmout.Direction = ParameterDirection.Output;
                paraAmout.SqlDbType = SqlDbType.Money;
                paraAmout.ParameterName = "@Amout";
                paraAmout.Size = 4;
                paras.Add(paraAmout);

                SqlParameter paraCurrencyName = new SqlParameter();
                paraCurrencyName.Direction = ParameterDirection.Output;
                paraCurrencyName.SqlDbType = SqlDbType.VarChar;
                paraCurrencyName.ParameterName = "@CurrencyName";
                paraCurrencyName.Size = 50;
                paras.Add(paraCurrencyName);

                //int i = SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.StoredProcedure, "dbo.CustomBala", paras.ToArray());
                object obj = SqlHelper.ExecuteScalar(this.ConnectString, CommandType.StoredProcedure, "dbo.CustomBala", paras.ToArray());

                if (obj != null)
                {
                    result.Message = "获取余额成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = paraAmout.Value.ToString() + "_" + paraCurrencyName.Value.ToString();
                }
                else
                {
                    result.Message = "获取余额失败";
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

        #endregion
    }
}
