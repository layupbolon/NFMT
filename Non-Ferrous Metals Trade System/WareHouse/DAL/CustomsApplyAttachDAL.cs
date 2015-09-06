/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CustomsApplyAttachDAL.cs
// 文件功能描述：报关申请附件dbo.St_CustomsApplyAttach数据交互类。
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
    /// 报关申请附件dbo.St_CustomsApplyAttach数据交互类。
    /// </summary>
    public class CustomsApplyAttachDAL : DataOperate , ICustomsApplyAttachDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public CustomsApplyAttachDAL()
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
            CustomsApplyAttach st_customsapplyattach = (CustomsApplyAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
                          returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName ="@CustomsApplyAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

			    SqlParameter customsapplyidpara = new SqlParameter("@CustomsApplyId",SqlDbType.Int,4);
            customsapplyidpara.Value = st_customsapplyattach.CustomsApplyId;
            paras.Add(customsapplyidpara);

			    SqlParameter attachidpara = new SqlParameter("@AttachId",SqlDbType.Int,4);
            attachidpara.Value = st_customsapplyattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }
        
		public override IModel CreateModel(DataRow dr)
        {
            CustomsApplyAttach customsapplyattach = new CustomsApplyAttach();

                    customsapplyattach.CustomsApplyAttachId = Convert.ToInt32(dr["CustomsApplyAttachId"]);
                    
                    if(dr["CustomsApplyId"] != DBNull.Value)
                    {
                    customsapplyattach.CustomsApplyId = Convert.ToInt32(dr["CustomsApplyId"]);
                    }
                    
                    if(dr["AttachId"] != DBNull.Value)
                    {
                    customsapplyattach.AttachId = Convert.ToInt32(dr["AttachId"]);
                    }
                    

            return customsapplyattach;
        }
        
        public override IModel CreateModel(SqlDataReader dr)
        {
            CustomsApplyAttach customsapplyattach = new CustomsApplyAttach();

                    int indexCustomsApplyAttachId = dr.GetOrdinal("CustomsApplyAttachId");
                    customsapplyattach.CustomsApplyAttachId = Convert.ToInt32(dr[indexCustomsApplyAttachId]);
                    
                    int indexCustomsApplyId = dr.GetOrdinal("CustomsApplyId");
                    if(dr["CustomsApplyId"] != DBNull.Value)
                    {
                    customsapplyattach.CustomsApplyId = Convert.ToInt32(dr[indexCustomsApplyId]);
                    }
                    
                    int indexAttachId = dr.GetOrdinal("AttachId");
                    if(dr["AttachId"] != DBNull.Value)
                    {
                    customsapplyattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
                    }
                    

            return customsapplyattach;
        }

        public override string TableName
        {
            get
            {
                return "St_CustomsApplyAttach";
            }
        }
		
        public override List<SqlParameter> CreateUpdateParameters(IModel obj) 
        { 
            CustomsApplyAttach st_customsapplyattach = (CustomsApplyAttach)obj;
            
            List<SqlParameter> paras = new List<SqlParameter>();
                
		    SqlParameter customsapplyattachidpara = new SqlParameter("@CustomsApplyAttachId",SqlDbType.Int,4);
            customsapplyattachidpara.Value = st_customsapplyattach.CustomsApplyAttachId;
            paras.Add(customsapplyattachidpara);

		    SqlParameter customsapplyidpara = new SqlParameter("@CustomsApplyId",SqlDbType.Int,4);
            customsapplyidpara.Value = st_customsapplyattach.CustomsApplyId;
            paras.Add(customsapplyidpara);

		    SqlParameter attachidpara = new SqlParameter("@AttachId",SqlDbType.Int,4);
            attachidpara.Value = st_customsapplyattach.AttachId;
            paras.Add(attachidpara);

             
             return paras;
        }    
        
        #endregion
    }
}
