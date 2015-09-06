﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NFMTSite_MVC
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NFMTEntities : DbContext
    {
        public NFMTEntities()
            : base("name=NFMTEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Apply> Apply { get; set; }
        public virtual DbSet<Attach> Attach { get; set; }
        public virtual DbSet<Con_Contract> Con_Contract { get; set; }
        public virtual DbSet<Con_ContractAttach> Con_ContractAttach { get; set; }
        public virtual DbSet<Con_ContractClause_Ref> Con_ContractClause_Ref { get; set; }
        public virtual DbSet<Con_ContractCorporationDetail> Con_ContractCorporationDetail { get; set; }
        public virtual DbSet<Con_ContractDept> Con_ContractDept { get; set; }
        public virtual DbSet<Con_ContractDetail> Con_ContractDetail { get; set; }
        public virtual DbSet<Con_ContractPrice> Con_ContractPrice { get; set; }
        public virtual DbSet<Con_ContractSub> Con_ContractSub { get; set; }
        public virtual DbSet<Con_ContractSubAttach> Con_ContractSubAttach { get; set; }
        public virtual DbSet<Con_ContractSubQP> Con_ContractSubQP { get; set; }
        public virtual DbSet<Con_ContractTypeDetail> Con_ContractTypeDetail { get; set; }
        public virtual DbSet<Con_Lc> Con_Lc { get; set; }
        public virtual DbSet<Con_LcAttach> Con_LcAttach { get; set; }
        public virtual DbSet<Con_Premium> Con_Premium { get; set; }
        public virtual DbSet<Con_SubCorporationDetail> Con_SubCorporationDetail { get; set; }
        public virtual DbSet<Con_SubDetail> Con_SubDetail { get; set; }
        public virtual DbSet<Con_SubPrice> Con_SubPrice { get; set; }
        public virtual DbSet<Con_SubTypeDetail> Con_SubTypeDetail { get; set; }
        public virtual DbSet<Con_Summary> Con_Summary { get; set; }
        public virtual DbSet<Con_SummaryApply> Con_SummaryApply { get; set; }
        public virtual DbSet<Con_SummaryApplyAttach> Con_SummaryApplyAttach { get; set; }
        public virtual DbSet<Con_SummaryApplyInvoice_Ref> Con_SummaryApplyInvoice_Ref { get; set; }
        public virtual DbSet<Con_SummaryApplyStock_Ref> Con_SummaryApplyStock_Ref { get; set; }
        public virtual DbSet<Doc_Document> Doc_Document { get; set; }
        public virtual DbSet<Doc_DocumentInvoice> Doc_DocumentInvoice { get; set; }
        public virtual DbSet<Doc_DocumentOrder> Doc_DocumentOrder { get; set; }
        public virtual DbSet<Doc_DocumentOrderAttach> Doc_DocumentOrderAttach { get; set; }
        public virtual DbSet<Doc_DocumentOrderDetail> Doc_DocumentOrderDetail { get; set; }
        public virtual DbSet<Doc_DocumentOrderInvoice> Doc_DocumentOrderInvoice { get; set; }
        public virtual DbSet<Doc_DocumentOrderStock> Doc_DocumentOrderStock { get; set; }
        public virtual DbSet<Doc_DocumentStock> Doc_DocumentStock { get; set; }
        public virtual DbSet<Fun_CashIn> Fun_CashIn { get; set; }
        public virtual DbSet<Fun_CashInAllot> Fun_CashInAllot { get; set; }
        public virtual DbSet<Fun_CashInAttach> Fun_CashInAttach { get; set; }
        public virtual DbSet<Fun_CashInContract_Ref> Fun_CashInContract_Ref { get; set; }
        public virtual DbSet<Fun_CashInCorp_Ref> Fun_CashInCorp_Ref { get; set; }
        public virtual DbSet<Fun_CashInInvoice_Ref> Fun_CashInInvoice_Ref { get; set; }
        public virtual DbSet<Fun_CashInStcok_Ref> Fun_CashInStcok_Ref { get; set; }
        public virtual DbSet<Fun_ContractFundsAllotStock_Ref> Fun_ContractFundsAllotStock_Ref { get; set; }
        public virtual DbSet<Fun_ContractPayApply_Ref> Fun_ContractPayApply_Ref { get; set; }
        public virtual DbSet<Fun_ContractReceivable_Ref> Fun_ContractReceivable_Ref { get; set; }
        public virtual DbSet<Fun_CorpFundsAllotContract_Ref> Fun_CorpFundsAllotContract_Ref { get; set; }
        public virtual DbSet<Fun_CorpFundsAllotStock_Ref> Fun_CorpFundsAllotStock_Ref { get; set; }
        public virtual DbSet<Fun_CorpReceivable_Ref> Fun_CorpReceivable_Ref { get; set; }
        public virtual DbSet<Fun_Funds> Fun_Funds { get; set; }
        public virtual DbSet<Fun_FundsLog> Fun_FundsLog { get; set; }
        public virtual DbSet<Fun_FundsLogAttach> Fun_FundsLogAttach { get; set; }
        public virtual DbSet<Fun_InvoicePayApply_Ref> Fun_InvoicePayApply_Ref { get; set; }
        public virtual DbSet<Fun_InvoiceReceivable_Ref> Fun_InvoiceReceivable_Ref { get; set; }
        public virtual DbSet<Fun_LcReceivable_Ref> Fun_LcReceivable_Ref { get; set; }
        public virtual DbSet<Fun_PayApply> Fun_PayApply { get; set; }
        public virtual DbSet<Fun_PayApplyAttach> Fun_PayApplyAttach { get; set; }
        public virtual DbSet<Fun_Payment> Fun_Payment { get; set; }
        public virtual DbSet<Fun_PaymentAttach> Fun_PaymentAttach { get; set; }
        public virtual DbSet<Fun_PaymentContractDetail> Fun_PaymentContractDetail { get; set; }
        public virtual DbSet<Fun_PaymentInvioceDetail> Fun_PaymentInvioceDetail { get; set; }
        public virtual DbSet<Fun_PaymentStockDetail> Fun_PaymentStockDetail { get; set; }
        public virtual DbSet<Fun_PaymentVirtual> Fun_PaymentVirtual { get; set; }
        public virtual DbSet<Fun_RecAllotDetail> Fun_RecAllotDetail { get; set; }
        public virtual DbSet<Fun_Receivable> Fun_Receivable { get; set; }
        public virtual DbSet<Fun_ReceivableAllot> Fun_ReceivableAllot { get; set; }
        public virtual DbSet<Fun_ReceivableAttach> Fun_ReceivableAttach { get; set; }
        public virtual DbSet<Fun_StcokReceivable_Ref> Fun_StcokReceivable_Ref { get; set; }
        public virtual DbSet<Fun_StockPayApply_Ref> Fun_StockPayApply_Ref { get; set; }
        public virtual DbSet<Inv_BusinessInvoice> Inv_BusinessInvoice { get; set; }
        public virtual DbSet<Inv_BusinessInvoiceDetail> Inv_BusinessInvoiceDetail { get; set; }
        public virtual DbSet<Inv_FinanceBusinessInvoice_Ref> Inv_FinanceBusinessInvoice_Ref { get; set; }
        public virtual DbSet<Inv_FinanceInvoice> Inv_FinanceInvoice { get; set; }
        public virtual DbSet<Inv_FinanceInvoiceDetail> Inv_FinanceInvoiceDetail { get; set; }
        public virtual DbSet<Inv_FinBusInvAllot> Inv_FinBusInvAllot { get; set; }
        public virtual DbSet<Inv_FinBusInvAllotDetail> Inv_FinBusInvAllotDetail { get; set; }
        public virtual DbSet<Inv_InvoiceApply> Inv_InvoiceApply { get; set; }
        public virtual DbSet<Inv_InvoiceApplyDetail> Inv_InvoiceApplyDetail { get; set; }
        public virtual DbSet<Inv_InvoiceApplyFinance> Inv_InvoiceApplyFinance { get; set; }
        public virtual DbSet<Inv_InvoiceApplySIDetail> Inv_InvoiceApplySIDetail { get; set; }
        public virtual DbSet<Inv_SI> Inv_SI { get; set; }
        public virtual DbSet<Inv_SIDetail> Inv_SIDetail { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceAttach> InvoiceAttach { get; set; }
        public virtual DbSet<Pri_Interest> Pri_Interest { get; set; }
        public virtual DbSet<Pri_InterestDetail> Pri_InterestDetail { get; set; }
        public virtual DbSet<Pri_PriceConfirm> Pri_PriceConfirm { get; set; }
        public virtual DbSet<Pri_PriceConfirmDetail> Pri_PriceConfirmDetail { get; set; }
        public virtual DbSet<Pri_Pricing> Pri_Pricing { get; set; }
        public virtual DbSet<Pri_PricingApply> Pri_PricingApply { get; set; }
        public virtual DbSet<Pri_PricingApplyDelay> Pri_PricingApplyDelay { get; set; }
        public virtual DbSet<Pri_PricingApplyDetail> Pri_PricingApplyDetail { get; set; }
        public virtual DbSet<Pri_PricingDetail> Pri_PricingDetail { get; set; }
        public virtual DbSet<Pri_PricingPerson> Pri_PricingPerson { get; set; }
        public virtual DbSet<Pri_StopLoss> Pri_StopLoss { get; set; }
        public virtual DbSet<Pri_StopLossApply> Pri_StopLossApply { get; set; }
        public virtual DbSet<Pri_StopLossApplyDetail> Pri_StopLossApplyDetail { get; set; }
        public virtual DbSet<Pri_StopLossDetail> Pri_StopLossDetail { get; set; }
        public virtual DbSet<Rep_ClientCurrent> Rep_ClientCurrent { get; set; }
        public virtual DbSet<St_ContractStockIn_Ref> St_ContractStockIn_Ref { get; set; }
        public virtual DbSet<St_CustomsApplyAttach> St_CustomsApplyAttach { get; set; }
        public virtual DbSet<St_CustomsApplyDetail> St_CustomsApplyDetail { get; set; }
        public virtual DbSet<St_CustomsAttach> St_CustomsAttach { get; set; }
        public virtual DbSet<St_CustomsClearance> St_CustomsClearance { get; set; }
        public virtual DbSet<St_CustomsClearanceApply> St_CustomsClearanceApply { get; set; }
        public virtual DbSet<St_CustomsDetail> St_CustomsDetail { get; set; }
        public virtual DbSet<St_Pledge> St_Pledge { get; set; }
        public virtual DbSet<St_PledgeApply> St_PledgeApply { get; set; }
        public virtual DbSet<St_PledgeApplyAttach> St_PledgeApplyAttach { get; set; }
        public virtual DbSet<St_PledgeApplyDetail> St_PledgeApplyDetail { get; set; }
        public virtual DbSet<St_PledgeAttach> St_PledgeAttach { get; set; }
        public virtual DbSet<St_PledgeDetial> St_PledgeDetial { get; set; }
        public virtual DbSet<St_Repo> St_Repo { get; set; }
        public virtual DbSet<St_RepoApply> St_RepoApply { get; set; }
        public virtual DbSet<St_RepoApplyAttach> St_RepoApplyAttach { get; set; }
        public virtual DbSet<St_RepoApplyDetail> St_RepoApplyDetail { get; set; }
        public virtual DbSet<St_RepoAttach> St_RepoAttach { get; set; }
        public virtual DbSet<St_RepoDetail> St_RepoDetail { get; set; }
        public virtual DbSet<St_SplitDoc> St_SplitDoc { get; set; }
        public virtual DbSet<St_SplitDocAttach> St_SplitDocAttach { get; set; }
        public virtual DbSet<St_SplitDocDetail> St_SplitDocDetail { get; set; }
        public virtual DbSet<St_SplitDocStock_Ref> St_SplitDocStock_Ref { get; set; }
        public virtual DbSet<St_Stock> St_Stock { get; set; }
        public virtual DbSet<St_StockExclusive> St_StockExclusive { get; set; }
        public virtual DbSet<St_StockIn> St_StockIn { get; set; }
        public virtual DbSet<St_StockInAttach> St_StockInAttach { get; set; }
        public virtual DbSet<St_StockInStock_Ref> St_StockInStock_Ref { get; set; }
        public virtual DbSet<St_StockLog> St_StockLog { get; set; }
        public virtual DbSet<St_StockLogAttach> St_StockLogAttach { get; set; }
        public virtual DbSet<St_StockMove> St_StockMove { get; set; }
        public virtual DbSet<St_StockMoveApply> St_StockMoveApply { get; set; }
        public virtual DbSet<St_StockMoveApplyAttach> St_StockMoveApplyAttach { get; set; }
        public virtual DbSet<St_StockMoveApplyDetail> St_StockMoveApplyDetail { get; set; }
        public virtual DbSet<St_StockMoveAttach> St_StockMoveAttach { get; set; }
        public virtual DbSet<St_StockMoveDetail> St_StockMoveDetail { get; set; }
        public virtual DbSet<St_StockName> St_StockName { get; set; }
        public virtual DbSet<St_StockOut> St_StockOut { get; set; }
        public virtual DbSet<St_StockOutApply> St_StockOutApply { get; set; }
        public virtual DbSet<St_StockOutApplyAttach> St_StockOutApplyAttach { get; set; }
        public virtual DbSet<St_StockOutApplyDetail> St_StockOutApplyDetail { get; set; }
        public virtual DbSet<St_StockOutAttach> St_StockOutAttach { get; set; }
        public virtual DbSet<St_StockOutDetail> St_StockOutDetail { get; set; }
        public virtual DbSet<St_StockReceipt> St_StockReceipt { get; set; }
        public virtual DbSet<St_StockReceiptDetail> St_StockReceiptDetail { get; set; }
        public virtual DbSet<St_StocktAttach> St_StocktAttach { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<test> test { get; set; }
    }
}
