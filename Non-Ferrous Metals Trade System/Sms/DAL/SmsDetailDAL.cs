/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsDetailDAL.cs
// 文件功能描述：消息明细表dbo.Sm_SmsDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月11日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Sms.Model;
using NFMT.DBUtility;
using NFMT.Sms.IDAL;
using NFMT.Common;

namespace NFMT.Sms.DAL
{
    /// <summary>
    /// 消息明细表dbo.Sm_SmsDetail数据交互类。
    /// </summary>
    public class SmsDetailDAL : DataOperate, ISmsDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SmsDetailDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringSms;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            SmsDetail sm_smsdetail = (SmsDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter smsidpara = new SqlParameter("@SmsId", SqlDbType.Int, 4);
            smsidpara.Value = sm_smsdetail.SmsId;
            paras.Add(smsidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = sm_smsdetail.EmpId;
            paras.Add(empidpara);

            SqlParameter readtimepara = new SqlParameter("@ReadTime", SqlDbType.DateTime, 8);
            readtimepara.Value = sm_smsdetail.ReadTime;
            paras.Add(readtimepara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            SmsDetail smsdetail = new SmsDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            smsdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexSmsId = dr.GetOrdinal("SmsId");
            if (dr["SmsId"] != DBNull.Value)
            {
                smsdetail.SmsId = Convert.ToInt32(dr[indexSmsId]);
            }

            int indexEmpId = dr.GetOrdinal("EmpId");
            if (dr["EmpId"] != DBNull.Value)
            {
                smsdetail.EmpId = Convert.ToInt32(dr[indexEmpId]);
            }

            int indexReadTime = dr.GetOrdinal("ReadTime");
            if (dr["ReadTime"] != DBNull.Value)
            {
                smsdetail.ReadTime = Convert.ToDateTime(dr[indexReadTime]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                smsdetail.DetailStatus = Convert.ToInt32(dr[indexDetailStatus]);
            }


            return smsdetail;
        }

        public override string TableName
        {
            get
            {
                return "Sm_SmsDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SmsDetail sm_smsdetail = (SmsDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = sm_smsdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter smsidpara = new SqlParameter("@SmsId", SqlDbType.Int, 4);
            smsidpara.Value = sm_smsdetail.SmsId;
            paras.Add(smsidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = sm_smsdetail.EmpId;
            paras.Add(empidpara);

            SqlParameter readtimepara = new SqlParameter("@ReadTime", SqlDbType.DateTime, 8);
            readtimepara.Value = sm_smsdetail.ReadTime;
            paras.Add(readtimepara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = sm_smsdetail.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        #endregion
    }
}
