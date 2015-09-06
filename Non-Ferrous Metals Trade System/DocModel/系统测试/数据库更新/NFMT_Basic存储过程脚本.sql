USE [NFMT_Basic]
GO
/****** Object:  StoredProcedure [dbo].[ClauseContract_RefInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClauseContract_RefInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ClauseContract_RefInsert]
GO
/****** Object:  StoredProcedure [dbo].[ClauseContract_RefLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClauseContract_RefLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ClauseContract_RefLoad]
GO
/****** Object:  StoredProcedure [dbo].[ClauseContract_RefUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClauseContract_RefUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ClauseContract_RefUpdate]
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseDetailInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseDetailInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractClauseDetailInsert]
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseDetailLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseDetailLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractClauseDetailLoad]
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseDetailUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseDetailUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractClauseDetailUpdate]
GO
/****** Object:  StoredProcedure [dbo].[ClauseContract_RefGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClauseContract_RefGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ClauseContract_RefGet]
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseDetailGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseDetailGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractClauseDetailGet]
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseDetailGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseDetailGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractClauseDetailGoBack]
GO
/****** Object:  StoredProcedure [dbo].[ClauseContract_RefGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClauseContract_RefGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ClauseContract_RefGoBack]
GO
/****** Object:  StoredProcedure [dbo].[ContractMasterGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractMasterGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractMasterGet]
GO
/****** Object:  StoredProcedure [dbo].[ContractMasterGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractMasterGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractMasterGoBack]
GO
/****** Object:  StoredProcedure [dbo].[ContractMasterInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractMasterInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractMasterInsert]
GO
/****** Object:  StoredProcedure [dbo].[ContractMasterLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractMasterLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractMasterLoad]
GO
/****** Object:  StoredProcedure [dbo].[ContractMasterUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractMasterUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractMasterUpdate]
GO
/****** Object:  StoredProcedure [dbo].[ContractMasterUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractMasterUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractMasterUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[CurrencyGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrencyGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CurrencyGet]
GO
/****** Object:  StoredProcedure [dbo].[CurrencyGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrencyGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CurrencyGoBack]
GO
/****** Object:  StoredProcedure [dbo].[CurrencyInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrencyInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CurrencyInsert]
GO
/****** Object:  StoredProcedure [dbo].[CurrencyLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrencyLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CurrencyLoad]
GO
/****** Object:  StoredProcedure [dbo].[CurrencyUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrencyUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CurrencyUpdate]
GO
/****** Object:  StoredProcedure [dbo].[CurrencyUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrencyUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CurrencyUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseDetailUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseDetailUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractClauseDetailUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractClauseGet]
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractClauseGoBack]
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractClauseInsert]
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractClauseLoad]
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractClauseUpdate]
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContractClauseUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[DeliverPlaceGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliverPlaceGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeliverPlaceGet]
GO
/****** Object:  StoredProcedure [dbo].[DeliverPlaceGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliverPlaceGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeliverPlaceGoBack]
GO
/****** Object:  StoredProcedure [dbo].[DeliverPlaceInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliverPlaceInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeliverPlaceInsert]
GO
/****** Object:  StoredProcedure [dbo].[DeliverPlaceLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliverPlaceLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeliverPlaceLoad]
GO
/****** Object:  StoredProcedure [dbo].[DeliverPlaceUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliverPlaceUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeliverPlaceUpdate]
GO
/****** Object:  StoredProcedure [dbo].[DeliverPlaceUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliverPlaceUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeliverPlaceUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[ExchangeGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ExchangeGet]
GO
/****** Object:  StoredProcedure [dbo].[ExchangeGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ExchangeGoBack]
GO
/****** Object:  StoredProcedure [dbo].[ExchangeInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ExchangeInsert]
GO
/****** Object:  StoredProcedure [dbo].[ExchangeLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ExchangeLoad]
GO
/****** Object:  StoredProcedure [dbo].[ExchangeUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ExchangeUpdate]
GO
/****** Object:  StoredProcedure [dbo].[ExchangeUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ExchangeUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[FuturesCodeGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesCodeGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FuturesCodeGet]
GO
/****** Object:  StoredProcedure [dbo].[FuturesCodeGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesCodeGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FuturesCodeGoBack]
GO
/****** Object:  StoredProcedure [dbo].[FuturesCodeInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesCodeInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FuturesCodeInsert]
GO
/****** Object:  StoredProcedure [dbo].[FuturesCodeLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesCodeLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FuturesCodeLoad]
GO
/****** Object:  StoredProcedure [dbo].[FuturesCodeUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesCodeUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FuturesCodeUpdate]
GO
/****** Object:  StoredProcedure [dbo].[FuturesCodeUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesCodeUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FuturesCodeUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[FuturesPriceGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesPriceGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FuturesPriceGet]
GO
/****** Object:  StoredProcedure [dbo].[FuturesPriceGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesPriceGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FuturesPriceGoBack]
GO
/****** Object:  StoredProcedure [dbo].[FuturesPriceInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesPriceInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FuturesPriceInsert]
GO
/****** Object:  StoredProcedure [dbo].[FuturesPriceLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesPriceLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FuturesPriceLoad]
GO
/****** Object:  StoredProcedure [dbo].[FuturesPriceUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesPriceUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FuturesPriceUpdate]
GO
/****** Object:  StoredProcedure [dbo].[FuturesPriceUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesPriceUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FuturesPriceUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[CorpDeptGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorpDeptGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CorpDeptGet]
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceDetailGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceDetailGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Inv_BusinessInvoiceDetailGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Inv_BusinessInvoiceGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Inv_BusinessInvoiceUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[MeasureUnitGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MeasureUnitGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MeasureUnitGet]
GO
/****** Object:  StoredProcedure [dbo].[MeasureUnitGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MeasureUnitGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MeasureUnitGoBack]
GO
/****** Object:  StoredProcedure [dbo].[MeasureUnitInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MeasureUnitInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MeasureUnitInsert]
GO
/****** Object:  StoredProcedure [dbo].[MeasureUnitLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MeasureUnitLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MeasureUnitLoad]
GO
/****** Object:  StoredProcedure [dbo].[MeasureUnitUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MeasureUnitUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MeasureUnitUpdate]
GO
/****** Object:  StoredProcedure [dbo].[MeasureUnitUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MeasureUnitUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MeasureUnitUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[PriceAuthorityGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceAuthorityGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PriceAuthorityGet]
GO
/****** Object:  StoredProcedure [dbo].[PriceAuthorityGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceAuthorityGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PriceAuthorityGoBack]
GO
/****** Object:  StoredProcedure [dbo].[PriceAuthorityInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceAuthorityInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PriceAuthorityInsert]
GO
/****** Object:  StoredProcedure [dbo].[PriceAuthorityLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceAuthorityLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PriceAuthorityLoad]
GO
/****** Object:  StoredProcedure [dbo].[PriceAuthorityUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceAuthorityUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PriceAuthorityUpdate]
GO
/****** Object:  StoredProcedure [dbo].[PriceAuthorityUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceAuthorityUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PriceAuthorityUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[ProducerGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProducerGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProducerGet]
GO
/****** Object:  StoredProcedure [dbo].[ProducerGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProducerGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProducerGoBack]
GO
/****** Object:  StoredProcedure [dbo].[ProducerInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProducerInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProducerInsert]
GO
/****** Object:  StoredProcedure [dbo].[ProducerLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProducerLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProducerLoad]
GO
/****** Object:  StoredProcedure [dbo].[ProducerUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProducerUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProducerUpdate]
GO
/****** Object:  StoredProcedure [dbo].[ProducerUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProducerUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProducerUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[RateGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RateGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RateGet]
GO
/****** Object:  StoredProcedure [dbo].[RateGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RateGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RateGoBack]
GO
/****** Object:  StoredProcedure [dbo].[RateInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RateInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RateInsert]
GO
/****** Object:  StoredProcedure [dbo].[RateLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RateLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RateLoad]
GO
/****** Object:  StoredProcedure [dbo].[RateUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RateUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RateUpdate]
GO
/****** Object:  StoredProcedure [dbo].[RateUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RateUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RateUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[St_StockLogGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[St_StockLogGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[St_StockLogGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceDetailUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceDetailUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Inv_BusinessInvoiceDetailUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[St_StockLogUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[St_StockLogUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[St_StockLogUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskNodeUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskNodeUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskNodeUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[ClauseContract_RefUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClauseContract_RefUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ClauseContract_RefUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[BrandAssetGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandAssetGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BrandAssetGet]
GO
/****** Object:  StoredProcedure [dbo].[BrandAssetGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandAssetGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BrandAssetGoBack]
GO
/****** Object:  StoredProcedure [dbo].[BrandGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BrandGet]
GO
/****** Object:  StoredProcedure [dbo].[BrandGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BrandGoBack]
GO
/****** Object:  StoredProcedure [dbo].[BrandInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BrandInsert]
GO
/****** Object:  StoredProcedure [dbo].[BrandLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BrandLoad]
GO
/****** Object:  StoredProcedure [dbo].[BrandUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BrandUpdate]
GO
/****** Object:  StoredProcedure [dbo].[BrandUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BrandUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[BDStyleDetailGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleDetailGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStyleDetailGet]
GO
/****** Object:  StoredProcedure [dbo].[BDStyleDetailGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleDetailGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStyleDetailGoBack]
GO
/****** Object:  StoredProcedure [dbo].[BDStyleDetailInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleDetailInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStyleDetailInsert]
GO
/****** Object:  StoredProcedure [dbo].[BDStyleDetailLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleDetailLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStyleDetailLoad]
GO
/****** Object:  StoredProcedure [dbo].[BDStyleDetailUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleDetailUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStyleDetailUpdate]
GO
/****** Object:  StoredProcedure [dbo].[BDStyleDetailUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleDetailUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStyleDetailUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[BDStyleGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStyleGet]
GO
/****** Object:  StoredProcedure [dbo].[BDStyleGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStyleGoBack]
GO
/****** Object:  StoredProcedure [dbo].[BDStyleInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStyleInsert]
GO
/****** Object:  StoredProcedure [dbo].[BDStyleLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStyleLoad]
GO
/****** Object:  StoredProcedure [dbo].[BDStyleUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStyleUpdate]
GO
/****** Object:  StoredProcedure [dbo].[BDStyleUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStyleUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[AreaGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AreaGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AreaGet]
GO
/****** Object:  StoredProcedure [dbo].[AreaGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AreaGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AreaGoBack]
GO
/****** Object:  StoredProcedure [dbo].[AreaInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AreaInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AreaInsert]
GO
/****** Object:  StoredProcedure [dbo].[AreaLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AreaLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AreaLoad]
GO
/****** Object:  StoredProcedure [dbo].[AreaUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AreaUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AreaUpdate]
GO
/****** Object:  StoredProcedure [dbo].[AreaUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AreaUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AreaUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[AssetGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AssetGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AssetGet]
GO
/****** Object:  StoredProcedure [dbo].[AssetGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AssetGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AssetGoBack]
GO
/****** Object:  StoredProcedure [dbo].[AssetInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AssetInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AssetInsert]
GO
/****** Object:  StoredProcedure [dbo].[AssetLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AssetLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AssetLoad]
GO
/****** Object:  StoredProcedure [dbo].[AssetUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AssetUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AssetUpdate]
GO
/****** Object:  StoredProcedure [dbo].[AssetUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AssetUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AssetUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[BDStatusDetailGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusDetailGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStatusDetailGet]
GO
/****** Object:  StoredProcedure [dbo].[BDStatusDetailGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusDetailGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStatusDetailGoBack]
GO
/****** Object:  StoredProcedure [dbo].[BDStatusDetailInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusDetailInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStatusDetailInsert]
GO
/****** Object:  StoredProcedure [dbo].[BDStatusDetailLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusDetailLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStatusDetailLoad]
GO
/****** Object:  StoredProcedure [dbo].[BDStatusDetailUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusDetailUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStatusDetailUpdate]
GO
/****** Object:  StoredProcedure [dbo].[BDStatusDetailUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusDetailUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStatusDetailUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[BDStatusGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStatusGet]
GO
/****** Object:  StoredProcedure [dbo].[BDStatusGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStatusGoBack]
GO
/****** Object:  StoredProcedure [dbo].[BDStatusInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStatusInsert]
GO
/****** Object:  StoredProcedure [dbo].[BDStatusLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStatusLoad]
GO
/****** Object:  StoredProcedure [dbo].[BDStatusUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStatusUpdate]
GO
/****** Object:  StoredProcedure [dbo].[BDStatusUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BDStatusUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[BankAccountGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankAccountGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BankAccountGet]
GO
/****** Object:  StoredProcedure [dbo].[BankAccountGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankAccountGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BankAccountGoBack]
GO
/****** Object:  StoredProcedure [dbo].[BankAccountInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankAccountInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BankAccountInsert]
GO
/****** Object:  StoredProcedure [dbo].[BankAccountLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankAccountLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BankAccountLoad]
GO
/****** Object:  StoredProcedure [dbo].[BankAccountUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankAccountUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BankAccountUpdate]
GO
/****** Object:  StoredProcedure [dbo].[BankAccountUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankAccountUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BankAccountUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[BankGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BankGet]
GO
/****** Object:  StoredProcedure [dbo].[BankGoBack]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BankGoBack]
GO
/****** Object:  StoredProcedure [dbo].[BankInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BankInsert]
GO
/****** Object:  StoredProcedure [dbo].[BankLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BankLoad]
GO
/****** Object:  StoredProcedure [dbo].[BankUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BankUpdate]
GO
/****** Object:  StoredProcedure [dbo].[BankUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BankUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Inv_BusinessInvoiceGet]
GO
/****** Object:  StoredProcedure [dbo].[St_StockLogInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[St_StockLogInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[St_StockLogInsert]
GO
/****** Object:  StoredProcedure [dbo].[St_StockLogLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[St_StockLogLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[St_StockLogLoad]
GO
/****** Object:  StoredProcedure [dbo].[St_StockLogUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[St_StockLogUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[St_StockLogUpdate]
GO
/****** Object:  StoredProcedure [dbo].[SelectPager]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectPager]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SelectPager]
GO
/****** Object:  StoredProcedure [dbo].[St_StockLogGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[St_StockLogGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[St_StockLogGet]
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Inv_BusinessInvoiceInsert]
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Inv_BusinessInvoiceLoad]
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Inv_BusinessInvoiceUpdate]
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceDetailInsert]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceDetailInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Inv_BusinessInvoiceDetailInsert]
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceDetailLoad]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceDetailLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Inv_BusinessInvoiceDetailLoad]
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceDetailUpdate]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceDetailUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Inv_BusinessInvoiceDetailUpdate]
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceDetailGet]    Script Date: 10/22/2014 11:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceDetailGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Inv_BusinessInvoiceDetailGet]
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceDetailGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceDetailGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_BusinessInvoiceDetailGet
// 存储过程功能描述：查询指定Inv_BusinessInvoiceDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年8月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Inv_BusinessInvoiceDetailGet]
	@DetailId int
AS

SELECT
	[DetailId],
	[BusinessInvoiceId],
	[InvoiceId],
	[StockId],
	[FeeType],
	[IntegerAmount],
	[NetAmount],
	[UnitPrice],
	[CalculateDay],
	[Bala],
	[DetailStatus]
FROM
	[dbo].[Inv_BusinessInvoiceDetail]
WHERE
	[DetailId] = @DetailId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceDetailUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceDetailUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_BusinessInvoiceDetailUpdate
// 存储过程功能描述：更新Inv_BusinessInvoiceDetail
// 创建人：CodeSmith
// 创建时间： 2014年8月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Inv_BusinessInvoiceDetailUpdate]
    @DetailId int,
@BusinessInvoiceId int = NULL,
@InvoiceId int = NULL,
@StockId int = NULL,
@FeeType int = NULL,
@IntegerAmount numeric(18, 4) = NULL,
@NetAmount numeric(18, 4) = NULL,
@UnitPrice numeric(18, 4) = NULL,
@CalculateDay numeric(18, 4) = NULL,
@Bala numeric(18, 4) = NULL,
@DetailStatus int = NULL
AS

UPDATE [dbo].[Inv_BusinessInvoiceDetail] SET
	[BusinessInvoiceId] = @BusinessInvoiceId,
	[InvoiceId] = @InvoiceId,
	[StockId] = @StockId,
	[FeeType] = @FeeType,
	[IntegerAmount] = @IntegerAmount,
	[NetAmount] = @NetAmount,
	[UnitPrice] = @UnitPrice,
	[CalculateDay] = @CalculateDay,
	[Bala] = @Bala,
	[DetailStatus] = @DetailStatus
WHERE
	[DetailId] = @DetailId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceDetailLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceDetailLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_BusinessInvoiceDetailLoad
// 存储过程功能描述：查询所有Inv_BusinessInvoiceDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年8月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Inv_BusinessInvoiceDetailLoad]
AS

SELECT
	[DetailId],
	[BusinessInvoiceId],
	[InvoiceId],
	[StockId],
	[FeeType],
	[IntegerAmount],
	[NetAmount],
	[UnitPrice],
	[CalculateDay],
	[Bala],
	[DetailStatus]
FROM
	[dbo].[Inv_BusinessInvoiceDetail]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceDetailInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceDetailInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_BusinessInvoiceDetailInsert
// 存储过程功能描述：新增一条Inv_BusinessInvoiceDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年8月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Inv_BusinessInvoiceDetailInsert]
	@BusinessInvoiceId int =NULL ,
	@InvoiceId int =NULL ,
	@StockId int =NULL ,
	@FeeType int =NULL ,
	@IntegerAmount numeric(18, 4) =NULL ,
	@NetAmount numeric(18, 4) =NULL ,
	@UnitPrice numeric(18, 4) =NULL ,
	@CalculateDay numeric(18, 4) =NULL ,
	@Bala numeric(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Inv_BusinessInvoiceDetail] (
	[BusinessInvoiceId],
	[InvoiceId],
	[StockId],
	[FeeType],
	[IntegerAmount],
	[NetAmount],
	[UnitPrice],
	[CalculateDay],
	[Bala],
	[DetailStatus]
) VALUES (
	@BusinessInvoiceId,
	@InvoiceId,
	@StockId,
	@FeeType,
	@IntegerAmount,
	@NetAmount,
	@UnitPrice,
	@CalculateDay,
	@Bala,
	@DetailStatus
)


SET @DetailId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_BusinessInvoiceUpdate
// 存储过程功能描述：更新Inv_BusinessInvoice
// 创建人：CodeSmith
// 创建时间： 2014年8月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Inv_BusinessInvoiceUpdate]
    @BusinessInvoiceId int,
@InvoiceId int = NULL,
@ContractId int = NULL,
@SubContractId int = NULL,
@AssetId int = NULL,
@IntegerAmount numeric(18, 4) = NULL,
@NetAmount numeric(18, 4) = NULL,
@MUId int = NULL,
@MarginRatio numeric(18, 4) = NULL,
@VATRatio numeric(18, 4) = NULL,
@VATBala numeric(18, 4) = NULL
AS

UPDATE [dbo].[Inv_BusinessInvoice] SET
	[InvoiceId] = @InvoiceId,
	[ContractId] = @ContractId,
	[SubContractId] = @SubContractId,
	[AssetId] = @AssetId,
	[IntegerAmount] = @IntegerAmount,
	[NetAmount] = @NetAmount,
	[MUId] = @MUId,
	[MarginRatio] = @MarginRatio,
	[VATRatio] = @VATRatio,
	[VATBala] = @VATBala
WHERE
	[BusinessInvoiceId] = @BusinessInvoiceId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_BusinessInvoiceLoad
// 存储过程功能描述：查询所有Inv_BusinessInvoice记录
// 创建人：CodeSmith
// 创建时间： 2014年8月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Inv_BusinessInvoiceLoad]
AS

SELECT
	[BusinessInvoiceId],
	[InvoiceId],
	[ContractId],
	[SubContractId],
	[AssetId],
	[IntegerAmount],
	[NetAmount],
	[MUId],
	[MarginRatio],
	[VATRatio],
	[VATBala]
FROM
	[dbo].[Inv_BusinessInvoice]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_BusinessInvoiceInsert
// 存储过程功能描述：新增一条Inv_BusinessInvoice记录
// 创建人：CodeSmith
// 创建时间： 2014年8月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Inv_BusinessInvoiceInsert]
	@InvoiceId int =NULL ,
	@ContractId int =NULL ,
	@SubContractId int =NULL ,
	@AssetId int =NULL ,
	@IntegerAmount numeric(18, 4) =NULL ,
	@NetAmount numeric(18, 4) =NULL ,
	@MUId int =NULL ,
	@MarginRatio numeric(18, 4) =NULL ,
	@VATRatio numeric(18, 4) =NULL ,
	@VATBala numeric(18, 4) =NULL ,
	@BusinessInvoiceId int OUTPUT
AS

INSERT INTO [dbo].[Inv_BusinessInvoice] (
	[InvoiceId],
	[ContractId],
	[SubContractId],
	[AssetId],
	[IntegerAmount],
	[NetAmount],
	[MUId],
	[MarginRatio],
	[VATRatio],
	[VATBala]
) VALUES (
	@InvoiceId,
	@ContractId,
	@SubContractId,
	@AssetId,
	@IntegerAmount,
	@NetAmount,
	@MUId,
	@MarginRatio,
	@VATRatio,
	@VATBala
)


SET @BusinessInvoiceId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[St_StockLogGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[St_StockLogGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockLogGet
// 存储过程功能描述：查询指定St_StockLog的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年8月18日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[St_StockLogGet]
	@StockLogId int
AS

SELECT
	[StockLogId],
	[StockId],
	[StockNameId],
	[RefNo],
	[LogDirection],
	[LogType],
	[ContractId],
	[SubContractId],
	[LogDate],
	[OpPerson],
	[Bundles],
	[GrossAmount],
	[NetAmount],
	[MUId],
	[BrandId],
	[DeliverPlaceId],
	[PaperNo],
	[PaperHolder],
	[CardNo],
	[Memo],
	[LogStatus],
	[LogSourceBase],
	[LogSource],
	[SourceId]
FROM
	[dbo].[St_StockLog]
WHERE
	[StockLogId] = @StockLogId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[SelectPager]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectPager]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


create proc [dbo].[SelectPager]
    @pageIndex int=1, --当前页
    @pageSize int=10, --一页显示条数
    @columnName nvarchar(800)=''*'', --列名
    @tableName nvarchar(500)='''',   --表名
    @orderStr nvarchar(500), --排序列，
    @whereStr varchar(1000)='''', --条件
    @rowCount int output     --总记录条数
as
begin
declare @beginRow int,--起始行
        @endRow int,  --结束行
        @sql nvarchar(3000) ,
        @sqlfrom nvarchar(510), 
        @sqlWhere nvarchar(1010),
        @sqlCount nvarchar(1520)
       
        set @beginRow =(@pageIndex-1)*@pageSize+1
        set @endRow = @pageIndex*@pageSize

        set @sql =N'' select row_number() over(order by ''+@orderStr+'') as rowSerial, ''+@columnName
        set @sqlfrom ='' from ''+@tableName+''''

    --是否在条件
    if len(@whereStr)>0
        set @sqlWhere='' where ''+@whereStr
    else
        set @sqlWhere='' ''

        --总记录条数
        set @sqlCount=''select @count=count(0) ''+@sqlfrom+'' ''+@sqlWhere
        exec sp_executesql @sqlCount, N''@count bigint output'',@count=@rowCount output


        set @sql =@sql+@sqlfrom+@sqlWhere
		
		set @sql=''select * from ( ''+@sql+'') as t where rowSerial between ''+convert(varchar,@beginRow) +'' and ''+convert(varchar,@endRow)
        
        print @sql
        exec (@sql)
end





' 
END
GO
/****** Object:  StoredProcedure [dbo].[St_StockLogUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[St_StockLogUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockLogUpdate
// 存储过程功能描述：更新St_StockLog
// 创建人：CodeSmith
// 创建时间： 2014年8月18日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[St_StockLogUpdate]
    @StockLogId int,
@StockId int = NULL,
@StockNameId int = NULL,
@RefNo varchar(50) = NULL,
@LogDirection int = NULL,
@LogType int,
@ContractId int = NULL,
@SubContractId int = NULL,
@LogDate datetime,
@OpPerson int = NULL,
@Bundles int,
@GrossAmount numeric(18, 4),
@NetAmount numeric(18, 4),
@MUId int = NULL,
@BrandId int = NULL,
@DeliverPlaceId int = NULL,
@PaperNo varchar(80) = NULL,
@PaperHolder int = NULL,
@CardNo varchar(200) = NULL,
@Memo varchar(4000),
@LogStatus int = NULL,
@LogSourceBase varchar(50) = NULL,
@LogSource varchar(200) = NULL,
@SourceId int = NULL
AS

UPDATE [dbo].[St_StockLog] SET
	[StockId] = @StockId,
	[StockNameId] = @StockNameId,
	[RefNo] = @RefNo,
	[LogDirection] = @LogDirection,
	[LogType] = @LogType,
	[ContractId] = @ContractId,
	[SubContractId] = @SubContractId,
	[LogDate] = @LogDate,
	[OpPerson] = @OpPerson,
	[Bundles] = @Bundles,
	[GrossAmount] = @GrossAmount,
	[NetAmount] = @NetAmount,
	[MUId] = @MUId,
	[BrandId] = @BrandId,
	[DeliverPlaceId] = @DeliverPlaceId,
	[PaperNo] = @PaperNo,
	[PaperHolder] = @PaperHolder,
	[CardNo] = @CardNo,
	[Memo] = @Memo,
	[LogStatus] = @LogStatus,
	[LogSourceBase] = @LogSourceBase,
	[LogSource] = @LogSource,
	[SourceId] = @SourceId
WHERE
	[StockLogId] = @StockLogId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[St_StockLogLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[St_StockLogLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockLogLoad
// 存储过程功能描述：查询所有St_StockLog记录
// 创建人：CodeSmith
// 创建时间： 2014年8月18日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[St_StockLogLoad]
AS

SELECT
	[StockLogId],
	[StockId],
	[StockNameId],
	[RefNo],
	[LogDirection],
	[LogType],
	[ContractId],
	[SubContractId],
	[LogDate],
	[OpPerson],
	[Bundles],
	[GrossAmount],
	[NetAmount],
	[MUId],
	[BrandId],
	[DeliverPlaceId],
	[PaperNo],
	[PaperHolder],
	[CardNo],
	[Memo],
	[LogStatus],
	[LogSourceBase],
	[LogSource],
	[SourceId]
FROM
	[dbo].[St_StockLog]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[St_StockLogInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[St_StockLogInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockLogInsert
// 存储过程功能描述：新增一条St_StockLog记录
// 创建人：CodeSmith
// 创建时间： 2014年8月18日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[St_StockLogInsert]
	@StockId int =NULL ,
	@StockNameId int =NULL ,
	@RefNo varchar(50) =NULL ,
	@LogDirection int =NULL ,
	@LogType int ,
	@ContractId int =NULL ,
	@SubContractId int =NULL ,
	@LogDate datetime ,
	@OpPerson int =NULL ,
	@Bundles int ,
	@GrossAmount numeric(18, 4) ,
	@NetAmount numeric(18, 4) ,
	@MUId int =NULL ,
	@BrandId int =NULL ,
	@DeliverPlaceId int =NULL ,
	@PaperNo varchar(80) =NULL ,
	@PaperHolder int =NULL ,
	@CardNo varchar(200) =NULL ,
	@Memo varchar(4000) ,
	@LogStatus int =NULL ,
	@LogSourceBase varchar(50) =NULL ,
	@LogSource varchar(200) =NULL ,
	@SourceId int =NULL ,
	@StockLogId int OUTPUT
AS

INSERT INTO [dbo].[St_StockLog] (
	[StockId],
	[StockNameId],
	[RefNo],
	[LogDirection],
	[LogType],
	[ContractId],
	[SubContractId],
	[LogDate],
	[OpPerson],
	[Bundles],
	[GrossAmount],
	[NetAmount],
	[MUId],
	[BrandId],
	[DeliverPlaceId],
	[PaperNo],
	[PaperHolder],
	[CardNo],
	[Memo],
	[LogStatus],
	[LogSourceBase],
	[LogSource],
	[SourceId]
) VALUES (
	@StockId,
	@StockNameId,
	@RefNo,
	@LogDirection,
	@LogType,
	@ContractId,
	@SubContractId,
	@LogDate,
	@OpPerson,
	@Bundles,
	@GrossAmount,
	@NetAmount,
	@MUId,
	@BrandId,
	@DeliverPlaceId,
	@PaperNo,
	@PaperHolder,
	@CardNo,
	@Memo,
	@LogStatus,
	@LogSourceBase,
	@LogSource,
	@SourceId
)


SET @StockLogId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_BusinessInvoiceGet
// 存储过程功能描述：查询指定Inv_BusinessInvoice的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年8月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Inv_BusinessInvoiceGet]
	@BusinessInvoiceId int
AS

SELECT
	[BusinessInvoiceId],
	[InvoiceId],
	[ContractId],
	[SubContractId],
	[AssetId],
	[IntegerAmount],
	[NetAmount],
	[MUId],
	[MarginRatio],
	[VATRatio],
	[VATBala]
FROM
	[dbo].[Inv_BusinessInvoice]
WHERE
	[BusinessInvoiceId] = @BusinessInvoiceId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BankUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BankUpdateStatus
// 存储过程功能描述：更新Bank中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BankUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.Bank''

set @str = ''update [dbo].[Bank] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where BankId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BankUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BankUpdate
// 存储过程功能描述：更新Bank
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BankUpdate]
    @BankId int,
@BankName varchar(50),
@BankEname varchar(50),
@BankFullName varchar(100) = NULL,
@BankShort varchar(20) = NULL,
@CapitalType int = NULL,
@BankLevel int = NULL,
@ParentId int = NULL,
@BankStatus int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Bank] SET
	[BankName] = @BankName,
	[BankEname] = @BankEname,
	[BankFullName] = @BankFullName,
	[BankShort] = @BankShort,
	[CapitalType] = @CapitalType,
	[BankLevel] = @BankLevel,
	[ParentId] = @ParentId,
	[BankStatus] = @BankStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[BankId] = @BankId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BankLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BankLoad
// 存储过程功能描述：查询所有Bank记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BankLoad]
AS

SELECT
	[BankId],
	[BankName],
	[BankEname],
	[BankFullName],
	[BankShort],
	[CapitalType],
	[BankLevel],
	[ParentId],
	[BankStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Bank]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BankInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BankInsert
// 存储过程功能描述：新增一条Bank记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BankInsert]
	@BankName varchar(50) ,
	@BankEname varchar(50) ,
	@BankFullName varchar(100) =NULL ,
	@BankShort varchar(20) =NULL ,
	@CapitalType int =NULL ,
	@BankLevel int =NULL ,
	@ParentId int =NULL ,
	@BankStatus int ,
	@CreatorId int ,
	@BankId int OUTPUT
AS

INSERT INTO [dbo].[Bank] (
	[BankName],
	[BankEname],
	[BankFullName],
	[BankShort],
	[CapitalType],
	[BankLevel],
	[ParentId],
	[BankStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@BankName,
	@BankEname,
	@BankFullName,
	@BankShort,
	@CapitalType,
	@BankLevel,
	@ParentId,
	@BankStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @BankId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BankGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BankGoBack
// 存储过程功能描述：撤返Bank，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BankGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Bank''

set @str = ''update [dbo].[Bank] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where BankId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BankGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BankGet
// 存储过程功能描述：查询指定Bank的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BankGet]
    /*
	@BankId int
    */
    @id int
