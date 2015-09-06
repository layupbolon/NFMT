
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CustomsClearance.cs
// 文件功能描述：报关dbo.St_CustomsClearance实体类。
// 创建人：CodeSmith
// 创建时间： 2014年10月29日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 报关dbo.St_CustomsClearance实体类。
    /// </summary>
    [Serializable]
    public class CustomsClearance : IModel
    {
        #region 字段

        private int customsId;
        private int customsApplyId;
        private int customser;
        private int customsCorpId;
        private DateTime customsDate;
        private int customsName;
        private decimal grossWeight;
        private decimal netWeight;
        private int currencyId;
        private decimal customsPrice;
        private decimal tariffRate;
        private decimal addedValueRate;
        private decimal otherFees;
        private string memo = String.Empty;
        private Common.StatusEnum customsStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.St_CustomsClearance";
        #endregion

        #region 构造函数

        public CustomsClearance()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 报关序号
        /// </summary>
        public int CustomsId
        {
            get { return customsId; }
            set { customsId = value; }
        }

        /// <summary>
        /// 报关申请序号
        /// </summary>
        public int CustomsApplyId
        {
            get { return customsApplyId; }
            set { customsApplyId = value; }
        }

        /// <summary>
        /// 报关人
        /// </summary>
        public int Customser
        {
            get { return customser; }
            set { customser = value; }
        }

        /// <summary>
        /// 实际报关公司
        /// </summary>
        public int CustomsCorpId
        {
            get { return customsCorpId; }
            set { customsCorpId = value; }
        }

        /// <summary>
        /// 报关时间
        /// </summary>
        public DateTime CustomsDate
        {
            get { return customsDate; }
            set { customsDate = value; }
        }

        /// <summary>
        /// 报关海关
        /// </summary>
        public int CustomsName
        {
            get { return customsName; }
            set { customsName = value; }
        }

        /// <summary>
        /// 报关总毛重
        /// </summary>
        public decimal GrossWeight
        {
            get { return grossWeight; }
            set { grossWeight = value; }
        }

        /// <summary>
        /// 报关总净重
        /// </summary>
        public decimal NetWeight
        {
            get { return netWeight; }
            set { netWeight = value; }
        }

        /// <summary>
        /// 币种
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
        }

        /// <summary>
        /// 报关单价
        /// </summary>
        public decimal CustomsPrice
        {
            get { return customsPrice; }
            set { customsPrice = value; }
        }

        /// <summary>
        /// 关税税率
        /// </summary>
        public decimal TariffRate
        {
            get { return tariffRate; }
            set { tariffRate = value; }
        }

        /// <summary>
        /// 增值税率
        /// </summary>
        public decimal AddedValueRate
        {
            get { return addedValueRate; }
            set { addedValueRate = value; }
        }

        /// <summary>
        /// 检验检疫费
        /// </summary>
        public decimal OtherFees
        {
            get { return otherFees; }
            set { otherFees = value; }
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
        /// 报关状态
        /// </summary>
        public Common.StatusEnum CustomsStatus
        {
            get { return customsStatus; }
            set { customsStatus = value; }
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
            get { return this.customsId; }
            set { this.customsId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return customsStatus; }
            set { customsStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WareHouse.DAL.CustomsClearanceDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.WareHouse";
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