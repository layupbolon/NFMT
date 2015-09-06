/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BankAccountDAL.cs
// 文件功能描述：银行账号dbo.BankAccount数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Data.Model;
using NFMT.DBUtility;
using NFMT.Data.IDAL;
using NFMT.Common;

namespace NFMT.Data.DAL
{
    /// <summary>
    /// 银行账号dbo.BankAccount数据交互类。
    /// </summary>
    public class BankAccountDAL : DataOperate, IBankAccountDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BankAccountDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringBasic;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            BankAccount bankaccount = (BankAccount)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@BankAccId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter companyidpara = new SqlParameter("@CompanyId", SqlDbType.Int, 4);
            companyidpara.Value = bankaccount.CompanyId;
            paras.Add(companyidpara);

            SqlParameter bankidpara = new SqlParameter("@BankId", SqlDbType.Int, 4);
            bankidpara.Value = bankaccount.BankId;
            paras.Add(bankidpara);

            SqlParameter accountnopara = new SqlParameter("@AccountNo", SqlDbType.VarChar, 80);
            accountnopara.Value = bankaccount.AccountNo;
            paras.Add(accountnopara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = bankaccount.CurrencyId;
            paras.Add(currencyidpara);

            if (!string.IsNullOrEmpty(bankaccount.BankAccDesc))
            {
                SqlParameter bankaccdescpara = new SqlParameter("@BankAccDesc", SqlDbType.VarChar, 400);
                bankaccdescpara.Value = bankaccount.BankAccDesc;
                paras.Add(bankaccdescpara);
            }

            SqlParameter bankaccstatuspara = new SqlParameter("@BankAccStatus", SqlDbType.Int, 4);
            bankaccstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(bankaccstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            BankAccount bankaccount = new BankAccount();

            int indexBankAccId = dr.GetOrdinal("BankAccId");
            bankaccount.BankAccId = Convert.ToInt32(dr[indexBankAccId]);

            int indexCompanyId = dr.GetOrdinal("CompanyId");
            bankaccount.CompanyId = Convert.ToInt32(dr[indexCompanyId]);

            int indexBankId = dr.GetOrdinal("BankId");
            bankaccount.BankId = Convert.ToInt32(dr[indexBankId]);

            int indexAccountNo = dr.GetOrdinal("AccountNo");
            bankaccount.AccountNo = Convert.ToString(dr[indexAccountNo]);

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            bankaccount.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);

            int indexBankAccDesc = dr.GetOrdinal("BankAccDesc");
            if (dr["BankAccDesc"] != DBNull.Value)
            {
                bankaccount.BankAccDesc = Convert.ToString(dr[indexBankAccDesc]);
            }

            int indexBankAccStatus = dr.GetOrdinal("BankAccStatus");
            if (dr["BankAccStatus"] != DBNull.Value)
            {
                bankaccount.BankAccStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexBankAccStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            bankaccount.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            bankaccount.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                bankaccount.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                bankaccount.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return bankaccount;
        }

        public override string TableName
        {
            get
            {
                return "BankAccount";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            BankAccount bankaccount = (BankAccount)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter bankaccidpara = new SqlParameter("@BankAccId", SqlDbType.Int, 4);
            bankaccidpara.Value = bankaccount.BankAccId;
            paras.Add(bankaccidpara);

            SqlParameter companyidpara = new SqlParameter("@CompanyId", SqlDbType.Int, 4);
            companyidpara.Value = bankaccount.CompanyId;
            paras.Add(companyidpara);

            SqlParameter bankidpara = new SqlParameter("@BankId", SqlDbType.Int, 4);
            bankidpara.Value = bankaccount.BankId;
            paras.Add(bankidpara);

            SqlParameter accountnopara = new SqlParameter("@AccountNo", SqlDbType.VarChar, 80);
            accountnopara.Value = bankaccount.AccountNo;
            paras.Add(accountnopara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = bankaccount.CurrencyId;
            paras.Add(currencyidpara);

            if (!string.IsNullOrEmpty(bankaccount.BankAccDesc))
            {
                SqlParameter bankaccdescpara = new SqlParameter("@BankAccDesc", SqlDbType.VarChar, 400);
                bankaccdescpara.Value = bankaccount.BankAccDesc;
                paras.Add(bankaccdescpara);
            }

            SqlParameter bankaccstatuspara = new SqlParameter("@BankAccStatus", SqlDbType.Int, 4);
            bankaccstatuspara.Value = bankaccount.BankAccStatus;
            paras.Add(bankaccstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 重载方法

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

        public override int MenuId
        {
            get
            {
                return 28;
            }
        }

        #endregion

        #region 新增方法

        public ResultModel InsertOrUpdateBankAccountInfo(UserModel user, string bankName, string bankAccountName,int corpId)
        {
            ResultModel result = new ResultModel();

            try
            {
                List<SqlParameter> paras = new List<SqlParameter>();
                SqlParameter sqlParaBankAccId = new SqlParameter();
                sqlParaBankAccId.Direction = ParameterDirection.Output;
                sqlParaBankAccId.SqlDbType = SqlDbType.Int;
                sqlParaBankAccId.ParameterName = "@bankAccountId";
                sqlParaBankAccId.Size = 4;

                paras.Add(sqlParaBankAccId);

                SqlParameter sqlParaBankId = new SqlParameter();
                sqlParaBankId.Direction = ParameterDirection.Output;
                sqlParaBankId.SqlDbType = SqlDbType.Int;
                sqlParaBankId.ParameterName = "@bankId";
                sqlParaBankId.Size = 4;

                paras.Add(sqlParaBankId);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendFormat("if not exists(select 1 from dbo.BankAccount ba left join dbo.Bank b on ba.BankId = b.BankId where ba.AccountNo = '{0}' and b.BankName = '{1}')", bankAccountName, bankName);
                sb.Append(" begin ");
                //sb.Append(" declare @bankId int,@bankAccountId int ");
                //sb.Append(" set @bankId = 0 ");
                sb.AppendFormat(" if not exists(select 1 from dbo.Bank where BankName = '{0}') ", bankName);
                sb.Append(" begin ");
                sb.Append(" INSERT INTO [NFMT_Basic].[dbo].[Bank]([BankName],[BankEname],[BankFullName],[BankShort],[CapitalType],[BankLevel],[ParentId],[BankStatus],[CreatorId],[CreateTime],[LastModifyId],[LastModifyTime]) ");
                sb.AppendFormat(" VALUES('{0}','{0}','{0}','{0}',73,1,0,{1},1,GETDATE(),1,GETDATE())", bankName, (int)Common.StatusEnum.已生效);
                sb.Append(" select @bankId = @@IDENTITY ");
                sb.Append(" end ");
                sb.AppendFormat(" select @bankId = BankId from dbo.Bank where BankName = '{0}' ", bankName);
                sb.Append(" INSERT INTO [NFMT_Basic].[dbo].[BankAccount]([CompanyId],[BankId],[AccountNo],[CurrencyId],[BankAccDesc],[BankAccStatus],[CreatorId],[CreateTime],[LastModifyId],[LastModifyTime]) ");
                sb.AppendFormat(" VALUES('{0}',@bankId,'{1}',6,'',{2},1,GETDATE(),1,GETDATE()) ", corpId, bankAccountName, (int)Common.StatusEnum.已生效);
                sb.Append(" select @bankAccountId = @@IDENTITY ");
                sb.Append(" end ");
                sb.Append(" else begin ");
                sb.AppendFormat(" select @bankAccountId = ba.BankAccId,@bankId = b.BankId from dbo.BankAccount ba left join dbo.Bank b on ba.BankId = b.BankId where ba.AccountNo = '{0}' and b.BankName = '{1}' ", bankAccountName, bankName);
                sb.Append(" end ");

                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sb.ToString(), paras.ToArray());

                result.ReturnValue = string.Format("{0},{1}", sqlParaBankId.Value, sqlParaBankAccId.Value);
                result.ResultStatus = 0;
                result.Message = "添加成功";
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        #endregion
    }
}
