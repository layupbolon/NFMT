/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BasicDataProvider.cs
// 文件功能描述：基本数据提供者。
// 创建人：pekah.chow
// 创建时间： 2014年6月27日
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Data
{
    public class BasicDataProvider
    {
        private static List<Model.Area> areas;
        private static List<Model.Asset> assets;
        private static List<Model.BankAccount> bankAccounts;
        private static List<Model.Bank> banks;
        private static List<Model.Brand> brands;
        private static List<Model.Currency> currencies;
        private static List<Model.DeliverPlace> deliverPlaces;
        private static List<Model.Exchange> exchanges;
        private static List<Model.FuturesCode> futuresCodes;
        private static List<Model.FuturesPrice> futuresPrices;
        private static List<Model.MeasureUnit> measureUnits;
        private static List<Model.Producer> producers;
        private static List<Model.Rate> rates;
        private static List<Model.BDStyle> styles;
        private static List<Model.BDStyleDetail> styleDetail;
        private static List<Model.ContractMaster> masters;
        private static List<Model.ContractClause> clauses;
        private static List<Model.ContractClauseDetail> clauseDetails;
        private static List<Model.ClauseContractRef> refs;

        static BasicDataProvider()
        {
            areas = new List<Model.Area>();
            assets = new List<Model.Asset>();
            bankAccounts = new List<Model.BankAccount>();
            banks = new List<Model.Bank>();
            brands = new List<Model.Brand>();
            currencies = new List<Model.Currency>();
            deliverPlaces = new List<Model.DeliverPlace>();
            exchanges = new List<Model.Exchange>();
            futuresCodes = new List<Model.FuturesCode>();
            futuresPrices = new List<Model.FuturesPrice>();
            measureUnits = new List<Model.MeasureUnit>();
            producers = new List<Model.Producer>();
            rates = new List<Model.Rate>();
            styles = new List<Model.BDStyle>();
            styleDetail = new List<Model.BDStyleDetail>();
            masters = new List<Model.ContractMaster>();
            clauses = new List<Model.ContractClause>();
            clauseDetails = new List<Model.ContractClauseDetail>();
            refs = new List<Model.ClauseContractRef>();
        }

        /// <summary>
        /// 获取系统中所有区域信息
        /// </summary>
        public static List<Model.Area> Areas
        {
            get
            {
                if (areas != null && areas.Count > 0)
                    return areas;

                areas = RegisterBasic<Model.Area>(new DAL.AreaDAL());

                return areas;
            }
        }

        /// <summary>
        /// 获取系统中所有品种信息
        /// </summary>
        public static List<Model.Asset> Assets
        {
            get
            {
                if (assets != null && assets.Count > 0)
                    return assets;

                assets = RegisterBasic<Model.Asset>(new DAL.AssetDAL());

                return assets;
            }
        }

        /// <summary>
        /// 获取系统中所有银行账号信息
        /// </summary>
        public static List<Model.BankAccount> BankAccounts
        {
            get
            {
                if (bankAccounts != null && bankAccounts.Count > 0)
                    return bankAccounts;

                bankAccounts = RegisterBasic<Model.BankAccount>(new DAL.BankAccountDAL());

                return bankAccounts;
            }
        }

        /// <summary>
        ///  获取系统中所有银行信息
        /// </summary>
        public static List<Model.Bank> Banks
        {
            get
            {
                if (banks != null && banks.Count > 0)
                    return banks;

                banks = RegisterBasic<Model.Bank>(new DAL.BankDAL());

                return banks;
            }
        }

        /// <summary>
        /// 获取系统中所有品牌信息
        /// </summary>
        public static List<Model.Brand> Brands
        {
            get
            {
                if (brands != null && brands.Count > 0)
                    return brands;

                brands = RegisterBasic<Model.Brand>(new DAL.BrandDAL());

                return brands;
            }
        }        

        /// <summary>
        /// 获取系统中所有币种信息
        /// </summary>
        public static List<Model.Currency> Currencies
        {
            get
            {
                if (currencies != null && currencies.Count > 0)
                    return currencies;

                currencies = RegisterBasic<Model.Currency>(new DAL.CurrencyDAL());

                return currencies;
            }
        }

        /// <summary>
        /// 获取系统中所有交货地信息
        /// </summary>
        public static List<Model.DeliverPlace> DeliverPlaces
        {
            get
            {
                if (deliverPlaces != null && deliverPlaces.Count > 0)
                    return deliverPlaces;

                deliverPlaces = RegisterBasic<Model.DeliverPlace>(new DAL.DeliverPlaceDAL());

                return deliverPlaces;
            }
        }

        /// <summary>
        /// 获取系统中所有交易所信息
        /// </summary>
        public static List<Model.Exchange> Exchanges
        {
            get
            {
                if (exchanges != null && exchanges.Count > 0)
                    return exchanges;

                exchanges = RegisterBasic<Model.Exchange>(new DAL.ExchangeDAL());

                return exchanges;
            }
        }

        /// <summary>
        /// 获取系统中所有期货合约信息
        /// </summary>
        public static List<Model.FuturesCode> FuturesCodes
        {
            get
            {
                if (futuresCodes != null && futuresCodes.Count > 0)
                    return futuresCodes;

                futuresCodes = RegisterBasic<Model.FuturesCode>(new DAL.FuturesCodeDAL());

                return futuresCodes;
            }
        }

        /// <summary>
        /// 获取系统中所有期货合约结算价信息
        /// </summary>
        public static List<Model.FuturesPrice> FuturesPrices
        {
            get
            {
                if (futuresPrices != null && futuresPrices.Count > 0)
                    return futuresPrices;

                futuresPrices = RegisterBasic<Model.FuturesPrice>(new DAL.FuturesPriceDAL());

                return futuresPrices;
            }
        }

        /// <summary>
        /// 获取系统中所有计量单位信息
        /// </summary>
        public static List<Model.MeasureUnit> MeasureUnits
        {
            get
            {
                if (measureUnits != null && measureUnits.Count > 0)
                    return measureUnits;

                measureUnits = RegisterBasic<Model.MeasureUnit>(new DAL.MeasureUnitDAL());

                return measureUnits;
            }
        }

        /// <summary>
        /// 获取系统中所有生产商信息
        /// </summary>
        public static List<Model.Producer> Producers
        {
            get
            {
                if (producers != null && producers.Count > 0)
                    return producers;

                producers = RegisterBasic<Model.Producer>(new DAL.ProducerDAL());

                return producers;
            }
        }

        /// <summary>
        /// 获取系统中所有汇率信息
        /// </summary>
        public static List<Model.Rate> Rates
        {
            get
            {
                if (rates != null && rates.Count > 0)
                    return rates;

                rates = RegisterBasic<Model.Rate>(new DAL.RateDAL());

                return rates;
            }
        }

        /// <summary>
        /// 获取系统中所有事项类型
        /// </summary>
        public static List<Model.BDStyle> BDStyles
        {
            get
            {
                if (styles != null && styles.Count > 0)
                    return styles;

                styles = RegisterBasic<Model.BDStyle>(new DAL.BDStyleDAL());

                return styles;
            }
        }

        /// <summary>
        /// 获取系统中所有类型明细
        /// </summary>
        public static List<Model.BDStyleDetail> StyleDetails
        {
            get
            {
                if (styleDetail != null && styleDetail.Count > 0)
                    return styleDetail;

                styleDetail = RegisterBasic<Model.BDStyleDetail>(new DAL.BDStyleDetailDAL());

                return styleDetail;
            }
        }

        /// <summary>
        /// 获取系统中所有合约模板
        /// </summary>
        public static List<Model.ContractMaster> ContractMasters
        {
            get
            {
                if (masters != null && masters.Count > 0)
                    return masters;

                masters = RegisterBasic<Model.ContractMaster>(new DAL.ContractMasterDAL());

                return masters;
            }
        }

        /// <summary>
        /// 获取系统中所有合约条款
        /// </summary>
        public static List<Model.ContractClause> ContractClauses
        {
            get
            {
                if (clauses != null && masters.Count > 0)
                    return clauses;

                clauses = RegisterBasic<Model.ContractClause>(new DAL.ContractClauseDAL());

                return clauses;
            }
        }

        /// <summary>
        /// 获取系统中所有合约条款与合约模板的关系
        /// </summary>
        public static List<Model.ClauseContractRef> ClauseContractRefs
        {
            get
            {
                if (refs != null && masters.Count > 0)
                    return refs;

                refs = RegisterBasic<Model.ClauseContractRef>(new DAL.ClauseContractRefDAL());

                return refs;
            }
        }

        /// <summary>
        /// 获取合约模板下的所有合约条款
        /// </summary>
        /// <param name="masterId">合约模板序号</param>
        /// <returns></returns>
        public static List<Model.ContractClause> LoadContractClausesByMaster(int masterId)
        {
            List<Model.ClauseContractRef> rs = ClauseContractRefs.Where(temp => temp.MasterId == masterId).ToList();

            IEnumerable<Model.ContractClause> s = from c in ContractClauses join r in rs on c.ClauseId equals r.ClauseId select c;

            if (s == null)
                return null;

            return s.ToList();
        }

        /// <summary>
        /// 获取所有合约条款明细
        /// </summary>
        public static List<Model.ContractClauseDetail> ClauseDetails
        {
            get
            {
                if (clauseDetails != null && clauseDetails.Count > 0)
                    return clauseDetails;

                clauseDetails = RegisterBasic<Model.ContractClauseDetail>(new DAL.ContractClauseDetailDAL());

                return clauseDetails;
            }
        }

        /// <summary>
        /// 获取合约条款下的所有合约明细
        /// </summary>
        /// <param name="clauseId">合约条款序号</param>
        /// <returns></returns>
        public static List<Model.ContractClauseDetail> LoadClauseDetailsByClause(int clauseId)
        {
            return ClauseDetails.Where(temp => temp.ClauseId == clauseId).ToList();
        }

        private static List<T> RegisterBasic<T>(Common.IDataOperate operate)
            where T : class,NFMT.Common.IModel
        {
            NFMT.Common.ResultModel result = operate.Load<T>(Common.DefaultValue.SysUser);
            if (result.ResultStatus != 0)
                throw new Exception("加载失败");

            List<T> ts  = result.ReturnValue as List<T>;

            if (ts == null)
                throw new Exception("加载失败");

            return ts;
        }

        /// <summary>
        /// 刷新基础数据
        /// </summary>
        public static void RefreshBasicData()
        {
            if (areas != null)
                areas.Clear();
            if (assets != null)
                assets.Clear();
            if (bankAccounts != null)
                bankAccounts.Clear();
            if (banks != null)
                banks.Clear();
            if (brands != null)
                brands.Clear();
            if (currencies != null)
                currencies.Clear();
            if (deliverPlaces != null)
                deliverPlaces.Clear();
            if (exchanges != null)
                exchanges.Clear();
            if (futuresCodes != null)
                futuresCodes.Clear();
            if (futuresPrices != null)
                futuresPrices.Clear();
            if (measureUnits != null)
                measureUnits.Clear();
            if (producers != null)
                producers.Clear();
            if (rates != null)
                rates.Clear();
            if (styleDetail != null)
                styleDetail.Clear();
            if (masters != null)
                masters.Clear();
            if (clauses != null)
                clauses.Clear();
            if (clauseDetails != null)
                clauseDetails.Clear();
            if (refs != null)
                refs.Clear();
        }
    }
}
