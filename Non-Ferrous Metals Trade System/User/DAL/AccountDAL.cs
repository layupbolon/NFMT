/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AccountDAL.cs
// 文件功能描述：账户表dbo.Account数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月26日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.User.Model;
using NFMT.DBUtility;
using NFMT.User.IDAL;
using NFMT.Common;

namespace NFMT.User.DAL
{
    /// <summary>
    /// 账户表dbo.Account数据交互类。
    /// </summary>
    public class AccountDAL : DataOperate, IAccountDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringUser;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            Account account = (Account)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@AccId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter accountnamepara = new SqlParameter("@AccountName", SqlDbType.VarChar, 20);
            accountnamepara.Value = account.AccountName;
            paras.Add(accountnamepara);

            SqlParameter passwordpara = new SqlParameter("@PassWord", SqlDbType.VarChar, 20);
            passwordpara.Value = account.PassWord;
            paras.Add(passwordpara);

            SqlParameter accstatuspara = new SqlParameter("@AccStatus", SqlDbType.Int, 4);
            accstatuspara.Value = account.AccStatus;
            paras.Add(accstatuspara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = account.EmpId;
            paras.Add(empidpara);

            SqlParameter isvalidpara = new SqlParameter("@IsValid", SqlDbType.Bit, 1);
            isvalidpara.Value = account.IsValid;
            paras.Add(isvalidpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Account account = new Account();

            int indexAccId = dr.GetOrdinal("AccId");
            account.AccId = Convert.ToInt32(dr[indexAccId]);

            int indexAccountName = dr.GetOrdinal("AccountName");
            account.AccountName = Convert.ToString(dr[indexAccountName]);

            int indexPassWord = dr.GetOrdinal("PassWord");
            account.PassWord = Convert.ToString(dr[indexPassWord]);

            int indexAccStatus = dr.GetOrdinal("AccStatus");
            if (dr["AccStatus"] != DBNull.Value)
            {
                account.AccStatus = (StatusEnum)Convert.ToInt32(dr[indexAccStatus]);
            }

            int indexEmpId = dr.GetOrdinal("EmpId");
            account.EmpId = Convert.ToInt32(dr[indexEmpId]);

            int indexIsValid = dr.GetOrdinal("IsValid");
            account.IsValid = Convert.ToBoolean(dr[indexIsValid]);

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            account.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            account.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                account.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                account.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return account;
        }

        public override string TableName
        {
            get
            {
                return "Account";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Account account = (Account)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter accidpara = new SqlParameter("@AccId", SqlDbType.Int, 4);
            accidpara.Value = account.AccId;
            paras.Add(accidpara);

            SqlParameter accountnamepara = new SqlParameter("@AccountName", SqlDbType.VarChar, 20);
            accountnamepara.Value = account.AccountName;
            paras.Add(accountnamepara);

            SqlParameter passwordpara = new SqlParameter("@PassWord", SqlDbType.VarChar, 20);
            passwordpara.Value = account.PassWord;
            paras.Add(passwordpara);

            SqlParameter accstatuspara = new SqlParameter("@AccStatus", SqlDbType.Int, 4);
            accstatuspara.Value = account.AccStatus;
            paras.Add(accstatuspara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = account.EmpId;
            paras.Add(empidpara);

            SqlParameter isvalidpara = new SqlParameter("@IsValid", SqlDbType.Bit, 1);
            isvalidpara.Value = account.IsValid;
            paras.Add(isvalidpara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Get(UserModel user, string accountName)
        {
            ResultModel result = new ResultModel();

            if (string.IsNullOrEmpty(accountName))
            {
                result.Message = "用户名不能为空";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@AccountName", accountName);
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                string cmdText = "select * from NFMT_User.dbo.Account where AccountName = @AccountName";
                dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringUser, CommandType.Text, cmdText, paras.ToArray());

                Account account = new Account();

                if (dr.Read())
                {
                    int indexAccId = dr.GetOrdinal("AccId");
                    account.AccId = Convert.ToInt32(dr[indexAccId]);

                    int indexAccountName = dr.GetOrdinal("AccountName");
                    if (dr["AccountName"] != DBNull.Value)
                        account.AccountName = Convert.ToString(dr[indexAccountName]);

                    int indexPassWord = dr.GetOrdinal("PassWord");
                    if (dr["PassWord"] != DBNull.Value)
                        account.PassWord = Convert.ToString(dr[indexPassWord]);

                    int indexAccStatus = dr.GetOrdinal("AccStatus");
                    if (dr["AccStatus"] != DBNull.Value)
                        account.AccStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr[indexAccStatus].ToString());

                    int indexEmpId = dr.GetOrdinal("EmpId");
                    if (dr["EmpId"] != DBNull.Value)
                        account.EmpId = Convert.ToInt32(dr[indexEmpId]);

                    int indexIsValid = dr.GetOrdinal("IsValid");
                    if (dr["IsValid"] != DBNull.Value)
                        account.IsValid = Convert.ToBoolean(dr[indexIsValid]);

                    int indexCreatorId = dr.GetOrdinal("CreatorId");
                    if (dr["CreatorId"] != DBNull.Value)
                        account.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

                    int indexCreateTime = dr.GetOrdinal("CreateTime");
                    if (dr["CreateTime"] != DBNull.Value)
                        account.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

                    int indexLastModifyId = dr.GetOrdinal("LastModifyId");
                    if (dr["LastModifyId"] != DBNull.Value)
                        account.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);

                    int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
                    if (dr["LastModifyTime"] != DBNull.Value)
                        account.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = account;
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

        public ResultModel CheckLogin(string accountName, string passWord)
        {
            ResultModel result = new ResultModel();

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@accountName", accountName);
            paras.Add(para);

            para = new SqlParameter("@passWord", passWord);
            paras.Add(para);

            try
            {
                string cmdText = "select count(*) from dbo.Account where AccountName=@accountName and PassWord=@passWord ";
                object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringUser, CommandType.Text, cmdText, paras.ToArray());

                int i = 0;
                if (int.TryParse(obj.ToString(), out i) && i > 0)
                {
                    cmdText = "select count(*) from dbo.Account where AccountName=@accountName and PassWord=@passWord and IsValid=1";
                    obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringUser, CommandType.Text, cmdText, paras.ToArray());

                    i = 0;
                    if (int.TryParse(obj.ToString(), out i) && i > 0)
                    {
                        result.AffectCount = 1;
                        result.Message = "验证成功";
                        result.ResultStatus = 0;
                        result.ReturnValue = i;
                    }
                    else
                    {
                        result.AffectCount = 0;
                        result.Message = "该账号已无效";
                        result.ResultStatus = -1;
                        result.ReturnValue = 0;
                    }
                }
                else
                {
                    result.Message = "账号或密码错误";
                    result.ResultStatus = -1;
                    result.AffectCount = 0;
                    result.ReturnValue = 0;
                }         
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel ValidateAccountName(UserModel user, string accountName)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select COUNT(1) from dbo.Account where AccountName = '{0}' and AccStatus <> {1}", accountName, (int)Common.StatusEnum.已作废);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                int i;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && int.TryParse(obj.ToString(), out i))
                {
                    if (i > 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "存在相同的账户名，请重新输入账户名";
                    }
                    else
                    {
                        result.ResultStatus = 0;
                        result.Message = "该账户可新增";
                    }
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取出错";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel ValidatePwd(UserModel user, string oldPwd)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select COUNT(1) from dbo.Account where EmpId = {0} and PassWord = '{1}' and AccStatus <> {2}", user.EmpId, oldPwd.Trim(), (int)Common.StatusEnum.已作废);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                int i;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && int.TryParse(obj.ToString(), out i))
                {
                    if (i == 1)
                    {
                        result.ResultStatus = 0;
                        result.Message = "密码验证正确";
                    }
                    else
                    {
                        result.ResultStatus = -1;
                        result.Message = "原密码不正确";
                    }
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "密码验证异常";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel ChangePwd(UserModel user, string newPwd)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Account set PassWord = '{0}' where EmpId = {1} and AccStatus = {2}", newPwd.Trim(), user.EmpId, (int)Common.StatusEnum.已生效);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "修改密码成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "修改密码失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel UpdateAccountValidate(UserModel user, int empId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Account set IsValid = 0 where EmpId = {0}", empId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "修改状态成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "修改状态失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel ResetPassword(UserModel user, int empId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format(" update dbo.Account set PassWord = '{0}' where EmpId = {1} ", NFMT.Common.DefaultValue.DefaultPassword, empId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "重置密码成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "重置密码失败";
                }
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
