using NFMT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.StockBasic.Model
{
    /// <summary>
    /// 出入库流水dbo.St_StockLog实体类。
    /// </summary>
    [Serializable]
    public class StockLog : IModel
    {
        #region 字段

        private int stockLogId;
        private int stockId;
        private int stockNameId;
        private string refNo = String.Empty;
        private int logDirection;
        private int logType;
        private int contractId;
        private int subContractId;
        private DateTime logDate;
        private int opPerson;
        private int assetId;
        private int bundles;
        private decimal grossAmount;
        private decimal netAmount;
        private decimal gapAmount;
        private int mUId;
        private int brandId;
        private int groupId;
        private int corpId;
        private int deptId;
        private int customsType;
        private int deliverPlaceId;
        private int producerId;
        private string paperNo = String.Empty;
        private int paperHolder;
        private string cardNo = String.Empty;
        private int stockType;
        private string format = String.Empty;
        private int originPlaceId;
        private string originPlace = String.Empty;
        private string memo = String.Empty;
        private StatusEnum logStatus;
        private string logSourceBase = String.Empty;
        private string logSource = String.Empty;
        private int sourceId;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.St_StockLog";
        #endregion

        #region 构造函数

        public StockLog()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 流水序号
        /// </summary>
        public int StockLogId
        {
            get { return stockLogId; }
            set { stockLogId = value; }
        }

        /// <summary>
        /// 库存序号
        /// </summary>
        public int StockId
        {
            get { return stockId; }
            set { stockId = value; }
        }

        /// <summary>
        /// 业务单序号
        /// </summary>
        public int StockNameId
        {
            get { return stockNameId; }
            set { stockNameId = value; }
        }

        /// <summary>
        /// 业务单号
        /// </summary>
        public string RefNo
        {
            get { return refNo; }
            set { refNo = value; }
        }

        /// <summary>
        /// 流水方向/进或出
        /// </summary>
        public int LogDirection
        {
            get { return logDirection; }
            set { logDirection = value; }
        }

        /// <summary>
        /// 操作类型/出库，出库冲销，入库，入库冲销，移库，移库冲销，质押，质押冲销，回购，回购冲销，收款，收款冲销，付款，付款冲销，开票，开票冲销，收票，收票冲销。
        /// </summary>
        public int LogType
        {
            get { return logType; }
            set { logType = value; }
        }

        /// <summary>
        /// 合约序号
        /// </summary>
        public int ContractId
        {
            get { return contractId; }
            set { contractId = value; }
        }

        /// <summary>
        /// 子合约序号
        /// </summary>
        public int SubContractId
        {
            get { return subContractId; }
            set { subContractId = value; }
        }

        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime LogDate
        {
            get { return logDate; }
            set { logDate = value; }
        }

        /// <summary>
        /// 操作人
        /// </summary>
        public int OpPerson
        {
            get { return opPerson; }
            set { opPerson = value; }
        }

        /// <summary>
        /// 品种
        /// </summary>
        public int AssetId
        {
            get { return assetId; }
            set { assetId = value; }
        }

        /// <summary>
        /// 捆数
        /// </summary>
        public int Bundles
        {
            get { return bundles; }
            set { bundles = value; }
        }

        /// <summary>
        /// 毛量
        /// </summary>
        public decimal GrossAmount
        {
            get { return grossAmount; }
            set { grossAmount = value; }
        }

        /// <summary>
        /// 净量
        /// </summary>
        public decimal NetAmount
        {
            get { return netAmount; }
            set { netAmount = value; }
        }

        /// <summary>
        /// 磅差
        /// </summary>
        public decimal GapAmount
        {
            get { return gapAmount; }
            set { gapAmount = value; }
        }

        /// <summary>
        /// 计量单位
        /// </summary>
        public int MUId
        {
            get { return mUId; }
            set { mUId = value; }
        }

        /// <summary>
        /// 品牌
        /// </summary>
        public int BrandId
        {
            get { return brandId; }
            set { brandId = value; }
        }

        /// <summary>
        /// 所属集团
        /// </summary>
        public int GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }

        /// <summary>
        /// 所属公司
        /// </summary>
        public int CorpId
        {
            get { return corpId; }
            set { corpId = value; }
        }

        /// <summary>
        /// 所属部门
        /// </summary>
        public int DeptId
        {
            get { return deptId; }
            set { deptId = value; }
        }

        /// <summary>
        /// 报关状态
        /// </summary>
        public int CustomsType
        {
            get { return customsType; }
            set { customsType = value; }
        }

        /// <summary>
        /// 交货地
        /// </summary>
        public int DeliverPlaceId
        {
            get { return deliverPlaceId; }
            set { deliverPlaceId = value; }
        }

        /// <summary>
        /// 生产商序号
        /// </summary>
        public int ProducerId
        {
            get { return producerId; }
            set { producerId = value; }
        }

        /// <summary>
        /// 权证编号
        /// </summary>
        public string PaperNo
        {
            get { return paperNo; }
            set { paperNo = value; }
        }

        /// <summary>
        /// 单据保管人
        /// </summary>
        public int PaperHolder
        {
            get { return paperHolder; }
            set { paperHolder = value; }
        }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }

        /// <summary>
        /// 库存类型(提报货)
        /// </summary>
        public int StockType
        {
            get { return stockType; }
            set { stockType = value; }
        }

        /// <summary>
        /// 规格
        /// </summary>
        public string Format
        {
            get { return format; }
            set { format = value; }
        }

        /// <summary>
        /// 产地序号
        /// </summary>
        public int OriginPlaceId
        {
            get { return originPlaceId; }
            set { originPlaceId = value; }
        }

        /// <summary>
        /// 产地
        /// </summary>
        public string OriginPlace
        {
            get { return originPlace; }
            set { originPlace = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }

        /// <summary>
        /// 流水状态
        /// </summary>
        public StatusEnum LogStatus
        {
            get { return logStatus; }
            set { logStatus = value; }
        }

        /// <summary>
        /// 流水来源库名
        /// </summary>
        public string LogSourceBase
        {
            get { return logSourceBase; }
            set { logSourceBase = value; }
        }

        /// <summary>
        /// 流水来源/表名记录
        /// </summary>
        public string LogSource
        {
            get { return logSource; }
            set { logSource = value; }
        }

        /// <summary>
        /// 来源编号/表序号记录
        /// </summary>
        public int SourceId
        {
            get { return sourceId; }
            set { sourceId = value; }
        }

        /// <summary>
        /// 创建人序号
        /// </summary>
        public int CreatorId
        {
            get { return creatorId; }
            set { creatorId = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 最后修改人序号
        /// </summary>
        public int LastModifyId
        {
            get { return lastModifyId; }
            set { lastModifyId = value; }
        }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifyTime
        {
            get { return lastModifyTime; }
            set { lastModifyTime = value; }
        }

        /// <summary>
        /// 数据序号
        /// </summary>
        public int Id
        {
            get { return this.stockLogId; }
            set { this.stockLogId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return logStatus; }
            set { logStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.StockBasic.DAL.StockLogDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.StockBasic";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }
        }

        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "NFMT";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}
