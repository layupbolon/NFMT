/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BankDAL.cs
// 文件功能描述：dbo.Bank数据交互类。
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
    /// dbo.Bank数据交互类。
    /// </summary>
    public class BankDAL : DataOperate, IBankDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BankDAL()
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
            Bank bank = (Bank)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@BankId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter banknamepara = new SqlParameter("@BankName", SqlDbType.VarChar, 50);
            banknamepara.Value = bank.BankName;
            paras.Add(banknamepara);

            SqlParameter bankenamepara = new SqlParameter("@BankEname", SqlDbType.VarChar, 50);
            bankenamepara.Value = bank.BankEname;
            paras.Add(bankenamepara);

            if (!string.IsNullOrEmpty(bank.BankFullName))
            {
                SqlParameter bankfullnamepara = new SqlParameter("@BankFullName", SqlDbType.VarChar, 100);
                bankfullnamepara.Value = bank.BankFullName;
                paras.Add(bankfullnamepara);
            }

            if (!string.IsNullOrEmpty(bank.BankShort))
            {
                SqlParameter bankshortpara = new SqlParameter("@BankShort", SqlDbType.VarChar, 20);
                bankshortpara.Value = bank.BankShort;
                paras.Add(bankshortpara);
            }

            SqlParameter capitaltypepara = new SqlParameter("@CapitalType", SqlDbType.Int, 4);
            capitaltypepara.Value = bank.CapitalType;
            paras.Add(capitaltypepara);

            SqlParameter banklevelpara = new SqlParameter("@BankLevel", SqlDbType.Int, 4);
            banklevelpara.Value = bank.BankLevel;
            paras.Add(banklevelpara);

            SqlParameter switchbackpara = new SqlParameter("@SwitchBack", SqlDbType.Bit, 1);
            switchbackpara.Value = bank.SwitchBack;
            paras.Add(switchbackpara);

            SqlParameter parentidpara = new SqlParameter("@ParentId", SqlDbType.Int, 4);
            parentidpara.Value = bank.ParentId;
            paras.Add(parentidpara);

            SqlParameter bankstatuspara = new SqlParameter("@BankStatus", SqlDbType.Int, 4);
            bankstatuspara.Value = bank.BankStatus;
            paras.Add(bankstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            Bank bank = new Bank();

            bank.BankId = Convert.ToInt32(dr["BankId"]);

            bank.BankName = Convert.ToString(dr["BankName"]);

            bank.BankEname = Convert.ToString(dr["BankEname"]);

            if (dr["BankFullName"] != DBNull.Value)
            {
                bank.BankFullName = Convert.ToString(dr["BankFullName"]);
            }

            if (dr["BankShort"] != DBNull.Value)
            {
                bank.BankShort = Convert.ToString(dr["BankShort"]);
            }

            if (dr["CapitalType"] != DBNull.Value)
            {
                bank.CapitalType = Convert.ToInt32(dr["CapitalType"]);
            }

            if (dr["BankLevel"] != DBNull.Value)
            {
                bank.BankLevel = Convert.ToInt32(dr["BankLevel"]);
            }

            if (dr["SwitchBack"] != DBNull.Value)
            {
                bank.SwitchBack = Convert.ToBoolean(dr["SwitchBack"]);
            }

            if (dr["ParentId"] != DBNull.Value)
            {
                bank.ParentId = Convert.ToInt32(dr["ParentId"]);
            }

            bank.BankStatus = (Common.StatusEnum)Convert.ToInt32(dr["BankStatus"]);

            bank.CreatorId = Convert.ToInt32(dr["CreatorId"]);

            bank.CreateTime = Convert.ToDateTime(dr["CreateTime"]);

            if (dr["LastModifyId"] != DBNull.Value)
            {
                bank.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                bank.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return bank;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Bank bank = new Bank();

            int indexBankId = dr.GetOrdinal("BankId");
            bank.BankId = Convert.ToInt32(dr[indexBankId]);

            int indexBankName = dr.GetOrdinal("BankName");
            bank.BankName = Convert.ToString(dr[indexBankName]);

            int indexBankEname = dr.GetOrdinal("BankEname");
            bank.BankEname = Convert.ToString(dr[indexBankEname]);

            int indexBankFullName = dr.GetOrdinal("BankFullName");
            if (dr["BankFullName"] != DBNull.Value)
            {
                bank.BankFullName = Convert.ToString(dr[indexBankFullName]);
            }

            int indexBankShort = dr.GetOrdinal("BankShort");
            if (dr["BankShort"] != DBNull.Value)
            {
                bank.BankShort = Convert.ToString(dr[indexBankShort]);
            }

            int indexCapitalType = dr.GetOrdinal("CapitalType");
            if (dr["CapitalType"] != DBNull.Value)
            {
                bank.CapitalType = Convert.ToInt32(dr[indexCapitalType]);
            }

            int indexBankLevel = dr.GetOrdinal("BankLevel");
            if (dr["BankLevel"] != DBNull.Value)
            {
                bank.BankLevel = Convert.ToInt32(dr[indexBankLevel]);
            }

            int indexSwitchBack = dr.GetOrdinal("SwitchBack");
            if (dr["SwitchBack"] != DBNull.Value)
            {
                bank.SwitchBack = Convert.ToBoolean(dr[indexSwitchBack]);
            }

            int indexParentId = dr.GetOrdinal("ParentId");
            if (dr["ParentId"] != DBNull.Value)
            {
                bank.ParentId = Convert.ToInt32(dr[indexParentId]);
            }

            int indexBankStatus = dr.GetOrdinal("BankStatus");
            bank.BankStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexBankStatus]);

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            bank.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            bank.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                bank.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                bank.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return bank;
        }

        public override string TableName
        {
            get
            {
                return "Bank";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Bank bank = (Bank)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter bankidpara = new SqlParameter("@BankId", SqlDbType.Int, 4);
            bankidpara.Value = bank.BankId;
            paras.Add(bankidpara);

            SqlParameter banknamepara = new SqlParameter("@BankName", SqlDbType.VarChar, 50);
            banknamepara.Value = bank.BankName;
            paras.Add(banknamepara);

            SqlParameter bankenamepara = new SqlParameter("@BankEname", SqlDbType.VarChar, 50);
            bankenamepara.Value = bank.BankEname;
            paras.Add(bankenamepara);

            if (!string.IsNullOrEmpty(bank.BankFullName))
            {
                SqlParameter bankfullnamepara = new SqlParameter("@BankFullName", SqlDbType.VarChar, 100);
                bankfullnamepara.Value = bank.BankFullName;
                paras.Add(bankfullnamepara);
            }

            if (!string.IsNullOrEmpty(bank.BankShort))
            {
                SqlParameter bankshortpara = new SqlParameter("@BankShort", SqlDbType.VarChar, 20);
                bankshortpara.Value = bank.BankShort;
                paras.Add(bankshortpara);
            }

            SqlParameter capitaltypepara = new SqlParameter("@CapitalType", SqlDbType.Int, 4);
            capitaltypepara.Value = bank.CapitalType;
            paras.Add(capitaltypepara);

            SqlParameter banklevelpara = new SqlParameter("@BankLevel", SqlDbType.Int, 4);
            banklevelpara.Value = bank.BankLevel;
            paras.Add(banklevelpara);

            SqlParameter switchbackpara = new SqlParameter("@SwitchBack", SqlDbType.Bit, 1);
            switchbackpara.Value = bank.SwitchBack;
            paras.Add(switchbackpara);

            SqlParameter parentidpara = new SqlParameter("@ParentId", SqlDbType.Int, 4);
            parentidpara.Value = bank.ParentId;
            paras.Add(parentidpara);

            SqlParameter bankstatuspara = new SqlParameter("@BankStatus", SqlDbType.Int, 4);
            bankstatuspara.Value = bank.BankStatus;
            paras.Add(bankstatuspara);

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
                return 27;
            }
        }

        #endregion
    }
}
