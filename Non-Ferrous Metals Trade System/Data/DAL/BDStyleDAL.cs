/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BDStyleDAL.cs
// 文件功能描述：dbo.BDStyle数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年4月22日
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
    /// dbo.BDStyle数据交互类。
    /// </summary>
    public class BDStyleDAL : DataOperate, IBDStyleDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BDStyleDAL()
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
            BDStyle bdstyle = (BDStyle)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@BDStyleId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter bdstylecodepara = new SqlParameter("@BDStyleCode", SqlDbType.VarChar, 80);
            bdstylecodepara.Value = bdstyle.BDStyleCode;
            paras.Add(bdstylecodepara);

            SqlParameter bdstylenamepara = new SqlParameter("@BDStyleName", SqlDbType.VarChar, 80);
            bdstylenamepara.Value = bdstyle.BDStyleName;
            paras.Add(bdstylenamepara);

            SqlParameter bdstylestatuspara = new SqlParameter("@BDStyleStatus", SqlDbType.Int, 4);
            bdstylestatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(bdstylestatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            BDStyle bdstyle = new BDStyle();

            int indexBDStyleId = dr.GetOrdinal("BDStyleId");
            bdstyle.BDStyleId = Convert.ToInt32(dr[indexBDStyleId]);

            int indexBDStyleCode = dr.GetOrdinal("BDStyleCode");
            bdstyle.BDStyleCode = Convert.ToString(dr[indexBDStyleCode]);

            int indexBDStyleName = dr.GetOrdinal("BDStyleName");
            bdstyle.BDStyleName = Convert.ToString(dr[indexBDStyleName]);

            int indexBDStyleStatus = dr.GetOrdinal("BDStyleStatus");
            bdstyle.BDStyleStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexBDStyleStatus]);

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                bdstyle.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                bdstyle.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                bdstyle.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                bdstyle.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return bdstyle;
        }

        public override string TableName
        {
            get
            {
                return "BDStyle";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            BDStyle bdstyle = (BDStyle)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter bdstyleidpara = new SqlParameter("@BDStyleId", SqlDbType.Int, 4);
            bdstyleidpara.Value = bdstyle.BDStyleId;
            paras.Add(bdstyleidpara);

            SqlParameter bdstylecodepara = new SqlParameter("@BDStyleCode", SqlDbType.VarChar, 80);
            bdstylecodepara.Value = bdstyle.BDStyleCode;
            paras.Add(bdstylecodepara);

            SqlParameter bdstylenamepara = new SqlParameter("@BDStyleName", SqlDbType.VarChar, 80);
            bdstylenamepara.Value = bdstyle.BDStyleName;
            paras.Add(bdstylenamepara);

            SqlParameter bdstylestatuspara = new SqlParameter("@BDStyleStatus", SqlDbType.Int, 4);
            bdstylestatuspara.Value = bdstyle.BDStyleStatus;
            paras.Add(bdstylestatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(StyleEnum style)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;

            try
            {
                string sql = string.Format("select * from dbo.BDStyleDetail where BDStyleId = {0}", (int)style);

                dr = SqlHelper.ExecuteReader(this.ConnectString, CommandType.Text, sql, null);

                DetailCollection detailCollection = new DetailCollection();

                while (dr.Read())
                {
                    Model.BDStyleDetail detail = new BDStyleDetail();

                    if (dr["StyleDetailId"] != DBNull.Value)
                    {
                        detail.StyleDetailId = Convert.ToInt32(dr["StyleDetailId"]);
                    }

                    if (dr["BDStyleId"] != DBNull.Value)
                    {
                        detail.BDStyleId = Convert.ToInt32(dr["BDStyleId"]);
                    }

                    if (dr["DetailCode"] != DBNull.Value)
                    {
                        detail.DetailCode = dr["DetailCode"].ToString();
                    }

                    if (dr["DetailName"] != DBNull.Value)
                    {
                        detail.DetailName = dr["DetailName"].ToString();
                    }

                    if (dr["DetailStatus"] != DBNull.Value)
                    {
                        detail.DetailStatus = (NFMT.Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
                    }

                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        detail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
                    }

                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        detail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    }

                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        detail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    }

                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        detail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    }

                    detailCollection.Add(detail);
                }

                result.AffectCount = detailCollection.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = detailCollection;
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

        public override int MenuId
        {
            get
            {
                return 75;
            }
        }

        #endregion
    }
}
