/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentOrderAttachDAL.cs
// 文件功能描述：制单指令附件dbo.Doc_DocumentOrderAttach数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月20日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Document.Model;
using NFMT.DBUtility;
using NFMT.Document.IDAL;
using NFMT.Common;

namespace NFMT.Document.DAL
{
    /// <summary>
    /// 制单指令附件dbo.Doc_DocumentOrderAttach数据交互类。
    /// </summary>
    public class DocumentOrderAttachDAL : DataOperate , IDocumentOrderAttachDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public DocumentOrderAttachDAL()
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
            DocumentOrderAttach doc_documentorderattach = (DocumentOrderAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
                          returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName ="@OrderAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

			    SqlParameter orderidpara = new SqlParameter("@OrderId",SqlDbType.Int,4);
            orderidpara.Value = doc_documentorderattach.OrderId;
            paras.Add(orderidpara);

			    SqlParameter attachidpara = new SqlParameter("@AttachId",SqlDbType.Int,4);
            attachidpara.Value = doc_documentorderattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }
        
		public override IModel CreateModel(SqlDataReader dr)
        {
            DocumentOrderAttach documentorderattach = new DocumentOrderAttach();

                    int indexOrderAttachId = dr.GetOrdinal("OrderAttachId");
                    documentorderattach.OrderAttachId = Convert.ToInt32(dr[indexOrderAttachId]);
                    
                    int indexOrderId = dr.GetOrdinal("OrderId");
                    if(dr["OrderId"] != DBNull.Value)
                    {
                    documentorderattach.OrderId = Convert.ToInt32(dr[indexOrderId]);
                    }
                    
                    int indexAttachId = dr.GetOrdinal("AttachId");
                    if(dr["AttachId"] != DBNull.Value)
                    {
                    documentorderattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
                    }
                    

            return documentorderattach;
        }

        public override string TableName
        {
            get
            {
                return "Doc_DocumentOrderAttach";
            }
        }
		
        public override List<SqlParameter> CreateUpdateParameters(IModel obj) 
        { 
            DocumentOrderAttach doc_documentorderattach = (DocumentOrderAttach)obj;
            
            List<SqlParameter> paras = new List<SqlParameter>();
                
		    SqlParameter orderattachidpara = new SqlParameter("@OrderAttachId",SqlDbType.Int,4);
            orderattachidpara.Value = doc_documentorderattach.OrderAttachId;
            paras.Add(orderattachidpara);

		    SqlParameter orderidpara = new SqlParameter("@OrderId",SqlDbType.Int,4);
            orderidpara.Value = doc_documentorderattach.OrderId;
            paras.Add(orderidpara);

		    SqlParameter attachidpara = new SqlParameter("@AttachId",SqlDbType.Int,4);
            attachidpara.Value = doc_documentorderattach.AttachId;
            paras.Add(attachidpara);

             
             return paras;
        }    
        
        #endregion
    }
}
