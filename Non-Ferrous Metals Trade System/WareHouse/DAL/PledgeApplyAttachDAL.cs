/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeApplyAttachDAL.cs
// 文件功能描述：质押申请附件dbo.PledgeApplyAttach数据交互类。
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
    /// 质押申请附件dbo.PledgeApplyAttach数据交互类。
    /// </summary>
    public class PledgeApplyAttachDAL : ApplyOperate, IPledgeApplyAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PledgeApplyAttachDAL()
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
            PledgeApplyAttach st_pledgeapplyattach = (PledgeApplyAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@PledgeApplyAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter pledgeapplyidpara = new SqlParameter("@PledgeApplyId", SqlDbType.Int, 4);
            pledgeapplyidpara.Value = st_pledgeapplyattach.PledgeApplyId;
            paras.Add(pledgeapplyidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_pledgeapplyattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PledgeApplyAttach pledgeapplyattach = new PledgeApplyAttach();

            int indexPledgeApplyAttachId = dr.GetOrdinal("PledgeApplyAttachId");
            pledgeapplyattach.PledgeApplyAttachId = Convert.ToInt32(dr[indexPledgeApplyAttachId]);

            int indexPledgeApplyId = dr.GetOrdinal("PledgeApplyId");
            if (dr["PledgeApplyId"] != DBNull.Value)
            {
                pledgeapplyattach.PledgeApplyId = Convert.ToInt32(dr[indexPledgeApplyId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                pledgeapplyattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return pledgeapplyattach;
        }

        public override string TableName
        {
            get
            {
                return "St_PledgeApplyAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PledgeApplyAttach st_pledgeapplyattach = (PledgeApplyAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter pledgeapplyattachidpara = new SqlParameter("@PledgeApplyAttachId", SqlDbType.Int, 4);
            pledgeapplyattachidpara.Value = st_pledgeapplyattach.PledgeApplyAttachId;
            paras.Add(pledgeapplyattachidpara);

            SqlParameter pledgeapplyidpara = new SqlParameter("@PledgeApplyId", SqlDbType.Int, 4);
            pledgeapplyidpara.Value = st_pledgeapplyattach.PledgeApplyId;
            paras.Add(pledgeapplyidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_pledgeapplyattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion

    }
}
