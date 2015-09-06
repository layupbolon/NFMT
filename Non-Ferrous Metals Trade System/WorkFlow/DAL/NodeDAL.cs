/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：NodeDAL.cs
// 文件功能描述：节点表dbo.Wf_Node数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月12日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.WorkFlow.Model;
using NFMT.DBUtility;
using NFMT.WorkFlow.IDAL;
using NFMT.Common;

namespace NFMT.WorkFlow.DAL
{
    /// <summary>
    /// 节点表dbo.Wf_Node数据交互类。
    /// </summary>
    public class NodeDAL : DataOperate , INodeDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public NodeDAL()
		{
		}
        
		#endregion

        #region 数据库操作
		
        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringWorkFlow;
            }
        }
        
        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            Node wf_node = (Node)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
                          returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName ="@NodeId";
            returnValue.Size = 4;
            paras.Add(returnValue);

			    SqlParameter masteridpara = new SqlParameter("@MasterId",SqlDbType.Int,4);
            masteridpara.Value = wf_node.MasterId;
            paras.Add(masteridpara);

			    SqlParameter nodestatuspara = new SqlParameter("@NodeStatus",SqlDbType.Int,4);
            nodestatuspara.Value = wf_node.NodeStatus;
            paras.Add(nodestatuspara);

			    SqlParameter nodenamepara = new SqlParameter("@NodeName",SqlDbType.VarChar,20);
            nodenamepara.Value = wf_node.NodeName;
            paras.Add(nodenamepara);

			    SqlParameter nodetypepara = new SqlParameter("@NodeType",SqlDbType.Int,4);
            nodetypepara.Value = wf_node.NodeType;
            paras.Add(nodetypepara);

			    SqlParameter isfirstpara = new SqlParameter("@IsFirst",SqlDbType.Bit,1);
             isfirstpara.Value = wf_node.IsFirst;
             paras.Add(isfirstpara);

			    SqlParameter islastpara = new SqlParameter("@IsLast",SqlDbType.Bit,1);
             islastpara.Value = wf_node.IsLast;
             paras.Add(islastpara);

			    SqlParameter prenodeidpara = new SqlParameter("@PreNodeId",SqlDbType.Int,4);
            prenodeidpara.Value = wf_node.PreNodeId;
            paras.Add(prenodeidpara);

			    SqlParameter auditempidpara = new SqlParameter("@AuditEmpId",SqlDbType.Int,4);
            auditempidpara.Value = wf_node.AuditEmpId;
            paras.Add(auditempidpara);

			    SqlParameter authgroupidpara = new SqlParameter("@AuthGroupId",SqlDbType.Int,4);
            authgroupidpara.Value = wf_node.AuthGroupId;
            paras.Add(authgroupidpara);

			    SqlParameter nodelevelpara = new SqlParameter("@NodeLevel",SqlDbType.Int,4);
            nodelevelpara.Value = wf_node.NodeLevel;
            paras.Add(nodelevelpara);


            return paras;
        }
        
		public override IModel CreateModel(DataRow dr)
        {
            Node node = new Node();

                    node.NodeId = Convert.ToInt32(dr["NodeId"]);
                    
                    node.MasterId = Convert.ToInt32(dr["MasterId"]);
                    
                    node.NodeStatus = Convert.ToInt32(dr["NodeStatus"]);
                    
                    node.NodeName = Convert.ToString(dr["NodeName"]);
                    
                    if(dr["NodeType"] != DBNull.Value)
                    {
                    node.NodeType = Convert.ToInt32(dr["NodeType"]);
                    }
                    
                    if(dr["IsFirst"] != DBNull.Value)
                    {
                    node.IsFirst = Convert.ToBoolean(dr["IsFirst"]);
                    }
                    
                    if(dr["IsLast"] != DBNull.Value)
                    {
                    node.IsLast = Convert.ToBoolean(dr["IsLast"]);
                    }
                    
                    if(dr["PreNodeId"] != DBNull.Value)
                    {
                    node.PreNodeId = Convert.ToInt32(dr["PreNodeId"]);
                    }
                    
                    if(dr["AuditEmpId"] != DBNull.Value)
                    {
                    node.AuditEmpId = Convert.ToInt32(dr["AuditEmpId"]);
                    }
                    
                    if(dr["AuthGroupId"] != DBNull.Value)
                    {
                    node.AuthGroupId = Convert.ToInt32(dr["AuthGroupId"]);
                    }
                    
                    if(dr["NodeLevel"] != DBNull.Value)
                    {
                    node.NodeLevel = Convert.ToInt32(dr["NodeLevel"]);
                    }
                    

            return node;
        }
        
        public override IModel CreateModel(SqlDataReader dr)
        {
            Node node = new Node();

                    int indexNodeId = dr.GetOrdinal("NodeId");
                    node.NodeId = Convert.ToInt32(dr[indexNodeId]);
                    
                    int indexMasterId = dr.GetOrdinal("MasterId");
                    node.MasterId = Convert.ToInt32(dr[indexMasterId]);
                    
                    int indexNodeStatus = dr.GetOrdinal("NodeStatus");
                    node.NodeStatus = Convert.ToInt32(dr[indexNodeStatus]);
                    
                    int indexNodeName = dr.GetOrdinal("NodeName");
                    node.NodeName = Convert.ToString(dr[indexNodeName]);
                    
                    int indexNodeType = dr.GetOrdinal("NodeType");
                    if(dr["NodeType"] != DBNull.Value)
                    {
                    node.NodeType = Convert.ToInt32(dr[indexNodeType]);
                    }
                    
                    int indexIsFirst = dr.GetOrdinal("IsFirst");
                    if(dr["IsFirst"] != DBNull.Value)
                    {
                    node.IsFirst = Convert.ToBoolean(dr[indexIsFirst]);
                    }
                    
                    int indexIsLast = dr.GetOrdinal("IsLast");
                    if(dr["IsLast"] != DBNull.Value)
                    {
                    node.IsLast = Convert.ToBoolean(dr[indexIsLast]);
                    }
                    
                    int indexPreNodeId = dr.GetOrdinal("PreNodeId");
                    if(dr["PreNodeId"] != DBNull.Value)
                    {
                    node.PreNodeId = Convert.ToInt32(dr[indexPreNodeId]);
                    }
                    
                    int indexAuditEmpId = dr.GetOrdinal("AuditEmpId");
                    if(dr["AuditEmpId"] != DBNull.Value)
                    {
                    node.AuditEmpId = Convert.ToInt32(dr[indexAuditEmpId]);
                    }
                    
                    int indexAuthGroupId = dr.GetOrdinal("AuthGroupId");
                    if(dr["AuthGroupId"] != DBNull.Value)
                    {
                    node.AuthGroupId = Convert.ToInt32(dr[indexAuthGroupId]);
                    }
                    
                    int indexNodeLevel = dr.GetOrdinal("NodeLevel");
                    if(dr["NodeLevel"] != DBNull.Value)
                    {
                    node.NodeLevel = Convert.ToInt32(dr[indexNodeLevel]);
                    }
                    

            return node;
        }

        public override string TableName
        {
            get
            {
                return "Wf_Node";
            }
        }
		
        public override List<SqlParameter> CreateUpdateParameters(IModel obj) 
        { 
            Node wf_node = (Node)obj;
            
            List<SqlParameter> paras = new List<SqlParameter>();
                
		    SqlParameter nodeidpara = new SqlParameter("@NodeId",SqlDbType.Int,4);
            nodeidpara.Value = wf_node.NodeId;
            paras.Add(nodeidpara);

		    SqlParameter masteridpara = new SqlParameter("@MasterId",SqlDbType.Int,4);
            masteridpara.Value = wf_node.MasterId;
            paras.Add(masteridpara);

		    SqlParameter nodestatuspara = new SqlParameter("@NodeStatus",SqlDbType.Int,4);
            nodestatuspara.Value = wf_node.NodeStatus;
            paras.Add(nodestatuspara);

		    SqlParameter nodenamepara = new SqlParameter("@NodeName",SqlDbType.VarChar,20);
            nodenamepara.Value = wf_node.NodeName;
            paras.Add(nodenamepara);

		    SqlParameter nodetypepara = new SqlParameter("@NodeType",SqlDbType.Int,4);
            nodetypepara.Value = wf_node.NodeType;
            paras.Add(nodetypepara);

		    SqlParameter isfirstpara = new SqlParameter("@IsFirst",SqlDbType.Bit,1);
             isfirstpara.Value = wf_node.IsFirst;
             paras.Add(isfirstpara);

		    SqlParameter islastpara = new SqlParameter("@IsLast",SqlDbType.Bit,1);
             islastpara.Value = wf_node.IsLast;
             paras.Add(islastpara);

		    SqlParameter prenodeidpara = new SqlParameter("@PreNodeId",SqlDbType.Int,4);
            prenodeidpara.Value = wf_node.PreNodeId;
            paras.Add(prenodeidpara);

		    SqlParameter auditempidpara = new SqlParameter("@AuditEmpId",SqlDbType.Int,4);
            auditempidpara.Value = wf_node.AuditEmpId;
            paras.Add(auditempidpara);

		    SqlParameter authgroupidpara = new SqlParameter("@AuthGroupId",SqlDbType.Int,4);
            authgroupidpara.Value = wf_node.AuthGroupId;
            paras.Add(authgroupidpara);

		    SqlParameter nodelevelpara = new SqlParameter("@NodeLevel",SqlDbType.Int,4);
            nodelevelpara.Value = wf_node.NodeLevel;
            paras.Add(nodelevelpara);

             
             return paras;
        }    
        
        #endregion
    }
}
