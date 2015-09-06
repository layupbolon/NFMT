/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BDStyleDetailDAL.cs
// 文件功能描述：基础类型编码明细表dbo.BDStyleDetail数据交互类。
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
    /// 基础类型编码明细表dbo.BDStyleDetail数据交互类。
    /// </summary>
    public class BDStyleDetailDAL : DataOperate, IBDStyleDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BDStyleDetailDAL()
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
            BDStyleDetail bdstyledetail = (BDStyleDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StyleDetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter bdstyleidpara = new SqlParameter("@BDStyleId", SqlDbType.Int, 4);
            bdstyleidpara.Value = bdstyledetail.BDStyleId;
            paras.Add(bdstyleidpara);

            SqlParameter detailcodepara = new SqlParameter("@DetailCode", SqlDbType.VarChar, 80);
            detailcodepara.Value = bdstyledetail.DetailCode;
            paras.Add(detailcodepara);

            SqlParameter detailnamepara = new SqlParameter("@DetailName", SqlDbType.VarChar, 80);
            detailnamepara.Value = bdstyledetail.DetailName;
            paras.Add(detailnamepara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            BDStyleDetail bdstyledetail = new BDStyleDetail();

            int indexStyleDetailId = dr.GetOrdinal("StyleDetailId");
            bdstyledetail.StyleDetailId = Convert.ToInt32(dr[indexStyleDetailId]);

            int indexBDStyleId = dr.GetOrdinal("BDStyleId");
            bdstyledetail.BDStyleId = Convert.ToInt32(dr[indexBDStyleId]);

            int indexDetailCode = dr.GetOrdinal("DetailCode");
            bdstyledetail.DetailCode = Convert.ToString(dr[indexDetailCode]);

            int indexDetailName = dr.GetOrdinal("DetailName");
            bdstyledetail.DetailName = Convert.ToString(dr[indexDetailName]);

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            bdstyledetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            bdstyledetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            bdstyledetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                bdstyledetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                bdstyledetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return bdstyledetail;
        }

        public override string TableName
        {
            get
            {
                return "BDStyleDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            BDStyleDetail bdstyledetail = (BDStyleDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter styledetailidpara = new SqlParameter("@StyleDetailId", SqlDbType.Int, 4);
            styledetailidpara.Value = bdstyledetail.StyleDetailId;
            paras.Add(styledetailidpara);

            SqlParameter bdstyleidpara = new SqlParameter("@BDStyleId", SqlDbType.Int, 4);
            bdstyleidpara.Value = bdstyledetail.BDStyleId;
            paras.Add(bdstyleidpara);

            SqlParameter detailcodepara = new SqlParameter("@DetailCode", SqlDbType.VarChar, 80);
            detailcodepara.Value = bdstyledetail.DetailCode;
            paras.Add(detailcodepara);

            SqlParameter detailnamepara = new SqlParameter("@DetailName", SqlDbType.VarChar, 80);
            detailnamepara.Value = bdstyledetail.DetailName;
            paras.Add(detailnamepara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = bdstyledetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel LoadTradeDirectionAuth(UserModel user)
        {
            ResultModel result = new ResultModel();            
            try
            {
                NFMT.Authority.TradeDirectionAuth auth = new NFMT.Authority.TradeDirectionAuth();
                auth.AuthColumnNames.Add("sd.StyleDetailId");
                int styleId = (int)NFMT.Data.StyleEnum.TradeDirection;

                result = this.LoadDetailAuth(user, styleId, auth);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel LoadTradeBorderAuth(UserModel user)
        {
            ResultModel result = new ResultModel();
            try
            {
                NFMT.Authority.TradeBorderAuth auth = new NFMT.Authority.TradeBorderAuth();
                auth.AuthColumnNames.Add("sd.StyleDetailId");
                int styleId = (int)NFMT.Data.StyleEnum.TradeBorder;

                result = this.LoadDetailAuth(user, styleId, auth);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel LoadContractLimitAuth(UserModel user)
        {
            ResultModel result = new ResultModel();
            try
            {
                NFMT.Authority.ContractLimitAuth auth = new NFMT.Authority.ContractLimitAuth();
                auth.AuthColumnNames.Add("sd.StyleDetailId");
                int styleId = (int)NFMT.Data.StyleEnum.ContractLimit;

                result = this.LoadDetailAuth(user, styleId, auth);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel LoadCustomsAuth(UserModel user)
        {
            ResultModel result = new ResultModel();
            try
            {
                NFMT.Authority.CustomsTypeAuth auth = new NFMT.Authority.CustomsTypeAuth();
                auth.AuthColumnNames.Add("sd.StyleDetailId");
                int styleId = (int)NFMT.Data.StyleEnum.CustomType;

                result = this.LoadDetailAuth(user, styleId, auth);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel LoadDetailAuth(UserModel user,int styleId,NFMT.Common.IAuthority auth)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                result = auth.CreateAuthorityStr(user);
                if (result.ResultStatus != 0)
                    return result;

                string cmdText = string.Format("select * from dbo.BDStyleDetail sd where sd.BDStyleId={0} {1}", styleId, result.ReturnValue.ToString());

                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, null);
                List<Model.BDStyleDetail> models = new List<Model.BDStyleDetail>();

                int i = 0;
                while (dr.Read())
                {
                    Model.BDStyleDetail model = this.CreateModel<Model.BDStyleDetail>(dr);
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

        #endregion
    }
}
