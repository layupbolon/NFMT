/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SplitDocAttachDAL.cs
// 文件功能描述：拆单附件dbo.St_SplitDocAttach数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月27日
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
    /// 拆单附件dbo.St_SplitDocAttach数据交互类。
    /// </summary>
    public class SplitDocAttachDAL : DataOperate , ISplitDocAttachDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public SplitDocAttachDAL()
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
            SplitDocAttach st_splitdocattach = (SplitDocAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
                          returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName ="@SplitDocAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

			    SqlParameter splitdocidpara = new SqlParameter("@SplitDocId",SqlDbType.Int,4);
            splitdocidpara.Value = st_splitdocattach.SplitDocId;
            paras.Add(splitdocidpara);

			    SqlParameter splitdocdetailidpara = new SqlParameter("@SplitDocDetailId",SqlDbType.Int,4);
            splitdocdetailidpara.Value = st_splitdocattach.SplitDocDetailId;
            paras.Add(splitdocdetailidpara);

			    SqlParameter attachidpara = new SqlParameter("@AttachId",SqlDbType.Int,4);
            attachidpara.Value = st_splitdocattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }
        
		public override IModel CreateModel(DataRow dr)
        {
            SplitDocAttach splitdocattach = new SplitDocAttach();

                    splitdocattach.SplitDocAttachId = Convert.ToInt32(dr["SplitDocAttachId"]);
                    
                    if(dr["SplitDocId"] != DBNull.Value)
                    {
                    splitdocattach.SplitDocId = Convert.ToInt32(dr["SplitDocId"]);
                    }
                    
                    if(dr["SplitDocDetailId"] != DBNull.Value)
                    {
                    splitdocattach.SplitDocDetailId = Convert.ToInt32(dr["SplitDocDetailId"]);
                    }
                    
                    if(dr["AttachId"] != DBNull.Value)
                    {
                    splitdocattach.AttachId = Convert.ToInt32(dr["AttachId"]);
                    }
                    

            return splitdocattach;
        }
        
        public override IModel CreateModel(SqlDataReader dr)
        {
            SplitDocAttach splitdocattach = new SplitDocAttach();

                    int indexSplitDocAttachId = dr.GetOrdinal("SplitDocAttachId");
                    splitdocattach.SplitDocAttachId = Convert.ToInt32(dr[indexSplitDocAttachId]);
                    
                    int indexSplitDocId = dr.GetOrdinal("SplitDocId");
                    if(dr["SplitDocId"] != DBNull.Value)
                    {
                    splitdocattach.SplitDocId = Convert.ToInt32(dr[indexSplitDocId]);
                    }
                    
                    int indexSplitDocDetailId = dr.GetOrdinal("SplitDocDetailId");
                    if(dr["SplitDocDetailId"] != DBNull.Value)
                    {
                    splitdocattach.SplitDocDetailId = Convert.ToInt32(dr[indexSplitDocDetailId]);
                    }
                    
                    int indexAttachId = dr.GetOrdinal("AttachId");
                    if(dr["AttachId"] != DBNull.Value)
                    {
                    splitdocattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
                    }
                    

            return splitdocattach;
        }

        public override string TableName
        {
            get
            {
                return "St_SplitDocAttach";
            }
        }
		
        public override List<SqlParameter> CreateUpdateParameters(IModel obj) 
        { 
            SplitDocAttach st_splitdocattach = (SplitDocAttach)obj;
            
            List<SqlParameter> paras = new List<SqlParameter>();
                
		    SqlParameter splitdocattachidpara = new SqlParameter("@SplitDocAttachId",SqlDbType.Int,4);
            splitdocattachidpara.Value = st_splitdocattach.SplitDocAttachId;
            paras.Add(splitdocattachidpara);

		    SqlParameter splitdocidpara = new SqlParameter("@SplitDocId",SqlDbType.Int,4);
            splitdocidpara.Value = st_splitdocattach.SplitDocId;
            paras.Add(splitdocidpara);

		    SqlParameter splitdocdetailidpara = new SqlParameter("@SplitDocDetailId",SqlDbType.Int,4);
            splitdocdetailidpara.Value = st_splitdocattach.SplitDocDetailId;
            paras.Add(splitdocdetailidpara);

		    SqlParameter attachidpara = new SqlParameter("@AttachId",SqlDbType.Int,4);
            attachidpara.Value = st_splitdocattach.AttachId;
            paras.Add(attachidpara);

             
             return paras;
        }    
        
        #endregion
    }
}
