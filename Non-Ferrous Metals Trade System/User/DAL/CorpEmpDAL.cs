/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorpEmpDAL.cs
// 文件功能描述：公司员工关联表dbo.CorpEmp数据交互类。
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
    /// 公司员工关联表dbo.CorpEmp数据交互类。
    /// </summary>
    public class CorpEmpDAL : DataOperate , ICorpEmpDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public CorpEmpDAL()
		{
		}
        
		#endregion

        #region 数据库操作
		
		/// <summary>
		/// 新增corpemp信息
		/// </summary>
        /// <param name="user">当前操作用户</param>
		/// <param name="obj">CorpEmp对象</param>
		/// <returns></returns>
		public override ResultModel Insert(UserModel user, IModel obj)
		{
            ResultModel result = new ResultModel();
            try
            {
                CorpEmp corpemp = (CorpEmp)obj;
                
                if (corpemp == null)
                {
                    result.Message = "新增对象不能为null";
                    return result;
                }
                
                List<SqlParameter> paras = new List<SqlParameter>();
                SqlParameter corpempidpara = new SqlParameter();
            corpempidpara.Direction = ParameterDirection.Output;
            corpempidpara.SqlDbType = SqlDbType.Int;
            corpempidpara.ParameterName ="@CorpEmpId";
            corpempidpara.Size = 4;
            paras.Add(corpempidpara);

			    SqlParameter empidpara = new SqlParameter("@EmpId",SqlDbType.Int,4);
            empidpara.Value = corpemp.EmpId;
            paras.Add(empidpara);

			    SqlParameter corpidpara = new SqlParameter("@CorpId",SqlDbType.Int,4);
            corpidpara.Value = corpemp.CorpId;
            paras.Add(corpidpara);

                
			    int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringUser,CommandType.StoredProcedure,"CorpEmpInsert",paras.ToArray());
                
                if (i == 1)
                {
                    result.AffectCount = i;
                    result.ResultStatus = 0;
                    result.Message = "CorpEmp添加成功";
                    result.ReturnValue = corpempidpara.Value;
                }
                else
                    result.Message = "CorpEmp添加失败";
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
            }
            
            return result;
		}

        /// <summary>
		/// 获取指定corpEmpId的corpemp对象
		/// </summary>
        /// <param name="user">当前操作用户</param>
		/// <param name="corpEmpId">主键值</param>
		/// <returns></returns>
		public override ResultModel Get(UserModel user, int corpEmpId)
		{
            ResultModel result = new ResultModel();
			
			if(corpEmpId<1)
			{
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
			SqlParameter para = new SqlParameter("@CorpEmpId",SqlDbType.Int,4);
            para.Value = corpEmpId;
            paras.Add(para);
            
            SqlDataReader dr = null;
            
            try
            {
                dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringUser, CommandType.StoredProcedure, "CorpEmpGet", paras.ToArray());
                
                CorpEmp corpemp = new CorpEmp();
                
                if (dr.Read())
                {
                    int indexCorpEmpId = dr.GetOrdinal("CorpEmpId");
                    corpemp.CorpEmpId = Convert.ToInt32(dr[indexCorpEmpId]);
                    
                    int indexEmpId = dr.GetOrdinal("EmpId");
                    if(dr["EmpId"] != DBNull.Value)
                    {
                    corpemp.EmpId = Convert.ToInt32(dr[indexEmpId]);
                    }
                    
                    int indexCorpId = dr.GetOrdinal("CorpId");
                    if(dr["CorpId"] != DBNull.Value)
                    {
                    corpemp.CorpId = Convert.ToInt32(dr[indexCorpId]);
                    }
                    
                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = corpemp;
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
		/// 获取corpemp集合
		/// </summary>
        /// <param name="user">当前操作用户</param>
		/// <returns></returns>
		public override ResultModel Load(UserModel user)
		{
            ResultModel result = new ResultModel();
            try
            {
                DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionStringUser,"CorpEmpLoad",null,CommandType.StoredProcedure);
                
                List< CorpEmp> corpEmps = new List< CorpEmp>();
                
                foreach(DataRow dr in dt.Rows)
                {
                    CorpEmp corpemp = new CorpEmp();
                    corpemp.CorpEmpId = Convert.ToInt32(dr["CorpEmpId"]);
                    
                    if(dr["EmpId"] != DBNull.Value)
                    {
                    corpemp.EmpId = Convert.ToInt32(dr["EmpId"]);
                    }
                    if(dr["CorpId"] != DBNull.Value)
                    {
                    corpemp.CorpId = Convert.ToInt32(dr["CorpId"]);
                    }
                    corpEmps.Add(corpemp);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = corpEmps;
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
		}
			
		/// <summary>
		/// 更新corpemp
		/// </summary>
        /// <param name="user">当前操作用户</param>
		/// <param name="obj">CorpEmp对象</param>
        /// <returns></returns>
		public override ResultModel Update(UserModel user, IModel obj)
		{
            ResultModel result = new ResultModel();
            
            try
            {
                CorpEmp corpemp = (CorpEmp)obj;
                
                if (corpemp==null)
				{
                    result.Message = "更新对象不能为null";
                    return result;
                }
                
			    List<SqlParameter> paras = new List<SqlParameter>();
                
			    SqlParameter corpempidpara = new SqlParameter("@CorpEmpId",SqlDbType.Int,4);
            corpempidpara.Value = corpemp.CorpEmpId;
            paras.Add(corpempidpara);

			    SqlParameter empidpara = new SqlParameter("@EmpId",SqlDbType.Int,4);
            empidpara.Value = corpemp.EmpId;
            paras.Add(empidpara);

			    SqlParameter corpidpara = new SqlParameter("@CorpId",SqlDbType.Int,4);
            corpidpara.Value = corpemp.CorpId;
            paras.Add(corpidpara);

                
                int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringUser,CommandType.StoredProcedure,"CorpEmpUpdate",paras.ToArray());
                
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
