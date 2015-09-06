/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockLogDAL.cs
// 文件功能描述：出入库流水dbo.St_StockLog数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年1月19日
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
    /// 出入库流水dbo.St_StockLog数据交互类。
    /// </summary>
    public class StockLogDAL : ExecOperate, IStockLogDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockLogDAL()
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
            StockLog st_stocklog = (StockLog)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockLogId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stocklog.StockId;
            paras.Add(stockidpara);

            SqlParameter stocknameidpara = new SqlParameter("@StockNameId", SqlDbType.Int, 4);
            stocknameidpara.Value = st_stocklog.StockNameId;
            paras.Add(stocknameidpara);

            if (!string.IsNullOrEmpty(st_stocklog.RefNo))
            {
                SqlParameter refnopara = new SqlParameter("@RefNo", SqlDbType.VarChar, 50);
                refnopara.Value = st_stocklog.RefNo;
                paras.Add(refnopara);
            }

            SqlParameter logdirectionpara = new SqlParameter("@LogDirection", SqlDbType.Int, 4);
            logdirectionpara.Value = st_stocklog.LogDirection;
            paras.Add(logdirectionpara);

            SqlParameter logtypepara = new SqlParameter("@LogType", SqlDbType.Int, 4);
            logtypepara.Value = st_stocklog.LogType;
            paras.Add(logtypepara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = st_stocklog.ContractId;
            paras.Add(contractidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = st_stocklog.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter logdatepara = new SqlParameter("@LogDate", SqlDbType.DateTime, 8);
            logdatepara.Value = st_stocklog.LogDate;
            paras.Add(logdatepara);

            SqlParameter oppersonpara = new SqlParameter("@OpPerson", SqlDbType.Int, 4);
            oppersonpara.Value = st_stocklog.OpPerson;
            paras.Add(oppersonpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = st_stocklog.AssetId;
            paras.Add(assetidpara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = st_stocklog.Bundles;
            paras.Add(bundlespara);

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = st_stocklog.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = st_stocklog.NetAmount;
            paras.Add(netamountpara);

            SqlParameter gapamountpara = new SqlParameter("@GapAmount", SqlDbType.Decimal, 9);
            gapamountpara.Value = st_stocklog.GapAmount;
            paras.Add(gapamountpara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = st_stocklog.MUId;
            paras.Add(muidpara);

            SqlParameter brandidpara = new SqlParameter("@BrandId", SqlDbType.Int, 4);
            brandidpara.Value = st_stocklog.BrandId;
            paras.Add(brandidpara);

            SqlParameter groupidpara = new SqlParameter("@GroupId", SqlDbType.Int, 4);
            groupidpara.Value = st_stocklog.GroupId;
            paras.Add(groupidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = st_stocklog.CorpId;
            paras.Add(corpidpara);

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = st_stocklog.DeptId;
            paras.Add(deptidpara);

            SqlParameter customstypepara = new SqlParameter("@CustomsType", SqlDbType.Int, 4);
            customstypepara.Value = st_stocklog.CustomsType;
            paras.Add(customstypepara);

            SqlParameter deliverplaceidpara = new SqlParameter("@DeliverPlaceId", SqlDbType.Int, 4);
            deliverplaceidpara.Value = st_stocklog.DeliverPlaceId;
            paras.Add(deliverplaceidpara);

            SqlParameter produceridpara = new SqlParameter("@ProducerId", SqlDbType.Int, 4);
            produceridpara.Value = st_stocklog.ProducerId;
            paras.Add(produceridpara);

            if (!string.IsNullOrEmpty(st_stocklog.PaperNo))
            {
                SqlParameter papernopara = new SqlParameter("@PaperNo", SqlDbType.VarChar, 80);
                papernopara.Value = st_stocklog.PaperNo;
                paras.Add(papernopara);
            }

            SqlParameter paperholderpara = new SqlParameter("@PaperHolder", SqlDbType.Int, 4);
            paperholderpara.Value = st_stocklog.PaperHolder;
            paras.Add(paperholderpara);

            if (!string.IsNullOrEmpty(st_stocklog.CardNo))
            {
                SqlParameter cardnopara = new SqlParameter("@CardNo", SqlDbType.VarChar, 200);
                cardnopara.Value = st_stocklog.CardNo;
                paras.Add(cardnopara);
            }

            SqlParameter stocktypepara = new SqlParameter("@StockType", SqlDbType.Int, 4);
            stocktypepara.Value = st_stocklog.StockType;
            paras.Add(stocktypepara);

            if (!string.IsNullOrEmpty(st_stocklog.Format))
            {
                SqlParameter formatpara = new SqlParameter("@Format", SqlDbType.VarChar, 200);
                formatpara.Value = st_stocklog.Format;
                paras.Add(formatpara);
            }

            SqlParameter originplaceidpara = new SqlParameter("@OriginPlaceId", SqlDbType.Int, 4);
            originplaceidpara.Value = st_stocklog.OriginPlaceId;
            paras.Add(originplaceidpara);

            if (!string.IsNullOrEmpty(st_stocklog.OriginPlace))
            {
                SqlParameter originplacepara = new SqlParameter("@OriginPlace", SqlDbType.VarChar, 200);
                originplacepara.Value = st_stocklog.OriginPlace;
                paras.Add(originplacepara);
            }

            SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
            memopara.Value = st_stocklog.Memo;
            paras.Add(memopara);

            SqlParameter logstatuspara = new SqlParameter("@LogStatus", SqlDbType.Int, 4);
            logstatuspara.Value = st_stocklog.LogStatus;
            paras.Add(logstatuspara);

            if (!string.IsNullOrEmpty(st_stocklog.LogSourceBase))
            {
                SqlParameter logsourcebasepara = new SqlParameter("@LogSourceBase", SqlDbType.VarChar, 50);
                logsourcebasepara.Value = st_stocklog.LogSourceBase;
                paras.Add(logsourcebasepara);
            }

            if (!string.IsNullOrEmpty(st_stocklog.LogSource))
            {
                SqlParameter logsourcepara = new SqlParameter("@LogSource", SqlDbType.VarChar, 200);
                logsourcepara.Value = st_stocklog.LogSource;
                paras.Add(logsourcepara);
            }

            SqlParameter sourceidpara = new SqlParameter("@SourceId", SqlDbType.Int, 4);
            sourceidpara.Value = st_stocklog.SourceId;
            paras.Add(sourceidpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockLog stocklog = new StockLog();

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            stocklog.StockLogId = Convert.ToInt32(dr[indexStockLogId]);

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                stocklog.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockNameId = dr.GetOrdinal("StockNameId");
            if (dr["StockNameId"] != DBNull.Value)
            {
                stocklog.StockNameId = Convert.ToInt32(dr[indexStockNameId]);
            }

            int indexRefNo = dr.GetOrdinal("RefNo");
            if (dr["RefNo"] != DBNull.Value)
            {
                stocklog.RefNo = Convert.ToString(dr[indexRefNo]);
            }

            int indexLogDirection = dr.GetOrdinal("LogDirection");
            if (dr["LogDirection"] != DBNull.Value)
            {
                stocklog.LogDirection = Convert.ToInt32(dr[indexLogDirection]);
            }

            int indexLogType = dr.GetOrdinal("LogType");
            stocklog.LogType = Convert.ToInt32(dr[indexLogType]);

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                stocklog.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexSubContractId = dr.GetOrdinal("SubContractId");
            if (dr["SubContractId"] != DBNull.Value)
            {
                stocklog.SubContractId = Convert.ToInt32(dr[indexSubContractId]);
            }

            int indexLogDate = dr.GetOrdinal("LogDate");
            stocklog.LogDate = Convert.ToDateTime(dr[indexLogDate]);

            int indexOpPerson = dr.GetOrdinal("OpPerson");
            if (dr["OpPerson"] != DBNull.Value)
            {
                stocklog.OpPerson = Convert.ToInt32(dr[indexOpPerson]);
            }

            int indexAssetId = dr.GetOrdinal("AssetId");
            if (dr["AssetId"] != DBNull.Value)
            {
                stocklog.AssetId = Convert.ToInt32(dr[indexAssetId]);
            }

            int indexBundles = dr.GetOrdinal("Bundles");
            stocklog.Bundles = Convert.ToInt32(dr[indexBundles]);

            int indexGrossAmount = dr.GetOrdinal("GrossAmount");
            stocklog.GrossAmount = Convert.ToDecimal(dr[indexGrossAmount]);

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            stocklog.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);

            int indexGapAmount = dr.GetOrdinal("GapAmount");
            if (dr["GapAmount"] != DBNull.Value)
            {
                stocklog.GapAmount = Convert.ToDecimal(dr[indexGapAmount]);
            }

            int indexMUId = dr.GetOrdinal("MUId");
            if (dr["MUId"] != DBNull.Value)
            {
                stocklog.MUId = Convert.ToInt32(dr[indexMUId]);
            }

            int indexBrandId = dr.GetOrdinal("BrandId");
            if (dr["BrandId"] != DBNull.Value)
            {
                stocklog.BrandId = Convert.ToInt32(dr[indexBrandId]);
            }

            int indexGroupId = dr.GetOrdinal("GroupId");
            if (dr["GroupId"] != DBNull.Value)
            {
                stocklog.GroupId = Convert.ToInt32(dr[indexGroupId]);
            }

            int indexCorpId = dr.GetOrdinal("CorpId");
            if (dr["CorpId"] != DBNull.Value)
            {
                stocklog.CorpId = Convert.ToInt32(dr[indexCorpId]);
            }

            int indexDeptId = dr.GetOrdinal("DeptId");
            if (dr["DeptId"] != DBNull.Value)
            {
                stocklog.DeptId = Convert.ToInt32(dr[indexDeptId]);
            }

            int indexCustomsType = dr.GetOrdinal("CustomsType");
            if (dr["CustomsType"] != DBNull.Value)
            {
                stocklog.CustomsType = Convert.ToInt32(dr[indexCustomsType]);
            }

            int indexDeliverPlaceId = dr.GetOrdinal("DeliverPlaceId");
            if (dr["DeliverPlaceId"] != DBNull.Value)
            {
                stocklog.DeliverPlaceId = Convert.ToInt32(dr[indexDeliverPlaceId]);
            }

            int indexProducerId = dr.GetOrdinal("ProducerId");
            if (dr["ProducerId"] != DBNull.Value)
            {
                stocklog.ProducerId = Convert.ToInt32(dr[indexProducerId]);
            }

            int indexPaperNo = dr.GetOrdinal("PaperNo");
            if (dr["PaperNo"] != DBNull.Value)
            {
                stocklog.PaperNo = Convert.ToString(dr[indexPaperNo]);
            }

            int indexPaperHolder = dr.GetOrdinal("PaperHolder");
            if (dr["PaperHolder"] != DBNull.Value)
            {
                stocklog.PaperHolder = Convert.ToInt32(dr[indexPaperHolder]);
            }

            int indexCardNo = dr.GetOrdinal("CardNo");
            if (dr["CardNo"] != DBNull.Value)
            {
                stocklog.CardNo = Convert.ToString(dr[indexCardNo]);
            }

            int indexStockType = dr.GetOrdinal("StockType");
            if (dr["StockType"] != DBNull.Value)
            {
                stocklog.StockType = Convert.ToInt32(dr[indexStockType]);
            }

            int indexFormat = dr.GetOrdinal("Format");
            if (dr["Format"] != DBNull.Value)
            {
                stocklog.Format = Convert.ToString(dr[indexFormat]);
            }

            int indexOriginPlaceId = dr.GetOrdinal("OriginPlaceId");
            if (dr["OriginPlaceId"] != DBNull.Value)
            {
                stocklog.OriginPlaceId = Convert.ToInt32(dr[indexOriginPlaceId]);
            }

            int indexOriginPlace = dr.GetOrdinal("OriginPlace");
            if (dr["OriginPlace"] != DBNull.Value)
            {
                stocklog.OriginPlace = Convert.ToString(dr[indexOriginPlace]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            stocklog.Memo = Convert.ToString(dr[indexMemo]);

            int indexLogStatus = dr.GetOrdinal("LogStatus");
            if (dr["LogStatus"] != DBNull.Value)
            {
                stocklog.LogStatus = (StatusEnum)Convert.ToInt32(dr[indexLogStatus]);
            }

            int indexLogSourceBase = dr.GetOrdinal("LogSourceBase");
            if (dr["LogSourceBase"] != DBNull.Value)
            {
                stocklog.LogSourceBase = Convert.ToString(dr[indexLogSourceBase]);
            }

            int indexLogSource = dr.GetOrdinal("LogSource");
            if (dr["LogSource"] != DBNull.Value)
            {
                stocklog.LogSource = Convert.ToString(dr[indexLogSource]);
            }

            int indexSourceId = dr.GetOrdinal("SourceId");
            if (dr["SourceId"] != DBNull.Value)
            {
                stocklog.SourceId = Convert.ToInt32(dr[indexSourceId]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                stocklog.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                stocklog.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                stocklog.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                stocklog.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return stocklog;
        }

        public override string TableName
        {
            get
            {
                return "St_StockLog";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockLog st_stocklog = (StockLog)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_stocklog.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stocklog.StockId;
            paras.Add(stockidpara);

            SqlParameter stocknameidpara = new SqlParameter("@StockNameId", SqlDbType.Int, 4);
            stocknameidpara.Value = st_stocklog.StockNameId;
            paras.Add(stocknameidpara);

            if (!string.IsNullOrEmpty(st_stocklog.RefNo))
            {
                SqlParameter refnopara = new SqlParameter("@RefNo", SqlDbType.VarChar, 50);
                refnopara.Value = st_stocklog.RefNo;
                paras.Add(refnopara);
            }

            SqlParameter logdirectionpara = new SqlParameter("@LogDirection", SqlDbType.Int, 4);
            logdirectionpara.Value = st_stocklog.LogDirection;
            paras.Add(logdirectionpara);

            SqlParameter logtypepara = new SqlParameter("@LogType", SqlDbType.Int, 4);
            logtypepara.Value = st_stocklog.LogType;
            paras.Add(logtypepara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = st_stocklog.ContractId;
            paras.Add(contractidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = st_stocklog.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter logdatepara = new SqlParameter("@LogDate", SqlDbType.DateTime, 8);
            logdatepara.Value = st_stocklog.LogDate;
            paras.Add(logdatepara);

            SqlParameter oppersonpara = new SqlParameter("@OpPerson", SqlDbType.Int, 4);
            oppersonpara.Value = st_stocklog.OpPerson;
            paras.Add(oppersonpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = st_stocklog.AssetId;
            paras.Add(assetidpara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = st_stocklog.Bundles;
            paras.Add(bundlespara);

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = st_stocklog.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = st_stocklog.NetAmount;
            paras.Add(netamountpara);

            SqlParameter gapamountpara = new SqlParameter("@GapAmount", SqlDbType.Decimal, 9);
            gapamountpara.Value = st_stocklog.GapAmount;
            paras.Add(gapamountpara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = st_stocklog.MUId;
            paras.Add(muidpara);

            SqlParameter brandidpara = new SqlParameter("@BrandId", SqlDbType.Int, 4);
            brandidpara.Value = st_stocklog.BrandId;
            paras.Add(brandidpara);

            SqlParameter groupidpara = new SqlParameter("@GroupId", SqlDbType.Int, 4);
            groupidpara.Value = st_stocklog.GroupId;
            paras.Add(groupidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = st_stocklog.CorpId;
            paras.Add(corpidpara);

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = st_stocklog.DeptId;
            paras.Add(deptidpara);

            SqlParameter customstypepara = new SqlParameter("@CustomsType", SqlDbType.Int, 4);
            customstypepara.Value = st_stocklog.CustomsType;
            paras.Add(customstypepara);

            SqlParameter deliverplaceidpara = new SqlParameter("@DeliverPlaceId", SqlDbType.Int, 4);
            deliverplaceidpara.Value = st_stocklog.DeliverPlaceId;
            paras.Add(deliverplaceidpara);

            SqlParameter produceridpara = new SqlParameter("@ProducerId", SqlDbType.Int, 4);
            produceridpara.Value = st_stocklog.ProducerId;
            paras.Add(produceridpara);

            if (!string.IsNullOrEmpty(st_stocklog.PaperNo))
            {
                SqlParameter papernopara = new SqlParameter("@PaperNo", SqlDbType.VarChar, 80);
                papernopara.Value = st_stocklog.PaperNo;
                paras.Add(papernopara);
            }

            SqlParameter paperholderpara = new SqlParameter("@PaperHolder", SqlDbType.Int, 4);
            paperholderpara.Value = st_stocklog.PaperHolder;
            paras.Add(paperholderpara);

            if (!string.IsNullOrEmpty(st_stocklog.CardNo))
            {
                SqlParameter cardnopara = new SqlParameter("@CardNo", SqlDbType.VarChar, 200);
                cardnopara.Value = st_stocklog.CardNo;
                paras.Add(cardnopara);
            }

            SqlParameter stocktypepara = new SqlParameter("@StockType", SqlDbType.Int, 4);
            stocktypepara.Value = st_stocklog.StockType;
            paras.Add(stocktypepara);

            if (!string.IsNullOrEmpty(st_stocklog.Format))
            {
                SqlParameter formatpara = new SqlParameter("@Format", SqlDbType.VarChar, 200);
                formatpara.Value = st_stocklog.Format;
                paras.Add(formatpara);
            }

            SqlParameter originplaceidpara = new SqlParameter("@OriginPlaceId", SqlDbType.Int, 4);
            originplaceidpara.Value = st_stocklog.OriginPlaceId;
            paras.Add(originplaceidpara);

            if (!string.IsNullOrEmpty(st_stocklog.OriginPlace))
            {
                SqlParameter originplacepara = new SqlParameter("@OriginPlace", SqlDbType.VarChar, 200);
                originplacepara.Value = st_stocklog.OriginPlace;
                paras.Add(originplacepara);
            }

            SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
            memopara.Value = st_stocklog.Memo;
            paras.Add(memopara);

            SqlParameter logstatuspara = new SqlParameter("@LogStatus", SqlDbType.Int, 4);
            logstatuspara.Value = st_stocklog.LogStatus;
            paras.Add(logstatuspara);

            if (!string.IsNullOrEmpty(st_stocklog.LogSourceBase))
            {
                SqlParameter logsourcebasepara = new SqlParameter("@LogSourceBase", SqlDbType.VarChar, 50);
                logsourcebasepara.Value = st_stocklog.LogSourceBase;
                paras.Add(logsourcebasepara);
            }

            if (!string.IsNullOrEmpty(st_stocklog.LogSource))
            {
                SqlParameter logsourcepara = new SqlParameter("@LogSource", SqlDbType.VarChar, 200);
                logsourcepara.Value = st_stocklog.LogSource;
                paras.Add(logsourcepara);
            }

            SqlParameter sourceidpara = new SqlParameter("@SourceId", SqlDbType.Int, 4);
            sourceidpara.Value = st_stocklog.SourceId;
            paras.Add(sourceidpara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetStockContractId(UserModel user, int stockId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select isnull(ContractId,0) as ContractId,isnull(SubContractId,0) as SubContractId from dbo.St_StockLog where StockId = {0}", stockId);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string str = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        str = dr["ContractId"].ToString() + "," + dr["SubContractId"].ToString();
                        break;
                    }
                    result.Message = "获取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = str;
                }
                else
                {
                    result.Message = "获取失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        public ResultModel LoadStockLogBySubId(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                string cmdText = string.Format("select * from dbo.St_StockLog where SubContractId={0}", subId);

                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, null);

                if (!dr.HasRows)
                {
                    result.ResultStatus = -1;
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                    return result;
                }

                List<Model.StockLog> models = new List<StockLog>();

                int i = 0;
                while (dr.Read())
                {
                    StockLog t = this.CreateModel<StockLog>(dr);
                    models.Add(t);
                    i++;
                }

                result.AffectCount = i;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = models;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        public override ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate)
        {
            if (operate == OperateEnum.修改)
            {
                ResultModel result = new ResultModel();
                result.ResultStatus = 0;
                return result;
            }

            return base.AllowOperate(user, obj, operate);
        }

        public ResultModel GetLastStockLogByStockId(UserModel user, int stockId) {
            string sql = string.Format("select top 1 * from dbo.St_StockLog where StockId = {0} and LogStatus >= {1} order by StockLogId desc ", stockId, (int)Common.StatusEnum.已生效);

            return Get(user, CommandType.Text, sql, null);
        }

        #endregion
    }
}