AS

SELECT
	[BankId],
	[BankName],
	[BankEname],
	[BankFullName],
	[BankShort],
	[CapitalType],
	[BankLevel],
	[ParentId],
	[BankStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Bank]
WHERE
	[BankId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BankAccountUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankAccountUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BankAccountUpdateStatus
// 存储过程功能描述：更新BankAccount中的状态
// 创建人：CodeSmith
// 创建时间： 2014年10月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BankAccountUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.BankAccount''

set @str = ''update [dbo].[BankAccount] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where BankAccId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BankAccountUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankAccountUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BankAccountUpdate
// 存储过程功能描述：更新BankAccount
// 创建人：CodeSmith
// 创建时间： 2014年10月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BankAccountUpdate]
    @BankAccId int,
@CompanyId int,
@BankId int,
@AccountNo varchar(80),
@CurrencyId int,
@BankAccDesc varchar(400) = NULL,
@BankAccStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[BankAccount] SET
	[CompanyId] = @CompanyId,
	[BankId] = @BankId,
	[AccountNo] = @AccountNo,
	[CurrencyId] = @CurrencyId,
	[BankAccDesc] = @BankAccDesc,
	[BankAccStatus] = @BankAccStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[BankAccId] = @BankAccId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BankAccountLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankAccountLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BankAccountLoad
// 存储过程功能描述：查询所有BankAccount记录
// 创建人：CodeSmith
// 创建时间： 2014年10月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BankAccountLoad]
AS

SELECT
	[BankAccId],
	[CompanyId],
	[BankId],
	[AccountNo],
	[CurrencyId],
	[BankAccDesc],
	[BankAccStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BankAccount]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BankAccountInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankAccountInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BankAccountInsert
// 存储过程功能描述：新增一条BankAccount记录
// 创建人：CodeSmith
// 创建时间： 2014年10月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BankAccountInsert]
	@CompanyId int ,
	@BankId int ,
	@AccountNo varchar(80) ,
	@CurrencyId int ,
	@BankAccDesc varchar(400) =NULL ,
	@BankAccStatus int =NULL ,
	@CreatorId int ,
	@BankAccId int OUTPUT
AS

INSERT INTO [dbo].[BankAccount] (
	[CompanyId],
	[BankId],
	[AccountNo],
	[CurrencyId],
	[BankAccDesc],
	[BankAccStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@CompanyId,
	@BankId,
	@AccountNo,
	@CurrencyId,
	@BankAccDesc,
	@BankAccStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @BankAccId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BankAccountGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankAccountGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BankAccountGoBack
// 存储过程功能描述：撤返BankAccount，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年10月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BankAccountGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.BankAccount''

set @str = ''update [dbo].[BankAccount] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where BankAccId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BankAccountGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankAccountGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BankAccountGet
// 存储过程功能描述：查询指定BankAccount的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年10月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BankAccountGet]
    /*
	@BankAccId int
    */
    @id int
AS

SELECT
	[BankAccId],
	[CompanyId],
	[BankId],
	[AccountNo],
	[CurrencyId],
	[BankAccDesc],
	[BankAccStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BankAccount]
WHERE
	[BankAccId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStatusUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStatusUpdateStatus
// 存储过程功能描述：更新BDStatus中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStatusUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.BDStatus''

set @str = ''update [dbo].[BDStatus] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where StatusId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStatusUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStatusUpdate
// 存储过程功能描述：更新BDStatus
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStatusUpdate]
    @StatusId int,
@StatusName varchar(20),
@StatusNameCode varchar(20),
@KeyName varchar(20) = NULL,
@TableName varchar(50) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[BDStatus] SET
	[StatusName] = @StatusName,
	[StatusNameCode] = @StatusNameCode,
	[KeyName] = @KeyName,
	[TableName] = @TableName,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[StatusId] = @StatusId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStatusLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStatusLoad
// 存储过程功能描述：查询所有BDStatus记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStatusLoad]
AS

SELECT
	[StatusId],
	[StatusName],
	[StatusNameCode],
	[KeyName],
	[TableName],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BDStatus]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStatusInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStatusInsert
// 存储过程功能描述：新增一条BDStatus记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStatusInsert]
	@StatusName varchar(20) ,
	@StatusNameCode varchar(20) ,
	@KeyName varchar(20) =NULL ,
	@TableName varchar(50) =NULL ,
	@CreatorId int =NULL ,
	@StatusId int OUTPUT
AS

INSERT INTO [dbo].[BDStatus] (
	[StatusName],
	[StatusNameCode],
	[KeyName],
	[TableName],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@StatusName,
	@StatusNameCode,
	@KeyName,
	@TableName,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @StatusId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStatusGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStatusGoBack
// 存储过程功能描述：撤返BDStatus，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStatusGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.BDStatus''

set @str = ''update [dbo].[BDStatus] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where StatusId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStatusGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStatusGet
// 存储过程功能描述：查询指定BDStatus的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStatusGet]
    /*
	@StatusId int
    */
    @id int
AS

SELECT
	[StatusId],
	[StatusName],
	[StatusNameCode],
	[KeyName],
	[TableName],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BDStatus]
WHERE
	[StatusId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStatusDetailUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusDetailUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStatusDetailUpdateStatus
// 存储过程功能描述：更新BDStatusDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStatusDetailUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.BDStatusDetail''

set @str = ''update [dbo].[BDStatusDetail] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where DetailId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStatusDetailUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusDetailUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStatusDetailUpdate
// 存储过程功能描述：更新BDStatusDetail
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStatusDetailUpdate]
    @DetailId int,
@StatusId int,
@StatusName varchar(80),
@StatusCode varchar(80),
@LastModifyId int = NULL
AS

UPDATE [dbo].[BDStatusDetail] SET
	[StatusId] = @StatusId,
	[StatusName] = @StatusName,
	[StatusCode] = @StatusCode,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DetailId] = @DetailId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStatusDetailLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusDetailLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStatusDetailLoad
// 存储过程功能描述：查询所有BDStatusDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStatusDetailLoad]
AS

SELECT
	[DetailId],
	[StatusId],
	[StatusName],
	[StatusCode],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BDStatusDetail]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStatusDetailInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusDetailInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStatusDetailInsert
