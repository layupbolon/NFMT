/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockReceiptDetailDAL.cs
// 文件功能描述：回执明细dbo.St_StockReceiptDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月11日
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
    /// 回执明细dbo.St_StockReceiptDetail数据交互类。
    /// </summary>
    public class StockReceiptDetailDAL : ExecOperate, IStockReceiptDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockReceiptDetailDAL()
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
            StockReceiptDetail st_stockreceiptdetail = (StockReceiptDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter receiptidpara = new SqlParameter("@ReceiptId", SqlDbType.Int, 4);
            receiptidpara.Value = st_stockreceiptdetail.ReceiptId;
            paras.Add(receiptidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = st_stockreceiptdetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter contractsubidpara = new SqlParameter("@ContractSubId", SqlDbType.Int, 4);
            contractsubidpara.Value = st_stockreceiptdetail.ContractSubId;
            paras.Add(contractsubidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stockreceiptdetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_stockreceiptdetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter prenetamountpara = new SqlParameter("@PreNetAmount", SqlDbType.Decimal, 9);
            prenetamountpara.Value = st_stockreceiptdetail.PreNetAmount;
            paras.Add(prenetamountpara);

            SqlParameter receiptamountpara = new SqlParameter("@ReceiptAmount", SqlDbType.Decimal, 9);
            receiptamountpara.Value = st_stockreceiptdetail.ReceiptAmount;
            paras.Add(receiptamountpara);

            SqlParameter qtymisspara = new SqlParameter("@QtyMiss", SqlDbType.Decimal, 9);
            qtymisspara.Value = st_stockreceiptdetail.QtyMiss;
            paras.Add(qtymisspara);

            SqlParameter qtyratepara = new SqlParameter("@QtyRate", SqlDbType.Decimal, 9);
            qtyratepara.Value = st_stockreceiptdetail.QtyRate;
            paras.Add(qtyratepara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockReceiptDetail stockreceiptdetail = new StockReceiptDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            stockreceiptdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexReceiptId = dr.GetOrdinal("ReceiptId");
            stockreceiptdetail.ReceiptId = Convert.ToInt32(dr[indexReceiptId]);

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                stockreceiptdetail.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexContractSubId = dr.GetOrdinal("ContractSubId");
            if (dr["ContractSubId"] != DBNull.Value)
            {
                stockreceiptdetail.ContractSubId = Convert.ToInt32(dr[indexContractSubId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                stockreceiptdetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                stockreceiptdetail.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexPreNetAmount = dr.GetOrdinal("PreNetAmount");
            if (dr["PreNetAmount"] != DBNull.Value)
            {
                stockreceiptdetail.PreNetAmount = Convert.ToDecimal(dr[indexPreNetAmount]);
            }

            int indexReceiptAmount = dr.GetOrdinal("ReceiptAmount");
            if (dr["ReceiptAmount"] != DBNull.Value)
            {
                stockreceiptdetail.ReceiptAmount = Convert.ToDecimal(dr[indexReceiptAmount]);
            }

            int indexQtyMiss = dr.GetOrdinal("QtyMiss");
            if (dr["QtyMiss"] != DBNull.Value)
            {
                stockreceiptdetail.QtyMiss = Convert.ToDecimal(dr[indexQtyMiss]);
            }

            int indexQtyRate = dr.GetOrdinal("QtyRate");
            if (dr["QtyRate"] != DBNull.Value)
            {
                stockreceiptdetail.QtyRate = Convert.ToDecimal(dr[indexQtyRate]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                stockreceiptdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                stockreceiptdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                stockreceiptdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                stockreceiptdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                stockreceiptdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return stockreceiptdetail;
        }

        public override string TableName
        {
            get
            {
                return "St_StockReceiptDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockReceiptDetail st_stockreceiptdetail = (StockReceiptDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = st_stockreceiptdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter receiptidpara = new SqlParameter("@ReceiptId", SqlDbType.Int, 4);
            receiptidpara.Value = st_stockreceiptdetail.ReceiptId;
            paras.Add(receiptidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = st_stockreceiptdetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter contractsubidpara = new SqlParameter("@ContractSubId", SqlDbType.Int, 4);
            contractsubidpara.Value = st_stockreceiptdetail.ContractSubId;
            paras.Add(contractsubidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stockreceiptdetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_stockreceiptdetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter prenetamountpara = new SqlParameter("@PreNetAmount", SqlDbType.Decimal, 9);
            prenetamountpara.Value = st_stockreceiptdetail.PreNetAmount;
            paras.Add(prenetamountpara);

            SqlParameter receiptamountpara = new SqlParameter("@ReceiptAmount", SqlDbType.Decimal, 9);
            receiptamountpara.Value = st_stockreceiptdetail.ReceiptAmount;
            paras.Add(receiptamountpara);

            SqlParameter qtymisspara = new SqlParameter("@QtyMiss", SqlDbType.Decimal, 9);
            qtymisspara.Value = st_stockreceiptdetail.QtyMiss;
            paras.Add(qtymisspara);

            SqlParameter qtyratepara = new SqlParameter("@QtyRate", SqlDbType.Decimal, 9);
            qtyratepara.Value = st_stockreceiptdetail.QtyRate;
            paras.Add(qtyratepara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = st_stockreceiptdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel LoadByStockLogId(UserModel user, int stockLogId)
        {
            string cmdText = string.Format("select * from dbo.St_StockReceiptDetail srd where srd.DetailStatus>={0} and srd.StockLogId = {1}", (int)NFMT.Common.StatusEnum.已生效, stockLogId);

            return Load<Model.StockReceiptDetail>(user, CommandType.Text, cmdText);
        }

        public ResultModel Load(UserModel user, int receiptId, Common.StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select * from dbo.St_StockReceiptDetail where ReceiptId={0} and DetailStatus>={1}", receiptId, (int)status);
                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, cmdText, null, CommandType.Text);

                List<StockReceiptDetail> stockReceiptDetails = new List<StockReceiptDetail>();

                foreach (DataRow dr in dt.Rows)
                {
                    StockReceiptDetail stockreceiptdetail = new StockReceiptDetail();
                    stockreceiptdetail.DetailId = Convert.ToInt32(dr["DetailId"]);

                    stockreceiptdetail.ReceiptId = Convert.ToInt32(dr["ReceiptId"]);

                    if (dr["ContractId"] != DBNull.Value)
                    {
                        stockreceiptdetail.ContractId = Convert.ToInt32(dr["ContractId"]);
                    }
                    if (dr["ContractSubId"] != DBNull.Value)
                    {
                        stockreceiptdetail.ContractSubId = Convert.ToInt32(dr["ContractSubId"]);
                    }
                    if (dr["StockId"] != DBNull.Value)
                    {
                        stockreceiptdetail.StockId = Convert.ToInt32(dr["StockId"]);
                    }
                    if (dr["StockLogId"] != DBNull.Value)
                    {
                        stockreceiptdetail.StockLogId = Convert.ToInt32(dr["StockLogId"]);
                    }
                    if (dr["PreNetAmount"] != DBNull.Value)
                    {
                        stockreceiptdetail.PreNetAmount = Convert.ToDecimal(dr["PreNetAmount"]);
                    }
                    if (dr["ReceiptAmount"] != DBNull.Value)
                    {
                        stockreceiptdetail.ReceiptAmount = Convert.ToDecimal(dr["ReceiptAmount"]);
                    }
                    if (dr["QtyMiss"] != DBNull.Value)
                    {
                        stockreceiptdetail.QtyMiss = Convert.ToDecimal(dr["QtyMiss"]);
                    }
                    if (dr["QtyRate"] != DBNull.Value)
                    {
                        stockreceiptdetail.QtyRate = Convert.ToDecimal(dr["QtyRate"]);
                    }
                    if (dr["DetailStatus"] != DBNull.Value)
                    {
                        stockreceiptdetail.DetailStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr["DetailStatus"].ToString());
                    }
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        stockreceiptdetail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
                    }
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        stockreceiptdetail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    }
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        stockreceiptdetail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    }
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        stockreceiptdetail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    }
                    stockReceiptDetails.Add(stockreceiptdetail);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = stockReceiptDetails;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public override ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate)
        {
            ResultModel result = new ResultModel();

            bool allow = false;

            switch (operate)
            {
                case OperateEnum.作废:
                    allow = (obj.Status >= StatusEnum.已录入 && obj.Status < StatusEnum.待审核) || obj.Status == StatusEnum.绑定合约;
                    break;
                case OperateEnum.修改:
                    allow = true;
                    break;
                case OperateEnum.提交审核:
                    allow = (obj.Status >= StatusEnum.已录入 && obj.Status < StatusEnum.待审核);
                    break;
                case OperateEnum.撤返:
                    allow = obj.Status == StatusEnum.待审核;
                    break;
                case OperateEnum.冻结:
                    allow = obj.Status == StatusEnum.已生效;
                    break;
                case OperateEnum.解除冻结:
                    allow = obj.Status == StatusEnum.已冻结;
                    break;
                case OperateEnum.执行完成:
                    allow = obj.Status == StatusEnum.已生效;
                    break;
                case OperateEnum.确认完成:
                    allow = obj.Status == StatusEnum.已生效;
                    break;
                case OperateEnum.执行完成撤销:
                    allow = (obj.Status == StatusEnum.已完成 || obj.Status == StatusEnum.部分完成);
                    break;
                case OperateEnum.确认完成撤销:
                    allow = (obj.Status == StatusEnum.已完成 || obj.Status == StatusEnum.部分完成);
                    break;
                case OperateEnum.关闭:
                    allow = obj.Status == StatusEnum.已生效;
                    break;
                default:
                    allow = true;
                    break;
            }

            if (!allow)
            {
                result.ResultStatus = -1;
                result.Message = string.Format("{0}的数据不能进行{1}操作", obj.Status.ToString("F"), operate.ToString("F"));
                return result;
            }

            if (!this.OperateAuthority(user, operate))
            {
                result.ResultStatus = -1;
                result.Message = string.Format("没有当前数据的{0}权限", operate.ToString("F"));
                return result;
            }

            result.ResultStatus = 0;
            return result;
        }

        #endregion
    }
}
