/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ProducerDAL.cs
// 文件功能描述：生产商dbo.Producer数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月16日
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
    /// 生产商dbo.Producer数据交互类。
    /// </summary>
    public class ProducerDAL : DataOperate, IProducerDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ProducerDAL()
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
            Producer producer = (Producer)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ProducerId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(producer.ProducerName))
            {
                SqlParameter producernamepara = new SqlParameter("@ProducerName", SqlDbType.VarChar, 80);
                producernamepara.Value = producer.ProducerName;
                paras.Add(producernamepara);
            }

            if (!string.IsNullOrEmpty(producer.ProducerFullName))
            {
                SqlParameter producerfullnamepara = new SqlParameter("@ProducerFullName", SqlDbType.VarChar, 400);
                producerfullnamepara.Value = producer.ProducerFullName;
                paras.Add(producerfullnamepara);
            }

            if (!string.IsNullOrEmpty(producer.ProducerShort))
            {
                SqlParameter producershortpara = new SqlParameter("@ProducerShort", SqlDbType.VarChar, 80);
                producershortpara.Value = producer.ProducerShort;
                paras.Add(producershortpara);
            }

            SqlParameter producerareapara = new SqlParameter("@ProducerArea", SqlDbType.Int, 4);
            producerareapara.Value = producer.ProducerArea;
            paras.Add(producerareapara);

            SqlParameter producerstatuspara = new SqlParameter("@ProducerStatus", SqlDbType.Int, 4);
            producerstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(producerstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Producer producer = new Producer();

            int indexProducerId = dr.GetOrdinal("ProducerId");
            producer.ProducerId = Convert.ToInt32(dr[indexProducerId]);

            int indexProducerName = dr.GetOrdinal("ProducerName");
            if (dr["ProducerName"] != DBNull.Value)
            {
                producer.ProducerName = Convert.ToString(dr[indexProducerName]);
            }

            int indexProducerFullName = dr.GetOrdinal("ProducerFullName");
            if (dr["ProducerFullName"] != DBNull.Value)
            {
                producer.ProducerFullName = Convert.ToString(dr[indexProducerFullName]);
            }

            int indexProducerShort = dr.GetOrdinal("ProducerShort");
            if (dr["ProducerShort"] != DBNull.Value)
            {
                producer.ProducerShort = Convert.ToString(dr[indexProducerShort]);
            }

            int indexProducerArea = dr.GetOrdinal("ProducerArea");
            if (dr["ProducerArea"] != DBNull.Value)
            {
                producer.ProducerArea = Convert.ToInt32(dr[indexProducerArea]);
            }

            int indexProducerStatus = dr.GetOrdinal("ProducerStatus");
            if (dr["ProducerStatus"] != DBNull.Value)
            {
                producer.ProducerStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexProducerStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                producer.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                producer.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                producer.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                producer.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return producer;
        }

        public override string TableName
        {
            get
            {
                return "Producer";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Producer producer = (Producer)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter produceridpara = new SqlParameter("@ProducerId", SqlDbType.Int, 4);
            produceridpara.Value = producer.ProducerId;
            paras.Add(produceridpara);

            if (!string.IsNullOrEmpty(producer.ProducerName))
            {
                SqlParameter producernamepara = new SqlParameter("@ProducerName", SqlDbType.VarChar, 80);
                producernamepara.Value = producer.ProducerName;
                paras.Add(producernamepara);
            }

            if (!string.IsNullOrEmpty(producer.ProducerFullName))
            {
                SqlParameter producerfullnamepara = new SqlParameter("@ProducerFullName", SqlDbType.VarChar, 400);
                producerfullnamepara.Value = producer.ProducerFullName;
                paras.Add(producerfullnamepara);
            }

            if (!string.IsNullOrEmpty(producer.ProducerShort))
            {
                SqlParameter producershortpara = new SqlParameter("@ProducerShort", SqlDbType.VarChar, 80);
                producershortpara.Value = producer.ProducerShort;
                paras.Add(producershortpara);
            }

            SqlParameter producerareapara = new SqlParameter("@ProducerArea", SqlDbType.Int, 4);
            producerareapara.Value = producer.ProducerArea;
            paras.Add(producerareapara);

            SqlParameter producerstatuspara = new SqlParameter("@ProducerStatus", SqlDbType.Int, 4);
            producerstatuspara.Value = producer.ProducerStatus;
            paras.Add(producerstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


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

        public override int MenuId
        {
            get
            {
                return 31;
            }
        }

        #endregion
    }
}
