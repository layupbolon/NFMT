/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsDAL.cs
// 文件功能描述：消息dbo.Sm_Sms数据交互类。
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
    /// 消息dbo.Sm_Sms数据交互类。
    /// </summary>
    public class SmsDAL : DataOperate, ISmsDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SmsDAL()
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
            Model.Sms sm_sms = (Model.Sms)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@SmsId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter smstypeidpara = new SqlParameter("@SmsTypeId", SqlDbType.Int, 4);
            smstypeidpara.Value = sm_sms.SmsTypeId;
            paras.Add(smstypeidpara);

            if (!string.IsNullOrEmpty(sm_sms.SmsHead))
            {
                SqlParameter smsheadpara = new SqlParameter("@SmsHead", SqlDbType.VarChar, 80);
                smsheadpara.Value = sm_sms.SmsHead;
                paras.Add(smsheadpara);
            }

            if (!string.IsNullOrEmpty(sm_sms.SmsBody))
            {
                SqlParameter smsbodypara = new SqlParameter("@SmsBody", SqlDbType.VarChar, 200);
                smsbodypara.Value = sm_sms.SmsBody;
                paras.Add(smsbodypara);
            }

            SqlParameter smsreltimepara = new SqlParameter("@SmsRelTime", SqlDbType.DateTime, 8);
            smsreltimepara.Value = sm_sms.SmsRelTime;
            paras.Add(smsreltimepara);

            SqlParameter smsstatuspara = new SqlParameter("@SmsStatus", SqlDbType.Int, 4);
            smsstatuspara.Value = sm_sms.SmsStatus;
            paras.Add(smsstatuspara);

            SqlParameter smslevelpara = new SqlParameter("@SmsLevel", SqlDbType.Int, 4);
            smslevelpara.Value = sm_sms.SmsLevel;
            paras.Add(smslevelpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);

            SqlParameter sourceidpara = new SqlParameter("@SourceId", SqlDbType.Int, 4);
            sourceidpara.Value = sm_sms.SourceId;
            paras.Add(sourceidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Model.Sms sms = new Model.Sms();

            int indexSmsId = dr.GetOrdinal("SmsId");
            sms.SmsId = Convert.ToInt32(dr[indexSmsId]);

            int indexSmsTypeId = dr.GetOrdinal("SmsTypeId");
            if (dr["SmsTypeId"] != DBNull.Value)
            {
                sms.SmsTypeId = Convert.ToInt32(dr[indexSmsTypeId]);
            }

            int indexSmsHead = dr.GetOrdinal("SmsHead");
            if (dr["SmsHead"] != DBNull.Value)
            {
                sms.SmsHead = Convert.ToString(dr[indexSmsHead]);
            }

            int indexSmsBody = dr.GetOrdinal("SmsBody");
            if (dr["SmsBody"] != DBNull.Value)
            {
                sms.SmsBody = Convert.ToString(dr[indexSmsBody]);
            }

            int indexSmsRelTime = dr.GetOrdinal("SmsRelTime");
            if (dr["SmsRelTime"] != DBNull.Value)
            {
                sms.SmsRelTime = Convert.ToDateTime(dr[indexSmsRelTime]);
            }

            int indexSmsStatus = dr.GetOrdinal("SmsStatus");
            if (dr["SmsStatus"] != DBNull.Value)
            {
                sms.SmsStatus = Convert.ToInt32(dr[indexSmsStatus]);
            }

            int indexSmsLevel = dr.GetOrdinal("SmsLevel");
            if (dr["SmsLevel"] != DBNull.Value)
            {
                sms.SmsLevel = Convert.ToInt32(dr[indexSmsLevel]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                sms.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                sms.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexSourceId = dr.GetOrdinal("SourceId");
            if (dr["SourceId"] != DBNull.Value)
            {
                sms.SourceId = Convert.ToInt32(dr[indexSourceId]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                sms.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                sms.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return sms;
        }

        public override string TableName
        {
            get
            {
                return "Sm_Sms";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Model.Sms sm_sms = (Model.Sms)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter smsidpara = new SqlParameter("@SmsId", SqlDbType.Int, 4);
            smsidpara.Value = sm_sms.SmsId;
            paras.Add(smsidpara);

            SqlParameter smstypeidpara = new SqlParameter("@SmsTypeId", SqlDbType.Int, 4);
            smstypeidpara.Value = sm_sms.SmsTypeId;
            paras.Add(smstypeidpara);

            if (!string.IsNullOrEmpty(sm_sms.SmsHead))
            {
                SqlParameter smsheadpara = new SqlParameter("@SmsHead", SqlDbType.VarChar, 80);
                smsheadpara.Value = sm_sms.SmsHead;
                paras.Add(smsheadpara);
            }

            if (!string.IsNullOrEmpty(sm_sms.SmsBody))
            {
                SqlParameter smsbodypara = new SqlParameter("@SmsBody", SqlDbType.VarChar, 200);
                smsbodypara.Value = sm_sms.SmsBody;
                paras.Add(smsbodypara);
            }

            SqlParameter smsreltimepara = new SqlParameter("@SmsRelTime", SqlDbType.DateTime, 8);
            smsreltimepara.Value = sm_sms.SmsRelTime;
            paras.Add(smsreltimepara);

            SqlParameter smsstatuspara = new SqlParameter("@SmsStatus", SqlDbType.Int, 4);
            smsstatuspara.Value = (int)SmsStatusEnum.待处理消息;
            paras.Add(smsstatuspara);

            SqlParameter smslevelpara = new SqlParameter("@SmsLevel", SqlDbType.Int, 4);
            smslevelpara.Value = sm_sms.SmsLevel;
            paras.Add(smslevelpara);

            SqlParameter sourceidpara = new SqlParameter("@SourceId", SqlDbType.Int, 4);
            sourceidpara.Value = sm_sms.SourceId;
            paras.Add(sourceidpara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetCurrentSms(UserModel user)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select s.SmsId,s.SmsHead,s.SmsBody,s.SmsRelTime,s.SmsStatus,s.SmsLevel,st.TypeName,st.ListUrl,st.ViewUrl,s.SourceId from dbo.Sm_Sms s left join dbo.Sm_SmsType st on s.SmsTypeId = st.SmsTypeId left join dbo.Sm_SmsDetail sd on s.SmsId = sd.SmsId where s.SmsStatus = {0} and st.SmsTypeStatus = {1} and sd.EmpId = {2}", (int)SmsStatusEnum.待处理消息, (int)Common.StatusEnum.已生效, user.EmpId);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                    result.Message = "获取成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel ReadSms(UserModel user, string smsIds)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Sm_Sms set SmsStatus = {0} where SmsId in ({1}) ; update dbo.Sm_SmsDetail set ReadTime = getdate() where SmsId in ({2}) and EmpId = {3}", (int)SmsStatusEnum.已处理消息, smsIds, smsIds, user.EmpId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "更新成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "更新失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        /// <summary>
        /// 添加消息(此方法无事务)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="sms"></param>
        /// <param name="smsDetails"></param>
        /// <returns></returns>
        public ResultModel AddSms(UserModel user, Model.Sms sms, List<Model.SmsDetail> smsDetails)
        {
            ResultModel result = new ResultModel();
            DAL.SmsDetailDAL smsDetailDAL = new SmsDetailDAL();

            try
            {
                result = this.Insert(user, sms);
                if (result.ResultStatus != 0)
                    return result;

                int smsId = (int)result.ReturnValue;

                if (smsDetails != null && smsDetails.Count > 0)
                {
                    foreach (Model.SmsDetail detail in smsDetails)
                    {
                        detail.SmsId = smsId;
                        result = smsDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                }
                if (result.ResultStatus == 0)
                    result.Message = "消息添加成功";
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public override int MenuId
        {
            get
            {
                return 89;
            }
        }

        #endregion
    }
}
