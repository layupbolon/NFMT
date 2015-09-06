/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockNoDAL.cs
// 文件功能描述：融资单号表dbo.Fin_StockNo数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年5月14日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Finance.Model;
using NFMT.DBUtility;
using NFMT.Finance.IDAL;
using NFMT.Common;

namespace NFMT.Finance.DAL
{
    /// <summary>
    /// 融资单号表dbo.Fin_StockNo数据交互类。
    /// </summary>
    public partial class StockNoDAL : DataOperate, IStockNoDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockNoDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringFinance;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            StockNo fin_stockno = (StockNo)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(fin_stockno.RefNo))
            {
                SqlParameter refnopara = new SqlParameter("@RefNo", SqlDbType.VarChar, 30);
                refnopara.Value = fin_stockno.RefNo;
                paras.Add(refnopara);
            }

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = fin_stockno.NetAmount;
            paras.Add(netamountpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            StockNo stockno = new StockNo();

            stockno.StockId = Convert.ToInt32(dr["StockId"]);

            if (dr["RefNo"] != DBNull.Value)
            {
                stockno.RefNo = Convert.ToString(dr["RefNo"]);
            }

            if (dr["NetAmount"] != DBNull.Value)
            {
                stockno.NetAmount = Convert.ToDecimal(dr["NetAmount"]);
            }


            return stockno;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockNo stockno = new StockNo();

            int indexStockId = dr.GetOrdinal("StockId");
            stockno.StockId = Convert.ToInt32(dr[indexStockId]);

            int indexRefNo = dr.GetOrdinal("RefNo");
            if (dr["RefNo"] != DBNull.Value)
            {
                stockno.RefNo = Convert.ToString(dr[indexRefNo]);
            }

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            if (dr["NetAmount"] != DBNull.Value)
            {
                stockno.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
            }


            return stockno;
        }

        public override string TableName
        {
            get
            {
                return "Fin_StockNo";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockNo fin_stockno = (StockNo)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = fin_stockno.StockId;
            paras.Add(stockidpara);

            if (!string.IsNullOrEmpty(fin_stockno.RefNo))
            {
                SqlParameter refnopara = new SqlParameter("@RefNo", SqlDbType.VarChar, 30);
                refnopara.Value = fin_stockno.RefNo;
                paras.Add(refnopara);
            }

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = fin_stockno.NetAmount;
            paras.Add(netamountpara);


            return paras;
        }

        #endregion

        #region 重载

        public override ResultModel Insert(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();
            try
            {
                if (obj == null)
                {
                    result.Message = "插入对象不能为null";
                    return result;
                }

                SqlParameter returnValue = new SqlParameter();
                obj.CreatorId = user.EmpId;

                if ((int)obj.Status == 0)
                    obj.Status = StatusEnum.已录入;

                object objRet = SqlHelper.ExecuteScalar(ConnectString, CommandType.StoredProcedure, string.Format("{0}Insert", obj.TableName), CreateInsertParameters(obj, ref returnValue).ToArray());

                int i;
                if (objRet != null && !string.IsNullOrEmpty(objRet.ToString()) && int.TryParse(objRet.ToString(),out i) && i>0)
                {
                    result.ResultStatus = 0;
                    result.Message = "添加成功";
                    result.ReturnValue = i;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "添加失败";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        #endregion
    }
}
