/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：UserOperationDAL.cs
// 文件功能描述：用户操作记录dbo.UserOperation数据交互类。
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
    /// 用户操作记录dbo.UserOperation数据交互类。
    /// </summary>
    public class UserOperationDAL : DataOperate , IUserOperationDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public UserOperationDAL()
		{
		}
        
		#endregion

        #region 数据库操作
		
		/// <summary>
		/// 新增useroperation信息
		/// </summary>
        /// <param name="user">当前操作用户</param>
		/// <param name="obj">UserOperation对象</param>
		/// <returns></returns>
		public override ResultModel Insert(UserModel user, IModel obj)
		{
            ResultModel result = new ResultModel();
            try
            {
                UserOperation useroperation = (UserOperation)obj;
                
                if (useroperation == null)
                {
                    result.Message = "新增对象不能为null";
                    return result;
                }
                
                List<SqlParameter> paras = new List<SqlParameter>();
                SqlParameter useroperationidpara = new SqlParameter();
            useroperationidpara.Direction = ParameterDirection.Output;
            useroperationidpara.SqlDbType = SqlDbType.Int;
            useroperationidpara.ParameterName ="@UserOperationId";
            useroperationidpara.Size = 4;
            paras.Add(useroperationidpara);

			    SqlParameter accidpara = new SqlParameter("@AccId",SqlDbType.Int,4);
            accidpara.Value = useroperation.AccId;
            paras.Add(accidpara);

			              SqlParameter operationtimepara = new SqlParameter("@OperationTime",SqlDbType.DateTime,8);
          operationtimepara.Value = useroperation.OperationTime;
          paras.Add(operationtimepara);

			    SqlParameter menuidpara = new SqlParameter("@MenuId",SqlDbType.Int,4);
            menuidpara.Value = useroperation.MenuId;
            paras.Add(menuidpara);

			    if(!string.IsNullOrEmpty(useroperation.OperationDesc))
            {
               SqlParameter operationdescpara = new SqlParameter("@OperationDesc",SqlDbType.VarChar,8000);
               operationdescpara.Value = useroperation.OperationDesc;
               paras.Add(operationdescpara);
            }

                
			    int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringUser,CommandType.StoredProcedure,"UserOperationInsert",paras.ToArray());
                
                if (i == 1)
                {
                    result.AffectCount = i;
                    result.ResultStatus = 0;
                    result.Message = "UserOperation添加成功";
                    result.ReturnValue = useroperationidpara.Value;
                }
                else
                    result.Message = "UserOperation添加失败";
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
            }
            
            return result;
		}

        /// <summary>
		/// 获取指定userOperationId的useroperation对象
		/// </summary>
        /// <param name="user">当前操作用户</param>
		/// <param name="userOperationId">主键值</param>
		/// <returns></returns>
		public override ResultModel Get(UserModel user, int userOperationId)
		{
            ResultModel result = new ResultModel();
			
			if(userOperationId<1)
			{
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
			SqlParameter para = new SqlParameter("@UserOperationId",SqlDbType.Int,4);
            para.Value = userOperationId;
            paras.Add(para);
            
            SqlDataReader dr = null;
            
            try
            {
                dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringUser, CommandType.StoredProcedure, "UserOperationGet", paras.ToArray());
                
                UserOperation useroperation = new UserOperation();
                
                if (dr.Read())
                {
                    int indexUserOperationId = dr.GetOrdinal("UserOperationId");
                    useroperation.UserOperationId = Convert.ToInt32(dr[indexUserOperationId]);
                    
                    int indexAccId = dr.GetOrdinal("AccId");
                    if(dr["AccId"] != DBNull.Value)
                    {
                    useroperation.AccId = Convert.ToInt32(dr[indexAccId]);
                    }
                    
                    int indexOperationTime = dr.GetOrdinal("OperationTime");
                    if(dr["OperationTime"] != DBNull.Value)
                    {
                    useroperation.OperationTime = Convert.ToDateTime(dr[indexOperationTime]);
                    }
                    
                    int indexMenuId = dr.GetOrdinal("MenuId");
                    if(dr["MenuId"] != DBNull.Value)
                    {
                    useroperation.MenuId = Convert.ToInt32(dr[indexMenuId]);
                    }
                    
                    int indexOperationDesc = dr.GetOrdinal("OperationDesc");
                    if(dr["OperationDesc"] != DBNull.Value)
                    {
                    useroperation.OperationDesc = Convert.ToString(dr[indexOperationDesc]);
                    }
                    
                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = useroperation;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
            }
            catch(Exception ex)
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
		/// 获取useroperation集合
		/// </summary>
        /// <param name="user">当前操作用户</param>
		/// <returns></returns>
		public override ResultModel Load(UserModel user)
		{
            ResultModel result = new ResultModel();
            try
            {
                DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionStringUser,"UserOperationLoad",null,CommandType.StoredProcedure);
                
                List< UserOperation> userOperations = new List< UserOperation>();
                
                foreach(DataRow dr in dt.Rows)
                {
                    UserOperation useroperation = new UserOperation();
                    useroperation.UserOperationId = Convert.ToInt32(dr["UserOperationId"]);
                    
                    if(dr["AccId"] != DBNull.Value)
                    {
                    useroperation.AccId = Convert.ToInt32(dr["AccId"]);
                    }
                    if(dr["OperationTime"] != DBNull.Value)
                    {
                    useroperation.OperationTime = Convert.ToDateTime(dr["OperationTime"]);
                    }
                    if(dr["MenuId"] != DBNull.Value)
                    {
                    useroperation.MenuId = Convert.ToInt32(dr["MenuId"]);
                    }
                    if(dr["OperationDesc"] != DBNull.Value)
                    {
                    useroperation.OperationDesc = Convert.ToString(dr["OperationDesc"]);
                    }
                    userOperations.Add(useroperation);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = userOperations;
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
		}
			
		/// <summary>
		/// 更新useroperation
		/// </summary>
        /// <param name="user">当前操作用户</param>
		/// <param name="obj">UserOperation对象</param>
        /// <returns></returns>
		public override ResultModel Update(UserModel user, IModel obj)
		{
            ResultModel result = new ResultModel();
            
            try
            {
                UserOperation useroperation = (UserOperation)obj;
                
                if (useroperation==null)
				{
                    result.Message = "更新对象不能为null";
                    return result;
                }
                
			    List<SqlParameter> paras = new List<SqlParameter>();
                
			    SqlParameter useroperationidpara = new SqlParameter("@UserOperationId",SqlDbType.Int,4);
            useroperationidpara.Value = useroperation.UserOperationId;
            paras.Add(useroperationidpara);

			    SqlParameter accidpara = new SqlParameter("@AccId",SqlDbType.Int,4);
            accidpara.Value = useroperation.AccId;
            paras.Add(accidpara);

			              SqlParameter operationtimepara = new SqlParameter("@OperationTime",SqlDbType.DateTime,8);
          operationtimepara.Value = useroperation.OperationTime;
          paras.Add(operationtimepara);

			    SqlParameter menuidpara = new SqlParameter("@MenuId",SqlDbType.Int,4);
            menuidpara.Value = useroperation.MenuId;
            paras.Add(menuidpara);

			    if(!string.IsNullOrEmpty(useroperation.OperationDesc))
            {
               SqlParameter operationdescpara = new SqlParameter("@OperationDesc",SqlDbType.VarChar,8000);
               operationdescpara.Value = useroperation.OperationDesc;
               paras.Add(operationdescpara);
            }

                
                int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringUser,CommandType.StoredProcedure,"UserOperationUpdate",paras.ToArray());
                
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

        public override string ConnectString
        {
            get
            {
                return SqlHelper.ConnectionStringUser;
            }
        }

        #endregion
    }
}
