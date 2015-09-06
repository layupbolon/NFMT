/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：UserLoginDAL.cs
// 文件功能描述：用户登录dbo.UserLogin数据交互类。
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
    /// 用户登录dbo.UserLogin数据交互类。
    /// </summary>
    public class UserLoginDAL : DataOperate , IUserLoginDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public UserLoginDAL()
		{
		}
        
		#endregion

        #region 数据库操作
		
		/// <summary>
		/// 新增userlogin信息
		/// </summary>
        /// <param name="user">当前操作用户</param>
		/// <param name="obj">UserLogin对象</param>
		/// <returns></returns>
		public override ResultModel Insert(UserModel user, IModel obj)
		{
            ResultModel result = new ResultModel();
            try
            {
                UserLogin userlogin = (UserLogin)obj;
                
                if (userlogin == null)
                {
                    result.Message = "新增对象不能为null";
                    return result;
                }
                
                List<SqlParameter> paras = new List<SqlParameter>();
                SqlParameter userloginidpara = new SqlParameter();
            userloginidpara.Direction = ParameterDirection.Output;
            userloginidpara.SqlDbType = SqlDbType.Int;
            userloginidpara.ParameterName ="@UserLoginId";
            userloginidpara.Size = 4;
            paras.Add(userloginidpara);

			    SqlParameter accidpara = new SqlParameter("@AccId",SqlDbType.Int,4);
            accidpara.Value = userlogin.AccId;
            paras.Add(accidpara);

			              SqlParameter logintimepara = new SqlParameter("@LoginTime",SqlDbType.DateTime,8);
          logintimepara.Value = userlogin.LoginTime;
          paras.Add(logintimepara);

			    if(!string.IsNullOrEmpty(userlogin.LoginIP))
            {
               SqlParameter loginippara = new SqlParameter("@LoginIP",SqlDbType.VarChar,80);
               loginippara.Value = userlogin.LoginIP;
               paras.Add(loginippara);
            }

			    if(!string.IsNullOrEmpty(userlogin.LoginMac))
            {
               SqlParameter loginmacpara = new SqlParameter("@LoginMac",SqlDbType.VarChar,80);
               loginmacpara.Value = userlogin.LoginMac;
               paras.Add(loginmacpara);
            }

			    SqlParameter borwserpara = new SqlParameter("@Borwser",SqlDbType.Int,4);
            borwserpara.Value = userlogin.Borwser;
            paras.Add(borwserpara);

                
			    int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringUser,CommandType.StoredProcedure,"UserLoginInsert",paras.ToArray());
                
                if (i == 1)
                {
                    result.AffectCount = i;
                    result.ResultStatus = 0;
                    result.Message = "UserLogin添加成功";
                    result.ReturnValue = userloginidpara.Value;
                }
                else
                    result.Message = "UserLogin添加失败";
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
            }
            
            return result;
		}

        /// <summary>
		/// 获取指定userLoginId的userlogin对象
		/// </summary>
        /// <param name="user">当前操作用户</param>
		/// <param name="userLoginId">主键值</param>
		/// <returns></returns>
		public override ResultModel Get(UserModel user, int userLoginId)
		{
            ResultModel result = new ResultModel();
			
			if(userLoginId<1)
			{
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
			SqlParameter para = new SqlParameter("@UserLoginId",SqlDbType.Int,4);
            para.Value = userLoginId;
            paras.Add(para);
            
            SqlDataReader dr = null;
            
            try
            {
                dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringUser, CommandType.StoredProcedure, "UserLoginGet", paras.ToArray());
                
                UserLogin userlogin = new UserLogin();
                
                if (dr.Read())
                {
                    int indexUserLoginId = dr.GetOrdinal("UserLoginId");
                    userlogin.UserLoginId = Convert.ToInt32(dr[indexUserLoginId]);
                    
                    int indexAccId = dr.GetOrdinal("AccId");
                    if(dr["AccId"] != DBNull.Value)
                    {
                    userlogin.AccId = Convert.ToInt32(dr[indexAccId]);
                    }
                    
                    int indexLoginTime = dr.GetOrdinal("LoginTime");
                    if(dr["LoginTime"] != DBNull.Value)
                    {
                    userlogin.LoginTime = Convert.ToDateTime(dr[indexLoginTime]);
                    }
                    
                    int indexLoginIP = dr.GetOrdinal("LoginIP");
                    if(dr["LoginIP"] != DBNull.Value)
                    {
                    userlogin.LoginIP = Convert.ToString(dr[indexLoginIP]);
                    }
                    
                    int indexLoginMac = dr.GetOrdinal("LoginMac");
                    if(dr["LoginMac"] != DBNull.Value)
                    {
                    userlogin.LoginMac = Convert.ToString(dr[indexLoginMac]);
                    }
                    
                    int indexBorwser = dr.GetOrdinal("Borwser");
                    if(dr["Borwser"] != DBNull.Value)
                    {
                    userlogin.Borwser = Convert.ToInt32(dr[indexBorwser]);
                    }
                    
                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = userlogin;
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
		/// 获取userlogin集合
		/// </summary>
        /// <param name="user">当前操作用户</param>
		/// <returns></returns>
		public override ResultModel Load(UserModel user)
		{
            ResultModel result = new ResultModel();
            try
            {
                DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionStringUser,"UserLoginLoad",null,CommandType.StoredProcedure);
                
                List< UserLogin> userLogins = new List< UserLogin>();
                
                foreach(DataRow dr in dt.Rows)
                {
                    UserLogin userlogin = new UserLogin();
                    userlogin.UserLoginId = Convert.ToInt32(dr["UserLoginId"]);
                    
                    if(dr["AccId"] != DBNull.Value)
                    {
                    userlogin.AccId = Convert.ToInt32(dr["AccId"]);
                    }
                    if(dr["LoginTime"] != DBNull.Value)
                    {
                    userlogin.LoginTime = Convert.ToDateTime(dr["LoginTime"]);
                    }
                    if(dr["LoginIP"] != DBNull.Value)
                    {
                    userlogin.LoginIP = Convert.ToString(dr["LoginIP"]);
                    }
                    if(dr["LoginMac"] != DBNull.Value)
                    {
                    userlogin.LoginMac = Convert.ToString(dr["LoginMac"]);
                    }
                    if(dr["Borwser"] != DBNull.Value)
                    {
                    userlogin.Borwser = Convert.ToInt32(dr["Borwser"]);
                    }
                    userLogins.Add(userlogin);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = userLogins;
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
		}
			
		/// <summary>
		/// 更新userlogin
		/// </summary>
        /// <param name="user">当前操作用户</param>
		/// <param name="obj">UserLogin对象</param>
        /// <returns></returns>
		public override ResultModel Update(UserModel user, IModel obj)
		{
            ResultModel result = new ResultModel();
            
            try
            {
                UserLogin userlogin = (UserLogin)obj;
                
                if (userlogin==null)
				{
                    result.Message = "更新对象不能为null";
                    return result;
                }
                
			    List<SqlParameter> paras = new List<SqlParameter>();
                
			    SqlParameter userloginidpara = new SqlParameter("@UserLoginId",SqlDbType.Int,4);
            userloginidpara.Value = userlogin.UserLoginId;
            paras.Add(userloginidpara);

			    SqlParameter accidpara = new SqlParameter("@AccId",SqlDbType.Int,4);
            accidpara.Value = userlogin.AccId;
            paras.Add(accidpara);

			              SqlParameter logintimepara = new SqlParameter("@LoginTime",SqlDbType.DateTime,8);
          logintimepara.Value = userlogin.LoginTime;
          paras.Add(logintimepara);

			    if(!string.IsNullOrEmpty(userlogin.LoginIP))
            {
               SqlParameter loginippara = new SqlParameter("@LoginIP",SqlDbType.VarChar,80);
               loginippara.Value = userlogin.LoginIP;
               paras.Add(loginippara);
            }

			    if(!string.IsNullOrEmpty(userlogin.LoginMac))
            {
               SqlParameter loginmacpara = new SqlParameter("@LoginMac",SqlDbType.VarChar,80);
               loginmacpara.Value = userlogin.LoginMac;
               paras.Add(loginmacpara);
            }

			    SqlParameter borwserpara = new SqlParameter("@Borwser",SqlDbType.Int,4);
            borwserpara.Value = userlogin.Borwser;
            paras.Add(borwserpara);

                
                int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringUser,CommandType.StoredProcedure,"UserLoginUpdate",paras.ToArray());
                
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
