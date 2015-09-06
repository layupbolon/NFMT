/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PayApplyAttachDAL.cs
// 文件功能描述：付款申请附件dbo.Fun_PayApplyAttach数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年3月13日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Funds.Model;
using NFMT.DBUtility;
using NFMT.Funds.IDAL;
using NFMT.Common;

namespace NFMT.Funds.DAL
{
    /// <summary>
    /// 付款申请附件dbo.Fun_PayApplyAttach数据交互类。
    /// </summary>
    public partial class PayApplyAttachDAL : DataOperate, IPayApplyAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PayApplyAttachDAL()
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
            PayApplyAttach fun_payapplyattach = (PayApplyAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@PayApplyAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = fun_payapplyattach.AttachId;
            paras.Add(attachidpara);

            SqlParameter payapplyidpara = new SqlParameter("@PayApplyId", SqlDbType.Int, 4);
            payapplyidpara.Value = fun_payapplyattach.PayApplyId;
            paras.Add(payapplyidpara);

            SqlParameter attachtypepara = new SqlParameter("@AttachType", SqlDbType.Int, 4);
            attachtypepara.Value = fun_payapplyattach.AttachType;
            paras.Add(attachtypepara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            PayApplyAttach payapplyattach = new PayApplyAttach();

            payapplyattach.PayApplyAttachId = Convert.ToInt32(dr["PayApplyAttachId"]);

            if (dr["AttachId"] != DBNull.Value)
            {
                payapplyattach.AttachId = Convert.ToInt32(dr["AttachId"]);
            }

            if (dr["PayApplyId"] != DBNull.Value)
            {
                payapplyattach.PayApplyId = Convert.ToInt32(dr["PayApplyId"]);
            }

            if (dr["AttachType"] != DBNull.Value)
            {
                payapplyattach.AttachType = Convert.ToInt32(dr["AttachType"]);
            }


            return payapplyattach;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PayApplyAttach payapplyattach = new PayApplyAttach();

            int indexPayApplyAttachId = dr.GetOrdinal("PayApplyAttachId");
            payapplyattach.PayApplyAttachId = Convert.ToInt32(dr[indexPayApplyAttachId]);

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                payapplyattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }

            int indexPayApplyId = dr.GetOrdinal("PayApplyId");
            if (dr["PayApplyId"] != DBNull.Value)
            {
                payapplyattach.PayApplyId = Convert.ToInt32(dr[indexPayApplyId]);
            }

            int indexAttachType = dr.GetOrdinal("AttachType");
            if (dr["AttachType"] != DBNull.Value)
            {
                payapplyattach.AttachType = Convert.ToInt32(dr[indexAttachType]);
            }


            return payapplyattach;
        }

        public override string TableName
        {
            get
            {
                return "Fun_PayApplyAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PayApplyAttach fun_payapplyattach = (PayApplyAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter payapplyattachidpara = new SqlParameter("@PayApplyAttachId", SqlDbType.Int, 4);
            payapplyattachidpara.Value = fun_payapplyattach.PayApplyAttachId;
            paras.Add(payapplyattachidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = fun_payapplyattach.AttachId;
            paras.Add(attachidpara);

            SqlParameter payapplyidpara = new SqlParameter("@PayApplyId", SqlDbType.Int, 4);
            payapplyidpara.Value = fun_payapplyattach.PayApplyId;
            paras.Add(payapplyidpara);

            SqlParameter attachtypepara = new SqlParameter("@AttachType", SqlDbType.Int, 4);
            attachtypepara.Value = fun_payapplyattach.AttachType;
            paras.Add(attachtypepara);


            return paras;
        }

        #endregion
    }
}