// 存储过程功能描述：新增一条BDStatusDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStatusDetailInsert]
	@StatusId int ,
	@StatusName varchar(80) ,
	@StatusCode varchar(80) ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[BDStatusDetail] (
	[StatusId],
	[StatusName],
	[StatusCode],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@StatusId,
	@StatusName,
	@StatusCode,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DetailId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStatusDetailGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusDetailGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStatusDetailGoBack
// 存储过程功能描述：撤返BDStatusDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStatusDetailGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.BDStatusDetail''

set @str = ''update [dbo].[BDStatusDetail] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where DetailId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStatusDetailGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStatusDetailGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStatusDetailGet
// 存储过程功能描述：查询指定BDStatusDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStatusDetailGet]
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[StatusId],
	[StatusName],
	[StatusCode],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BDStatusDetail]
WHERE
	[DetailId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AssetUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AssetUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AssetUpdateStatus
// 存储过程功能描述：更新Asset中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AssetUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.Asset''

set @str = ''update [dbo].[Asset] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where AssetId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AssetUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AssetUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AssetUpdate
// 存储过程功能描述：更新Asset
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AssetUpdate]
    @AssetId int,
@AssetName varchar(20),
@MUId int,
@MisTake numeric(18, 8) = NULL,
@AssetStatus int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Asset] SET
	[AssetName] = @AssetName,
	[MUId] = @MUId,
	[MisTake] = @MisTake,
	[AssetStatus] = @AssetStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[AssetId] = @AssetId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AssetLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AssetLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AssetLoad
