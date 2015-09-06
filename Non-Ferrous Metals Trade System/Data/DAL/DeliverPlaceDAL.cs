/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DeliverPlaceDAL.cs
// 文件功能描述：交货地dbo.DeliverPlace数据交互类。
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
    /// 交货地dbo.DeliverPlace数据交互类。
    /// </summary>
    public class DeliverPlaceDAL : DataOperate, IDeliverPlaceDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeliverPlaceDAL()
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
            DeliverPlace deliverplace = (DeliverPlace)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DPId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter dptypepara = new SqlParameter("@DPType", SqlDbType.Int, 4);
            dptypepara.Value = deliverplace.DPType;
            paras.Add(dptypepara);

            SqlParameter dpareapara = new SqlParameter("@DPArea", SqlDbType.Int, 4);
            dpareapara.Value = deliverplace.DPArea;
            paras.Add(dpareapara);

            SqlParameter dpcompanypara = new SqlParameter("@DPCompany", SqlDbType.Int, 4);
            dpcompanypara.Value = deliverplace.DPCompany;
            paras.Add(dpcompanypara);

            SqlParameter dpnamepara = new SqlParameter("@DPName", SqlDbType.VarChar, 80);
            dpnamepara.Value = deliverplace.DPName;
            paras.Add(dpnamepara);

            if (!string.IsNullOrEmpty(deliverplace.DPFullName))
            {
                SqlParameter dpfullnamepara = new SqlParameter("@DPFullName", SqlDbType.VarChar, 400);
                dpfullnamepara.Value = deliverplace.DPFullName;
                paras.Add(dpfullnamepara);
            }

            SqlParameter dpstatuspara = new SqlParameter("@DPStatus", SqlDbType.Int, 4);
            dpstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(dpstatuspara);

            SqlParameter dpaddresspara = new SqlParameter("@DPAddress", SqlDbType.VarChar, 400);
            dpaddresspara.Value = deliverplace.DPAddress;
            paras.Add(dpaddresspara);

            if (!string.IsNullOrEmpty(deliverplace.DPEAddress))
            {
                SqlParameter dpeaddresspara = new SqlParameter("@DPEAddress", SqlDbType.VarChar, 400);
                dpeaddresspara.Value = deliverplace.DPEAddress;
                paras.Add(dpeaddresspara);
            }

            if (!string.IsNullOrEmpty(deliverplace.DPTel))
            {
                SqlParameter dptelpara = new SqlParameter("@DPTel", SqlDbType.VarChar, 80);
                dptelpara.Value = deliverplace.DPTel;
                paras.Add(dptelpara);
            }

            if (!string.IsNullOrEmpty(deliverplace.DPContact))
            {
                SqlParameter dpcontactpara = new SqlParameter("@DPContact", SqlDbType.VarChar, 80);
                dpcontactpara.Value = deliverplace.DPContact;
                paras.Add(dpcontactpara);
            }

            if (!string.IsNullOrEmpty(deliverplace.DPFax))
            {
                SqlParameter dpfaxpara = new SqlParameter("@DPFax", SqlDbType.VarChar, 80);
                dpfaxpara.Value = deliverplace.DPFax;
                paras.Add(dpfaxpara);
            }

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            DeliverPlace deliverplace = new DeliverPlace();

            int indexDPId = dr.GetOrdinal("DPId");
            deliverplace.DPId = Convert.ToInt32(dr[indexDPId]);

            int indexDPType = dr.GetOrdinal("DPType");
            deliverplace.DPType = Convert.ToInt32(dr[indexDPType]);

            int indexDPArea = dr.GetOrdinal("DPArea");
            deliverplace.DPArea = Convert.ToInt32(dr[indexDPArea]);

            int indexDPCompany = dr.GetOrdinal("DPCompany");
            if (dr["DPCompany"] != DBNull.Value)
            {
                deliverplace.DPCompany = Convert.ToInt32(dr[indexDPCompany]);
            }

            int indexDPName = dr.GetOrdinal("DPName");
            deliverplace.DPName = Convert.ToString(dr[indexDPName]);

            int indexDPFullName = dr.GetOrdinal("DPFullName");
            if (dr["DPFullName"] != DBNull.Value)
            {
                deliverplace.DPFullName = Convert.ToString(dr[indexDPFullName]);
            }

            int indexDPStatus = dr.GetOrdinal("DPStatus");
            deliverplace.DPStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDPStatus]);

            int indexDPAddress = dr.GetOrdinal("DPAddress");
            deliverplace.DPAddress = Convert.ToString(dr[indexDPAddress]);

            int indexDPEAddress = dr.GetOrdinal("DPEAddress");
            if (dr["DPEAddress"] != DBNull.Value)
            {
                deliverplace.DPEAddress = Convert.ToString(dr[indexDPEAddress]);
            }

            int indexDPTel = dr.GetOrdinal("DPTel");
            if (dr["DPTel"] != DBNull.Value)
            {
                deliverplace.DPTel = Convert.ToString(dr[indexDPTel]);
            }

            int indexDPContact = dr.GetOrdinal("DPContact");
            if (dr["DPContact"] != DBNull.Value)
            {
                deliverplace.DPContact = Convert.ToString(dr[indexDPContact]);
            }

            int indexDPFax = dr.GetOrdinal("DPFax");
            if (dr["DPFax"] != DBNull.Value)
            {
                deliverplace.DPFax = Convert.ToString(dr[indexDPFax]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            deliverplace.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            deliverplace.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                deliverplace.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                deliverplace.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return deliverplace;
        }

        public override string TableName
        {
            get
            {
                return "DeliverPlace";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            DeliverPlace deliverplace = (DeliverPlace)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter dpidpara = new SqlParameter("@DPId", SqlDbType.Int, 4);
            dpidpara.Value = deliverplace.DPId;
            paras.Add(dpidpara);

            SqlParameter dptypepara = new SqlParameter("@DPType", SqlDbType.Int, 4);
            dptypepara.Value = deliverplace.DPType;
            paras.Add(dptypepara);

            SqlParameter dpareapara = new SqlParameter("@DPArea", SqlDbType.Int, 4);
            dpareapara.Value = deliverplace.DPArea;
            paras.Add(dpareapara);

            SqlParameter dpcompanypara = new SqlParameter("@DPCompany", SqlDbType.Int, 4);
            dpcompanypara.Value = deliverplace.DPCompany;
            paras.Add(dpcompanypara);

            SqlParameter dpnamepara = new SqlParameter("@DPName", SqlDbType.VarChar, 80);
            dpnamepara.Value = deliverplace.DPName;
            paras.Add(dpnamepara);

            if (!string.IsNullOrEmpty(deliverplace.DPFullName))
            {
                SqlParameter dpfullnamepara = new SqlParameter("@DPFullName", SqlDbType.VarChar, 400);
                dpfullnamepara.Value = deliverplace.DPFullName;
                paras.Add(dpfullnamepara);
            }

            SqlParameter dpstatuspara = new SqlParameter("@DPStatus", SqlDbType.Int, 4);
            dpstatuspara.Value = deliverplace.DPStatus;
            paras.Add(dpstatuspara);

            SqlParameter dpaddresspara = new SqlParameter("@DPAddress", SqlDbType.VarChar, 400);
            dpaddresspara.Value = deliverplace.DPAddress;
            paras.Add(dpaddresspara);

            if (!string.IsNullOrEmpty(deliverplace.DPEAddress))
            {
                SqlParameter dpeaddresspara = new SqlParameter("@DPEAddress", SqlDbType.VarChar, 400);
                dpeaddresspara.Value = deliverplace.DPEAddress;
                paras.Add(dpeaddresspara);
            }

            if (!string.IsNullOrEmpty(deliverplace.DPTel))
            {
                SqlParameter dptelpara = new SqlParameter("@DPTel", SqlDbType.VarChar, 80);
                dptelpara.Value = deliverplace.DPTel;
                paras.Add(dptelpara);
            }

            if (!string.IsNullOrEmpty(deliverplace.DPContact))
            {
                SqlParameter dpcontactpara = new SqlParameter("@DPContact", SqlDbType.VarChar, 80);
                dpcontactpara.Value = deliverplace.DPContact;
                paras.Add(dpcontactpara);
            }

            if (!string.IsNullOrEmpty(deliverplace.DPFax))
            {
                SqlParameter dpfaxpara = new SqlParameter("@DPFax", SqlDbType.VarChar, 80);
                dpfaxpara.Value = deliverplace.DPFax;
                paras.Add(dpfaxpara);
            }

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
                return 97;
            }
        }

        #endregion
    }
}
