/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CustomsAttachDAL.cs
// 文件功能描述：报关附件dbo.St_CustomsAttach数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月29日
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
    /// 报关附件dbo.St_CustomsAttach数据交互类。
    /// </summary>
    public class CustomsAttachDAL : DataOperate , ICustomsAttachDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public CustomsAttachDAL()
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
            CustomsAttach st_customsattach = (CustomsAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
                          returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName ="@CustomsAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

			    SqlParameter customsidpara = new SqlParameter("@CustomsId",SqlDbType.Int,4);
            customsidpara.Value = st_customsattach.CustomsId;
            paras.Add(customsidpara);

			    SqlParameter attachidpara = new SqlParameter("@AttachId",SqlDbType.Int,4);
            attachidpara.Value = st_customsattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }
        
		public override IModel CreateModel(DataRow dr)
        {
            CustomsAttach customsattach = new CustomsAttach();

                    customsattach.CustomsAttachId = Convert.ToInt32(dr["CustomsAttachId"]);
                    
                    if(dr["CustomsId"] != DBNull.Value)
                    {
                    customsattach.CustomsId = Convert.ToInt32(dr["CustomsId"]);
                    }
                    
                    if(dr["AttachId"] != DBNull.Value)
                    {
                    customsattach.AttachId = Convert.ToInt32(dr["AttachId"]);
                    }
                    

            return customsattach;
        }
        
        public override IModel CreateModel(SqlDataReader dr)
        {
            CustomsAttach customsattach = new CustomsAttach();

                    int indexCustomsAttachId = dr.GetOrdinal("CustomsAttachId");
                    customsattach.CustomsAttachId = Convert.ToInt32(dr[indexCustomsAttachId]);
                    
                    int indexCustomsId = dr.GetOrdinal("CustomsId");
                    if(dr["CustomsId"] != DBNull.Value)
                    {
                    customsattach.CustomsId = Convert.ToInt32(dr[indexCustomsId]);
                    }
                    
                    int indexAttachId = dr.GetOrdinal("AttachId");
                    if(dr["AttachId"] != DBNull.Value)
                    {
                    customsattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
                    }
                    

            return customsattach;
        }

        public override string TableName
        {
            get
            {
                return "St_CustomsAttach";
            }
        }
		
        public override List<SqlParameter> CreateUpdateParameters(IModel obj) 
        { 
            CustomsAttach st_customsattach = (CustomsAttach)obj;
            
            List<SqlParameter> paras = new List<SqlParameter>();
                
		    SqlParameter customsattachidpara = new SqlParameter("@CustomsAttachId",SqlDbType.Int,4);
            customsattachidpara.Value = st_customsattach.CustomsAttachId;
            paras.Add(customsattachidpara);

		    SqlParameter customsidpara = new SqlParameter("@CustomsId",SqlDbType.Int,4);
            customsidpara.Value = st_customsattach.CustomsId;
            paras.Add(customsidpara);

		    SqlParameter attachidpara = new SqlParameter("@AttachId",SqlDbType.Int,4);
            attachidpara.Value = st_customsattach.AttachId;
            paras.Add(attachidpara);

             
             return paras;
        }    
        
        #endregion
    }
}