// 存储过程功能描述：查询所有Asset记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AssetLoad]
AS

SELECT
	[AssetId],
	[AssetName],
	[MUId],
	[MisTake],
	[AssetStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Asset]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AssetInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AssetInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AssetInsert
// 存储过程功能描述：新增一条Asset记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AssetInsert]
	@AssetName varchar(20) ,
	@MUId int ,
	@MisTake numeric(18, 8) =NULL ,
	@AssetStatus int ,
	@CreatorId int ,
	@AssetId int OUTPUT
AS

INSERT INTO [dbo].[Asset] (
	[AssetName],
	[MUId],
	[MisTake],
	[AssetStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@AssetName,
	@MUId,
	@MisTake,
	@AssetStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @AssetId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AssetGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AssetGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AssetGoBack
// 存储过程功能描述：撤返Asset，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AssetGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Asset''

set @str = ''update [dbo].[Asset] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where AssetId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AssetGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AssetGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AssetGet
// 存储过程功能描述：查询指定Asset的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AssetGet]
    /*
	@AssetId int
    */
    @id int
AS

SELECT
	[AssetId],
	[AssetName],
	[MUId],
	[MisTake],
	[AssetStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Asset]
WHERE
	[AssetId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AreaUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AreaUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AreaUpdateStatus
// 存储过程功能描述：更新Area中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AreaUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.Area''

set @str = ''update [dbo].[Area] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where AreaId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AreaUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AreaUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AreaUpdate
// 存储过程功能描述：更新Area
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AreaUpdate]
    @AreaId int,
@AreaName varchar(50),
@AreaFullName varchar(100),
@AreaShort varchar(80),
@AreaCode varchar(20) = NULL,
@AreaZip varchar(20) = NULL,
@AreaLevel int = NULL,
@ParentID int = NULL,
@AreaStatus int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Area] SET
	[AreaName] = @AreaName,
	[AreaFullName] = @AreaFullName,
	[AreaShort] = @AreaShort,
	[AreaCode] = @AreaCode,
	[AreaZip] = @AreaZip,
	[AreaLevel] = @AreaLevel,
	[ParentID] = @ParentID,
	[AreaStatus] = @AreaStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[AreaId] = @AreaId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AreaLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AreaLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AreaLoad
// 存储过程功能描述：查询所有Area记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AreaLoad]
AS

SELECT
	[AreaId],
	[AreaName],
	[AreaFullName],
	[AreaShort],
	[AreaCode],
	[AreaZip],
	[AreaLevel],
	[ParentID],
	[AreaStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Area]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AreaInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AreaInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AreaInsert
// 存储过程功能描述：新增一条Area记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AreaInsert]
	@AreaName varchar(50) ,
	@AreaFullName varchar(100) ,
	@AreaShort varchar(80) ,
	@AreaCode varchar(20) =NULL ,
	@AreaZip varchar(20) =NULL ,
	@AreaLevel int =NULL ,
	@ParentID int =NULL ,
	@AreaStatus int ,
	@CreatorId int ,
	@AreaId int OUTPUT
AS

INSERT INTO [dbo].[Area] (
	[AreaName],
	[AreaFullName],
	[AreaShort],
	[AreaCode],
	[AreaZip],
	[AreaLevel],
	[ParentID],
	[AreaStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@AreaName,
	@AreaFullName,
	@AreaShort,
	@AreaCode,
	@AreaZip,
	@AreaLevel,
	@ParentID,
	@AreaStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @AreaId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AreaGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AreaGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AreaGoBack
// 存储过程功能描述：撤返Area，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AreaGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Area''

set @str = ''update [dbo].[Area] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where AreaId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AreaGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AreaGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AreaGet
// 存储过程功能描述：查询指定Area的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AreaGet]
    /*
	@AreaId int
    */
    @id int
AS

SELECT
	[AreaId],
	[AreaName],
	[AreaFullName],
	[AreaShort],
	[AreaCode],
	[AreaZip],
	[AreaLevel],
	[ParentID],
	[AreaStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Area]
WHERE
	[AreaId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStyleUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStyleUpdateStatus
// 存储过程功能描述：更新BDStyle中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStyleUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.BDStyle''

set @str = ''update [dbo].[BDStyle] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where BDStyleId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStyleUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStyleUpdate
// 存储过程功能描述：更新BDStyle
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStyleUpdate]
    @BDStyleId int,
@BDStyleCode varchar(80),
@BDStyleName varchar(80),
@BDStyleStatus int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[BDStyle] SET
	[BDStyleCode] = @BDStyleCode,
	[BDStyleName] = @BDStyleName,
	[BDStyleStatus] = @BDStyleStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[BDStyleId] = @BDStyleId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStyleLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStyleLoad
// 存储过程功能描述：查询所有BDStyle记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStyleLoad]
AS

SELECT
	[BDStyleId],
	[BDStyleCode],
	[BDStyleName],
	[BDStyleStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BDStyle]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStyleInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStyleInsert
// 存储过程功能描述：新增一条BDStyle记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStyleInsert]
	@BDStyleCode varchar(80) ,
	@BDStyleName varchar(80) ,
	@BDStyleStatus int ,
	@CreatorId int =NULL ,
	@BDStyleId int OUTPUT
AS

INSERT INTO [dbo].[BDStyle] (
	[BDStyleCode],
	[BDStyleName],
	[BDStyleStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@BDStyleCode,
	@BDStyleName,
	@BDStyleStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @BDStyleId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStyleGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStyleGoBack
// 存储过程功能描述：撤返BDStyle，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStyleGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.BDStyle''

set @str = ''update [dbo].[BDStyle] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where BDStyleId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStyleGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStyleGet
// 存储过程功能描述：查询指定BDStyle的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStyleGet]
    /*
	@BDStyleId int
    */
    @id int
AS

SELECT
	[BDStyleId],
	[BDStyleCode],
	[BDStyleName],
	[BDStyleStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BDStyle]
WHERE
	[BDStyleId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStyleDetailUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleDetailUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStyleDetailUpdateStatus
// 存储过程功能描述：更新BDStyleDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStyleDetailUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.BDStyleDetail''

set @str = ''update [dbo].[BDStyleDetail] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where StyleDetailId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStyleDetailUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleDetailUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStyleDetailUpdate
// 存储过程功能描述：更新BDStyleDetail
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStyleDetailUpdate]
    @StyleDetailId int,
@BDStyleId int,
@DetailCode varchar(80),
@DetailName varchar(80),
@DetailStatus int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[BDStyleDetail] SET
	[BDStyleId] = @BDStyleId,
	[DetailCode] = @DetailCode,
	[DetailName] = @DetailName,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[StyleDetailId] = @StyleDetailId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStyleDetailLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleDetailLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStyleDetailLoad
// 存储过程功能描述：查询所有BDStyleDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStyleDetailLoad]
AS

SELECT
	[StyleDetailId],
	[BDStyleId],
	[DetailCode],
	[DetailName],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BDStyleDetail]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStyleDetailInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleDetailInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStyleDetailInsert
// 存储过程功能描述：新增一条BDStyleDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStyleDetailInsert]
	@BDStyleId int ,
	@DetailCode varchar(80) ,
	@DetailName varchar(80) ,
	@DetailStatus int ,
	@CreatorId int ,
	@StyleDetailId int OUTPUT
AS

INSERT INTO [dbo].[BDStyleDetail] (
	[BDStyleId],
	[DetailCode],
	[DetailName],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@BDStyleId,
	@DetailCode,
	@DetailName,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @StyleDetailId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStyleDetailGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleDetailGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStyleDetailGoBack
// 存储过程功能描述：撤返BDStyleDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStyleDetailGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.BDStyleDetail''

set @str = ''update [dbo].[BDStyleDetail] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where StyleDetailId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BDStyleDetailGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BDStyleDetailGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStyleDetailGet
// 存储过程功能描述：查询指定BDStyleDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BDStyleDetailGet]
    /*
	@StyleDetailId int
    */
    @id int
AS

SELECT
	[StyleDetailId],
	[BDStyleId],
	[DetailCode],
	[DetailName],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BDStyleDetail]
WHERE
	[StyleDetailId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BrandUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BrandUpdateStatus
// 存储过程功能描述：更新Brand中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BrandUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Brand''

set @str = ''update [dbo].[Brand] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where BrandId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BrandUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BrandUpdate
// 存储过程功能描述：更新Brand
// 创建人：CodeSmith
// 创建时间： 2014年7月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BrandUpdate]
    @BrandId int,
@ProducerId int = NULL,
@BrandName varchar(80) = NULL,
@BrandFullName varchar(400) = NULL,
@BrandInfo varchar(800) = NULL,
@BrandStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Brand] SET
	[ProducerId] = @ProducerId,
	[BrandName] = @BrandName,
	[BrandFullName] = @BrandFullName,
	[BrandInfo] = @BrandInfo,
	[BrandStatus] = @BrandStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[BrandId] = @BrandId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BrandLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BrandLoad
// 存储过程功能描述：查询所有Brand记录
// 创建人：CodeSmith
// 创建时间： 2014年7月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BrandLoad]
AS

SELECT
	[BrandId],
	[ProducerId],
	[BrandName],
	[BrandFullName],
	[BrandInfo],
	[BrandStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Brand]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BrandInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BrandInsert
// 存储过程功能描述：新增一条Brand记录
// 创建人：CodeSmith
// 创建时间： 2014年7月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BrandInsert]
	@ProducerId int =NULL ,
	@BrandName varchar(80) =NULL ,
	@BrandFullName varchar(400) =NULL ,
	@BrandInfo varchar(800) =NULL ,
	@BrandStatus int =NULL ,
	@CreatorId int =NULL ,
	@BrandId int OUTPUT
AS

INSERT INTO [dbo].[Brand] (
	[ProducerId],
	[BrandName],
	[BrandFullName],
	[BrandInfo],
	[BrandStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ProducerId,
	@BrandName,
	@BrandFullName,
	@BrandInfo,
	@BrandStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @BrandId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BrandGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BrandGoBack
// 存储过程功能描述：撤返Brand，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BrandGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Brand''

set @str = ''update [dbo].[Brand] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where BrandId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BrandGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BrandGet
// 存储过程功能描述：查询指定Brand的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BrandGet]
    /*
	@BrandId int
    */
    @id int
AS

SELECT
	[BrandId],
	[ProducerId],
	[BrandName],
	[BrandFullName],
	[BrandInfo],
	[BrandStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Brand]
WHERE
	[BrandId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BrandAssetGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandAssetGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BrandAssetGoBack
// 存储过程功能描述：撤返BrandAsset，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BrandAssetGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.BrandAsset''

set @str = ''update [dbo].[BrandAsset] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where RefId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BrandAssetGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BrandAssetGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BrandAssetGet
// 存储过程功能描述：查询指定BrandAsset的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BrandAssetGet]
    /*
	@RefId int
    */
    @id int
AS

SELECT
	[RefId],
	[BrandId],
	[AssetId],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BrandAsset]
WHERE
	[RefId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ClauseContract_RefUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClauseContract_RefUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ClauseContract_RefUpdateStatus
// 存储过程功能描述：更新ClauseContract_Ref中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ClauseContract_RefUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.ClauseContract_Ref''

set @str = ''update [dbo].[ClauseContract_Ref] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where RefId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskNodeUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskNodeUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Wf_TaskNodeUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.Wf_TaskNode''

set @str = ''update [dbo].[Wf_TaskNode] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where TaskNodeId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[St_StockLogUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[St_StockLogUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockLogUpdateStatus
// 存储过程功能描述：更新St_StockLog中的状态
// 创建人：CodeSmith
// 创建时间： 2014年8月18日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[St_StockLogUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.St_StockLog''

set @str = ''update [dbo].[St_StockLog] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where StockLogId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceDetailUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceDetailUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_BusinessInvoiceDetailUpdateStatus
// 存储过程功能描述：更新Inv_BusinessInvoiceDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年8月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Inv_BusinessInvoiceDetailUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Inv_BusinessInvoiceDetail''

set @str = ''update [dbo].[Inv_BusinessInvoiceDetail] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where DetailId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[St_StockLogGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[St_StockLogGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockLogGoBack
// 存储过程功能描述：撤返St_StockLog，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年8月18日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[St_StockLogGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.St_StockLog''

set @str = ''update [dbo].[St_StockLog] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where StockLogId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RateUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RateUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RateUpdateStatus
// 存储过程功能描述：更新Rate中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RateUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Rate''

set @str = ''update [dbo].[Rate] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where RateId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RateUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RateUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RateUpdate
// 存储过程功能描述：更新Rate
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RateUpdate]
    @RateId int,
@FromCurrencyId int = NULL,
@ToCurrencyId int = NULL,
@RateValue numeric(18, 4) = NULL,
@RateStatus int = NULL,
@LastModifyId int = NULL,
@CreateTime datetime=null
AS

UPDATE [dbo].[Rate] SET
	[FromCurrencyId] = @FromCurrencyId,
	[ToCurrencyId] = @ToCurrencyId,
	[RateValue] = @RateValue,
	[RateStatus] = @RateStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate(),
    [CreateTime] = @CreateTime

WHERE
	[RateId] = @RateId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RateLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RateLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RateLoad
// 存储过程功能描述：查询所有Rate记录
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RateLoad]
AS

SELECT
	[RateId],
	[FromCurrencyId],
	[ToCurrencyId],
	[RateValue],
	[RateStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Rate]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RateInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RateInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RateInsert
// 存储过程功能描述：新增一条Rate记录
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RateInsert]
	@FromCurrencyId int =NULL ,
	@ToCurrencyId int =NULL ,
	@RateValue numeric(18, 4) =NULL ,
	@RateStatus int =NULL ,
	@CreatorId int =NULL ,
	@RateId int OUTPUT
AS

INSERT INTO [dbo].[Rate] (
	[FromCurrencyId],
	[ToCurrencyId],
	[RateValue],
	[RateStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@FromCurrencyId,
	@ToCurrencyId,
	@RateValue,
	@RateStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @RateId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RateGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RateGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RateGoBack
// 存储过程功能描述：撤返Rate，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RateGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Rate''

set @str = ''update [dbo].[Rate] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where RateId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RateGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RateGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RateGet
// 存储过程功能描述：查询指定Rate的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RateGet]
    /*
	@RateId int
    */
    @id int
AS

SELECT
	[RateId],
	[FromCurrencyId],
	[ToCurrencyId],
	[RateValue],
	[RateStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Rate]
WHERE
	[RateId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ProducerUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProducerUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ProducerUpdateStatus
// 存储过程功能描述：更新Producer中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ProducerUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Producer''

set @str = ''update [dbo].[Producer] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where ProducerId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ProducerUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProducerUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ProducerUpdate
// 存储过程功能描述：更新Producer
// 创建人：CodeSmith
// 创建时间： 2014年7月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ProducerUpdate]
    @ProducerId int,
@ProducerName varchar(80) = NULL,
@ProducerFullName varchar(400) = NULL,
@ProducerShort varchar(80) = NULL,
@ProducerArea int = NULL,
@ProducerStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Producer] SET
	[ProducerName] = @ProducerName,
	[ProducerFullName] = @ProducerFullName,
	[ProducerShort] = @ProducerShort,
	[ProducerArea] = @ProducerArea,
	[ProducerStatus] = @ProducerStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[ProducerId] = @ProducerId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ProducerLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProducerLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ProducerLoad
// 存储过程功能描述：查询所有Producer记录
// 创建人：CodeSmith
// 创建时间： 2014年7月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ProducerLoad]
AS

SELECT
	[ProducerId],
	[ProducerName],
	[ProducerFullName],
	[ProducerShort],
	[ProducerArea],
	[ProducerStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Producer]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ProducerInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProducerInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ProducerInsert
// 存储过程功能描述：新增一条Producer记录
// 创建人：CodeSmith
// 创建时间： 2014年7月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ProducerInsert]
	@ProducerName varchar(80) =NULL ,
	@ProducerFullName varchar(400) =NULL ,
	@ProducerShort varchar(80) =NULL ,
	@ProducerArea int =NULL ,
	@ProducerStatus int =NULL ,
	@CreatorId int =NULL ,
	@ProducerId int OUTPUT
AS

INSERT INTO [dbo].[Producer] (
	[ProducerName],
	[ProducerFullName],
	[ProducerShort],
	[ProducerArea],
	[ProducerStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ProducerName,
	@ProducerFullName,
	@ProducerShort,
	@ProducerArea,
	@ProducerStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @ProducerId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ProducerGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProducerGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ProducerGoBack
// 存储过程功能描述：撤返Producer，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ProducerGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Producer''

set @str = ''update [dbo].[Producer] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where ProducerId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ProducerGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProducerGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ProducerGet
// 存储过程功能描述：查询指定Producer的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ProducerGet]
    /*
	@ProducerId int
    */
    @id int
AS

SELECT
	[ProducerId],
	[ProducerName],
	[ProducerFullName],
	[ProducerShort],
	[ProducerArea],
	[ProducerStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Producer]
WHERE
	[ProducerId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[PriceAuthorityUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceAuthorityUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].PriceAuthorityUpdateStatus
// 存储过程功能描述：更新PriceAuthority中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[PriceAuthorityUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.PriceAuthority''

set @str = ''update [dbo].[PriceAuthority] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where PAId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[PriceAuthorityUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceAuthorityUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].PriceAuthorityUpdate
// 存储过程功能描述：更新PriceAuthority
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[PriceAuthorityUpdate]
    @PAId int,
@PAName varchar(80),
@PAMobile varchar(80) = NULL,
@PAPhone varchar(80) = NULL,
@PAStatus int,
@CompanyId int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[PriceAuthority] SET
	[PAName] = @PAName,
	[PAMobile] = @PAMobile,
	[PAPhone] = @PAPhone,
	[PAStatus] = @PAStatus,
	[CompanyId] = @CompanyId,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[PAId] = @PAId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[PriceAuthorityLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceAuthorityLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].PriceAuthorityLoad
// 存储过程功能描述：查询所有PriceAuthority记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[PriceAuthorityLoad]
AS

SELECT
	[PAId],
	[PAName],
	[PAMobile],
	[PAPhone],
	[PAStatus],
	[CompanyId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[PriceAuthority]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[PriceAuthorityInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceAuthorityInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].PriceAuthorityInsert
// 存储过程功能描述：新增一条PriceAuthority记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[PriceAuthorityInsert]
	@PAName varchar(80) ,
	@PAMobile varchar(80) =NULL ,
	@PAPhone varchar(80) =NULL ,
	@PAStatus int ,
	@CompanyId int ,
	@CreatorId int ,
	@PAId int OUTPUT
AS

INSERT INTO [dbo].[PriceAuthority] (
	[PAName],
	[PAMobile],
	[PAPhone],
	[PAStatus],
	[CompanyId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@PAName,
	@PAMobile,
	@PAPhone,
	@PAStatus,
	@CompanyId,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @PAId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[PriceAuthorityGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceAuthorityGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].PriceAuthorityGoBack
// 存储过程功能描述：撤返PriceAuthority，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[PriceAuthorityGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.PriceAuthority''

set @str = ''update [dbo].[PriceAuthority] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where PAId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[PriceAuthorityGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceAuthorityGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].PriceAuthorityGet
// 存储过程功能描述：查询指定PriceAuthority的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[PriceAuthorityGet]
    /*
	@PAId int
    */
    @id int
AS

SELECT
	[PAId],
	[PAName],
	[PAMobile],
	[PAPhone],
	[PAStatus],
	[CompanyId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[PriceAuthority]
WHERE
	[PAId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[MeasureUnitUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MeasureUnitUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].MeasureUnitUpdateStatus
// 存储过程功能描述：更新MeasureUnit中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[MeasureUnitUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.MeasureUnit''

set @str = ''update [dbo].[MeasureUnit] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where MUId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[MeasureUnitUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MeasureUnitUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].MeasureUnitUpdate
// 存储过程功能描述：更新MeasureUnit
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[MeasureUnitUpdate]
    @MUId int,
@MUName varchar(20),
@BaseId int = NULL,
@TransformRate decimal(8, 2),
@MUStatus int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[MeasureUnit] SET
	[MUName] = @MUName,
	[BaseId] = @BaseId,
	[TransformRate] = @TransformRate,
	[MUStatus] = @MUStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[MUId] = @MUId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[MeasureUnitLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MeasureUnitLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].MeasureUnitLoad
// 存储过程功能描述：查询所有MeasureUnit记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[MeasureUnitLoad]
AS

SELECT
	[MUId],
	[MUName],
	[BaseId],
	[TransformRate],
	[MUStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[MeasureUnit]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[MeasureUnitInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MeasureUnitInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].MeasureUnitInsert
// 存储过程功能描述：新增一条MeasureUnit记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[MeasureUnitInsert]
	@MUName varchar(20) ,
	@BaseId int =NULL ,
	@TransformRate decimal(8, 2) ,
	@MUStatus int ,
	@CreatorId int ,
	@MUId int OUTPUT
AS

INSERT INTO [dbo].[MeasureUnit] (
	[MUName],
	[BaseId],
	[TransformRate],
	[MUStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@MUName,
	@BaseId,
	@TransformRate,
	@MUStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @MUId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[MeasureUnitGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MeasureUnitGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].MeasureUnitGoBack
// 存储过程功能描述：撤返MeasureUnit，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[MeasureUnitGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.MeasureUnit''

set @str = ''update [dbo].[MeasureUnit] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where MUId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[MeasureUnitGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MeasureUnitGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].MeasureUnitGet
// 存储过程功能描述：查询指定MeasureUnit的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[MeasureUnitGet]
    /*
	@MUId int
    */
    @id int
AS

SELECT
	[MUId],
	[MUName],
	[BaseId],
	[TransformRate],
	[MUStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[MeasureUnit]
WHERE
	[MUId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_BusinessInvoiceUpdateStatus
// 存储过程功能描述：更新Inv_BusinessInvoice中的状态
// 创建人：CodeSmith
// 创建时间： 2014年8月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Inv_BusinessInvoiceUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Inv_BusinessInvoice''

set @str = ''update [dbo].[Inv_BusinessInvoice] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where BusinessInvoiceId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_BusinessInvoiceGoBack
// 存储过程功能描述：撤返Inv_BusinessInvoice，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年8月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Inv_BusinessInvoiceGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Inv_BusinessInvoice''

set @str = ''update [dbo].[Inv_BusinessInvoice] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where BusinessInvoiceId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Inv_BusinessInvoiceDetailGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inv_BusinessInvoiceDetailGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_BusinessInvoiceDetailGoBack
// 存储过程功能描述：撤返Inv_BusinessInvoiceDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年8月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Inv_BusinessInvoiceDetailGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Inv_BusinessInvoiceDetail''

set @str = ''update [dbo].[Inv_BusinessInvoiceDetail] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where DetailId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CorpDeptGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorpDeptGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorpDeptGet
// 存储过程功能描述：查询指定CorpDept的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CorpDeptGet]
    /*
	@CorpEmpId int
    */
    @id int
AS

SELECT
	[CorpEmpId],
	[DeptId],
	[CorpId],
	[RefStatus]
FROM
	[dbo].[CorpDept]
WHERE
	[CorpEmpId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[FuturesPriceUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesPriceUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].FuturesPriceUpdateStatus
// 存储过程功能描述：更新FuturesPrice中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[FuturesPriceUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.FuturesPrice''

set @str = ''update [dbo].[FuturesPrice] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where FPId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[FuturesPriceUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesPriceUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].FuturesPriceUpdate
// 存储过程功能描述：更新FuturesPrice
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[FuturesPriceUpdate]
    @FPId int,
@TradeDate datetime,
@TradeCode varchar(80),
@DeliverDate datetime,
@SettlePrice numeric(19, 4) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[FuturesPrice] SET
	[TradeDate] = @TradeDate,
	[TradeCode] = @TradeCode,
	[DeliverDate] = @DeliverDate,
	[SettlePrice] = @SettlePrice,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[FPId] = @FPId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[FuturesPriceLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesPriceLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].FuturesPriceLoad
// 存储过程功能描述：查询所有FuturesPrice记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[FuturesPriceLoad]
AS

SELECT
	[FPId],
	[TradeDate],
	[TradeCode],
	[DeliverDate],
	[SettlePrice],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[FuturesPrice]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[FuturesPriceInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesPriceInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].FuturesPriceInsert
// 存储过程功能描述：新增一条FuturesPrice记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[FuturesPriceInsert]
	@TradeDate datetime ,
	@TradeCode varchar(80) ,
	@DeliverDate datetime ,
	@SettlePrice numeric(19, 4) =NULL ,
	@CreatorId int ,
	@FPId int OUTPUT
AS

INSERT INTO [dbo].[FuturesPrice] (
	[TradeDate],
	[TradeCode],
	[DeliverDate],
	[SettlePrice],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@TradeDate,
	@TradeCode,
	@DeliverDate,
	@SettlePrice,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @FPId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[FuturesPriceGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesPriceGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].FuturesPriceGoBack
// 存储过程功能描述：撤返FuturesPrice，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[FuturesPriceGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.FuturesPrice''

set @str = ''update [dbo].[FuturesPrice] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where FPId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[FuturesPriceGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesPriceGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].FuturesPriceGet
// 存储过程功能描述：查询指定FuturesPrice的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[FuturesPriceGet]
    /*
	@FPId int
    */
    @id int
AS

SELECT
	[FPId],
	[TradeDate],
	[TradeCode],
	[DeliverDate],
	[SettlePrice],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[FuturesPrice]
WHERE
	[FPId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[FuturesCodeUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesCodeUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].FuturesCodeUpdateStatus
// 存储过程功能描述：更新FuturesCode中的状态
// 创建人：CodeSmith
// 创建时间： 2014年10月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[FuturesCodeUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.FuturesCode''

set @str = ''update [dbo].[FuturesCode] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where FuturesCodeId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[FuturesCodeUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesCodeUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].FuturesCodeUpdate
// 存储过程功能描述：更新FuturesCode
// 创建人：CodeSmith
// 创建时间： 2014年10月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[FuturesCodeUpdate]
    @FuturesCodeId int,
@ExchageId int = NULL,
@CodeSize numeric(19, 4) = NULL,
@FirstTradeDate datetime = NULL,
@LastTradeDate datetime = NULL,
@MUId int = NULL,
@CurrencyId int = NULL,
@AssetId int = NULL,
@FuturesCodeStatus int = NULL,
@TradeCode varchar(80) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[FuturesCode] SET
	[ExchageId] = @ExchageId,
	[CodeSize] = @CodeSize,
	[FirstTradeDate] = @FirstTradeDate,
	[LastTradeDate] = @LastTradeDate,
	[MUId] = @MUId,
	[CurrencyId] = @CurrencyId,
	[AssetId] = @AssetId,
	[FuturesCodeStatus] = @FuturesCodeStatus,
	[TradeCode] = @TradeCode,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[FuturesCodeId] = @FuturesCodeId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[FuturesCodeLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesCodeLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].FuturesCodeLoad
// 存储过程功能描述：查询所有FuturesCode记录
// 创建人：CodeSmith
// 创建时间： 2014年10月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[FuturesCodeLoad]
AS

SELECT
	[FuturesCodeId],
	[ExchageId],
	[CodeSize],
	[FirstTradeDate],
	[LastTradeDate],
	[MUId],
	[CurrencyId],
	[AssetId],
	[FuturesCodeStatus],
	[TradeCode],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[FuturesCode]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[FuturesCodeInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesCodeInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].FuturesCodeInsert
// 存储过程功能描述：新增一条FuturesCode记录
// 创建人：CodeSmith
// 创建时间： 2014年10月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[FuturesCodeInsert]
	@ExchageId int =NULL ,
	@CodeSize numeric(19, 4) =NULL ,
	@FirstTradeDate datetime =NULL ,
	@LastTradeDate datetime =NULL ,
	@MUId int =NULL ,
	@CurrencyId int =NULL ,
	@AssetId int =NULL ,
	@FuturesCodeStatus int =NULL ,
	@TradeCode varchar(80) =NULL ,
	@CreatorId int =NULL ,
	@FuturesCodeId int OUTPUT
AS

INSERT INTO [dbo].[FuturesCode] (
	[ExchageId],
	[CodeSize],
	[FirstTradeDate],
	[LastTradeDate],
	[MUId],
	[CurrencyId],
	[AssetId],
	[FuturesCodeStatus],
	[TradeCode],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ExchageId,
	@CodeSize,
	@FirstTradeDate,
	@LastTradeDate,
	@MUId,
	@CurrencyId,
	@AssetId,
	@FuturesCodeStatus,
	@TradeCode,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @FuturesCodeId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[FuturesCodeGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesCodeGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].FuturesCodeGoBack
// 存储过程功能描述：撤返FuturesCode，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年10月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[FuturesCodeGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.FuturesCode''

set @str = ''update [dbo].[FuturesCode] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where FuturesCodeId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[FuturesCodeGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FuturesCodeGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].FuturesCodeGet
// 存储过程功能描述：查询指定FuturesCode的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年10月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[FuturesCodeGet]
    /*
	@FuturesCodeId int
    */
    @id int
AS

SELECT
	[FuturesCodeId],
	[ExchageId],
	[CodeSize],
	[FirstTradeDate],
	[LastTradeDate],
	[MUId],
	[CurrencyId],
	[AssetId],
	[FuturesCodeStatus],
	[TradeCode],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[FuturesCode]
WHERE
	[FuturesCodeId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ExchangeUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ExchangeUpdateStatus
// 存储过程功能描述：更新Exchange中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ExchangeUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.Exchange''

set @str = ''update [dbo].[Exchange] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where ExchangeId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ExchangeUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ExchangeUpdate
// 存储过程功能描述：更新Exchange
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ExchangeUpdate]
    @ExchangeId int,
@ExchangeName varchar(50),
@ExchangeCode varchar(20),
@ExchangeStatus int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Exchange] SET
	[ExchangeName] = @ExchangeName,
	[ExchangeCode] = @ExchangeCode,
	[ExchangeStatus] = @ExchangeStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[ExchangeId] = @ExchangeId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ExchangeLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ExchangeLoad
// 存储过程功能描述：查询所有Exchange记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ExchangeLoad]
AS

SELECT
	[ExchangeId],
	[ExchangeName],
	[ExchangeCode],
	[ExchangeStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Exchange]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ExchangeInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ExchangeInsert
// 存储过程功能描述：新增一条Exchange记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ExchangeInsert]
	@ExchangeName varchar(50) ,
	@ExchangeCode varchar(20) ,
	@ExchangeStatus int ,
	@CreatorId int ,
	@ExchangeId int OUTPUT
AS

INSERT INTO [dbo].[Exchange] (
	[ExchangeName],
	[ExchangeCode],
	[ExchangeStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ExchangeName,
	@ExchangeCode,
	@ExchangeStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @ExchangeId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ExchangeGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ExchangeGoBack
// 存储过程功能描述：撤返Exchange，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ExchangeGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Exchange''

set @str = ''update [dbo].[Exchange] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where ExchangeId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ExchangeGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ExchangeGet
// 存储过程功能描述：查询指定Exchange的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ExchangeGet]
    /*
	@ExchangeId int
    */
    @id int
AS

SELECT
	[ExchangeId],
	[ExchangeName],
	[ExchangeCode],
	[ExchangeStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Exchange]
WHERE
	[ExchangeId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeliverPlaceUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliverPlaceUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DeliverPlaceUpdateStatus
// 存储过程功能描述：更新DeliverPlace中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DeliverPlaceUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.DeliverPlace''

set @str = ''update [dbo].[DeliverPlace] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where DPId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeliverPlaceUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliverPlaceUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DeliverPlaceUpdate
// 存储过程功能描述：更新DeliverPlace
// 创建人：CodeSmith
// 创建时间： 2014年7月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DeliverPlaceUpdate]
    @DPId int,
@DPType int,
@DPArea int,
@DPCompany int = NULL,
@DPName varchar(80),
@DPFullName varchar(400) = NULL,
@DPStatus int,
@DPAddress varchar(400),
@DPEAddress varchar(400) = NULL,
@DPTel varchar(80) = NULL,
@DPContact varchar(80) = NULL,
@DPFax varchar(80) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[DeliverPlace] SET
	[DPType] = @DPType,
	[DPArea] = @DPArea,
	[DPCompany] = @DPCompany,
	[DPName] = @DPName,
	[DPFullName] = @DPFullName,
	[DPStatus] = @DPStatus,
	[DPAddress] = @DPAddress,
	[DPEAddress] = @DPEAddress,
	[DPTel] = @DPTel,
	[DPContact] = @DPContact,
	[DPFax] = @DPFax,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DPId] = @DPId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeliverPlaceLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliverPlaceLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DeliverPlaceLoad
// 存储过程功能描述：查询所有DeliverPlace记录
// 创建人：CodeSmith
// 创建时间： 2014年7月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DeliverPlaceLoad]
AS

SELECT
	[DPId],
	[DPType],
	[DPArea],
	[DPCompany],
	[DPName],
	[DPFullName],
	[DPStatus],
	[DPAddress],
	[DPEAddress],
	[DPTel],
	[DPContact],
	[DPFax],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[DeliverPlace]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeliverPlaceInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliverPlaceInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DeliverPlaceInsert
// 存储过程功能描述：新增一条DeliverPlace记录
// 创建人：CodeSmith
// 创建时间： 2014年7月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DeliverPlaceInsert]
	@DPType int ,
	@DPArea int ,
	@DPCompany int =NULL ,
	@DPName varchar(80) ,
	@DPFullName varchar(400) =NULL ,
	@DPStatus int ,
	@DPAddress varchar(400) ,
	@DPEAddress varchar(400) =NULL ,
	@DPTel varchar(80) =NULL ,
	@DPContact varchar(80) =NULL ,
	@DPFax varchar(80) =NULL ,
	@CreatorId int ,
	@DPId int OUTPUT
AS

INSERT INTO [dbo].[DeliverPlace] (
	[DPType],
	[DPArea],
	[DPCompany],
	[DPName],
	[DPFullName],
	[DPStatus],
	[DPAddress],
	[DPEAddress],
	[DPTel],
	[DPContact],
	[DPFax],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@DPType,
	@DPArea,
	@DPCompany,
	@DPName,
	@DPFullName,
	@DPStatus,
	@DPAddress,
	@DPEAddress,
	@DPTel,
	@DPContact,
	@DPFax,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DPId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeliverPlaceGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliverPlaceGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DeliverPlaceGoBack
// 存储过程功能描述：撤返DeliverPlace，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DeliverPlaceGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.DeliverPlace''

set @str = ''update [dbo].[DeliverPlace] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where DPId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeliverPlaceGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliverPlaceGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DeliverPlaceGet
// 存储过程功能描述：查询指定DeliverPlace的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DeliverPlaceGet]
    /*
	@DPId int
    */
    @id int
AS

SELECT
	[DPId],
	[DPType],
	[DPArea],
	[DPCompany],
	[DPName],
	[DPFullName],
	[DPStatus],
	[DPAddress],
	[DPEAddress],
	[DPTel],
	[DPContact],
	[DPFax],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[DeliverPlace]
WHERE
	[DPId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractClauseUpdateStatus
// 存储过程功能描述：更新ContractClause中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractClauseUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.ContractClause''

set @str = ''update [dbo].[ContractClause] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where ClauseId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractClauseUpdate
// 存储过程功能描述：更新ContractClause
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractClauseUpdate]
    @ClauseId int,
@ClauseText varchar(max) = NULL,
@ClauseEnText varchar(max) = NULL,
@ClauseStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[ContractClause] SET
	[ClauseText] = @ClauseText,
	[ClauseEnText] = @ClauseEnText,
	[ClauseStatus] = @ClauseStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[ClauseId] = @ClauseId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractClauseLoad
// 存储过程功能描述：查询所有ContractClause记录
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractClauseLoad]
AS

SELECT
	[ClauseId],
	[ClauseText],
	[ClauseEnText],
	[ClauseStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[ContractClause]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractClauseInsert
// 存储过程功能描述：新增一条ContractClause记录
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractClauseInsert]
	@ClauseText varchar(max) =NULL ,
	@ClauseEnText varchar(max) =NULL ,
	@ClauseStatus int =NULL ,
	@CreatorId int =NULL ,
	@ClauseId int OUTPUT
AS

INSERT INTO [dbo].[ContractClause] (
	[ClauseText],
	[ClauseEnText],
	[ClauseStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ClauseText,
	@ClauseEnText,
	@ClauseStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @ClauseId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractClauseGoBack
// 存储过程功能描述：撤返ContractClause，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractClauseGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.ContractClause''

set @str = ''update [dbo].[ContractClause] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where ClauseId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractClauseGet
// 存储过程功能描述：查询指定ContractClause的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractClauseGet]
    /*
	@ClauseId int
    */
    @id int
AS

SELECT
	[ClauseId],
	[ClauseText],
	[ClauseEnText],
	[ClauseStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[ContractClause]
WHERE
	[ClauseId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseDetailUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseDetailUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractClauseDetailUpdateStatus
// 存储过程功能描述：更新ContractClauseDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractClauseDetailUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.ContractClauseDetail''

set @str = ''update [dbo].[ContractClauseDetail] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where ClauseDetailId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CurrencyUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrencyUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CurrencyUpdateStatus
// 存储过程功能描述：更新Currency中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CurrencyUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.Currency''

set @str = ''update [dbo].[Currency] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where CurrencyId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CurrencyUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrencyUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CurrencyUpdate
// 存储过程功能描述：更新Currency
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CurrencyUpdate]
    @CurrencyId int,
@CurrencyName varchar(20),
@CurrencyStatus int,
@CurrencyFullName varchar(20) = NULL,
@CurencyShort varchar(20) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Currency] SET
	[CurrencyName] = @CurrencyName,
	[CurrencyStatus] = @CurrencyStatus,
	[CurrencyFullName] = @CurrencyFullName,
	[CurencyShort] = @CurencyShort,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[CurrencyId] = @CurrencyId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CurrencyLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrencyLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CurrencyLoad
// 存储过程功能描述：查询所有Currency记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CurrencyLoad]
AS

SELECT
	[CurrencyId],
	[CurrencyName],
	[CurrencyStatus],
	[CurrencyFullName],
	[CurencyShort],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Currency]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CurrencyInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrencyInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CurrencyInsert
// 存储过程功能描述：新增一条Currency记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CurrencyInsert]
	@CurrencyName varchar(20) ,
	@CurrencyStatus int ,
	@CurrencyFullName varchar(20) =NULL ,
	@CurencyShort varchar(20) =NULL ,
	@CreatorId int ,
	@CurrencyId int OUTPUT
AS

INSERT INTO [dbo].[Currency] (
	[CurrencyName],
	[CurrencyStatus],
	[CurrencyFullName],
	[CurencyShort],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@CurrencyName,
	@CurrencyStatus,
	@CurrencyFullName,
	@CurencyShort,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @CurrencyId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CurrencyGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrencyGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CurrencyGoBack
// 存储过程功能描述：撤返Currency，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CurrencyGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Currency''

set @str = ''update [dbo].[Currency] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where CurrencyId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CurrencyGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrencyGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CurrencyGet
// 存储过程功能描述：查询指定Currency的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CurrencyGet]
    /*
	@CurrencyId int
    */
    @id int
AS

SELECT
	[CurrencyId],
	[CurrencyName],
	[CurrencyStatus],
	[CurrencyFullName],
	[CurencyShort],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Currency]
WHERE
	[CurrencyId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractMasterUpdateStatus]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractMasterUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractMasterUpdateStatus
// 存储过程功能描述：更新ContractMaster中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月8日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractMasterUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.ContractMaster''

set @str = ''update [dbo].[ContractMaster] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where MasterId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractMasterUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractMasterUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractMasterUpdate
// 存储过程功能描述：更新ContractMaster
// 创建人：CodeSmith
// 创建时间： 2014年7月8日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractMasterUpdate]
    @MasterId int,
@MasterName varchar(200) = NULL,
@MasterEname varchar(200) = NULL,
@MasterStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[ContractMaster] SET
	[MasterName] = @MasterName,
	[MasterEname] = @MasterEname,
	[MasterStatus] = @MasterStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[MasterId] = @MasterId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractMasterLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractMasterLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractMasterLoad
// 存储过程功能描述：查询所有ContractMaster记录
// 创建人：CodeSmith
// 创建时间： 2014年7月8日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractMasterLoad]
AS

SELECT
	[MasterId],
	[MasterName],
	[MasterEname],
	[MasterStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[ContractMaster]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractMasterInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractMasterInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractMasterInsert
// 存储过程功能描述：新增一条ContractMaster记录
// 创建人：CodeSmith
// 创建时间： 2014年7月8日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractMasterInsert]
	@MasterName varchar(200) =NULL ,
	@MasterEname varchar(200) =NULL ,
	@MasterStatus int =NULL ,
	@CreatorId int =NULL ,
	@MasterId int OUTPUT
AS

INSERT INTO [dbo].[ContractMaster] (
	[MasterName],
	[MasterEname],
	[MasterStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@MasterName,
	@MasterEname,
	@MasterStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @MasterId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractMasterGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractMasterGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractMasterGoBack
// 存储过程功能描述：撤返ContractMaster，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractMasterGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.ContractMaster''

set @str = ''update [dbo].[ContractMaster] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where MasterId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractMasterGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractMasterGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractMasterGet
// 存储过程功能描述：查询指定ContractMaster的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractMasterGet]
    /*
	@MasterId int
    */
    @id int
AS

SELECT
	[MasterId],
	[MasterName],
	[MasterEname],
	[MasterStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[ContractMaster]
WHERE
	[MasterId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ClauseContract_RefGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClauseContract_RefGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ClauseContract_RefGoBack
// 存储过程功能描述：撤返ClauseContract_Ref，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ClauseContract_RefGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.ClauseContract_Ref''

set @str = ''update [dbo].[ClauseContract_Ref] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where RefId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseDetailGoBack]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseDetailGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractClauseDetailGoBack
// 存储过程功能描述：撤返ContractClauseDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractClauseDetailGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.ContractClauseDetail''

set @str = ''update [dbo].[ContractClauseDetail] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where ClauseDetailId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseDetailGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseDetailGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractClauseDetailGet
// 存储过程功能描述：查询指定ContractClauseDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractClauseDetailGet]
    /*
	@ClauseDetailId int
    */
    @id int
AS

SELECT
	[ClauseDetailId],
	[ClauseId],
	[DetailDisplayType],
	[DetailDataType],
	[FormatSerial],
	[DetailValue],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[ContractClauseDetail]
WHERE
	[ClauseDetailId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ClauseContract_RefGet]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClauseContract_RefGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ClauseContract_RefGet
// 存储过程功能描述：查询指定ClauseContract_Ref的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ClauseContract_RefGet]
    /*
	@RefId int
    */
    @id int
AS

SELECT
	[RefId],
	[MasterId],
	[ClauseId],
	[Sort],
	[IsChose],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[ClauseContract_Ref]
WHERE
	[RefId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseDetailUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseDetailUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractClauseDetailUpdate
// 存储过程功能描述：更新ContractClauseDetail
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractClauseDetailUpdate]
    @ClauseDetailId int,
@ClauseId int = NULL,
@DetailDisplayType int = NULL,
@DetailDataType int = NULL,
@FormatSerial int = NULL,
@DetailValue varchar(200) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[ContractClauseDetail] SET
	[ClauseId] = @ClauseId,
	[DetailDisplayType] = @DetailDisplayType,
	[DetailDataType] = @DetailDataType,
	[FormatSerial] = @FormatSerial,
	[DetailValue] = @DetailValue,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[ClauseDetailId] = @ClauseDetailId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseDetailLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseDetailLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractClauseDetailLoad
// 存储过程功能描述：查询所有ContractClauseDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractClauseDetailLoad]
AS

SELECT
	[ClauseDetailId],
	[ClauseId],
	[DetailDisplayType],
	[DetailDataType],
	[FormatSerial],
	[DetailValue],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[ContractClauseDetail]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContractClauseDetailInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractClauseDetailInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractClauseDetailInsert
// 存储过程功能描述：新增一条ContractClauseDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContractClauseDetailInsert]
	@ClauseId int =NULL ,
	@DetailDisplayType int =NULL ,
	@DetailDataType int =NULL ,
	@FormatSerial int =NULL ,
	@DetailValue varchar(200) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@ClauseDetailId int OUTPUT
AS

INSERT INTO [dbo].[ContractClauseDetail] (
	[ClauseId],
	[DetailDisplayType],
	[DetailDataType],
	[FormatSerial],
	[DetailValue],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ClauseId,
	@DetailDisplayType,
	@DetailDataType,
	@FormatSerial,
	@DetailValue,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @ClauseDetailId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ClauseContract_RefUpdate]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClauseContract_RefUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ClauseContract_RefUpdate
// 存储过程功能描述：更新ClauseContract_Ref
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ClauseContract_RefUpdate]
    @RefId int,
@MasterId int = NULL,
@ClauseId int = NULL,
@Sort int = NULL,
@IsChose bit = NULL,
@RefStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[ClauseContract_Ref] SET
	[MasterId] = @MasterId,
	[ClauseId] = @ClauseId,
	[Sort] = @Sort,
	[IsChose] = @IsChose,
	[RefStatus] = @RefStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[RefId] = @RefId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ClauseContract_RefLoad]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClauseContract_RefLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ClauseContract_RefLoad
// 存储过程功能描述：查询所有ClauseContract_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ClauseContract_RefLoad]
AS

SELECT
	[RefId],
	[MasterId],
	[ClauseId],
	[Sort],
	[IsChose],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[ClauseContract_Ref]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ClauseContract_RefInsert]    Script Date: 10/22/2014 11:00:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClauseContract_RefInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ClauseContract_RefInsert
// 存储过程功能描述：新增一条ClauseContract_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ClauseContract_RefInsert]
	@MasterId int =NULL ,
	@ClauseId int =NULL ,
	@Sort int =NULL ,
	@IsChose bit =NULL ,
	@RefStatus int =NULL ,
	@CreatorId int =NULL ,
	@RefId int OUTPUT
AS

INSERT INTO [dbo].[ClauseContract_Ref] (
	[MasterId],
	[ClauseId],
	[Sort],
	[IsChose],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@MasterId,
	@ClauseId,
	@Sort,
	@IsChose,
	@RefStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @RefId = @@IDENTITY

' 
END
GO
