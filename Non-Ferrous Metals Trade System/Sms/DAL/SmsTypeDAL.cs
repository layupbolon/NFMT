/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsTypeDAL.cs
// 文件功能描述：消息类型dbo.Sm_SmsType数据交互类。
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
    /// 消息类型dbo.Sm_SmsType数据交互类。
    /// </summary>
    public class SmsTypeDAL : DataOperate, ISmsTypeDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SmsTypeDAL()
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
            SmsType sm_smstype = (SmsType)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@SmsTypeId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(sm_smstype.TypeName))
            {
                SqlParameter typenamepara = new SqlParameter("@TypeName", SqlDbType.VarChar, 200);
                typenamepara.Value = sm_smstype.TypeName;
                paras.Add(typenamepara);
            }

            if (!string.IsNullOrEmpty(sm_smstype.ListUrl))
            {
                SqlParameter listurlpara = new SqlParameter("@ListUrl", SqlDbType.VarChar, 200);
                listurlpara.Value = sm_smstype.ListUrl;
                paras.Add(listurlpara);
            }

            if (!string.IsNullOrEmpty(sm_smstype.ViewUrl))
            {
                SqlParameter viewurlpara = new SqlParameter("@ViewUrl", SqlDbType.VarChar, 200);
                viewurlpara.Value = sm_smstype.ViewUrl;
                paras.Add(viewurlpara);
            }

            SqlParameter smstypestatuspara = new SqlParameter("@SmsTypeStatus", SqlDbType.Int, 4);
            smstypestatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(smstypestatuspara);

            if (!string.IsNullOrEmpty(sm_smstype.SourceBaseName))
            {
                SqlParameter sourcebasenamepara = new SqlParameter("@SourceBaseName", SqlDbType.VarChar, 50);
                sourcebasenamepara.Value = sm_smstype.SourceBaseName;
                paras.Add(sourcebasenamepara);
            }

            if (!string.IsNullOrEmpty(sm_smstype.SourceTableName))
            {
                SqlParameter sourcetablenamepara = new SqlParameter("@SourceTableName", SqlDbType.VarChar, 50);
                sourcetablenamepara.Value = sm_smstype.SourceTableName;
                paras.Add(sourcetablenamepara);
            }


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            SmsType smstype = new SmsType();

            int indexSmsTypeId = dr.GetOrdinal("SmsTypeId");
            smstype.SmsTypeId = Convert.ToInt32(dr[indexSmsTypeId]);

            int indexTypeName = dr.GetOrdinal("TypeName");
            if (dr["TypeName"] != DBNull.Value)
            {
                smstype.TypeName = Convert.ToString(dr[indexTypeName]);
            }

            int indexListUrl = dr.GetOrdinal("ListUrl");
            if (dr["ListUrl"] != DBNull.Value)
            {
                smstype.ListUrl = Convert.ToString(dr[indexListUrl]);
            }

            int indexViewUrl = dr.GetOrdinal("ViewUrl");
            if (dr["ViewUrl"] != DBNull.Value)
            {
                smstype.ViewUrl = Convert.ToString(dr[indexViewUrl]);
            }

            int indexSmsTypeStatus = dr.GetOrdinal("SmsTypeStatus");
            if (dr["SmsTypeStatus"] != DBNull.Value)
            {
                smstype.SmsTypeStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexSmsTypeStatus]);
            }

            int indexSourceBaseName = dr.GetOrdinal("SourceBaseName");
            if (dr["SourceBaseName"] != DBNull.Value)
            {
                smstype.SourceBaseName = Convert.ToString(dr[indexSourceBaseName]);
            }

            int indexSourceTableName = dr.GetOrdinal("SourceTableName");
            if (dr["SourceTableName"] != DBNull.Value)
            {
                smstype.SourceTableName = Convert.ToString(dr[indexSourceTableName]);
            }


            return smstype;
        }

        public override string TableName
        {
            get
            {
                return "Sm_SmsType";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SmsType sm_smstype = (SmsType)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter smstypeidpara = new SqlParameter("@SmsTypeId", SqlDbType.Int, 4);
            smstypeidpara.Value = sm_smstype.SmsTypeId;
            paras.Add(smstypeidpara);

            if (!string.IsNullOrEmpty(sm_smstype.TypeName))
            {
                SqlParameter typenamepara = new SqlParameter("@TypeName", SqlDbType.VarChar, 200);
                typenamepara.Value = sm_smstype.TypeName;
                paras.Add(typenamepara);
            }

            if (!string.IsNullOrEmpty(sm_smstype.ListUrl))
            {
                SqlParameter listurlpara = new SqlParameter("@ListUrl", SqlDbType.VarChar, 200);
                listurlpara.Value = sm_smstype.ListUrl;
                paras.Add(listurlpara);
            }

            if (!string.IsNullOrEmpty(sm_smstype.ViewUrl))
            {
                SqlParameter viewurlpara = new SqlParameter("@ViewUrl", SqlDbType.VarChar, 200);
                viewurlpara.Value = sm_smstype.ViewUrl;
                paras.Add(viewurlpara);
            }

            SqlParameter smstypestatuspara = new SqlParameter("@SmsTypeStatus", SqlDbType.Int, 4);
            smstypestatuspara.Value = sm_smstype.SmsTypeStatus;
            paras.Add(smstypestatuspara);

            if (!string.IsNullOrEmpty(sm_smstype.SourceBaseName))
            {
                SqlParameter sourcebasenamepara = new SqlParameter("@SourceBaseName", SqlDbType.VarChar, 50);
                sourcebasenamepara.Value = sm_smstype.SourceBaseName;
                paras.Add(sourcebasenamepara);
            }

            if (!string.IsNullOrEmpty(sm_smstype.SourceTableName))
            {
                SqlParameter sourcetablenamepara = new SqlParameter("@SourceTableName", SqlDbType.VarChar, 50);
                sourcetablenamepara.Value = sm_smstype.SourceTableName;
                paras.Add(sourcetablenamepara);
            }


            return paras;
        }

        #endregion

        #region 重载方法

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "更新对象不能为null";
                    return result;
                }

                obj.LastModifyId = user.EmpId;

                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, string.Format("{0}Update", obj.TableName), CreateUpdateParameters(obj).ToArray());

                if (i == 1)
                {
                    result.Message = "更新成功";
                    result.ResultStatus = 0;
                }
                else
                    result.Message = "更新失败";

                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        #endregion

        #region 新增方法

        public ResultModel InsertOrGet(UserModel user, int masterId)
        {
            ResultModel result = new ResultModel();
            int readyStatus = (int)Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" declare @viewUrl varchar(200) ");
            sb.Append(Environment.NewLine);
            sb.AppendFormat(" select @viewUrl = RefuseUrl from NFMT_WorkFlow.dbo.Wf_FlowMasterConfig where MasterId = {0} and ConfigStatus = {1} ", masterId, readyStatus);
            sb.Append(Environment.NewLine);
            sb.AppendFormat(" if exists(select 1 from NFMT_Sms.dbo.Sm_SmsType where ViewUrl = @viewUrl and SmsTypeStatus = {0}) ", readyStatus);
            sb.Append(Environment.NewLine);
            sb.Append(" begin ");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	select top 1 SmsTypeId from NFMT_Sms.dbo.Sm_SmsType where ViewUrl = @viewUrl and SmsTypeStatus = {0} ", readyStatus);
            sb.Append(Environment.NewLine);
            sb.Append(" end else begin ");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("INSERT INTO [NFMT_Sms].[dbo].[Sm_SmsType]([TypeName],[ListUrl],[ViewUrl],[SmsTypeStatus],[SourceBaseName],[SourceTableName])VALUES('任务退回提醒','MainForm.aspx',@viewUrl,{0},'','')", readyStatus);
            sb.Append(Environment.NewLine);
            sb.Append("select @@IDENTITY end");

            try
            {
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sb.ToString(), null);
                int smsTypeId = 0;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && int.TryParse(obj.ToString(), out smsTypeId))
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = smsTypeId;
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


        #endregion
    }
}
