/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsReadDAL.cs
// 文件功能描述：消息已读dbo.Sm_SmsRead数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月9日
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
    /// 消息已读dbo.Sm_SmsRead数据交互类。
    /// </summary>
    public class SmsReadDAL : DataOperate, ISmsReadDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SmsReadDAL()
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

        /// <summary>
        /// 新增sm_smsread信息
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">SmsRead对象</param>
        /// <returns></returns>
        public override ResultModel Insert(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();
            try
            {
                SmsRead sm_smsread = (SmsRead)obj;

                if (sm_smsread == null)
                {
                    result.Message = "新增对象不能为null";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();
                SqlParameter smsreadidpara = new SqlParameter();
                smsreadidpara.Direction = ParameterDirection.Output;
                smsreadidpara.SqlDbType = SqlDbType.Int;
                smsreadidpara.ParameterName = "@SmsReadId";
                smsreadidpara.Size = 4;
                paras.Add(smsreadidpara);

                SqlParameter smsidpara = new SqlParameter("@SmsId", SqlDbType.Int, 4);
                smsidpara.Value = sm_smsread.SmsId;
                paras.Add(smsidpara);

                SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
                empidpara.Value = sm_smsread.EmpId;
                paras.Add(empidpara);

                SqlParameter lastreadtimepara = new SqlParameter("@LastReadTime", SqlDbType.DateTime, 8);
                lastreadtimepara.Value = sm_smsread.LastReadTime;
                paras.Add(lastreadtimepara);

                SqlParameter readstatuspara = new SqlParameter("@ReadStatus", SqlDbType.Int, 4);
                readstatuspara.Value = sm_smsread.ReadStatus;
                paras.Add(readstatuspara);


                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, "Sm_SmsReadInsert", paras.ToArray());

                if (i == 1)
                {
                    result.AffectCount = i;
                    result.ResultStatus = 0;
                    result.Message = "SmsRead添加成功";
                    result.ReturnValue = smsreadidpara.Value;
                }
                else
                    result.Message = "SmsRead添加失败";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 获取指定smsReadId的sm_smsread对象
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="smsReadId">主键值</param>
        /// <returns></returns>
        public override ResultModel Get(UserModel user, int smsReadId)
        {
            ResultModel result = new ResultModel();

            if (smsReadId < 1)
            {
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@SmsReadId", SqlDbType.Int, 4);
            para.Value = smsReadId;
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.StoredProcedure, "Sm_SmsReadGet", paras.ToArray());

                SmsRead smsread = new SmsRead();

                if (dr.Read())
                {
                    int indexSmsReadId = dr.GetOrdinal("SmsReadId");
                    smsread.SmsReadId = Convert.ToInt32(dr[indexSmsReadId]);

                    int indexSmsId = dr.GetOrdinal("SmsId");
                    if (dr["SmsId"] != DBNull.Value)
                    {
                        smsread.SmsId = Convert.ToInt32(dr[indexSmsId]);
                    }

                    int indexEmpId = dr.GetOrdinal("EmpId");
                    if (dr["EmpId"] != DBNull.Value)
                    {
                        smsread.EmpId = Convert.ToInt32(dr[indexEmpId]);
                    }

                    int indexLastReadTime = dr.GetOrdinal("LastReadTime");
                    if (dr["LastReadTime"] != DBNull.Value)
                    {
                        smsread.LastReadTime = Convert.ToDateTime(dr[indexLastReadTime]);
                    }

                    int indexReadStatus = dr.GetOrdinal("ReadStatus");
                    if (dr["ReadStatus"] != DBNull.Value)
                    {
                        smsread.ReadStatus = Convert.ToInt32(dr[indexReadStatus]);
                    }

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = smsread;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
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

        /// <summary>
        /// 获取sm_smsread集合
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <returns></returns>
        public override ResultModel Load(UserModel user)
        {
            ResultModel result = new ResultModel();
            try
            {
                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, "Sm_SmsReadLoad", null, CommandType.StoredProcedure);

                List<SmsRead> smsReads = new List<SmsRead>();

                foreach (DataRow dr in dt.Rows)
                {
                    SmsRead smsread = new SmsRead();
                    smsread.SmsReadId = Convert.ToInt32(dr["SmsReadId"]);

                    if (dr["SmsId"] != DBNull.Value)
                    {
                        smsread.SmsId = Convert.ToInt32(dr["SmsId"]);
                    }
                    if (dr["EmpId"] != DBNull.Value)
                    {
                        smsread.EmpId = Convert.ToInt32(dr["EmpId"]);
                    }
                    if (dr["LastReadTime"] != DBNull.Value)
                    {
                        smsread.LastReadTime = Convert.ToDateTime(dr["LastReadTime"]);
                    }
                    if (dr["ReadStatus"] != DBNull.Value)
                    {
                        smsread.ReadStatus = Convert.ToInt32(dr["ReadStatus"]);
                    }
                    smsReads.Add(smsread);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = smsReads;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SmsRead sm_smsread = (SmsRead)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter smsreadidpara = new SqlParameter("@SmsReadId", SqlDbType.Int, 4);
            smsreadidpara.Value = sm_smsread.SmsReadId;
            paras.Add(smsreadidpara);

            SqlParameter smsidpara = new SqlParameter("@SmsId", SqlDbType.Int, 4);
            smsidpara.Value = sm_smsread.SmsId;
            paras.Add(smsidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = sm_smsread.EmpId;
            paras.Add(empidpara);

            SqlParameter lastreadtimepara = new SqlParameter("@LastReadTime", SqlDbType.DateTime, 8);
            lastreadtimepara.Value = sm_smsread.LastReadTime;
            paras.Add(lastreadtimepara);

            SqlParameter readstatuspara = new SqlParameter("@ReadStatus", SqlDbType.Int, 4);
            readstatuspara.Value = sm_smsread.ReadStatus;
            paras.Add(readstatuspara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel ReadSms(UserModel user, int smsId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Sm_SmsRead set ReadStatus = {0} where SmsId = {1}", NFMT.Data.DetailProvider.Details(Data.StyleEnum.已读状态)["read"].StyleDetailId, smsId);
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

        #endregion
    }
}
