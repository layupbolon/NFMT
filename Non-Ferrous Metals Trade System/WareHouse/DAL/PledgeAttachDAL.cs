/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeAttachDAL.cs
// 文件功能描述：质押附件dbo.PledgeAttach数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.WareHouse.Model;
using NFMT.DBUtility;
using NFMT.WareHouse.IDAL;
using NFMT.Common;

namespace NFMT.WareHouse.DAL
{
    /// <summary>
    /// 质押附件dbo.PledgeAttach数据交互类。
    /// </summary>
    public class PledgeAttachDAL : DataOperate, IPledgeAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PledgeAttachDAL()
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
            PledgeAttach st_pledgeattach = (PledgeAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@PledgeAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter pledgeidpara = new SqlParameter("@PledgeId", SqlDbType.Int, 4);
            pledgeidpara.Value = st_pledgeattach.PledgeId;
            paras.Add(pledgeidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_pledgeattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PledgeAttach pledgeattach = new PledgeAttach();

            int indexPledgeAttachId = dr.GetOrdinal("PledgeAttachId");
            pledgeattach.PledgeAttachId = Convert.ToInt32(dr[indexPledgeAttachId]);

            int indexPledgeId = dr.GetOrdinal("PledgeId");
            if (dr["PledgeId"] != DBNull.Value)
            {
                pledgeattach.PledgeId = Convert.ToInt32(dr[indexPledgeId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                pledgeattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return pledgeattach;
        }

        public override string TableName
        {
            get
            {
                return "St_PledgeAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PledgeAttach st_pledgeattach = (PledgeAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter pledgeattachidpara = new SqlParameter("@PledgeAttachId", SqlDbType.Int, 4);
            pledgeattachidpara.Value = st_pledgeattach.PledgeAttachId;
            paras.Add(pledgeattachidpara);

            SqlParameter pledgeidpara = new SqlParameter("@PledgeId", SqlDbType.Int, 4);
            pledgeidpara.Value = st_pledgeattach.PledgeId;
            paras.Add(pledgeidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_pledgeattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion
    }
}
