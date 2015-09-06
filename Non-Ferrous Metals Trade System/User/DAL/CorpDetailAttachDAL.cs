/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorpDetailAttachDAL.cs
// 文件功能描述：客户附件表dbo.CorpDetailAttach数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
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
    /// 客户附件表dbo.CorpDetailAttach数据交互类。
    /// </summary>
    public partial class CorpDetailAttachDAL : DataOperate, ICorpDetailAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CorpDetailAttachDAL()
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
            CorpDetailAttach corpdetailattach = (CorpDetailAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@CorpDetailAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = corpdetailattach.DetailId;
            paras.Add(detailidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = corpdetailattach.AttachId;
            paras.Add(attachidpara);

            SqlParameter attachtypepara = new SqlParameter("@AttachType", SqlDbType.Int, 4);
            attachtypepara.Value = corpdetailattach.AttachType;
            paras.Add(attachtypepara);

            SqlParameter corpdetailattachstatuspara = new SqlParameter("@CorpDetailAttachStatus", SqlDbType.Int, 4);
            corpdetailattachstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(corpdetailattachstatuspara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            CorpDetailAttach corpdetailattach = new CorpDetailAttach();

            corpdetailattach.CorpDetailAttachId = Convert.ToInt32(dr["CorpDetailAttachId"]);

            if (dr["DetailId"] != DBNull.Value)
            {
                corpdetailattach.DetailId = Convert.ToInt32(dr["DetailId"]);
            }

            if (dr["AttachId"] != DBNull.Value)
            {
                corpdetailattach.AttachId = Convert.ToInt32(dr["AttachId"]);
            }

            if (dr["AttachType"] != DBNull.Value)
            {
                corpdetailattach.AttachType = Convert.ToInt32(dr["AttachType"]);
            }

            if (dr["CorpDetailAttachStatus"] != DBNull.Value)
            {
                corpdetailattach.CorpDetailAttachStatus = (Common.StatusEnum)Convert.ToInt32(dr["CorpDetailAttachStatus"]);
            }


            return corpdetailattach;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            CorpDetailAttach corpdetailattach = new CorpDetailAttach();

            int indexCorpDetailAttachId = dr.GetOrdinal("CorpDetailAttachId");
            corpdetailattach.CorpDetailAttachId = Convert.ToInt32(dr[indexCorpDetailAttachId]);

            int indexDetailId = dr.GetOrdinal("DetailId");
            if (dr["DetailId"] != DBNull.Value)
            {
                corpdetailattach.DetailId = Convert.ToInt32(dr[indexDetailId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                corpdetailattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }

            int indexAttachType = dr.GetOrdinal("AttachType");
            if (dr["AttachType"] != DBNull.Value)
            {
                corpdetailattach.AttachType = Convert.ToInt32(dr[indexAttachType]);
            }

            int indexCorpDetailAttachStatus = dr.GetOrdinal("CorpDetailAttachStatus");
            if (dr["CorpDetailAttachStatus"] != DBNull.Value)
            {
                corpdetailattach.CorpDetailAttachStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexCorpDetailAttachStatus]);
            }


            return corpdetailattach;
        }

        public override string TableName
        {
            get
            {
                return "CorpDetailAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CorpDetailAttach corpdetailattach = (CorpDetailAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter corpdetailattachidpara = new SqlParameter("@CorpDetailAttachId", SqlDbType.Int, 4);
            corpdetailattachidpara.Value = corpdetailattach.CorpDetailAttachId;
            paras.Add(corpdetailattachidpara);

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = corpdetailattach.DetailId;
            paras.Add(detailidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = corpdetailattach.AttachId;
            paras.Add(attachidpara);

            SqlParameter attachtypepara = new SqlParameter("@AttachType", SqlDbType.Int, 4);
            attachtypepara.Value = corpdetailattach.AttachType;
            paras.Add(attachtypepara);

            SqlParameter corpdetailattachstatuspara = new SqlParameter("@CorpDetailAttachStatus", SqlDbType.Int, 4);
            corpdetailattachstatuspara.Value = corpdetailattach.CorpDetailAttachStatus;
            paras.Add(corpdetailattachstatuspara);


            return paras;
        }

        #endregion
    }
}
