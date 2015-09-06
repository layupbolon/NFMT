/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SQLHelper.cs
// 文件功能描述：ado.net封装，提供SQL Server数据库访问与操作。
// 创建人：pekah.chow
// 创建时间： 2014-03-21
// 修改人：Eric Yin 
// 修改：2014-3-24
// 修改内容：添加注释
----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DBUtility
{
    public abstract class SqlHelper
    {
        //public static readonly string ConnectionStringUser = ConfigurationManager.ConnectionStrings["ConnectionStringUser"].ConnectionString;
        //public static readonly string ConnectionStringWorkFlow = ConfigurationManager.ConnectionStrings["ConnectionStringWorkFlow"].ConnectionString;
        //public static readonly string ConnectionStringSms = ConfigurationManager.ConnectionStrings["ConnectionStringSms"].ConnectionString;
        public static readonly string ConnectionStringNFMT = string.Empty;

        static SqlHelper()
        {
            ConnectionStringNFMT = "server = 192.168.13.164 ;database = NFMT_Basic ;uid = sa ; pwd = maike!QAZSE$;";
        }

        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 执行数据库语句,返回影响行数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">执行类型</param>
        /// <param name="cmdText">执行语句</param>
        /// <param name="commandParameters">数据库参数</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //调用内部函数准备各种参数
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行数据库语句,返回影响行数
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="cmdType">执行类型</param>
        /// <param name="cmdText">执行语句</param>
        /// <param name="commandParameters">数据库参数</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            //调用内部函数准备各种参数
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行数据库语句,返回影响行数
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="cmdType">执行类型</param>
        /// <param name="cmdText">执行语句</param>
        /// <param name="commandParameters">数据库参数</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            //调用内部函数准备各种参数
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行数据库语句,返回SqlDataReader
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">执行类型</param>
        /// <param name="cmdText">执行语句</param>
        /// <param name="commandParameters">数据库参数</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                //调用内部函数准备各种参数
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行数据库语句,返回DataTable
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdText">执行语句</param>
        /// <param name="paras">参数</param>
        /// <param name="type">执行类型</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteDataTable(string connectionString, string cmdText, SqlParameter[] paras, CommandType type)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, type, cmdText, paras);
                SqlDataAdapter dp = new SqlDataAdapter(cmd);
                dp.Fill(ds);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0];
                else
                    return null;
            }
        }

        /// <summary>
        /// 执行数据库语句，返回结果集中第一行第一列的值
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">执行类型</param>
        /// <param name="cmdText">执行语句</param>
        /// <param name="commandParameters">数据库参数</param>
        /// <returns>查询所返回的结果集中第一行第一列的值</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //调用内部函数准备各种参数
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行数据库语句，返回结果集中第一行第一列的值
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="cmdType">执行类型</param>
        /// <param name="cmdText">执行语句</param>
        /// <param name="commandParameters">数据库参数</param>
        /// <returns>查询所返回的结果集中第一行第一列的值</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            //调用内部函数准备各种参数
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 将参数值添加到哈希表
        /// </summary>
        /// <param name="cacheKey">哈希表键值</param>
        /// <param name="commandParameters">数据库参数</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// 取出哈希表指定键的值,返回数据库参数
        /// </summary>
        /// <param name="cacheKey">哈希表键值</param>
        /// <returns>数据库参数</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// 准备执行的各个参数
        /// </summary>
        /// <param name="cmd">执行命令</param>
        /// <param name="conn">数据库连接字符串</param>
        /// <param name="trans">事务</param>
        /// <param name="cmdType">执行类型</param>
        /// <param name="cmdText">执行语句</param>
        /// <param name="cmdParms">参数</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
    }
}
