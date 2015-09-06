/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PaymentAttachDAL.cs
// 文件功能描述：财务付款附件dbo.Fun_PaymentAttach数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年8月15日
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
    /// 财务付款附件dbo.Fun_PaymentAttach数据交互类。
    /// </summary>
    public class PaymentAttachDAL : ExecOperate, IPaymentAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PaymentAttachDAL()
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
            PaymentAttach fun_paymentattach = (PaymentAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@PaymentAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = fun_paymentattach.AttachId;
            paras.Add(attachidpara);

            SqlParameter paymentidpara = new SqlParameter("@PaymentId", SqlDbType.Int, 4);
            paymentidpara.Value = fun_paymentattach.PaymentId;
            paras.Add(paymentidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PaymentAttach paymentattach = new PaymentAttach();

            int indexPaymentAttachId = dr.GetOrdinal("PaymentAttachId");
            paymentattach.PaymentAttachId = Convert.ToInt32(dr[indexPaymentAttachId]);

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                paymentattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }

            int indexPaymentId = dr.GetOrdinal("PaymentId");
            if (dr["PaymentId"] != DBNull.Value)
            {
                paymentattach.PaymentId = Convert.ToInt32(dr[indexPaymentId]);
            }


            return paymentattach;
        }

        public override string TableName
        {
            get
            {
                return "Fun_PaymentAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PaymentAttach fun_paymentattach = (PaymentAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter paymentattachidpara = new SqlParameter("@PaymentAttachId", SqlDbType.Int, 4);
            paymentattachidpara.Value = fun_paymentattach.PaymentAttachId;
            paras.Add(paymentattachidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = fun_paymentattach.AttachId;
            paras.Add(attachidpara);

            SqlParameter paymentidpara = new SqlParameter("@PaymentId", SqlDbType.Int, 4);
            paymentidpara.Value = fun_paymentattach.PaymentId;
            paras.Add(paymentidpara);


            return paras;
        }

        #endregion
    }
}
