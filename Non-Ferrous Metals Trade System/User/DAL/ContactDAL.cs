/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContactDAL.cs
// 文件功能描述：联系人dbo.Contact数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.User.Model;
using NFMT.DBUtility;
using NFMT.User.IDAL;
using NFMT.Common;

namespace NFMT.User.DAL
{
    /// <summary>
    /// 联系人dbo.Contact数据交互类。
    /// </summary>
    public class ContactDAL : DataOperate, IContactDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContactDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringUser;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            Contact contact = (Contact)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ContactId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter contactnamepara = new SqlParameter("@ContactName", SqlDbType.VarChar, 80);
            contactnamepara.Value = contact.ContactName;
            paras.Add(contactnamepara);

            if (!string.IsNullOrEmpty(contact.ContactCode))
            {
                SqlParameter contactcodepara = new SqlParameter("@ContactCode", SqlDbType.VarChar, 80);
                contactcodepara.Value = contact.ContactCode;
                paras.Add(contactcodepara);
            }

            if (!string.IsNullOrEmpty(contact.ContactTel))
            {
                SqlParameter contacttelpara = new SqlParameter("@ContactTel", SqlDbType.VarChar, 80);
                contacttelpara.Value = contact.ContactTel;
                paras.Add(contacttelpara);
            }

            if (!string.IsNullOrEmpty(contact.ContactFax))
            {
                SqlParameter contactfaxpara = new SqlParameter("@ContactFax", SqlDbType.VarChar, 80);
                contactfaxpara.Value = contact.ContactFax;
                paras.Add(contactfaxpara);
            }

            if (!string.IsNullOrEmpty(contact.ContactAddress))
            {
                SqlParameter contactaddresspara = new SqlParameter("@ContactAddress", SqlDbType.VarChar, 400);
                contactaddresspara.Value = contact.ContactAddress;
                paras.Add(contactaddresspara);
            }

            SqlParameter companyidpara = new SqlParameter("@CompanyId", SqlDbType.Int, 4);
            companyidpara.Value = contact.CompanyId;
            paras.Add(companyidpara);

            SqlParameter contactstatuspara = new SqlParameter("@ContactStatus", SqlDbType.Int, 4);
            contactstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(contactstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Contact contact = new Contact();

            int indexContactId = dr.GetOrdinal("ContactId");
            contact.ContactId = Convert.ToInt32(dr[indexContactId]);

            int indexContactName = dr.GetOrdinal("ContactName");
            contact.ContactName = Convert.ToString(dr[indexContactName]);

            int indexContactCode = dr.GetOrdinal("ContactCode");
            if (dr["ContactCode"] != DBNull.Value)
            {
                contact.ContactCode = Convert.ToString(dr[indexContactCode]);
            }

            int indexContactTel = dr.GetOrdinal("ContactTel");
            if (dr["ContactTel"] != DBNull.Value)
            {
                contact.ContactTel = Convert.ToString(dr[indexContactTel]);
            }

            int indexContactFax = dr.GetOrdinal("ContactFax");
            if (dr["ContactFax"] != DBNull.Value)
            {
                contact.ContactFax = Convert.ToString(dr[indexContactFax]);
            }

            int indexContactAddress = dr.GetOrdinal("ContactAddress");
            if (dr["ContactAddress"] != DBNull.Value)
            {
                contact.ContactAddress = Convert.ToString(dr[indexContactAddress]);
            }

            int indexCompanyId = dr.GetOrdinal("CompanyId");
            if (dr["CompanyId"] != DBNull.Value)
            {
                contact.CompanyId = Convert.ToInt32(dr[indexCompanyId]);
            }

            int indexContactStatus = dr.GetOrdinal("ContactStatus");
            if (dr["ContactStatus"] != DBNull.Value)
            {
                contact.ContactStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexContactStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            contact.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            contact.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                contact.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                contact.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return contact;
        }

        public override string TableName
        {
            get
            {
                return "Contact";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Contact contact = (Contact)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter contactidpara = new SqlParameter("@ContactId", SqlDbType.Int, 4);
            contactidpara.Value = contact.ContactId;
            paras.Add(contactidpara);

            SqlParameter contactnamepara = new SqlParameter("@ContactName", SqlDbType.VarChar, 80);
            contactnamepara.Value = contact.ContactName;
            paras.Add(contactnamepara);

            if (!string.IsNullOrEmpty(contact.ContactCode))
            {
                SqlParameter contactcodepara = new SqlParameter("@ContactCode", SqlDbType.VarChar, 80);
                contactcodepara.Value = contact.ContactCode;
                paras.Add(contactcodepara);
            }

            if (!string.IsNullOrEmpty(contact.ContactTel))
            {
                SqlParameter contacttelpara = new SqlParameter("@ContactTel", SqlDbType.VarChar, 80);
                contacttelpara.Value = contact.ContactTel;
                paras.Add(contacttelpara);
            }

            if (!string.IsNullOrEmpty(contact.ContactFax))
            {
                SqlParameter contactfaxpara = new SqlParameter("@ContactFax", SqlDbType.VarChar, 80);
                contactfaxpara.Value = contact.ContactFax;
                paras.Add(contactfaxpara);
            }

            if (!string.IsNullOrEmpty(contact.ContactAddress))
            {
                SqlParameter contactaddresspara = new SqlParameter("@ContactAddress", SqlDbType.VarChar, 400);
                contactaddresspara.Value = contact.ContactAddress;
                paras.Add(contactaddresspara);
            }

            SqlParameter companyidpara = new SqlParameter("@CompanyId", SqlDbType.Int, 4);
            companyidpara.Value = contact.CompanyId;
            paras.Add(companyidpara);

            SqlParameter contactstatuspara = new SqlParameter("@ContactStatus", SqlDbType.Int, 4);
            contactstatuspara.Value = contact.ContactStatus;
            paras.Add(contactstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public override int MenuId
        {
            get
            {
                return 20;
            }
        }

        #endregion
    }
}
