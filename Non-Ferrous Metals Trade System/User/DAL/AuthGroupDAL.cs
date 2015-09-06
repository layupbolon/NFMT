/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AuthGroupDAL.cs
// 文件功能描述：权限组dbo.AuthGroup数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月18日
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
    /// 权限组dbo.AuthGroup数据交互类。
    /// </summary>
    public class AuthGroupDAL : DataOperate, IAuthGroupDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AuthGroupDAL()
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
            AuthGroup authgroup = (AuthGroup)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@AuthGroupId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(authgroup.AuthGroupName))
            {
                SqlParameter authgroupnamepara = new SqlParameter("@AuthGroupName", SqlDbType.VarChar, 800);
                authgroupnamepara.Value = authgroup.AuthGroupName;
                paras.Add(authgroupnamepara);
            }

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = authgroup.AssetId;
            paras.Add(assetidpara);

            SqlParameter tradedirectionpara = new SqlParameter("@TradeDirection", SqlDbType.Int, 4);
            tradedirectionpara.Value = authgroup.TradeDirection;
            paras.Add(tradedirectionpara);

            SqlParameter tradeborderpara = new SqlParameter("@TradeBorder", SqlDbType.Int, 4);
            tradeborderpara.Value = authgroup.TradeBorder;
            paras.Add(tradeborderpara);

            SqlParameter contractinoutpara = new SqlParameter("@ContractInOut", SqlDbType.Int, 4);
            contractinoutpara.Value = authgroup.ContractInOut;
            paras.Add(contractinoutpara);

            SqlParameter contractlimitpara = new SqlParameter("@ContractLimit", SqlDbType.Int, 4);
            contractlimitpara.Value = authgroup.ContractLimit;
            paras.Add(contractlimitpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = authgroup.CorpId;
            paras.Add(corpidpara);

            SqlParameter authgroupstatuspara = new SqlParameter("@AuthGroupStatus", SqlDbType.Int, 4);
            authgroupstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(authgroupstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            AuthGroup authgroup = new AuthGroup();

            authgroup.AuthGroupId = Convert.ToInt32(dr["AuthGroupId"]);

            if (dr["AuthGroupName"] != DBNull.Value)
            {
                authgroup.AuthGroupName = Convert.ToString(dr["AuthGroupName"]);
            }

            if (dr["AssetId"] != DBNull.Value)
            {
                authgroup.AssetId = Convert.ToInt32(dr["AssetId"]);
            }

            if (dr["TradeDirection"] != DBNull.Value)
            {
                authgroup.TradeDirection = Convert.ToInt32(dr["TradeDirection"]);
            }

            if (dr["TradeBorder"] != DBNull.Value)
            {
                authgroup.TradeBorder = Convert.ToInt32(dr["TradeBorder"]);
            }

            if (dr["ContractInOut"] != DBNull.Value)
            {
                authgroup.ContractInOut = Convert.ToInt32(dr["ContractInOut"]);
            }

            if (dr["ContractLimit"] != DBNull.Value)
            {
                authgroup.ContractLimit = Convert.ToInt32(dr["ContractLimit"]);
            }

            if (dr["CorpId"] != DBNull.Value)
            {
                authgroup.CorpId = Convert.ToInt32(dr["CorpId"]);
            }

            if (dr["AuthGroupStatus"] != DBNull.Value)
            {
                authgroup.AuthGroupStatus = (Common.StatusEnum)Convert.ToInt32(dr["AuthGroupStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                authgroup.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                authgroup.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                authgroup.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                authgroup.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return authgroup;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            AuthGroup authgroup = new AuthGroup();

            int indexAuthGroupId = dr.GetOrdinal("AuthGroupId");
            authgroup.AuthGroupId = Convert.ToInt32(dr[indexAuthGroupId]);

            int indexAuthGroupName = dr.GetOrdinal("AuthGroupName");
            if (dr["AuthGroupName"] != DBNull.Value)
            {
                authgroup.AuthGroupName = Convert.ToString(dr[indexAuthGroupName]);
            }

            int indexAssetId = dr.GetOrdinal("AssetId");
            if (dr["AssetId"] != DBNull.Value)
            {
                authgroup.AssetId = Convert.ToInt32(dr[indexAssetId]);
            }

            int indexTradeDirection = dr.GetOrdinal("TradeDirection");
            if (dr["TradeDirection"] != DBNull.Value)
            {
                authgroup.TradeDirection = Convert.ToInt32(dr[indexTradeDirection]);
            }

            int indexTradeBorder = dr.GetOrdinal("TradeBorder");
            if (dr["TradeBorder"] != DBNull.Value)
            {
                authgroup.TradeBorder = Convert.ToInt32(dr[indexTradeBorder]);
            }

            int indexContractInOut = dr.GetOrdinal("ContractInOut");
            if (dr["ContractInOut"] != DBNull.Value)
            {
                authgroup.ContractInOut = Convert.ToInt32(dr[indexContractInOut]);
            }

            int indexContractLimit = dr.GetOrdinal("ContractLimit");
            if (dr["ContractLimit"] != DBNull.Value)
            {
                authgroup.ContractLimit = Convert.ToInt32(dr[indexContractLimit]);
            }

            int indexCorpId = dr.GetOrdinal("CorpId");
            if (dr["CorpId"] != DBNull.Value)
            {
                authgroup.CorpId = Convert.ToInt32(dr[indexCorpId]);
            }

            int indexAuthGroupStatus = dr.GetOrdinal("AuthGroupStatus");
            if (dr["AuthGroupStatus"] != DBNull.Value)
            {
                authgroup.AuthGroupStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexAuthGroupStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                authgroup.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                authgroup.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                authgroup.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                authgroup.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return authgroup;
        }

        public override string TableName
        {
            get
            {
                return "AuthGroup";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            AuthGroup authgroup = (AuthGroup)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter authgroupidpara = new SqlParameter("@AuthGroupId", SqlDbType.Int, 4);
            authgroupidpara.Value = authgroup.AuthGroupId;
            paras.Add(authgroupidpara);

            if (!string.IsNullOrEmpty(authgroup.AuthGroupName))
            {
                SqlParameter authgroupnamepara = new SqlParameter("@AuthGroupName", SqlDbType.VarChar, 800);
                authgroupnamepara.Value = authgroup.AuthGroupName;
                paras.Add(authgroupnamepara);
            }

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = authgroup.AssetId;
            paras.Add(assetidpara);

            SqlParameter tradedirectionpara = new SqlParameter("@TradeDirection", SqlDbType.Int, 4);
            tradedirectionpara.Value = authgroup.TradeDirection;
            paras.Add(tradedirectionpara);

            SqlParameter tradeborderpara = new SqlParameter("@TradeBorder", SqlDbType.Int, 4);
            tradeborderpara.Value = authgroup.TradeBorder;
            paras.Add(tradeborderpara);

            SqlParameter contractinoutpara = new SqlParameter("@ContractInOut", SqlDbType.Int, 4);
            contractinoutpara.Value = authgroup.ContractInOut;
            paras.Add(contractinoutpara);

            SqlParameter contractlimitpara = new SqlParameter("@ContractLimit", SqlDbType.Int, 4);
            contractlimitpara.Value = authgroup.ContractLimit;
            paras.Add(contractlimitpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = authgroup.CorpId;
            paras.Add(corpidpara);

            SqlParameter authgroupstatuspara = new SqlParameter("@AuthGroupStatus", SqlDbType.Int, 4);
            authgroupstatuspara.Value = authgroup.AuthGroupStatus;
            paras.Add(authgroupstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel LoadByEmpId(int empId)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                int readyStatus = (int)Common.StatusEnum.已生效;
                string cmdText = string.Format("select ag.* from dbo.AuthGroup ag inner join dbo.AuthGroupDetail agd on ag.AuthGroupId = agd.AuthGroupId where agd.EmpId ={0} and ag.AuthGroupStatus = {1} and agd.DetailStatus = {1}", empId, readyStatus);

                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, null);
                List<AuthGroup> models = new List<AuthGroup>();

                int i = 0;
                while (dr.Read())
                {
                    AuthGroup model = this.CreateModel<AuthGroup>(dr);
                    models.Add(model);
                    i++;
                }

                result.AffectCount = i;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = models;
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
                return 99;
            }
        }

        #endregion
    }
}
